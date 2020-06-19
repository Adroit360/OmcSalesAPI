using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmcSales.API.Models
{
    public class FillingStation
    {
        public int FillingStationId { get; set; }

        public FillingStation()
        {
            Pumps = new List<Pump>();
            Creditors = new List<Creditor>();
            Debtors = new List<Debtor>();
        }
        public string Name { get; set; }
        public string Location { get; set; }
        public string UserId { get; set; }
        public string ManagerEmail { get; set; }
        public virtual List<Pump> Pumps { get; set; }
        public virtual List<Debtor> Debtors { get; set; }
        public virtual List<Creditor> Creditors { get; set; }

    }
}
