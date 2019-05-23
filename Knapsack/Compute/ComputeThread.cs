using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Knapsack.Models;

namespace Knapsack.Compute
{
    public class ComputeThread
    {
        private Thread thread;

        public ComputeThread(ComputeModel model)
        {
            thread = new Thread(this.func);
            thread.Start(model);
        }

        void func(object ComputeModel)
        {
            ComputeModel model = (ComputeModel) ComputeModel;
            if (model.Items.Count == 0)
                return;
            using (var db = new ApplicationContext())
            {
                var task = db.Tasks.FirstOrDefault(t => t.TaskId == model.TaskId);
                /*for (var combLength = 1; combLength <= model.Items.Count; ++combLength)
                {
                    var curCombination = new List<int>();
                    foreach (var item in model.Items)
                    {
                        
                    }
                }*/
                foreach (var item in model.Items)
                {
                    Console.WriteLine(item.ItemId + ": " + item.ItemName);
                }
            }
        }
    }
}