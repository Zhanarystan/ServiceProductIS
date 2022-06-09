using API.DTOs;
using API.Models;

namespace API.Core
{
    public class MappingProfiles: AutoMapper.Profile
    {
        public MappingProfiles()
        {
            CreateMap<EstablishmentProduct, EstablishmentProductDto>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.EstablishmentId, o => o.MapFrom(s => s.EstablishmentId))
                .ForMember(d => d.EstablishmentName, o => o.MapFrom(s => s.Establishment.Name))
                .ForMember(d => d.Longitude, o => o.MapFrom(s => s.Establishment.Longitude))
                .ForMember(d => d.Latitude, o => o.MapFrom(s => s.Establishment.Latitude))
                .ForMember(d => d.StartWorkingTime, o => o.MapFrom(s => s.Establishment.StartWorkingTime))
                .ForMember(d => d.EndWorkingTime, o => o.MapFrom(s => s.Establishment.EndWorkingTime))
                .ForMember(d => d.IsOpen, o => o.MapFrom(s => s.Establishment.IsOpen))
                .ForMember(d => d.Address, o => o.MapFrom(s => s.Establishment.Address))
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ProductId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.Name))
                .ForMember(d => d.Price, o => o.MapFrom(s => s.Price))
                .ForMember(d => d.Amount, o => o.MapFrom(s => s.Amount))
                .ForMember(d => d.Metric, o => o.MapFrom(s => s.Product.Metric.Name))
                .ForMember(d => d.ProductBarCode, o => o.MapFrom(s => s.Product.BarCode))
                .ForMember(d => d.PhotoUrl, o => o.MapFrom(s => s.Establishment.PhotoUrl));

            CreateMap<EstablishmentService, EstablishmentServiceDto>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.EstablishmentId, o => o.MapFrom(s => s.EstablishmentId))
                .ForMember(d => d.EstablishmentName, o => o.MapFrom(s => s.Establishment.Name))
                .ForMember(d => d.Longitude, o => o.MapFrom(s => s.Establishment.Longitude))
                .ForMember(d => d.Latitude, o => o.MapFrom(s => s.Establishment.Latitude))
                .ForMember(d => d.StartWorkingTime, o => o.MapFrom(s => s.Establishment.StartWorkingTime))
                .ForMember(d => d.EndWorkingTime, o => o.MapFrom(s => s.Establishment.EndWorkingTime))
                .ForMember(d => d.IsOpen, o => o.MapFrom(s => s.Establishment.IsOpen))
                .ForMember(d => d.Address, o => o.MapFrom(s => s.Establishment.Address))
                .ForMember(d => d.ServiceId, o => o.MapFrom(s => s.ServiceId))
                .ForMember(d => d.ServiceName, o => o.MapFrom(s => s.Service.Name))
                .ForMember(d => d.Price, o => o.MapFrom(s => s.Price))
                .ForMember(d => d.Metric, o => o.MapFrom(s => s.Service.Metric.Name));

            CreateMap<Product, ProductDto>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.BarCode, o => o.MapFrom(s => s.BarCode))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.IsCustom, o => o.MapFrom(s => s.IsCustom))
                .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
                .ForMember(d => d.Metric, o => o.MapFrom(s => s.Metric.Name))
                .ForMember(d => d.Manufacturer, o => o.MapFrom(s => s.Manufacturer.Name));

            CreateMap<Service, ServiceDto>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
                .ForMember(d => d.Metric, o => o.MapFrom(s => s.Metric.Name));

            CreateMap<Establishment, EstablishmentDto>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.BankCardNumber, o => o.MapFrom(s => s.BankCardNumber))
                .ForMember(d => d.Longitude, o => o.MapFrom(s => s.Longitude))
                .ForMember(d => d.Latitude, o => o.MapFrom(s => s.Latitude))
                .ForMember(d => d.StartWorkingTime, o => o.MapFrom(s => s.StartWorkingTime))
                .ForMember(d => d.EndWorkingTime, o => o.MapFrom(s => s.EndWorkingTime))
                .ForMember(d => d.IsOpen, o => o.MapFrom(s => s.IsOpen))
                .ForMember(d => d.Address, o => o.MapFrom(s => s.Address))
                .ForMember(d => d.City, o => o.MapFrom(s => s.City.Name))
                .ForMember(d => d.PhotoUrl, o => o.MapFrom(s => s.PhotoUrl))
                .ForMember(d => d.Products, o => o.MapFrom(s => s.Products))
                .ForMember(d => d.Services, o => o.MapFrom(s => s.Services));

            CreateMap<Establishment, EstablishmentListDto>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.BankCardNumber, o => o.MapFrom(s => s.BankCardNumber))
                .ForMember(d => d.Longitude, o => o.MapFrom(s => s.Longitude))
                .ForMember(d => d.Latitude, o => o.MapFrom(s => s.Latitude))
                .ForMember(d => d.StartWorkingTime, o => o.MapFrom(s => s.StartWorkingTime))
                .ForMember(d => d.EndWorkingTime, o => o.MapFrom(s => s.EndWorkingTime))
                .ForMember(d => d.IsOpen, o => o.MapFrom(s => s.IsOpen))
                .ForMember(d => d.Address, o => o.MapFrom(s => s.Address))
                .ForMember(d => d.City, o => o.MapFrom(s => s.City.Name));

            CreateMap<AppUser, AppUserDto>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Username, o => o.MapFrom(s => s.UserName))
                .ForMember(d => d.Email, o => o.MapFrom(s => s.Email))
                .ForMember(d => d.IIN, o => o.MapFrom(s => s.IIN));
        }
    }
}