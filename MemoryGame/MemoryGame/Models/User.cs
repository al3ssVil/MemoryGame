using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame.Models
{
    public class User
    {
        public string? Name { get; set; }
        public string? ImagePath { get; set; }
        public int GamesWon { get; set; }
        public int GamesLost { get; set; }
        public List<GameStatistic> GameHistory { get; set; } = new List<GameStatistic>();
    }

    public class GameStatistic
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public string? Category { get; set; }
        public int TimeRemain { get; set; } 
        public bool IsWon { get; set; }
    }

    public class SavedGame
    {
        public string? PlayerName { get; set; }
        public int TimeRemaining { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public List<CardState> CardStates { get; set; } = new List<CardState>();
    }
    public class CardState
    {
        public string? ImagePath { get; set; }
        public string? Content { get; set; }
        public string? Tag { get; set; }
        public bool IsFlipped { get; set; } 
    }
}
