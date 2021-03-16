using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels
{
  public  class ProductsInAdminVm
    {
        public int PageId { get; set; } = 1;
        public int InPageCount { get; set; }
        public string Search { get; set; } = null;
        public int CatagoryId { get; set; } = 0;


    }
}
