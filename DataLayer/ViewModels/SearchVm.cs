using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace DataLayer.ViewModels
{
    public class SearchVm
    {
        public string name { get; set; }
        public string q { get; set; }
        public int brand { get; set; }
        public int minPrice { get; set; }
        public string maxPrice { get; set; }
        [DataType(DataType.Date,ErrorMessage ="تاریخ را صحیح وارد کنید")]
        public DateTime? minDate { get; set; }
        [DataType(DataType.Date, ErrorMessage = "تاریخ را صحیح وارد کنید")]
        public DateTime? maxDate { get; set; }
        public bool available { get; set; }
        public bool isFractional { get; set; }
        public bool discount { get; set; }
    }
}
