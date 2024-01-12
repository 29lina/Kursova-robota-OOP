using BLL.DTO;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Abstraction
{
    public interface IUserService
    {
        public Task<int> CreateUser(UserDTO userDTO);
        public Task<int> Login(LoginDTO login);
        public  Task DeleteUser(int id);
        public Task UpdateUser(UserDTO user);
        public Task<UserDTO> GetUserById(int id);
        public Task<ICollection<UserDTO>> GetAllUsers();
        public Task<ResultDTO> GetResult(int id);
    }
}
