using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OmcSales.API.Models
{
    public class Creditor
    {
        public int CreditorId { get; set; }

        public string CreditorName { get; set; }

        public DateTime Date { get; set; }

        public string AttendantName { get; set; }

        public decimal Amount { get; set; }

        public int ProductId { get; set; }

        public int StationId { get; set; }

        [ForeignKey("StationId")]
        public FillingStation Station { get; set; }
    }
}
