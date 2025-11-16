using System;
using System.Text.RegularExpressions;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using NotepadApp.Models;

namespace NotepadApp.Views
{
    public partial class FindReplaceDialog : Window
    {
        private readonly DocumentModel _document;
        private readonly bool _isReplaceMode;
        private int _lastSearchIndex = 0;
        private TextBox? _findTextBox;
        private TextBox? _replaceTextBox;
        private CheckBox? _matchCaseCheckBox;

        public bool IsReplaceMode => _isReplaceMode;

        public FindReplaceDialog(DocumentModel document, bool isReplaceMode = false)
        {
            AvaloniaXamlLoader.Load(this);
            _document = document;
            _isReplaceMode = isReplaceMode;
            DataContext = this;
        }

        protected override void OnLoaded(RoutedEventArgs e)
        {
            base.OnLoaded(e);
            _findTextBox = this.FindControl<TextBox>("FindTextBox");
            _replaceTextBox = this.FindControl<TextBox>("ReplaceTextBox");
            _matchCaseCheckBox = this.FindControl<CheckBox>("MatchCaseCheckBox");
            _findTextBox?.Focus();
        }

        private void FindNext_Click(object? sender, RoutedEventArgs e)
        {
            if (_findTextBox == null || _matchCaseCheckBox == null) return;
            var searchText = _findTextBox.Text;
            if (string.IsNullOrEmpty(searchText)) return;

            var content = _document.Content;
            var index = content.IndexOf(searchText, _lastSearchIndex, 
                _matchCaseCheckBox.IsChecked == true ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase);

            if (index >= 0)
            {
                _lastSearchIndex = index + searchText.Length;
            }
            else
            {
                _lastSearchIndex = 0;
            }
        }

        private void Replace_Click(object? sender, RoutedEventArgs e)
        {
            if (_findTextBox == null || _replaceTextBox == null || _matchCaseCheckBox == null) return;
            var searchText = _findTextBox.Text;
            var replaceText = _replaceTextBox.Text;

            if (string.IsNullOrEmpty(searchText)) return;

            var index = _document.Content.IndexOf(searchText, _lastSearchIndex,
                _matchCaseCheckBox.IsChecked == true ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase);

            if (index >= 0)
            {
                _document.Content = _document.Content.Remove(index, searchText.Length)
                    .Insert(index, replaceText);
                _document.IsModified = true;
                _lastSearchIndex = index + replaceText.Length;
            }
        }

        private void ReplaceAll_Click(object? sender, RoutedEventArgs e)
        {
            if (_findTextBox == null || _replaceTextBox == null || _matchCaseCheckBox == null) return;
            var searchText = _findTextBox.Text;
            var replaceText = _replaceTextBox.Text;

            if (string.IsNullOrEmpty(searchText)) return;

            var options = _matchCaseCheckBox.IsChecked == true 
                ? StringComparison.Ordinal 
                : StringComparison.OrdinalIgnoreCase;

            var count = 0;
            var content = _document.Content;
            var index = 0;

            while ((index = content.IndexOf(searchText, index, options)) >= 0)
            {
                content = content.Remove(index, searchText.Length).Insert(index, replaceText);
                index += replaceText.Length;
                count++;
            }

            _document.Content = content;
            _document.IsModified = true;
        }

        private void Cancel_Click(object? sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
