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
#pragma warning disable CS8622
#pragma warning disable CS8618

namespace MemoryGame.ViewModels
{
    class GameViewModel : INotifyPropertyChanged
    {
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


        public ObservableCollection<string> Categories { get; set; }
        public string SelectedCategory { get; set; }

        List<string> imageAnimals = new List<string> 
        { 
            "../../../Images/Image1.jpg",
            "../../../Images/Image2.jpg",
            "../../../Images/Image3.jpg",
            "../../../Images/Image4.jpg",
            "../../../Images/Image5.jpg",
            "../../../Images/Image6.jpg",
            "../../../Images/Image7.jpg",
            "../../../Images/Image8.jpg",
            "../../../Images/Image9.jpg",
            "../../../Images/Image10.jpg",
            "../../../Images/Image11.jpg",
            "../../../Images/Image12.jpg",
            "../../../Images/Image13.jpg",
            "../../../Images/Image14.jpg",
            "../../../Images/Image15.jpg",
            "../../../Images/Image16.jpg",
            "../../../Images/Image17.jpg",
            "../../../Images/Image18.jpg",
            "../../../Images/Image19.jpg",
            "../../../Images/Image20.jpg"
        };
        List<string> imageNature = new List<string> { };
        List<string> imageFlowers = new List<string> { };


        public ObservableCollection<Button> GameCards { get; set; }

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
                    //OnPropertyChanged(nameof(SelectedRows)); 
                    //if(_selectedRows != 0)
                      // GenerateGameBoard(); 
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
                    //OnPropertyChanged(nameof(SelectedColumns)); 
                    //if(_selectedColumns != 0)
                        //GenerateGameBoard(); 
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

        public ICommand StartTimerCommand { get; private set; }


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
                ElapsedTime = _elapsedTime;  // Acesta va actualiza binding-ul pe UI
                OnPropertyChanged(nameof(ElapsedTime));
            }

            if (_elapsedTime == 0)  
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
                MessageBox.Show("You ran out of time!");  
            }
        }
        private void StopTimer()
        {
            _gameTimer.Stop();
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
                IsGameInactive = false;
                WelcomeTextVisibility = false;
                //MessageBox.Show($"Starting a new game with category: {SelectedCategory} and size: {SelectedRows}x{SelectedColumns}");
                OnPropertyChanged(nameof(SelectedRows));
                OnPropertyChanged(nameof(SelectedColumns));
                GenerateGameBoard();
                StartTimer(null);
            }
        }

        private void OpenGame(object obj)
        {
            WelcomeTextVisibility = false;
            MessageBox.Show("Opening saved game...");
        }

        private void SaveGame(object obj)
        {
            MessageBox.Show("Game saved.");
        }

        private void ShowStatistics(object obj)
        {
            MessageBox.Show("Showing game statistics...");
        }

        private void Exit(object obj)
        {
            MessageBox.Show("Exiting the game...");
            Application.Current.Shutdown();
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

                double windowWidth = Application.Current.MainWindow.ActualWidth;
                double windowHeight = Application.Current.MainWindow.ActualHeight-50;

                //MessageBox.Show($"{windowWidth}, {windowHeight}");
                ButtonWidth = windowWidth / SelectedColumns-15*SelectedColumns;
                ButtonHeight = windowHeight / SelectedRows-10*SelectedRows;

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