using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class ResultDTO
    {
        public ICollection<GameResultDTO> GameResults { get; set; }
        public int RatingX { get; set; }
        public int Rating0 { get; set; }
        public string? UserName { get; set; }
    }
}
