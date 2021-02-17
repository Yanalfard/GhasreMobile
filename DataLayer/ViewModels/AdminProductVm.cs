using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels
{
    public class AdminProductVm
    {
        public TblProduct product { get; set; }
        public List<string> Keywords { get; set; }
        public List<string> Colors { get; set; }
        public List<string> ColorName { get; set; }
        public List<int> ColorsCounts { get; set; }
        public List<int?> PropertyId { get; set; }
        public List<string> Value { get; set; }
    }
}
