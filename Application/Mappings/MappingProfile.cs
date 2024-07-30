using System;
using Application.DTOs;
using AutoMapper;
using Domain.Models;

namespace Application.Mappings
{
    public class MappingProfile : Profile
    {
		public MappingProfile()
		{
            CreateMap<Customer, CustomerDto>()
            .ForMember(dto => dto.GenderName, opt => opt.MapFrom(src => src.Gender.GenderName))
            .ForMember(dto => dto.Addresses, opt => opt.MapFrom(src => src.Addresses))
            .ReverseMap();

            //CreateMap<Customer, CreateCustomerDto>();

            CreateMap<Gender, GenderDto>();
            CreateMap<AddressDto, Address>()
                        .ForMember(dest => dest.Country, opt => opt.Ignore())  
                        .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
                        .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => DateTime.UtcNow));  

            CreateMap<Country, CountryDto>();


            CreateMap<Address, AddressDto>()
                .ForMember(dto => dto.CountryName, opt => opt.MapFrom(src => src.Country.CountryName))
                .ReverseMap()
                .ForMember(domain => domain.Country, opt => opt.Ignore()); // Ignore during reverse mapping if necessary
        }
 
	}
}
