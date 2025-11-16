using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using NotepadApp.Models;

namespace NotepadApp.Views
{
    public partial class GoToDialog : Window
    {
        private readonly DocumentModel _document;
        private TextBox? _lineNumberTextBox;

        public GoToDialog(DocumentModel document)
        {
            AvaloniaXamlLoader.Load(this);
            _document = document;
        }

        protected override void OnLoaded(RoutedEventArgs e)
        {
            base.OnLoaded(e);
            _lineNumberTextBox = this.FindControl<TextBox>("LineNumberTextBox");
            _lineNumberTextBox?.Focus();
        }

        private void GoTo_Click(object? sender, RoutedEventArgs e)
        {
            if (_lineNumberTextBox != null && int.TryParse(_lineNumberTextBox.Text, out int lineNumber))
            {
                var lines = _document.Content.Split('\n');
                if (lineNumber > 0 && lineNumber <= lines.Length)
                {
                    Close();
                }
            }
        }

        private void Cancel_Click(object? sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
