using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Knapsack
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var tasks = db.tasks.ToList();
                Console.WriteLine("tasks:");
                foreach (Task t in tasks)
                {
                    Console.WriteLine($"{t.task_id}.{t.name}: {t.status}");
                }
            }

            Console.Read();
        }
    }
}