using BLL.DTO;
using BLL.Services;
using BLL.Services.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        IGameService _gameService;
        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> CheckGame( [FromBody] List<List<string>> board, int id)
        {
            try
            {
                var result = await _gameService.CheckGame(board, id).ConfigureAwait(false);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> PostResult(GameResultDTO result)
        {
            try
            {
                await _gameService.CreateResult(result).ConfigureAwait(false);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
