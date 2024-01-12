using AutoMapper;
using BLL.DTO;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ConfigurationMapper : Profile
    {
        public ConfigurationMapper()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<GameResult, GameResultDTO>();
            CreateMap<GameResultDTO, GameResult>()
            .ForMember(dest => dest.User, opt => opt.Ignore());
        }
    }
}
