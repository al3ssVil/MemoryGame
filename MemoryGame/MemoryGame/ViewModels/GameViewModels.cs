using MemoryGame.Helpers;
using MemoryGame.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Application = System.Windows.Application;
using System.Windows.Threading;
using MemoryGame.Models;
using System.Text.Json;
using System.IO;
using System.Text;
using MainWindow = MemoryGame.Views.MainWindow;
#pragma warning disable CS8622
#pragma warning disable CS8618

namespace MemoryGame.ViewModels
{
    class GameViewModel : INotifyPropertyChanged
    {
        #region IComand
        public ICommand SelectCategoryCommand { get; private set; }
        public ICommand NewGameCommand { get; private set; }
        public ICommand OpenGameCommand { get; private set; }
        public ICommand SaveGameCommand { get; private set; }
        public ICommand ShowStatisticsCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }
        public ICommand SetStandardBoardSizeCommand { get; private set; }
        public ICommand SetCustomBoardSizeCommand { get; private set; }
        public ICommand ShowAboutCommand { get; private set; }
        public ICommand CardClickedCommand { get; private set; }
        public ICommand StartTimerCommand { get; private set; }

        #endregion  

        #region  Game State and Configuration
        public ObservableCollection<string> Categories { get; set; }=new ObservableCollection<string>();
        public string SelectedCategory { get; set; }

        List<string> imageAnimals = new List<string> 
        { 
            "../../../Images/Animals/Animal1.jpg",
            "../../../Images/Animals/Animal2.jpg",
            "../../../Images/Animals/Animal3.jpg",
            "../../../Images/Animals/Animal4.jpg",
            "../../../Images/Animals/Animal5.jpg",
            "../../../Images/Animals/Animal6.jpg",
            "../../../Images/Animals/Animal7.jpg",
            "../../../Images/Animals/Animal8.jpg",
            "../../../Images/Animals/Animal9.jpg",
            "../../../Images/Animals/Animal10.jpg",
            "../../../Images/Animals/Animal11.jpg",
            "../../../Images/Animals/Animal12.jpg",
            "../../../Images/Animals/Animal13.jpg",
            "../../../Images/Animals/Animal14.jpg",
            "../../../Images/Animals/Animal15.jpg",
            "../../../Images/Animals/Animal16.jpg",
            "../../../Images/Animals/Animal17.jpg",
            "../../../Images/Animals/Animal18.jpg",
            "../../../Images/Animals/Animal19.jpg",
            "../../../Images/Animals/Animal20.jpg"
        };
        List<string> imageNature = new List<string> 
        {
            "../../../Images/Nature/Nature1.jpg",
            "../../../Images/Nature/Nature2.jpg",
            "../../../Images/Nature/Nature3.jpg",
            "../../../Images/Nature/Nature4.jpg",
            "../../../Images/Nature/Nature5.jpg",
            "../../../Images/Nature/Nature6.jpg",
            "../../../Images/Nature/Nature7.jpg",
            "../../../Images/Nature/Nature8.jpg",
            "../../../Images/Nature/Nature9.jpg",
            "../../../Images/Nature/Nature10.jpg",
            "../../../Images/Nature/Nature11.jpg",
            "../../../Images/Nature/Nature12.jpg",
            "../../../Images/Nature/Nature13.jpg",
            "../../../Images/Nature/Nature14.jpg",
            "../../../Images/Nature/Nature15.jpg",
            "../../../Images/Nature/Nature16.jpg",
            "../../../Images/Nature/Nature17.jpg",
            "../../../Images/Nature/Nature18.jpg",
            "../../../Images/Nature/Nature19.jpg",
            "../../../Images/Nature/Nature20.jpg"
        };
        List<string> imageFlowers = new List<string> 
        {
            "../../../Images/Flowers/Flower1.jpg",
            "../../../Images/Flowers/Flower2.jpg",
            "../../../Images/Flowers/Flower3.jpg",
            "../../../Images/Flowers/Flower4.jpg",
            "../../../Images/Flowers/Flower5.jpg",
            "../../../Images/Flowers/Flower6.jpg",
            "../../../Images/Flowers/Flower7.jpg",
            "../../../Images/Flowers/Flower8.jpg",
            "../../../Images/Flowers/Flower9.jpg",
            "../../../Images/Flowers/Flower10.jpg",
            "../../../Images/Flowers/Flower11.jpg",
            "../../../Images/Flowers/Flower12.jpg",
            "../../../Images/Flowers/Flower13.jpg",
            "../../../Images/Flowers/Flower14.jpg",
            "../../../Images/Flowers/Flower15.jpg",
            "../../../Images/Flowers/Flower16.jpg",
            "../../../Images/Flowers/Flower17.jpg",
            "../../../Images/Flowers/Flower18.jpg",
            "../../../Images/Flowers/Flower19.jpg",
            "../../../Images/Flowers/Flower20.jpg"
        };


