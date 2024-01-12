using AutoMapper;
using BLL.DTO;
using BLL.Services.Abstraction;
using DAL.Abstracts;
using DAL.Models;
using DAL.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<GameResult> _resultRepository;
        private readonly IMapper _mapper;
        public UserService(IRepository<User> userRepository, IRepository<GameResult> resultRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _resultRepository = resultRepository;
            _mapper = mapper;
        }
        public async Task<int> CreateUser(UserDTO userDTO)
        {
            await _userRepository.Create(_mapper.Map<User>(userDTO));
            var user = (await _userRepository.Get())
            .Where(u => u.Name == userDTO.Name)
            .FirstOrDefault();
            return user.Id;
        }
        public async Task<int> Login(LoginDTO login)
        {
            var user = (await _userRepository.Get())
           .Where(d => d.Name == login.Name)
           .FirstOrDefault();
            if (user != null && user.Password == login.Password)
            {
                return user.Id;
            }
            else return 0;
        }
        public async Task DeleteUser(int id)
        {
            await _userRepository.Delete(id).ConfigureAwait(false);
        }
        public async Task UpdateUser(UserDTO user)
        {
            await _userRepository.Update(_mapper.Map<User>(user)).ConfigureAwait(false);
        }
        public async Task<UserDTO> GetUserById(int id)
        {
            return _mapper.Map<UserDTO>(await _userRepository.GetById(id).ConfigureAwait(false));
        }
        public async Task<ICollection<UserDTO>> GetAllUsers()
        {
            return _mapper.Map<ICollection<UserDTO>>(await _userRepository.Get().ConfigureAwait(false));
        }
        public async Task<ResultDTO> GetResult(int id)
        {
            var gameResults = (await _resultRepository.Get().ConfigureAwait(false))
                .Where(d => d.UserId == id)
                .ToList();
            var user = await _userRepository.GetById(id).ConfigureAwait(false);
            var results = new ResultDTO();
            results.GameResults = _mapper.Map<ICollection<GameResultDTO>>(gameResults);
            results.UserName = user.Name;
            results.RatingX = user.RatingX;
            results.Rating0 = user.Rating0;
            int i=0;
            foreach (var result in results.GameResults) 
            {
                i++;
                result.Counter = i;
            }
            return results;
        }
    }
}
