using AutoMapper;
using WebModel = StudentAdminPortal.API.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentAdminPortal.API.DataModels;

namespace StudentAdminPortal.API.Profiles
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<Student, WebModel.Student>()
                .ReverseMap();

            CreateMap<Gender, WebModel.Gender>()
                .ReverseMap();

            CreateMap<Address, WebModel.Address>()
                .ReverseMap();

            // Mapping for update method
            CreateMap<Student, WebModel.StudentRequest>()
                .ReverseMap()
                .ForPath(dest => dest.Address.PhysicalAddress, from => from.MapFrom(src => src.PhysicalAddress))
                .ForPath(dest => dest.Address.PostalAddress, from => from.MapFrom(src => src.PostalAddress));
        }
    }
}