        public ObservableCollection<Button> GameCards { get; set; }
        public bool isContinued = false;

        private Button? firstCard = null;
        private Button? secondCard = null;
        private bool isChecking = false;
        public ObservableCollection<int> RowOptions { get; set; }
        public ObservableCollection<int> ColumnOptions { get; set; }

        private bool _isGameInActive = true;
        public bool IsGameInactive
        {
            get { return _isGameInActive; }
            set
            {
                if (_isGameInActive != value)
                {
                    _isGameInActive = value;
                    OnPropertyChanged(nameof(IsGameInactive));
                }
            }
        }

        private int _selectedRows;
        public int SelectedRows
        {
            get { return _selectedRows; }
            set
            {
                if (_selectedRows != value)
                {
                    _selectedRows = value;
                    if (_selectedColumns != 0 && _selectedRows != 0)
                        WelcomeTextVisibility = false;
                }
            }
        }

        private int _selectedColumns;
        public int SelectedColumns
        {
            get { return _selectedColumns; }
            set
            {
                if (_selectedColumns != value)
                {
                    _selectedColumns = value;
                    if(_selectedColumns != 0&&_selectedRows!=0)
                        WelcomeTextVisibility = false;
                }
            }
        }
        public double ButtonWidth { get; set; }
        public double ButtonHeight { get; set; }


        private bool _welcomeTextVisibility = true; 
        public bool WelcomeTextVisibility
        {
            get { return _welcomeTextVisibility; }
            set
            {
                if (_welcomeTextVisibility != value)
                {
                    _welcomeTextVisibility = value;
                    OnPropertyChanged(nameof(WelcomeTextVisibility)); 
                }
            }
        }

        private DispatcherTimer _gameTimer;
        private int _elapsedTime; 
        public int ElapsedTime
        {
            get { return _elapsedTime; }
            set
            {
                if (_elapsedTime != value)
                {
                    _elapsedTime = value;
                    OnPropertyChanged(nameof(ElapsedTime)); 
                }
            }
        }

        private int _customTimer=60;
        public int CustomTimer
        {
            get { return _customTimer; }
            set
            {
                if (_customTimer != value)
                {
                    if (value < 0 || value == 0)
                        MessageBox.Show("Invalid value, enter another one");
                    else
                    {
                        _customTimer = value;
                        OnPropertyChanged(nameof(CustomTimer));
                    }
                }
            }
        }

        public bool IsTimerRunning { get; private set; } = false; 

