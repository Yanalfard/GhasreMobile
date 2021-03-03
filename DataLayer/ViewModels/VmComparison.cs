using DataLayer.Models;
using DataLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class VmComparison
    {
        public List<TblProperty> Features { get; set; }
        public List<TblProductPropertyRel> ProductFeatures { get; set; }
        public List<CompareItemVm> Items { get; set; }

        public VmComparison(List<TblProperty> features, List<TblProductPropertyRel> productFeatures, List<CompareItemVm> items)
        {
            Features = features;
            ProductFeatures = productFeatures;
            Items = items;
        }
    }
}
