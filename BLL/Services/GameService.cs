using AutoMapper;
using BLL.DTO;
using BLL.Services.Abstraction;
using DAL.Abstracts;
using DAL.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class GameService : IGameService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<GameResult> _resultRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public GameService(IRepository<User> userRepository, IRepository<GameResult> resultRepository, IMapper mapper, IConfiguration config)
        {
            _userRepository = userRepository;
            _resultRepository = resultRepository;
            _mapper = mapper;
            _config = config;
        }
        static readonly List<List<int>> WinningCombinations = new List<List<int>>
        {
        new List<int> { 0, 1, 2 }, // Верхня горизонталь
        new List<int> { 3, 4, 5 }, // Середня горизонталь
        new List<int> { 6, 7, 8 }, // Нижня горизонталь
        new List<int> { 0, 3, 6 }, // Ліва вертикаль
        new List<int> { 1, 4, 7 }, // Средня вертикаль
        new List<int> { 2, 5, 8 }, // Права вертикаль
        new List<int> { 0, 4, 8 }, // Діагональ зліва направо
        new List<int> { 2, 4, 6 }  // Діагональ зправа наліво
        };
        public async Task<int> CheckGame(List<List<string>> board, int id)
        {
            var result = new GameResult();
            var winner = await CheckWinner(board).ConfigureAwait(false);
            result.UserId = id;
            if (winner == null)
            {
                if (IsBoardFull(board))
                {
                    result.Winner = 3;
                    await _resultRepository.Create(result).ConfigureAwait(false);
                    return 3; // Нічия
                }
                else
                {
                    return 0; // Гра продовжується
                }
            }
            else if (winner == "X")
            {
                result.Winner = 1;
                var user = await _userRepository.GetById(id).ConfigureAwait(false);
                user.RatingX = user.RatingX + 1;
                await _userRepository.Update(user);
                await _resultRepository.Create(result).ConfigureAwait(false);
                return 1; // Виграв гравець Х
            }
            else
            {
                result.Winner = 2;
                var user = await _userRepository.GetById(id).ConfigureAwait(false);
                user.Rating0 = user.Rating0 + 1;
                await _userRepository.Update(user);
                await _resultRepository.Create(result).ConfigureAwait(false);
                return 2; // Виграв гравець О
            }   
        }
        static private async Task<string> CheckWinner(List<List<string>> board)
        {
            if (board.Count != 3 || board.Any(row => row.Count != 3))
            {
                throw new ArgumentException("Невірний розмір ігрового поля");
            }

            foreach (var combination in WinningCombinations)
            {
                var symbol = board[combination[0] / 3][combination[0] % 3];
                if (symbol != " " && board[combination[1] / 3][combination[1] % 3] == symbol && board[combination[2] / 3][combination[2] % 3] == symbol)
                {
                    return symbol;
                }
            }

            return null;
        }
        static private bool IsBoardFull(List<List<string>> board)
        {
            if (board.Count != 3 || board.Any(row => row.Count != 3))
            {
                throw new ArgumentException("Невірний розмір ігрового поля");
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (i >= board.Count || j >= board[i].Count)
                    {
                        continue;
                    }

                    if (board[i][j] == " ")
                    {
                        return false;
                    }
                }
            }
            return true; // Всі клітинки заповнені, гра в нічию
        }
        public async Task CreateResult(GameResultDTO result)
        {
            await _resultRepository.Create(_mapper.Map<GameResult>(result));
        }
    }
}
