using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ViewModels
{
    public class AdminDashboardVm
    {
        public int CustomersCount { get; set; }
        public int EmployeesCount { get; set; }
        public int AgenciesCount { get; set; }
        public int BrandsCount { get; set; }
        public int AllOrderCount { get; set; }
        public int OrderSucssesCount { get; set; }
        public int OrderCancelCount { get; set; }
        public int AllOrderFractionalCount { get; set; }
        public int OnlineOrderCount { get; set; }
        public int allTicketCount { get; set; }
        public int IsSeenTicketCount { get; set; }
        public int NotSeenTicketCount { get; set; }
        public int AllTopicCount { get; set; }
        public int AllPostCount { get; set; }
        public int AllNotificationCount { get; set; }
        public int AllRegularQuestionCount { get; set; }
        public int AllNotIsValidComment { get; set; }

    }
}
