using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RaceBet.Model
{
    public enum Sport
    {
        Football=1,
        Basketball=2
    }
    public class Match
    {
        public int ID { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime MatchDateTime { get; set; }
        [Required]
        public string TeamA { get; set; }
        [Required]
        public string TeamB { get; set; }
        [Required]
        [Range(1, 2)]
        public Sport Sport { get; set; }
    }
}
