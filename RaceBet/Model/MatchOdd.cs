using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RaceBet.Model
{
    public class MatchOdd
    {
        public int ID { get; set; }
        public int MatchId { get; set; }
        [Required]
        public string Specifier { get; set; }
        [Required]
        public decimal Odd { get; set; }
        public Match Match { get; set; }
    }
}
