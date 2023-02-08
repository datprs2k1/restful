using API.Data;
using API.Models;
using AutoMapper;

namespace API.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<PersonModel, Person>()
                .ReverseMap();
        }
    }
}
