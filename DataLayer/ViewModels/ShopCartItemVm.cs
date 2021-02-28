using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels
{
    public class ShopCartItemVm
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string ImageName { get; set; }
        public int Count { get; set; }
        public long? PriceBeforeDiscount { get; set; }
        public long? PriceAfterDiscount { get; set; }

    }
}
