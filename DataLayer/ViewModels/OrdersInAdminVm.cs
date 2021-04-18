using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels
{
    public class OrdersInAdminVm
    {
        public int PageId { get; set; } = 1;
        public int InPageCount { get; set; } = 0;
        public int OrderId { get; set; } = 0;
        public string TellNo { get; set; } = null;
        public string StartDate { get; set; } = null;
        public string EndDate { get; set; } = null;
        public int StatusSelected { get; set; } = -2;
    }
}
