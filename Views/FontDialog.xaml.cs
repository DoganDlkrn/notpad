using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using NotepadApp.Services;

namespace NotepadApp.Views
{
    public partial class FontDialog : Window
    {
        private ListBox? _fontListBox;
        private ComboBox? _sizeComboBox;
        
        public string SelectedFontFamily { get; private set; } = "Consolas";
        public double SelectedFontSize { get; private set; } = 14;

        public FontDialog(AppSettings settings)
        {
            AvaloniaXamlLoader.Load(this);
        }

        protected override void OnLoaded(RoutedEventArgs e)
        {
            base.OnLoaded(e);
            _fontListBox = this.FindControl<ListBox>("FontListBox");
            _sizeComboBox = this.FindControl<ComboBox>("SizeComboBox");
            
            if (_fontListBox != null && _fontListBox.Items.Count > 0)
                _fontListBox.SelectedIndex = 0;
            if (_sizeComboBox != null && _sizeComboBox.Items.Count > 0)
                _sizeComboBox.SelectedIndex = 1;
        }

        private void OK_Click(object? sender, RoutedEventArgs e)
        {
            if (_fontListBox?.SelectedItem is ListBoxItem fontItem &&
                _sizeComboBox?.SelectedItem is ComboBoxItem sizeItem)
            {
                SelectedFontFamily = fontItem.Content?.ToString() ?? "Consolas";
                if (double.TryParse(sizeItem.Content?.ToString(), out double size))
                {
                    SelectedFontSize = size;
                }
                Close(true);
            }
        }

        private void Cancel_Click(object? sender, RoutedEventArgs e)
        {
            Close(false);
        }
    }
}
