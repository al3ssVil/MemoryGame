using MemoryGame.Models;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Text.Json;
#pragma warning disable CS8618 

namespace MemoryGame.ViewModels
{
    public class StatisticsViewModel : INotifyPropertyChanged
    {
        private string _statisticsText;

        public string StatisticsText
        {
            get { return _statisticsText; }
            set
            {
                _statisticsText = value;
                OnPropertyChanged(nameof(StatisticsText));
            }
        }

        public StatisticsViewModel()
        {
            var users = LoadUsersFromFile("../../../Data/users.json");
            GenerateStatistics(users);
        }

        private List<User> LoadUsersFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    string json = File.ReadAllText(filePath);

                    if (string.IsNullOrEmpty(json))
                    {
                        return new List<User>();
                    }

                    var users = JsonSerializer.Deserialize<List<User>>(json);

                    return users ?? new List<User>();
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"Error deserializing JSON file: {ex.Message}");
                    return new List<User>();
                }
            }

            return new List<User>();
        }

        private void GenerateStatistics(List<User> users)
        {
            StringBuilder stats = new StringBuilder();

            foreach (var user in users)
            {
                string userInfo = $"{user.Name} - Games Played: {user.GamesWon + user.GamesLost} - Games Won: {user.GamesWon}";
                stats.AppendLine(userInfo);
            }

            StatisticsText = stats.ToString();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.