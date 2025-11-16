using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Threading;
using NotepadApp.Models;

namespace NotepadApp.UserControls
{
    public partial class TextEditor : UserControl
    {
        private DocumentModel? _document;
        private TextBox? _textEditorBox;
        private TextBlock? _lineNumbersTextBlock;

        public static readonly StyledProperty<bool> WordWrapProperty =
            AvaloniaProperty.Register<TextEditor, bool>(nameof(WordWrap), true);

        public static readonly StyledProperty<DocumentModel?> DocumentProperty =
            AvaloniaProperty.Register<TextEditor, DocumentModel?>(nameof(Document));

        public bool WordWrap
        {
            get => GetValue(WordWrapProperty);
            set
            {
                SetValue(WordWrapProperty, value);
                if (_textEditorBox != null)
                {
                    _textEditorBox.TextWrapping = value ? TextWrapping.Wrap : TextWrapping.NoWrap;
                }
            }
        }

        public DocumentModel? Document
        {
            get => GetValue(DocumentProperty);
            set
            {
                SetValue(DocumentProperty, value);
                _document = value;
                if (_document != null && _textEditorBox != null)
                {
                    _textEditorBox.Text = _document.Content ?? string.Empty;
                    UpdateLineNumbers();
                }
            }
        }

        public TextEditor()
        {
            AvaloniaXamlLoader.Load(this);
            this.PropertyChanged += TextEditor_PropertyChanged;
        }

        private void TextEditor_PropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property == WordWrapProperty && _textEditorBox != null)
            {
                _textEditorBox.TextWrapping = WordWrap ? TextWrapping.Wrap : TextWrapping.NoWrap;
            }
            else if (e.Property == DocumentProperty)
            {
                _document = Document;
                if (_document != null && _textEditorBox != null)
                {
                    _textEditorBox.Text = _document.Content ?? string.Empty;
                    UpdateLineNumbers();
                }
            }
        }

        protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
        {
            base.OnAttachedToVisualTree(e);
            InitializeControls();
        }

        private void InitializeControls()
        {
            // Kontrolleri bul
            _textEditorBox = this.FindControl<TextBox>("TextEditorBox");
            _lineNumbersTextBlock = this.FindControl<TextBlock>("LineNumbersTextBlock");
            
            if (_textEditorBox == null)
            {
                _textEditorBox = this.GetLogicalDescendants().OfType<TextBox>().FirstOrDefault();
            }
            
            if (_lineNumbersTextBlock == null)
            {
                _lineNumbersTextBlock = this.GetLogicalDescendants().OfType<TextBlock>().FirstOrDefault();
            }
            
            if (_textEditorBox != null)
            {
                // Renkler ve görünürlük
                _textEditorBox.Background = new SolidColorBrush(Colors.White);
                _textEditorBox.Foreground = new SolidColorBrush(Colors.Black);
                _textEditorBox.CaretBrush = new SolidColorBrush(Colors.Black);
                _textEditorBox.IsVisible = true;
                _textEditorBox.Opacity = 1.0;
                _textEditorBox.IsEnabled = true;
                _textEditorBox.Focusable = true;
                
                // Event handler
                _textEditorBox.TextChanged += TextEditorBox_TextChanged;
                
                // Document set et
                var doc = Document ?? _document;
                if (doc != null)
                {
                    _document = doc;
                    _textEditorBox.Text = doc.Content ?? string.Empty;
                }
                else
                {
                    _textEditorBox.Text = string.Empty;
                }
                
                UpdateLineNumbers();
                
                // Focus ver
                Dispatcher.UIThread.Post(() =>
                {
                    if (_textEditorBox != null)
                    {
                        _textEditorBox.Focus();
                    }
                }, DispatcherPriority.Loaded);
            }
        }

        private void TextEditorBox_TextChanged(object? sender, TextChangedEventArgs e)
        {
            if (_document != null && _textEditorBox != null)
            {
                _document.Content = _textEditorBox.Text ?? string.Empty;
                _document.IsModified = true;
            }
            UpdateLineNumbers();
        }

        private void UpdateLineNumbers()
        {
            if (_textEditorBox == null || _lineNumbersTextBlock == null) return;

            var text = _textEditorBox.Text ?? string.Empty;
            if (string.IsNullOrEmpty(text))
            {
                _lineNumbersTextBlock.Text = "1";
                return;
            }

            var lines = text.Split('\n');
            var lineNumbers = string.Join("\n", Enumerable.Range(1, lines.Length).Select(i => i.ToString()));
            _lineNumbersTextBlock.Text = lineNumbers;
        }
    }
}
