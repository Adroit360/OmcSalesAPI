using System;
using System.Collections.Generic;
using OmcSales.API.Models;

namespace OmcSales.API.Helpers.DTOs
{
    public class DailySaleDTO
    {
        public DailySaleDTO()
        {
           
            Debtors = new List<Debtor>();
            Creditors = new List<Creditor>();
            NozzleValues = new List<NozzleValueDTO>();
            TankValues = new List<TankValueDTO>();
        }
        public int StationId { get; set; }
        public DateTime Date { get; set; }
       
        public List<Debtor> Debtors { get; set; }
        public List<Creditor> Creditors { get; set; }
        public List<NozzleValueDTO> NozzleValues { get; set; }
        public List<TankValueDTO> TankValues { get; set; }

    }
}
