using AutoMapper;
using FreelancerAPI.Application.DTOs;
using FreelancerAPI.Domain.Entities;

namespace FreelancerAPI.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Freelancer, FreelancerDto>()
                .ForMember(dest => dest.Skillsets, opt => opt.MapFrom(src => 
                    src.FreelancerSkillsets.Select(fs => fs.Skillset.Name).ToList()))
                .ForMember(dest => dest.Hobbies, opt => opt.MapFrom(src => 
                    src.FreelancerHobbies.Select(fh => fh.Hobby.Name).ToList()));

            CreateMap<CreateFreelancerDto, Freelancer>();
            CreateMap<UpdateFreelancerDto, Freelancer>();
        }
    }
}