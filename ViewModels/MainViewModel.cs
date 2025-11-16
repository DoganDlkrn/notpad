using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Media;
using NotepadApp.Models;
using NotepadApp.Services;
using NotepadApp.Views;

namespace NotepadApp.ViewModels
{
    public interface ICommand
    {
        event EventHandler? CanExecuteChanged;
        bool CanExecute(object? parameter);
        void Execute(object? parameter);
    }

    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly FileService _fileService;
        private readonly SettingsService _settingsService;
        private DocumentModel _currentDocument;
        private AppSettings _settings;
        private bool _isFullScreen = false;
        private WindowState _previousWindowState;
        private bool _showStatusBar = true;

        public ObservableCollection<DocumentModel> Documents { get; } = new ObservableCollection<DocumentModel>();

        public DocumentModel CurrentDocument
        {
            get => _currentDocument;
            set
            {
                _currentDocument = value;
                OnPropertyChanged();
                UpdateStatus();
            }
        }

        public bool WordWrap
        {
            get => _settings?.WordWrap ?? true;
            set
            {
                if (_settings != null)
                {
                    _settings.WordWrap = value;
                    _settingsService.SaveSettings(_settings);
                    OnPropertyChanged();
                }
            }
        }

        public bool ShowLineNumbers
        {
            get => _settings?.ShowLineNumbers ?? true;
            set
            {
                if (_settings != null)
                {
                    _settings.ShowLineNumbers = value;
                    _settingsService.SaveSettings(_settings);
                    OnPropertyChanged();
                }
            }
        }

        public bool ShowStatusBar
        {
            get => _showStatusBar;
            set
            {
                _showStatusBar = value;
                OnPropertyChanged();
            }
        }

        public double ZoomLevel
        {
            get => _settings?.ZoomLevel ?? 100;
            set
            {
                if (_settings != null)
                {
                    _settings.ZoomLevel = value;
                    _settingsService.SaveSettings(_settings);
                    OnPropertyChanged();
                }
            }
        }

        public string StatusText { get; set; } = "Hazır";
        public string LineColumnInfo { get; set; } = "Satır 1, Sütun 1";
        public string WordCountInfo { get; set; } = "Kelime: 0";
        public string CharacterCountInfo { get; set; } = "Karakter: 0";

        public ICommand NewFileCommand { get; }
        public ICommand OpenFileCommand { get; }
        public ICommand SaveFileCommand { get; }
        public ICommand SaveAsFileCommand { get; }
        public ICommand PrintCommand { get; }
        public ICommand ExitCommand { get; }
        public ICommand FindCommand { get; }
        public ICommand ReplaceCommand { get; }
        public ICommand UndoCommand { get; }
        public ICommand RedoCommand { get; }
        public ICommand CutCommand { get; }
        public ICommand CopyCommand { get; }
        public ICommand PasteCommand { get; }
        public ICommand SelectAllCommand { get; }
        public ICommand InsertDateTimeCommand { get; }
        public ICommand FontDialogCommand { get; }
        public ICommand ToggleWordWrapCommand { get; }
        public ICommand ToggleLineNumbersCommand { get; }
        public ICommand ToggleStatusBarCommand { get; }
        public ICommand ToggleFullScreenCommand { get; }
        public ICommand ZoomInCommand { get; }
        public ICommand ZoomOutCommand { get; }
        public ICommand ZoomResetCommand { get; }
        public ICommand AboutCommand { get; }

