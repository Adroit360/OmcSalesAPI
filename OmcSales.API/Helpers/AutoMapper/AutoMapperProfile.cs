using System;
using AutoMapper;
using OmcSales.API.Helpers.DTOs;
using OmcSales.API.Models;

namespace OmcSales.API.Helpers.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            UserMappings();
            ProductMappings();
            FillingStationMappings();
            PumpMappings();
            NozzleMappings();
            ValuesMappings();
        }

        void UserMappings()
        {

            CreateMap<ApplicationUser, UserForReturnDTO>();
            CreateMap<LoginDTO, ApplicationUser>();
        }

        void ProductMappings()
        {
            CreateMap<Product, ProductForReturnDTO>();
        }

        void FillingStationMappings()
        {
            CreateMap<FillingStation, FillingStationDTO>();

            CreateMap<FillingStationDTO, FillingStation>();

        }

        void PumpMappings()
        {
            CreateMap<Pump, PumpDTO>();
            CreateMap<PumpDTO, Pump>();
        }

        void NozzleMappings()
        {
            CreateMap<Nozzle, NozzleDTO>();
            CreateMap<NozzleDTO, Nozzle>();

        }

        void ValuesMappings()
        {
            CreateMap<TankValue, TankValueDTO>();
            CreateMap<TankValueDTO, TankValue>();


            CreateMap<NozzleValue, NozzleValueDTO>();
            CreateMap<NozzleValueDTO, NozzleValue>();

        }

    }
}
