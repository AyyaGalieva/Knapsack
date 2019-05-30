using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Knapsack.Models;
using Microsoft.EntityFrameworkCore;

namespace Knapsack.Compute
{
    public class ComputeThread
    {
        private Thread thread;

        public ComputeThread(ComputeModel model)
        {
            thread = new Thread(this.Func);
            thread.Start(model);
        }

        void Func(object ComputeModel)
        {
            var startTime = DateTime.Now;
            var model = (ComputeModel) ComputeModel;
            var itemsCount = model.Items.Count;
            var combCount = Math.Pow(2, itemsCount) - 1;
            var checkedCombCount = 0;
            var maxWorth = 0;
            
            using (var db = new ApplicationContext())
            {
                var task = db.Tasks.Include(e => e.ExecutionProcess).Include(e => e.Details).FirstOrDefault(t => t.TaskId == model.TaskId);
                if (task == null) return;

                if (itemsCount == 0)
                {
                    task.PercentComplete = 100;
                    db.SaveChanges();
                    return;
                }
                
                var set = new List<int>();
                var str = "";
                foreach (var item in model.Items)
                {
                    set.Add(item.ItemId);
                    str = string.Concat(str, item.ItemId.ToString(), ",");
                }
                str = str.Remove(str.Length - 1);
                
                task.ExecutionProcess.AllItems = str;
                db.SaveChanges();

                var oldTimeSpan = TimeSpan.Zero;
                var size = task.ExecutionProcess.CurCombSize;
                var end = task.ExecutionProcess.CurCombEnd;
                var lastComb = task.ExecutionProcess.CurrentItemsCombination;
                checkedCombCount = task.ExecutionProcess.CheckedCombCount;
                var flag = string.Equals(lastComb, "");
                if (!flag)
                    oldTimeSpan = TimeSpan.Parse(task.Details.ExecutionTime);
                
                foreach (var subset in EnumerateAllSubsets(set, (end!=0)?end:set.Count, (size!=0)?size:set.Count, db, task))
                {
                    var comb = "";
                    foreach (var t in subset)
                    {
                        comb = string.Concat(comb, t.ToString(), ",");
                    }
                    comb = comb.Remove(comb.Length-1);                
                    
                    if (flag)
                    {
                        task.ExecutionProcess.CurrentItemsCombination = comb;
                        db.SaveChanges();
                    }
                    else
                    {
                        flag = string.Equals(lastComb, comb);
                    }

                    var sumWorth = 0;
                    var sumWeight = 0;
                    for (var i = 0; i < subset.Count; ++i)
                    {
                        var item = db.Items.FirstOrDefault(it => it.ItemId == subset[i]);
                        if (item != null)
                        {
                            sumWeight += item.Weight;
                            sumWorth += item.Worth;
                        }
                    }

                    if (flag)
                    {
                        if (sumWeight <= task.Capacity)
                        {
                            if (sumWorth >= maxWorth)
                            {
                                maxWorth = sumWorth;
                                task.ExecutionProcess.BestCombination = comb;
                                task.ExecutionProcess.CurrentMaxWorth = maxWorth;
                                db.SaveChanges();
                            }
                        }
                    }

                    if (flag)
                    {
                        checkedCombCount++;
                        var endTime = DateTime.Now;
                        var execTime = endTime.Subtract(startTime);
                        task.Details.ExecutionTime = execTime.Add(oldTimeSpan).ToString();
                        task.ExecutionProcess.CheckedCombCount = checkedCombCount;
                        var percent = (int) (100 * checkedCombCount / combCount);
                        task.PercentComplete = percent;
                        db.SaveChanges();
                    }

                }
                task.Details.MaxWorth = maxWorth;
                db.SaveChanges();
            }
        }

        private IEnumerable<List<int>> EnumerateAllSubsets(List<int> set, int end, int initSize, ApplicationContext db, Task task)
        {
            for (var size = initSize; size > 0; --size)
            {
                task.ExecutionProcess.CurCombSize = size;
                db.SaveChanges();
                foreach (var subset in EnumerateSubsetsWithSize(set, size, end, true, db, task))
                    yield return subset;
            }
        }

        IEnumerable<List<int>> EnumerateSubsetsWithSize(List<int> set, int size, int end, bool first, ApplicationContext db, Task task)
        {
            if (size == set.Count)
                yield return set;
            else
                for (var i = end; i-- > 0;)
                {
                    if (first)
                    {
                        task.ExecutionProcess.CurCombEnd = end;
                        db.SaveChanges();
                    }
                    var tmpSet = new List<int>(set);
                    tmpSet.RemoveAt(i);
                    foreach (var subset in EnumerateSubsetsWithSize(tmpSet, size, i, false, db, task))
                        yield return subset;
                }
        }
    }
}
