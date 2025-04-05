using MemoryGame.Helpers;
using MemoryGame.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;

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

        // Lista de categorii pentru joc
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

        // Opțiuni pentru rânduri și coloane (dimensiunea personalizată a tablei)
        public ObservableCollection<int> RowOptions { get; set; }
        public ObservableCollection<int> ColumnOptions { get; set; }
        private int _selectedRows;
        public int SelectedRows
        {
            get { return _selectedRows; }
            set
            {
                if (_selectedRows != value)
                {
                    _selectedRows = value;
                    OnPropertyChanged(nameof(SelectedRows)); // Notify UI
                    if(_selectedRows != 0)
                       GenerateGameBoard(); // Recreate the game board with new dimensions
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
                    OnPropertyChanged(nameof(SelectedColumns)); // Notify UI
                    if(_selectedColumns != 0)
                        GenerateGameBoard(); // Recreate the game board with new dimensions
                    if(_selectedColumns != 0&&_selectedRows!=0)
                        WelcomeTextVisibility = false;
                }
            }
        }

        public double ButtonWidth { get; set; }
        public double ButtonHeight { get; set; }

        // Lista de carduri pentru joc
        public ObservableCollection<Button> GameCards { get; set; }

        private bool _welcomeTextVisibility = true;  // Textul de bun venit este vizibil la început

        public bool WelcomeTextVisibility
        {
            get { return _welcomeTextVisibility; }
            set
            {
                if (_welcomeTextVisibility != value)
                {
                    _welcomeTextVisibility = value;
                    OnPropertyChanged(nameof(WelcomeTextVisibility));  // Notifică UI-ul pentru schimbare
                }
            }
        }

        // Constructor
        public GameViewModel()
        {
            // Inițializarea categoriilor și opțiunilor pentru rânduri/coloane
            Categories = new ObservableCollection<string> { "Animals", "Nature", "Flowers" };
            SelectedCategory = Categories[0];  // Categorie implicită

            RowOptions = new ObservableCollection<int> { 2, 3, 4, 5, 6 };
            ColumnOptions = new ObservableCollection<int> { 2, 3, 4, 5, 6 };

            Random rng = new Random();
            imageAnimals.OrderBy(x => rng.Next()).ToList();
            imageNature.OrderBy(x => rng.Next()).ToList();
            imageFlowers.OrderBy(x => rng.Next()).ToList();

            // Inițializarea comenzilor
            SelectCategoryCommand = new RelayCommand(SelectCategory);
            NewGameCommand = new RelayCommand(NewGame);
            OpenGameCommand = new RelayCommand(OpenGame);
            SaveGameCommand = new RelayCommand(SaveGame);
            ShowStatisticsCommand = new RelayCommand(ShowStatistics);
            ExitCommand = new RelayCommand(Exit);
            SetStandardBoardSizeCommand = new RelayCommand(SetStandardBoardSize);
            SetCustomBoardSizeCommand = new RelayCommand(SetCustomBoardSize);
            ShowAboutCommand = new RelayCommand(ShowAbout);
        }

        // Eveniment pentru PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        // Funcție de notificare pentru schimbarea proprietăților
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Selecția categoriei
        private void SelectCategory(object category)
        {
            MessageBox.Show($"Category selected: {category}");
            // Logica pentru a încărca imagini pentru categoria selectată
        }

        // Începerea unui joc nou
        private void NewGame(object obj)
        {
            WelcomeTextVisibility = false;
            MessageBox.Show($"Starting a new game with category: {SelectedCategory} and size: {SelectedRows}x{SelectedColumns}");
            // Logica pentru a inițializa tabla de joc cu categoria și dimensiunile selectate
            GenerateGameBoard();
        }

        // Deschiderea unui joc salvat
        private void OpenGame(object obj)
        {
            WelcomeTextVisibility = false;
            MessageBox.Show("Opening saved game...");
        }

        // Salvarea jocului
        private void SaveGame(object obj)
        {
            MessageBox.Show("Game saved.");
        }

        // Afișarea statisticilor
        private void ShowStatistics(object obj)
        {
            MessageBox.Show("Showing game statistics...");
        }

        // Ieșirea din joc
        private void Exit(object obj)
        {
            MessageBox.Show("Exiting the game...");
            Application.Current.Shutdown();  // Închide aplicația
        }

        // Selectarea dimensiunii implicite a tablei 4x4
        private void SetStandardBoardSize(object obj)
        {
            MessageBox.Show("Standard board size selected: 4x4");
            SelectedRows = 4;
            SelectedColumns = 4;
            GenerateGameBoard();
        }

        // Setarea unei dimensiuni personalizate pentru tablă
        private void SetCustomBoardSize(object obj)
        {
            WelcomeTextVisibility = false;
            MessageBox.Show($"Custom board size selected: {SelectedRows}x{SelectedColumns}");
            GenerateGameBoard();
        }

        // Afișarea ferestrei "About"
        private void ShowAbout(object obj)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.Show();
        }

        // Metodă pentru generarea tablei de joc
        private void GenerateGameBoard()
        {
            if (SelectedColumns > 0 && SelectedRows > 0)
            {
                GameCards = new ObservableCollection<Button>();
           
                // Calculăm lățimea și înălțimea butoanelor
                ButtonWidth = (9000 - 10 * (SelectedColumns)) / SelectedColumns-20;
                ButtonHeight = (9000 - 10 * (SelectedRows)) / SelectedRows-20;

                // Creăm o listă de imagini în funcție de categoria selectată
                List<string> imagePaths = new List<string>();

                int maxImages = SelectedRows * SelectedColumns / 2;  // Jumătate din numărul total de carduri, pentru perechi

                if (SelectedCategory == "Animals")
                {
                    for (int i = 0; i < maxImages; i++)
                    {
                        // Verifică dacă sunt imagini suficiente
                        if (i < imageAnimals.Count)
                        {
                            imagePaths.Add(imageAnimals[i]);
                            imagePaths.Add(imageAnimals[i]); // Adăugăm fiecare imagine de două ori pentru a crea perechi
                        }
                    }
                }
                else if (SelectedCategory == "Nature")
                {
                    for (int i = 0; i < maxImages; i++)
                    {
                        // Verifică dacă sunt imagini suficiente
                        if (i < imageNature.Count)
                        {
                            imagePaths.Add(imageNature[i]);
                            imagePaths.Add(imageNature[i]); // Adăugăm fiecare imagine de două ori
                        }
                    }
                }
                else // Default pentru "Flowers"
                {
                    for (int i = 0; i < maxImages; i++)
                    {
                        // Verifică dacă sunt imagini suficiente
                        if (i < imageFlowers.Count)
                        {
                            imagePaths.Add(imageFlowers[i]);
                            imagePaths.Add(imageFlowers[i]); // Adăugăm fiecare imagine de două ori
                        }
                    }
                }

                // Amestecăm imaginile pentru a le distribui aleatoriu
                Random rng = new Random();
                var shuffledImages = imagePaths.OrderBy(x => rng.Next()).ToList();

                // Adăugăm butoanele în colecția GameCards
                int imageIndex = 0;
                for (int i = 0; i < SelectedRows * SelectedColumns; i++)
                {
                    var cardButton = new Button
                    {
                        Content = "?",  // Placeholder pentru card
                        Command = new RelayCommand(CardClicked),
                        Width = ButtonWidth,  // Setează lățimea calculată
                        Height = ButtonHeight
                    };

                    var imageBrush = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri(shuffledImages[imageIndex], UriKind.Relative))
                    };

                    // Setează imaginea ca fundal
                    cardButton.Background = imageBrush;

                    // Asociază imaginea cu butonul folosind un Tag
                    cardButton.Tag = shuffledImages[imageIndex];

                    // Crește indexul pentru următoarea imagine
                    imageIndex++;

                    // Adăugăm butonul în colecția GameCards
                    GameCards.Add(cardButton);
                }

                // Notifică UI-ul pentru actualizarea butoanelor
                OnPropertyChanged(nameof(GameCards));

            }
        }


        // Handler pentru click-ul pe card (pentru flip)
        private void CardClicked(object obj)
        {
            Button clickedButton = obj as Button;
            if (clickedButton != null)
            {
                // Dezactivează butonul și arată imaginea
                clickedButton.IsEnabled = false;

                MessageBox.Show("Card clicked. Image shown behind the button.");
            }
        }

       
    }
}
