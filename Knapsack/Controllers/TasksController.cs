using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Knapsack.Compute;
using Microsoft.AspNetCore.Mvc;
using Knapsack.Models;
using Microsoft.EntityFrameworkCore;

namespace Knapsack.Controllers
{
    public class TasksController : Controller
    {
        private readonly ApplicationContext db;

        public TasksController(ApplicationContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            var tasks = db.Tasks.Include(t => t.Details).ToList();
            return View(tasks);
        }
        
        public IActionResult Init()
                {
                    var tasks = db.Tasks.Include(t => t.Details).ToList();
                    foreach (var task in tasks)
                    {
                        if (task.PercentComplete != 100)
                        {
                            var items = db.Tasks.Where(t => t.TaskId == task.TaskId).SelectMany(t => t.TaskItems)
                                .Select(ti => ti.Item);
                            
                            var computeModel = new ComputeModel { TaskId = task.TaskId, Items = new List<ItemViewModel>()};
                            foreach (var i in items)
                            {
                                var item = new ItemViewModel {ItemId = i.ItemId, IsChecked = false, ItemName = i.ItemName, Worth = i.Worth, Weight = i.Weight};
                                computeModel.Items.Add(item);
                            }
                            ComputeThread thread = new ComputeThread(computeModel);
          
                        }
                    }
                    return RedirectToAction("Index", tasks);
                }
        
        public IActionResult CreateTask()
        {
            var items = db.Items.ToList();
            var viewItems = new List<ItemViewModel>();
            foreach (var i in items)
            {
                viewItems.Add(new ItemViewModel {ItemId = i.ItemId, ItemName = i.ItemName, Worth = i.Worth, Weight = i.Weight, IsChecked = false});
            }
            return View(new CreateTaskViewModel() { TaskName = "", ItemViewModels = viewItems});
        }

        [HttpPost]
        public IActionResult AddTask(CreateTaskViewModel model)
        {
            var newTask = new Task
            {
                TaskName = model.TaskName, 
                Status = "in progress", 
                Capacity = model.Capacity, 
                PercentComplete = 0
            };
            db.Tasks.Add(newTask);
            db.SaveChanges();
            
            var execProcess = new ExecutionProcess
            {
                TaskId = newTask.TaskId, 
                BestCombination = "", 
                CurrentMaxWorth = 0, 
                CurrentItemsCombination = "", 
                AllItems = "", CurCombEnd = 0, 
                CurCombSize = 0, 
                CheckedCombCount = 0
            };
            db.ExecutionProcesses.Add(execProcess);
            db.SaveChanges();
            
            var details = new Details {TaskId = newTask.TaskId, ExecutionTime = 0, MaxWorth = 0};
            db.Details.Add(details);
            db.SaveChanges();
            
            foreach (var i in model.ItemViewModels)
            {
                if (i.IsChecked)
                {
                    newTask.TaskItems.Add(new TaskItem { TaskId = newTask.TaskId, ItemId = i.ItemId});
                    db.SaveChanges();
                }
            }

            var items = db.Tasks.Where(t => t.TaskId == newTask.TaskId).SelectMany(t => t.TaskItems)
                .Select(ti => ti.Item);
            var computeModel = new ComputeModel { TaskId = newTask.TaskId, Items = new List<ItemViewModel>()};
            foreach (var i in items)
            {
                var item = new ItemViewModel
                {
                    ItemId = i.ItemId, 
                    IsChecked = false, 
                    ItemName = i.ItemName, 
                    Worth = i.Worth, 
                    Weight = i.Weight
                };
                computeModel.Items.Add(item);
            }
            
            ComputeThread thread = new ComputeThread(computeModel);
            return RedirectToAction("Index");
        }
        
        public IActionResult DeleteTask(int taskId)
        {
            var task = db.Tasks.FirstOrDefault(t => t.TaskId == taskId);
            if (task != null)
            {
                db.Tasks.Remove(task);
                db.SaveChanges();
            }
            return View("Index", db.Tasks.ToList());
        }
        
        public IActionResult CreateItem(string taskName, int capacity)
        {
            @ViewData["TaskName"] = taskName;
            @ViewData["Capacity"] = capacity;
            return View();
        }

        public IActionResult AddItem(string taskName, int capacity, string itemName, int worth, int weight)
        {
            var newItem = new Item {ItemName = itemName, Worth = worth, Weight = weight};
            var newItemId = newItem.ItemId;
            System.Console.WriteLine(newItemId);
            db.Items.Add(newItem);
            db.SaveChanges();
            var items = db.Items.ToList();
            var viewItems = new List<ItemViewModel>();
            foreach (var i in items)
            {
                System.Console.WriteLine(i.ItemId);
                viewItems.Add(new ItemViewModel {ItemId = i.ItemId, ItemName = i.ItemName, Worth = i.Worth, Weight = i.Weight, IsChecked = false});
                if (i.ItemId == newItemId)
                {
                    viewItems[newItemId].IsChecked = true;
                }
            }
            return View("CreateTask", new CreateTaskViewModel {TaskName = taskName, Capacity = capacity, ItemViewModels = viewItems});
        }

        public IActionResult ShowDetails(int taskId)
        {
            var task = db.Tasks.Include(e => e.Details).Include(e => e.ExecutionProcess).FirstOrDefault(t => t.TaskId == taskId);
            var viewModel = new DetailsViewModel();
            viewModel.TaskName = task.TaskName;
            viewModel.Capacity = task.Capacity;
            viewModel.MaxWorth = task.Details.MaxWorth;
            
            var strsAll = task.ExecutionProcess.AllItems.Split(",");
            var strsTaken = task.ExecutionProcess.BestCombination.Split(",");
            var allItems = new List<int>();
            var takenItems = new List<int>();
            foreach (var id in strsAll)
            {
                allItems.Add(Convert.ToInt32(id));
            }
            foreach (var id in strsTaken)
            {
                takenItems.Add(Convert.ToInt32(id));
            }
            var items = new List<ItemViewModel>();
            foreach (var i in allItems)
            {
                var item = db.Items.FirstOrDefault(it => it.ItemId == i);
                var itemViewModel = new ItemViewModel{IsChecked = false, ItemId = i, ItemName = item.ItemName, Weight = item.Weight, Worth = item.Worth};
                items.Add(itemViewModel);
            }
            foreach (var j in takenItems)
            {
                var item = items.FirstOrDefault(it => it.ItemId == j);
                if (item != null)
                    item.IsChecked = true;
            }

            viewModel.ItemViewModels = items;
            
            return View(viewModel);
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}