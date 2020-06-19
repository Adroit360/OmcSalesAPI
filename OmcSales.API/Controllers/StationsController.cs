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
    public class StationsController : ControllerBase
    {
        public ApplicationDbContext DbContext { get; set; }
        public IMapper Mapper { get; set; }
        public StationsController(ApplicationDbContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            Mapper = mapper;
        }

        [HttpGet("foruser/{userId}")]
        public async Task<ActionResult<IEnumerable<FillingStationDTO>>> GetStationsForUser(string userId)
        {
            var stations = DbContext.FillingStations.Where(i => i.UserId == userId);
            var stationsToReturn = Mapper.Map<List<FillingStationDTO>>(stations);
            return Ok(stationsToReturn);
        }

        [HttpGet("formanager/{email}")]
        public async Task<ActionResult<FillingStationDTO>> GetStationForManager(string email)
        {
            var station = await DbContext.FillingStations.FirstOrDefaultAsync(i => i.ManagerEmail == email);
            var stationToReturn = Mapper.Map<FillingStationDTO>(station);
            return Ok(stationToReturn);
        }


        [HttpGet("pumps/{stationId}")]
        public async Task<ActionResult<List<PumpDTO>>> GetPumpsForStation(int stationId)
        {
            var pumps = DbContext.Pumps.Where(i => i.StationId == stationId).Include(i => i.Nozzles);
            var pumpsToReturn = Mapper.Map<List<PumpDTO>>(pumps);

            return Ok(pumpsToReturn);
        }


        [HttpGet("tanks/{stationId}")]
        public async Task<ActionResult<List<Tank>>> GetTanksForStation(int stationId)
        {
            var tanks = DbContext.Tanks.Where(i => i.StationId == stationId);
            return Ok(tanks);
        }


        [HttpPost("add")]
        public async Task<ActionResult> AddStation(FillingStationDTO fillingStationDTO)
        {
            var similarManagerEmail = await DbContext.FillingStations.AnyAsync(i => i.ManagerEmail.ToLower() == fillingStationDTO.ManagerEmail.ToLower());
            if (similarManagerEmail)
            {
                return BadRequest($"The manager with id {fillingStationDTO.ManagerEmail} is already assigned a different station");
            }


            var fillingStation = Mapper.Map<FillingStation>(fillingStationDTO);


            await DbContext.FillingStations.AddAsync(fillingStation);

            await DbContext.SaveChangesAsync();

            foreach(var pump in fillingStation.Pumps)
            {
                
                foreach(var nozzle in pump.Nozzles)
                {

                        var productTankForStation = DbContext.Tanks.FirstOrDefault(i => i.ProductId == nozzle.ProductId && i.StationId == pump.StationId);
                        if(productTankForStation == null)
                        {
                            try
                            {
                                var productName = DbContext.Products.Find(nozzle.ProductId).ProductName;
                                DbContext.Tanks.Add(new Tank
                                {
                                    StationId = pump.StationId,
                                    ProductId = nozzle.ProductId,
                                    TankName = productName + " Tank"
                                });
                            }
                            catch(Exception ex) {  }
                        }
                        DbContext.SaveChanges();

                }
            }


           

            return Ok();
        }

        [HttpPost("edit")]
        public async Task<ActionResult> EditStation(FillingStationDTO fillingStationDTO)
        {
            var fillingStation = Mapper.Map<FillingStation>(fillingStationDTO);

            var stationFromDB = await DbContext.FillingStations.FindAsync(fillingStationDTO.FillingStationId);

            stationFromDB.Name = fillingStationDTO.Name;
            stationFromDB.Location = fillingStationDTO.Location;
            stationFromDB.ManagerEmail = fillingStationDTO.ManagerEmail;

            await DbContext.SaveChangesAsync();

            foreach (var pump in fillingStation.Pumps)
            {


                try
                {
                    var _pump = await DbContext.Pumps.FindAsync(pump.PumpId);

                    if(_pump == null)
                    {
                        await DbContext.Pumps.AddAsync(pump);
                    }
                    else
                    {
                        _pump.PumpName = pump.PumpName;
                        _pump.AttendantName = pump.AttendantName;
                    }
                }
                catch { }

                foreach (var nozzle in pump.Nozzles)
                {
                    var _nozzle = await DbContext.Nozzles.FindAsync(nozzle.NozzleId);

                    if (_nozzle == null)
                    {
                        await DbContext.Nozzles.AddAsync(nozzle);
                    }
                    else
                    {
                        _nozzle.NozzleName = nozzle.NozzleName;
                        _nozzle.ProductId = nozzle.ProductId;
                    }

                    var productTankForStation = DbContext.Tanks.FirstOrDefault(i => i.ProductId == nozzle.ProductId && i.StationId == pump.StationId);
                    if (productTankForStation == null)
                    {
                        try
                        {
                            var productName = DbContext.Products.Find(nozzle.ProductId).ProductName;
                            DbContext.Tanks.Add(new Tank
                            {
                                StationId = pump.StationId,
                                ProductId = nozzle.ProductId,
                                TankName = productName + " Tank"
                            });
                        }
                        catch (Exception ex) { }
                    }
                    
                    //}
                    //catch { }

                }
            }



            DbContext.SaveChanges();
            return Ok();
        }

        [HttpPost("addpump")]
        public async Task<ActionResult<PumpDTO>> AddPump(PumpDTO pumpDTO) {

            try
            {
                var pump = Mapper.Map<Pump>(pumpDTO);
                await DbContext.Pumps.AddAsync(pump);
                await DbContext.SaveChangesAsync();

                var pumpDTOToReturn = Mapper.Map<PumpDTO>(pump);
                return Ok(pumpDTOToReturn);
            }
            catch
            {
                return BadRequest("Cannot add pump");
            }
            
        }

        [HttpDelete("deletepump/{pumpId}")]
        public async Task<ActionResult> DeletePump(int pumpId)
        {
            var pump = await DbContext.Pumps.FindAsync(pumpId);

            var anyNozzleHasValue = pump.Nozzles.Any(i => i.NozzleValues.Count > 0);

            if (anyNozzleHasValue)
            {
                return BadRequest("Cannot delete pump because its has nozzle values");
            }

            DbContext.Pumps.Remove(pump);
            await DbContext.SaveChangesAsync();
            return Ok();
        }


        [HttpPost("addnozzle")]
        public async Task<ActionResult<NozzleDTO>> AddNozzle(NozzleDTO nozzleDTO)
        {
            try
            {
                var nozzle = Mapper.Map<Nozzle>(nozzleDTO);
                await DbContext.Nozzles.AddAsync(nozzle);
                await DbContext.SaveChangesAsync();
                var nozzleDTOToReturn = Mapper.Map<NozzleDTO>(nozzle);
                return Ok(nozzleDTOToReturn);
            }
            catch
            {
                return BadRequest("Cannot add nozzle");
            }
        }


        [HttpDelete("deletenozzle/{nozzleId}")]
        public async Task<ActionResult> DeleteNozzle(int nozzleId)
        {
            var nozzle = await DbContext.Nozzles.FindAsync(nozzleId);

            var nozzleHasValues = nozzle.NozzleValues.Count > 0;
            if (nozzleHasValues)
            {
                return BadRequest("Cannot delete nozzle because its has nozzle values");
            }

            DbContext.Nozzles.Remove(nozzle);
            await DbContext.SaveChangesAsync();
            return Ok();
        }


        [HttpDelete("delete/{stationId}")]
        public async Task<ActionResult> DeleteStation(int stationId)
        {
            var station = DbContext.FillingStations.FirstOrDefault(i => i.FillingStationId == stationId);

            if(station != null)
            {
                DbContext.FillingStations.Remove(station);
                await DbContext.SaveChangesAsync();
            }
            else
            {
                BadRequest("Could not delete this station");
            }

            return Ok();
        }
    }
}