using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Abstraction
{
    public interface IGameService
    {
        public Task<int> CheckGame(List<List<string>> board, int id);
        public Task CreateResult(GameResultDTO result);
    }
}
