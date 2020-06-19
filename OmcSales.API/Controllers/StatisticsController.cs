using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OmcSales.API.Helpers.DTOs;
using OmcSales.API.Models;

namespace OmcSales.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        public ApplicationDbContext DbContext { get; set; }
        public IMapper Mapper { get; set; }

        public StatisticsController(ApplicationDbContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            Mapper = mapper;
        }

        [HttpGet("forstation/{stationId}")]
        public async Task<ActionResult<List<StatisticsDTO>>> GetStatisticsForStation(int stationId)
        {
            try
            {
                List<StatisticsDTO> statisticsDTOs = new List<StatisticsDTO>();

                var tanks = DbContext.Tanks.Where(i => i.StationId == stationId).Include(i=>i.Tankvalues);

                foreach(var tank in tanks)
                {
                    var statisticsDTO = new StatisticsDTO
                    {
                        Tank = tank
                    };

                    var firstTankValue = tank.Tankvalues.FirstOrDefault();
                    var firstOpening = (firstTankValue == null | firstTankValue?.Opening == 0) ? 1 : firstTankValue.Opening;

                    var lastTankValue = tank.Tankvalues.LastOrDefault();
                    var lastClosing = lastTankValue == null  ? 1 : lastTankValue.Closing;

                    statisticsDTO.FuelPercentage = (lastClosing/firstOpening) * 100;
                    statisticsDTO.LastUpdated = lastTankValue.Date;
                    statisticsDTO.FirstOpening = firstOpening;

                    statisticsDTO.LastClosing = lastClosing;

                    statisticsDTOs.Add(statisticsDTO);
                }

                return Ok(statisticsDTOs);
            }
            catch (Exception ex)
            {
                return BadRequest("Error occured");
            }
        }
    }
}