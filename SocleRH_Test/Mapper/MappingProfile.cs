using AutoMapper;
using SocleRH_Test.Dto;
using SocleRH_Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocleRH_Test.Mapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Currency, CurrencyDto>();
            CreateMap<CurrencyDto, Currency>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<Expense, ExpenseDto>();
            CreateMap<ExpenseDto, Expense>();
           
        }
    }
}