        public MainViewModel(FileService fileService)
        {
            _fileService = fileService;
            _settingsService = new SettingsService();
            _settings = _settingsService.LoadSettings();

            NewFileCommand = new RelayCommand(_ => CreateNewTab());
            OpenFileCommand = new RelayCommand(async _ => await OpenFileAsync());
            SaveFileCommand = new RelayCommand(async _ => await SaveFileAsync(), _ => CurrentDocument != null);
            SaveAsFileCommand = new RelayCommand(async _ => await SaveAsFileAsync(), _ => CurrentDocument != null);
            PrintCommand = new RelayCommand(async _ => await PrintFileAsync(), _ => CurrentDocument != null);
            ExitCommand = new RelayCommand(_ => Environment.Exit(0));
            FindCommand = new RelayCommand(_ => ShowFindDialog());
            ReplaceCommand = new RelayCommand(_ => ShowReplaceDialog());
            UndoCommand = new RelayCommand(_ => { });
            RedoCommand = new RelayCommand(_ => { });
            CutCommand = new RelayCommand(_ => { });
            CopyCommand = new RelayCommand(_ => { });
            PasteCommand = new RelayCommand(_ => { });
            SelectAllCommand = new RelayCommand(_ => { });
            InsertDateTimeCommand = new RelayCommand(_ => InsertDateTime());
            FontDialogCommand = new RelayCommand(_ => ShowFontDialog());
            ToggleWordWrapCommand = new RelayCommand(_ => WordWrap = !WordWrap);
            ToggleLineNumbersCommand = new RelayCommand(_ => ShowLineNumbers = !ShowLineNumbers);
            ToggleStatusBarCommand = new RelayCommand(_ => ShowStatusBar = !ShowStatusBar);
            ToggleFullScreenCommand = new RelayCommand(_ => ToggleFullScreen());
            ZoomInCommand = new RelayCommand(_ => ZoomLevel = Math.Min(ZoomLevel + 10, 500));
            ZoomOutCommand = new RelayCommand(_ => ZoomLevel = Math.Max(ZoomLevel - 10, 50));
            ZoomResetCommand = new RelayCommand(_ => ZoomLevel = 100);
            AboutCommand = new RelayCommand(_ => ShowAboutDialog());
        }

        public void CreateNewTab()
        {
            var document = new DocumentModel();
            Documents.Add(document);
            CurrentDocument = document;
        }

        private async Task OpenFileAsync()
        {
            var content = await _fileService.OpenFileAsync();
            if (content != null)
            {
                var document = new DocumentModel
                {
                    Content = content,
                    IsModified = false
                };
                Documents.Add(document);
                CurrentDocument = document;
            }
        }

        private async Task SaveFileAsync()
        {
            if (CurrentDocument == null) return;

            if (CurrentDocument.HasPath)
            {
                if (await _fileService.SaveFileAsync(CurrentDocument.FilePath, CurrentDocument.Content))
                {
                    CurrentDocument.IsModified = false;
                    StatusText = "Dosya kaydedildi";
                    UpdateStatus();
                }
            }
            else
            {
                await SaveAsFileAsync();
            }
        }

        private async Task SaveAsFileAsync()
        {
            if (CurrentDocument == null) return;

            var filePath = await _fileService.SaveFileAsAsync(CurrentDocument.Content);
            if (filePath != null)
            {
                CurrentDocument.FilePath = filePath;
                CurrentDocument.IsModified = false;
                StatusText = "Dosya kaydedildi";
                UpdateStatus();
            }
        }

        private async Task PrintFileAsync()
        {
            if (CurrentDocument == null) return;
            await _fileService.PrintFileAsync(CurrentDocument.Content, CurrentDocument.FileName);
        }

        private void ShowFindDialog()
        {
            if (CurrentDocument == null) return;
            var findDialog = new FindReplaceDialog(CurrentDocument);
            findDialog.Show();
        }

        private void ShowReplaceDialog()
        {
            if (CurrentDocument == null) return;
            var replaceDialog = new FindReplaceDialog(CurrentDocument, true);
            replaceDialog.Show();
        }

        private void InsertDateTime()
        {
            if (CurrentDocument == null) return;
            var dateTime = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
            CurrentDocument.Content += dateTime;
            CurrentDocument.IsModified = true;
            UpdateStatus();
        }

        private void ShowFontDialog()
        {
            // Font dialog basitleştirildi
        }

        private void ToggleFullScreen()
        {
            var app = Application.Current?.ApplicationLifetime as Avalonia.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime;
            var window = app?.MainWindow;
            if (window == null) return;

            if (!_isFullScreen)
            {
                _previousWindowState = window.WindowState;
                window.WindowState = WindowState.FullScreen;
                _isFullScreen = true;
            }
            else
            {
                window.WindowState = _previousWindowState;
                _isFullScreen = false;
            }
        }

