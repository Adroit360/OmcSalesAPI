using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OmcSales.API.Models
{
    public class Debtor
    {
        public int DebtorId { get; set; }

        public string DebtorName { get; set; }

        public DateTime Date { get; set; }

        public string AttendantName { get; set; }

        public decimal Amount { get; set; }

        public int ProductId { get; set; }

        public int StationId { get; set; }

        [ForeignKey("StationId")]
        public FillingStation Station { get; set; }
    }
}


