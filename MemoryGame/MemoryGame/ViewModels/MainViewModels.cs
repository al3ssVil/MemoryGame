using MemoryGame.Helpers;
using MemoryGame.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Text.Json;
using MemoryGame.Views;

namespace MemoryGame.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();
        private User? selectedUser;
        private string? newUserName;
        private string? selectedImage;
        private int currentImageIndex = 0;
        public bool CanPlayOrDelete => SelectedUser != null;

        public ICommand NewUserCommand { get; }
        public ICommand DeleteUserCommand { get; }
        public ICommand PlayCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand NextImageCommand { get; }
        public ICommand PreviousImageCommand { get; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string prop) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public string? NewUserName//accept null
        {

            get => newUserName;

            set
            {
                newUserName = value;
                OnPropertyChanged(nameof(NewUserName));
            }
        }

        public User? SelectedUser
        {
           
            get => selectedUser;
            set
            {
                selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
                OnPropertyChanged(nameof(CanPlayOrDelete));
                if (selectedUser != null)
                {
                    SelectedImage = selectedUser.ImagePath; 
                }
            }
        }
 
        private List<string> availableImages = new List<string>
        {
            "../Images/Users/Image1.jpg",
            "../Images/Users/Image2.jpg",
            "../Images/Users/Image3.jpg",
            "../Images/Users/Image4.jpg",
            "../Images/Users/Image5.jpg",
            "../Images/Users/Image6.jpg",
            "../Images/Users/Image7.jpg",
            "../Images/Users/Image8.jpg",
            "../Images/Users/Image9.jpg",
            "../Images/Users/Image10.jpg",
            "../Images/Users/Image11.jpg",
            "../Images/Users/Image12.jpg",
            "../Images/Users/Image13.jpg",
            "../Images/Users/Image14.jpg",
            "../Images/Users/Image15.jpg",
            "../Images/Users/Image16.jpg",
            "../Images/Users/Image17.jpg",
            "../Images/Users/Image18.jpg",
            "../Images/Users/Image19.jpg",
            "../Images/Users/Image20.jpg"
        };

        public string? SelectedImage
        {
            get => selectedImage;
            set
            {
                selectedImage = value;
                OnPropertyChanged(nameof(SelectedImage));
            }
        }
        private void CancelApp()
        {
            Application.Current.Shutdown();
        }

        public MainViewModel()
        {
            NewUserCommand = new RelayCommand(_ => AddNewUser());
            DeleteUserCommand = new RelayCommand(_ => DeleteUser(), _ => CanPlayOrDelete);
            PlayCommand = new RelayCommand(_ => PlayGame(), _ => CanPlayOrDelete);
            CancelCommand = new RelayCommand(_ => CancelApp());
            NextImageCommand = new RelayCommand(_ => NextImage());
            PreviousImageCommand = new RelayCommand(_ => PreviousImage());

            SelectedImage = availableImages.FirstOrDefault();

            LoadUsers();
        }

        private void AddNewUser()
        {
            if (string.IsNullOrWhiteSpace(NewUserName))
            {
                MessageBox.Show("Please enter a name for the user.", "Missing Name", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            foreach (var user in Users)
            {
                if (user.Name != null && user.Name.Equals(NewUserName, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("The entered name already exists! Please choose another name.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            var imageToUse = SelectedImage ?? "../Images/Image1.jpg";

            var newUser = new User { Name = NewUserName, ImagePath = imageToUse };
            Users.Add(newUser);
            SaveUsers();
            NewUserName = string.Empty;
        }

        private void DeleteUser()
        {
            if (SelectedUser != null)
            {
                DeleteSavedGame(SelectedUser);
                Users.Remove(SelectedUser);
                SaveUsers();
            }
        }

        private void DeleteSavedGame(User user)
        {
            try
            { 
                if (File.Exists("../../../Data/saved_game.json"))
                {
                    string json = File.ReadAllText("../../../Data/saved_game.json");
                    var allGames = JsonSerializer.Deserialize<List<SavedGame>>(json) ?? new List<SavedGame>();

                    allGames.RemoveAll(g => g.PlayerName == user.Name);

                    string updatedJson = JsonSerializer.Serialize(allGames, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText("../../../Data/saved_game.json", updatedJson);
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while deleting saved game: {ex.Message}");
            }
        }

        private void PlayGame()
        {
            GameWindow gameWindow = new GameWindow();
            var gameViewModel = gameWindow.DataContext as GameViewModel;

            if (gameViewModel != null)
            {
                gameViewModel.SelectedUser = SelectedUser;
            }

            gameWindow.Show();
            Application.Current.MainWindow.Close();
        }

        private void NextImage()
        {
            if (availableImages.Count == 0) return;
            currentImageIndex = (currentImageIndex + 1) % availableImages.Count;
            SelectedImage = availableImages[currentImageIndex];
        }

        private void PreviousImage()
        {
            if (availableImages.Count == 0) return;
            currentImageIndex = (currentImageIndex - 1 + availableImages.Count) % availableImages.Count;
            SelectedImage = availableImages[currentImageIndex];
        }

        private void LoadUsers()
        {
            if (File.Exists("../../../Data/users.json"))
            {
                var json = File.ReadAllText("../../../Data/users.json");
                if (json != null)
                {
                    var list = System.Text.Json.JsonSerializer.Deserialize<List<User>>(json);
                    if(list != null)
                        foreach (var u in list)
                            Users.Add(u);
                }
            }
        }

        private void SaveUsers()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true 
            };
            var json = System.Text.Json.JsonSerializer.Serialize(Users, options);
            File.WriteAllText("../../../Data/users.json", json);
        }
    }
}