        public void UpdateStatus()
        {
            if (CurrentDocument == null) return;

            var content = CurrentDocument.Content;
            var lines = content.Split('\n');
            var currentLine = lines.Length;
            var currentColumn = lines.LastOrDefault()?.Length ?? 0;
            var wordCount = content.Split(new[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length;
            var charCount = content.Length;

            LineColumnInfo = $"Satır {currentLine}, Sütun {currentColumn + 1}";
            WordCountInfo = $"Kelime: {wordCount}";
            CharacterCountInfo = $"Karakter: {charCount}";

            OnPropertyChanged(nameof(LineColumnInfo));
            OnPropertyChanged(nameof(WordCountInfo));
            OnPropertyChanged(nameof(CharacterCountInfo));
        }

        public async Task CloseTabAsync(DocumentModel document)
        {
            if (document.IsModified)
            {
                var app = Application.Current?.ApplicationLifetime as Avalonia.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime;
                var window = app?.MainWindow;
                if (window != null)
                {
                    var dialog = new Window
                    {
                        Title = "Kaydedilmemiş Değişiklikler",
                        Content = new StackPanel
                        {
                            Children =
                            {
                                new TextBlock { Text = $"{document.FileName} dosyasında kaydedilmemiş değişiklikler var. Kaydetmek ister misiniz?", Margin = new Thickness(10), TextWrapping = Avalonia.Media.TextWrapping.Wrap },
                                new StackPanel
                                {
                                    Orientation = Avalonia.Layout.Orientation.Horizontal,
                                    HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Right,
                                    Margin = new Thickness(10),
                                    Children =
                                    {
                                        new Button { Content = "Kaydet", Margin = new Thickness(5), Tag = "Save" },
                                        new Button { Content = "Kaydetme", Margin = new Thickness(5), Tag = "DontSave" },
                                        new Button { Content = "İptal", Margin = new Thickness(5), Tag = "Cancel" }
                                    }
                                }
                            }
                        },
                        Width = 400,
                        Height = 200,
                        WindowStartupLocation = WindowStartupLocation.CenterOwner
                    };
                    
                    var result = await dialog.ShowDialog<string>(window);
                    
                    if (result == "Save")
                    {
                        await SaveFileAsync();
                    }
                    else if (result == "Cancel")
                    {
                        return;
                    }
                }
            }

            Documents.Remove(document);
            if (Documents.Count > 0)
            {
                CurrentDocument = Documents.Last();
            }
            else
            {
                CreateNewTab();
            }
        }

        public void CloseTab(DocumentModel document)
        {
            CloseTabAsync(document).Wait();
        }

        public async Task<bool> CanCloseAsync()
        {
            foreach (var doc in Documents)
            {
                if (doc.IsModified)
                {
                    var app = Application.Current?.ApplicationLifetime as Avalonia.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime;
                    var window = app?.MainWindow;
                    if (window != null)
                    {
                        var dialog = new Window
                        {
                            Title = "Onay",
                            Content = new StackPanel
                            {
                                Children =
                                {
                                    new TextBlock { Text = "Kaydedilmemiş değişiklikler var. Çıkmak istediğinize emin misiniz?", Margin = new Thickness(10), TextWrapping = Avalonia.Media.TextWrapping.Wrap },
                                    new StackPanel
                                    {
                                        Orientation = Avalonia.Layout.Orientation.Horizontal,
                                        HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Right,
                                        Margin = new Thickness(10),
                                        Children =
                                        {
                                            new Button { Content = "Evet", Margin = new Thickness(5), Tag = "Yes" },
                                            new Button { Content = "Hayır", Margin = new Thickness(5), Tag = "No" }
                                        }
                                    }
                                }
                            },
                            Width = 350,
                            Height = 150,
                            WindowStartupLocation = WindowStartupLocation.CenterOwner
                        };
                        
                        var result = await dialog.ShowDialog<string>(window);
                        return result == "Yes";
                    }
                }
            }
            return true;
        }

        private void ShowAboutDialog()
        {
            var app = Application.Current?.ApplicationLifetime as Avalonia.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime;
            var window = app?.MainWindow;
            if (window != null)
            {
                var dialog = new Window
                {
                    Title = "Hakkında",
                    Content = new TextBlock 
                    { 
                        Text = "NotepadApp v1.0\n\nGelişmiş metin düzenleyici uygulaması\n\nÖzellikler:\n• Çoklu sekme desteği\n• Bul ve Değiştir\n• Yazı tipi özelleştirme\n• Zoom özellikleri\n• Ve daha fazlası...",
                        Margin = new Thickness(10),
                        TextWrapping = Avalonia.Media.TextWrapping.Wrap
                    },
                    Width = 400,
                    Height = 300,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner
                };
                dialog.Show(window);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object?> _execute;
        private readonly Func<object?, bool>? _canExecute;

        public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;
        
        public void Execute(object? parameter) => _execute(parameter);
    }
}
