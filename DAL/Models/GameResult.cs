using DAL.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class GameResult : TEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Winner { get; set; }
        public User User { get; set; }
    }
}
