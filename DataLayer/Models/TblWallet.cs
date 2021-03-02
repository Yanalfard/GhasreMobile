using System;
using System.Collections.Generic;

namespace DataLayer.Models
{
    public partial class TblWallet
    {
        public int WalletId { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public bool IsDeposit { get; set; }
        public string Description { get; set; }
        public bool IsFinaly{get;set;}
        public int ClientId { get; set; }

        public virtual TblClient Client { get; set; }
    }
}
