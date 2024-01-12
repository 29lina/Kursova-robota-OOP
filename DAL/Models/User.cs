using DAL.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class User : TEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public List<GameResult> GameResultList { get; set; }
        public int RatingX { get; set; }
        public int Rating0 { get; set; }
    }
}
