using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using NotepadApp.ViewModels;
using NotepadApp.Models;
using NotepadApp.Services;

namespace NotepadApp
{
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;
        private FileService _fileService;

        public MainWindow()
        {
            AvaloniaXamlLoader.Load(this);
            _fileService = new FileService();
            _fileService.SetParentWindow(this);
            _viewModel = new MainViewModel(_fileService);
            DataContext = _viewModel;
            
            // İlk sekme oluştur
            _viewModel.CreateNewTab();
            
            // Klavye kısayolları
            this.KeyDown += MainWindow_KeyDown;
        }

        private void MainWindow_KeyDown(object? sender, KeyEventArgs e)
        {
            // F11 - Tam ekran
            if (e.Key == Key.F11)
            {
                _viewModel.ToggleFullScreenCommand.Execute(null);
            }
            // F5 - Tarih/Saat
            else if (e.Key == Key.F5)
            {
                _viewModel.InsertDateTimeCommand.Execute(null);
            }
        }

        private void CloseTab_Click(object? sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is DocumentModel document)
            {
                _viewModel.CloseTab(document);
            }
        }
    }
}
