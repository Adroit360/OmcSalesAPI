using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OmcSales.API.Helpers.DTOs;
using OmcSales.API.Models;

namespace OmcSales.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailySalesController : ControllerBase
    {
        public ApplicationDbContext DbContext { get; set; }
        public IMapper Mapper { get; set; }

        public DailySalesController(ApplicationDbContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            Mapper = mapper;
        }

        [HttpPost("add")]
        public async Task<ActionResult> AddDailySale(DailySaleDTO dailySalesDTO)
        {
            try
            {
                var tankValues = Mapper.Map<List<TankValue>>(dailySalesDTO.TankValues);
                foreach(var item in tankValues)
                {
                    //if (item.Opening <= 0 || item.Closing <= 0)
                    //    continue;

                    await DbContext.TankValues.AddAsync(item);
                }


                var nozzleValues = Mapper.Map<List<NozzleValue>>(dailySalesDTO.NozzleValues);
                foreach (var item in nozzleValues)
                {
                    //if (item.Opening <= 0 || item.Closing <= 0)
                    //    continue;

                    await DbContext.NozzleValues.AddAsync(item);
                }

                var debtors = dailySalesDTO.Debtors;
                foreach (var item in debtors)
                {
                    //if (string.IsNullOrEmpty(item.DebtorName) || item.ProductId == 0 || item.StationId == 0)
                    //    continue;

                    await DbContext.Debtors.AddAsync(item);
                }

                var creditors = dailySalesDTO.Creditors;
                foreach (var item in creditors)
                {
                    //if (string.IsNullOrEmpty(item.CreditorName) || item.ProductId == 0 || item.StationId == 0)
                    //    continue;

                    await DbContext.Creditors.AddAsync(item);
                }

                await DbContext.SaveChangesAsync();

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest("Sorry an error occured");
            }
        }

        [HttpGet("dates/{stationId}")]
        public async Task<ActionResult<List<string>>> GetValuesDates(int stationId)
        {
            var dates = DbContext.NozzleValues.Where(i => i.Nozzle.Pump.StationId == stationId).Select(i => i.Date.ToString("dddd, dd MMMM yyyy HH:mm:ss"));
            return Ok(dates.ToList());
        }

        [HttpGet("forstation/{stationId}/{_date}")]
        public async Task<ActionResult<List<DailySaleDTO>>> GetDailySales(int stationId,string _date)
        {
            _date = Uri.UnescapeDataString(_date);

            DateTime date = Convert.ToDateTime(_date);

            var stationExists = DbContext.FillingStations.Any(i=>i.FillingStationId == stationId);

            if (!stationExists)
                return BadRequest("Station Not Found");

            var nozzleValues = DbContext.NozzleValues.Where(i => i.Nozzle.Pump.StationId == stationId).ToList().Where(i => compareDates(i.Date,date)).ToList();
            var nozzleValuesTOReturn = Mapper.Map<List<NozzleValueDTO>>(nozzleValues);

            var tankValues = DbContext.TankValues.Where(i => i.Tank.StationId == stationId).ToList().Where(i => compareDates(i.Date, date)).ToList();
            var tankValuesTOReturn = Mapper.Map<List<TankValueDTO>>(tankValues);

            var debtors = DbContext.Debtors.Where(i => i.StationId == stationId).ToList().Where(i => compareDates(i.Date, date)).ToList();

            var creditors = DbContext.Creditors.Where(i => i.StationId == stationId).ToList().Where(i=> compareDates(i.Date, date)).ToList();

            var dailySalesRecord = new DailySaleDTO
            {
                StationId = stationId,
                Date = date,
                TankValues = tankValuesTOReturn,
                NozzleValues = nozzleValuesTOReturn,
                Debtors = debtors,
                Creditors = creditors
            };

            return Ok(dailySalesRecord);
        }

        bool compareDates(DateTime date1,DateTime date2)
        {
            return date1.Year == date2.Year
                && date1.Month == date2.Month
                && date1.Day == date2.Day
                && date1.Hour == date2.Hour
                && date1.Minute == date2.Minute
                && date1.Second == date2.Second;
        }
    }
}