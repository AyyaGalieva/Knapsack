using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Knapsack
{
    public class CreateTaskViewModel
    {
        public string TaskName { get; set; }
        public int Capacity { get; set; }
        public List<ItemViewModel> ItemViewModels { get; set; }
    }
}