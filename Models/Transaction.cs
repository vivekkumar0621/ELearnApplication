using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ELearnApplication.Models
{
    public class Transaction
    {
        [Key]
        public string TransactionId { get; set; }

        [Column("UserId")]
        public string EmailId { get; set; }

        public string ServiceId { get; set; }

        public DateTime StartTime { get; set; }

    }

    public class TransactionDetail
    {
        public string TransactionId { get; set; }
        public string ServiceName { get; set; }
        public DateTime StartDate { get; set; }
        public double Amount { get; set; }
    }

}