        private User? _selectedUser;
        public User? SelectedUser
        {
            get => _selectedUser;
            set
            {
                if (_selectedUser != value)
                {
                    _selectedUser = value;
                    OnPropertyChanged(nameof(SelectedUser));
                }
            }
        }
        #endregion


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public GameViewModel()
        {
            Categories = new ObservableCollection<string> { "Animals", "Nature", "Flowers" };
            SelectedCategory = Categories[0]; 

            RowOptions = new ObservableCollection<int> { 2, 3, 4, 5, 6 };
            ColumnOptions = new ObservableCollection<int> { 2, 3, 4, 5, 6 };

            Random rng = new Random();
            imageAnimals.OrderBy(x => rng.Next()).ToList();
            imageNature.OrderBy(x => rng.Next()).ToList();
            imageFlowers.OrderBy(x => rng.Next()).ToList();
            SelectCategoryCommand = new RelayCommand(SelectCategory);
            NewGameCommand = new RelayCommand(NewGame);
            OpenGameCommand = new RelayCommand(OpenGame);
            SaveGameCommand = new RelayCommand(SaveGame);
            ShowStatisticsCommand = new RelayCommand(ShowStatistics);
            ExitCommand = new RelayCommand(Exit);
            SetStandardBoardSizeCommand = new RelayCommand(SetStandardBoardSize);
            SetCustomBoardSizeCommand = new RelayCommand(SetCustomBoardSize);
            ShowAboutCommand = new RelayCommand(ShowAbout);
            CardClickedCommand = new RelayCommand(CardClicked);

            _gameTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1) 
            };
            _gameTimer.Tick += OnTimerTick; 

            StartTimerCommand = new RelayCommand(StartTimer);
        }

        private void StartTimer(object? obj)
        {
            if (SelectedColumns == 4 && SelectedRows == 4)
                _elapsedTime = 60;
            else
            {
                _elapsedTime = CustomTimer;
                if (_elapsedTime<=0)
                {
                    MessageBox.Show("Please enter a positive timer in order to start the game");
                    return;
                }   
            }
            Application.Current.Dispatcher.Invoke(() =>
            {
                ElapsedTime = _elapsedTime;  
            });
            _gameTimer.Start();
            IsTimerRunning = true;
            OnPropertyChanged(nameof(IsTimerRunning));
            OnPropertyChanged(nameof(ElapsedTime));
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            if (_elapsedTime > 0)
            {
                _elapsedTime--;
                ElapsedTime = _elapsedTime;  // Update UI
                OnPropertyChanged(nameof(ElapsedTime));
            }

            if (_elapsedTime == 0)  
            {
                SaveGameStatistics(false);
                ResetGame();
                MessageBox.Show($"{SelectedUser?.Name} ran out of time!");
                if (isContinued)
                {
                    if (File.Exists("../../../Data/saved_game.json"))
                    {
                        string json = File.ReadAllText("../../../Data/saved_game.json");
                        var allGames = JsonSerializer.Deserialize<List<SavedGame>>(json);

                        allGames?.RemoveAll(g => g.PlayerName == SelectedUser?.Name);

                        string updatedJson = JsonSerializer.Serialize(allGames, new JsonSerializerOptions { WriteIndented = true });
                        File.WriteAllText("../../../Data/saved_game.json", updatedJson);
                    }
                }
            }
        }

        private void SaveGameStatistics(bool isWon)
        {
            if (SelectedUser != null)
            {
                var gameStatistic = new GameStatistic
                {
                    Rows = SelectedRows,
                    Columns = SelectedColumns,
                    Category = SelectedCategory,
                    TimeRemain = ElapsedTime,
                    IsWon = isWon
                };

                SelectedUser.GameHistory.Add(gameStatistic);

                if (isWon)
                {
                    SelectedUser.GamesWon++;
                }
                else
                {
                    SelectedUser.GamesLost++;
                }

                SaveUserStatistics(SelectedUser);
            }
        }

        private void SaveUserStatistics(User user)
        {
            List<User> users = LoadUsersFromFile();

            var existingUser = users.FirstOrDefault(u => u.Name == user.Name);

            if (existingUser != null)
            {
                existingUser.GamesWon = user.GamesWon;
                existingUser.GamesLost = user.GamesLost;
                existingUser.GameHistory = user.GameHistory;
            }

            SaveUsersToFile(users);
        }

        private List<User> LoadUsersFromFile()
        {
            if (File.Exists("../../../Data/users.json"))
            {
                try
                {
                    string json = File.ReadAllText("../../../Data/users.json");

                    if (string.IsNullOrEmpty(json))
                    {
                        return new List<User>();
                    }

                    var users = JsonSerializer.Deserialize<List<User>>(json);

                    if (users == null)
                    {
                        MessageBox.Show("Failed to deserialize users.");
                        return new List<User>();
                    }

                    return users;
                }
                catch (JsonException ex)
                {
                    MessageBox.Show($"Error deserializing JSON: {ex.Message}");
                    return new List<User>();
                }
            }
            else
            {
                return new List<User>();
            }
        }

        private void SaveUsersToFile(List<User> users)
        {
            string json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            //MessageBox.Show($"Serialized JSON: {json}");
            try
            {
                File.WriteAllText("../../../Data/users.json", json);
                //MessageBox.Show("Users data saved successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving users data: {ex.Message}");
            }
        }

        private void StopTimer()
        {
            _gameTimer.Stop();
            ElapsedTime = 0;
            IsTimerRunning = false;
        }

        private void SelectCategory(object category)
        {
            //MessageBox.Show($"Category selected: {category}");
        }

        private void NewGame(object obj)
        {
            if (SelectedColumns % 2 == 1 && SelectedRows % 2 == 1)
            {
                MessageBox.Show("It must be an even number of cards, choose other values");
                return;
            }
            if (SelectedColumns != 0 && SelectedRows != 0)
            {
                isContinued = false;
                IsGameInactive = false;
                WelcomeTextVisibility = false;
                //MessageBox.Show($"Starting a new game with category: {SelectedCategory} and size: {SelectedRows}x{SelectedColumns}");
                OnPropertyChanged(nameof(SelectedRows));
                OnPropertyChanged(nameof(SelectedColumns));
                GenerateGameBoard();
                StartTimer(null);
            }
        }
        
        private void ResetGame()
        {
            StopTimer();
            _gameTimer.Stop();
            IsTimerRunning = false;
            OnPropertyChanged(nameof(IsTimerRunning));
            IsGameInactive = true;
            foreach (var card in GameCards.ToList())
            {
                GameCards.Remove(card);
            }
        }

        private void CheckForWin()
        {
            if(GameCards.Count == 0)
            {
                return;
            }
            bool allMatched = GameCards.All(card => !card.IsEnabled);

            if (allMatched)
            {
                SaveGameStatistics(true);
                ResetGame();
                MessageBox.Show($"Congratulations {SelectedUser?.Name}! You won the game!");
                if(isContinued)
                {
                    if (File.Exists("../../../Data/saved_game.json"))
                    {
                        string json = File.ReadAllText("../../../Data/saved_game.json");
                        var allGames = JsonSerializer.Deserialize<List<SavedGame>>(json);

                        allGames?.RemoveAll(g => g.PlayerName == SelectedUser?.Name);

                        string updatedJson = JsonSerializer.Serialize(allGames, new JsonSerializerOptions { WriteIndented = true });
                        File.WriteAllText("../../../Data/saved_game.json", updatedJson);
                    }
                }
            }
        }

        private void OpenGame(object obj)
        {
            try
            {
                WelcomeTextVisibility = false;
                if (File.Exists("../../../Data/saved_game.json"))
                {
                    string json = File.ReadAllText("../../../Data/saved_game.json");
                    var allGames = JsonSerializer.Deserialize<List<SavedGame>>(json);

                    var savedGame = allGames?.FirstOrDefault(g => g.PlayerName == SelectedUser?.Name);

                    if (savedGame != null)
                    {
                        isContinued= true;
                        IsGameInactive = false;
                        _elapsedTime = savedGame.TimeRemaining;
                        SelectedRows = savedGame.Rows;
                        SelectedColumns = savedGame.Columns;

                        GameCards = new ObservableCollection<Button>();

                        double windowWidth = Application.Current.MainWindow.ActualWidth - 200;
                        double windowHeight = Application.Current.MainWindow.ActualHeight - 50;

                        ButtonWidth = windowWidth / SelectedColumns - 5 * SelectedColumns;
                        ButtonHeight = windowHeight / SelectedRows - 5 * SelectedRows;

                        foreach (var cardState in savedGame.CardStates)
                        {
                            var cardButton = new Button
                            {
                                Width = ButtonWidth,
                                Height = ButtonHeight,
                                Tag = cardState.Tag,
                                Background = cardState.IsFlipped ? Brushes.Transparent : Brushes.Gray,
                                Foreground = Brushes.White,
                                FontSize = 24,
                                FontWeight = FontWeights.Bold,
                                Command = CardClickedCommand,
                                CommandParameter = null,
                            };
                            if (cardState.IsFlipped&& cardState.ImagePath != null)
                            {
                                cardButton.Background = new ImageBrush
                                {
                                    ImageSource = new BitmapImage(new Uri(cardState.ImagePath, UriKind.Relative)),
                                    Stretch = Stretch.Uniform
                                };
                                cardButton.IsEnabled = false;
                            }
                            else
                            {
                                cardButton.Content = "?";
                                cardButton.CommandParameter = cardButton;
                            }

                            GameCards.Add(cardButton);
                        }
                        OnPropertyChanged(nameof(SelectedRows));
                        OnPropertyChanged(nameof(SelectedColumns));

                        OnPropertyChanged(nameof(GameCards));

                        _gameTimer.Start();
                        IsTimerRunning = true;
                        OnPropertyChanged(nameof(IsTimerRunning));
                        OnPropertyChanged(nameof(ElapsedTime));

                        //MessageBox.Show("Game loaded successfully.");
                    }
                    else
                    {
                        MessageBox.Show("No saved game found for this user.");
                    }
                }
                else
                {
                    MessageBox.Show("No saved game found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading the game: {ex.Message}");
            }
        }

        private void SaveGame(object obj)
        {
            if (IsGameInactive == true)
            {
                MessageBox.Show("You must start a game before you can save it!");
                return;
            }
            try
            {
                var savedGame = new SavedGame
                {
                    PlayerName = SelectedUser?.Name,
                    TimeRemaining = _elapsedTime,
                    Rows = SelectedRows,
                    Columns = SelectedColumns
                };

                StopTimer();

                foreach (var card in GameCards)
                {
                    if (card.Tag is string imagePath) 
                    {
                        var cardState = new CardState
                        {
                            ImagePath = imagePath,
                            Content = card.Content?.ToString(),
                            Tag = card.Tag?.ToString(),
                            IsFlipped = !(bool)card.IsEnabled
                        };

                        savedGame.CardStates.Add(cardState);
                    }
                }

                List<SavedGame> allGames = new List<SavedGame>();
                if (File.Exists("../../../Data/saved_game.json"))
                {
                    var existingJson = File.ReadAllText("../../../Data/saved_game.json");
                    allGames = JsonSerializer.Deserialize<List<SavedGame>>(existingJson) ?? new List<SavedGame>();
                }

                allGames.RemoveAll(g => g.PlayerName == SelectedUser?.Name);

                allGames.Add(savedGame);

                string updatedJson = JsonSerializer.Serialize(allGames, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText("../../../Data/saved_game.json", updatedJson);

                ResetGame();

                //MessageBox.Show("Game saved.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving the game: {ex.Message}");
            }
        }

        private void ShowStatistics(object obj)
        {
            StatisticsWindow statistics = new StatisticsWindow();
            statistics.Show();
        }

        private void Exit(object obj)
        {
            MessageBox.Show("Exiting the game...");
            if(GameCards!=null) 
                SaveGame(GameCards);
            Window? currentWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);

            if (currentWindow != null)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();

                currentWindow.Close();
            }
        }

        private void SetStandardBoardSize(object obj)
        {
            //MessageBox.Show("Standard board size selected: 4x4");
            SelectedRows = 4;
            SelectedColumns = 4;
        }

        private void SetCustomBoardSize(object obj)
        {
            WelcomeTextVisibility = false;
            //MessageBox.Show($"Custom board size selected: {SelectedRows}x{SelectedColumns}");
        }

        private void ShowAbout(object obj)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.Show();
        }

        private void GenerateGameBoard()
        {
            if (SelectedColumns > 0 && SelectedRows > 0)
            {
                GameCards = new ObservableCollection<Button>();

                double windowWidth = Application.Current.MainWindow.ActualWidth-200;
                double windowHeight = Application.Current.MainWindow.ActualHeight-50;

                //MessageBox.Show($"{windowWidth}, {windowHeight}");
                ButtonWidth = windowWidth / SelectedColumns-5*SelectedColumns;
                ButtonHeight = windowHeight / SelectedRows-5*SelectedRows;

                List<string> imagePaths = new List<string>();

                int maxImages = SelectedRows * SelectedColumns / 2;  

                if (SelectedCategory == "Animals")
                {
                    for (int i = 0; i < maxImages; i++)
                    {
                        if (i < imageAnimals.Count)
                        {
                            imagePaths.Add(imageAnimals[i]);
                            imagePaths.Add(imageAnimals[i]); 
                        }
                    }
                }
                else if (SelectedCategory == "Nature")
                {
                    for (int i = 0; i < maxImages; i++)
                    {
                        if (i < imageNature.Count)
                        {
                            imagePaths.Add(imageNature[i]);
                            imagePaths.Add(imageNature[i]);
                        }
                    }
                }
                else 
                {
                    for (int i = 0; i < maxImages; i++)
                    {
                        if (i < imageFlowers.Count)
                        {
                            imagePaths.Add(imageFlowers[i]);
                            imagePaths.Add(imageFlowers[i]); 
                        }
                    }
                }

                Random rng = new Random();
                var shuffledImages = imagePaths.OrderBy(x => rng.Next()).ToList();

                int imageIndex = 0;
                for (int i = 0; i < SelectedRows * SelectedColumns; i++)
                {
                    var cardButton = new Button
                    {
                        Width = ButtonWidth,
                        Height = ButtonHeight,
                        Content = "?",
                        Tag = shuffledImages[imageIndex], 
                        Background = Brushes.Gray, 
                        Foreground = Brushes.White,
                        FontSize = 24,
                        FontWeight = FontWeights.Bold,
                        Command = CardClickedCommand,
                        CommandParameter = null 
                    };

                    cardButton.CommandParameter = cardButton;

                    imageIndex++;

                    GameCards.Add(cardButton);
                }

                OnPropertyChanged(nameof(GameCards));

            }
        }

        private void CardClicked(object obj)
        {
            if (isChecking) return; 

            if (obj is Button clickedButton)
            {
                string? imagePath = clickedButton.Tag as string;

                if (clickedButton.IsEnabled && imagePath!=null)
                {
                    double buttonWidth = clickedButton.ActualWidth;
                    double buttonHeight = clickedButton.ActualHeight;

                    var imageBrush = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri(imagePath, UriKind.Relative)),
                        Stretch = Stretch.Uniform,
                        AlignmentX = AlignmentX.Center,
                        AlignmentY = AlignmentY.Center
                    };

                    clickedButton.Background = imageBrush;
                    clickedButton.Content = null; 
                    clickedButton.IsEnabled = false; 

                    if (firstCard == null)
                    {
                        firstCard = clickedButton;
                    }
                    else if (secondCard == null)
                    {
                        secondCard = clickedButton;
                        isChecking = true;

                        Task.Delay(1000).ContinueWith(_ =>
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                if (firstCard.Tag.ToString() == secondCard.Tag.ToString())
                                {
                                    firstCard = null;
                                    secondCard = null;
                                }
                                else
                                {
                                    firstCard.Background = Brushes.Gray;
                                    firstCard.Content = "?";
                                    firstCard.IsEnabled = true;

                                    secondCard.Background = Brushes.Gray;
                                    secondCard.Content = "?";
                                    secondCard.IsEnabled = true;

                                    firstCard = null;
                                    secondCard = null;
                                }
                                CheckForWin();
                                isChecking = false;
                            });
                        });
                    }
                }
            }
        }
    }
}
#pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
#pragma warning restore CS8618