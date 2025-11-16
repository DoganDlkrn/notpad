using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using Avalonia;
using NotepadApp.Models;

namespace NotepadApp.Services
{
    public class FileService
    {
        private Window? _parentWindow;

        public void SetParentWindow(Window window)
        {
            _parentWindow = window;
        }

        public async Task<string?> OpenFileAsync()
        {
            if (_parentWindow == null) return null;

            var topLevel = TopLevel.GetTopLevel(_parentWindow);
            if (topLevel == null) return null;

            var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title = "Dosya Aç",
                AllowMultiple = false,
                FileTypeFilter = new[]
                {
                    new FilePickerFileType("Metin Dosyaları")
                    {
                        Patterns = new[] { "*.txt" }
                    },
                    new FilePickerFileType("Tüm Dosyalar")
                    {
                        Patterns = new[] { "*.*" }
                    }
                }
            });

            if (files.Count >= 1)
            {
                var file = files[0];
                try
                {
                    await using var stream = await file.OpenReadAsync();
                    using var reader = new StreamReader(stream, Encoding.UTF8);
                    return await reader.ReadToEndAsync();
                }
                catch (Exception ex)
                {
                    await ShowErrorAsync($"Dosya açılamadı: {ex.Message}");
                    return null;
                }
            }

            return null;
        }

        public string? GetOpenFilePath()
        {
            // Bu metod async olmadığı için null döndürüyoruz
            // Gerçek dosya yolu async metodda alınacak
            return null;
        }

        public async Task<string?> SaveFileAsAsync(string content)
        {
            if (_parentWindow == null) return null;

            var topLevel = TopLevel.GetTopLevel(_parentWindow);
            if (topLevel == null) return null;

            var file = await topLevel.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
            {
                Title = "Dosyayı Kaydet",
                DefaultExtension = "txt",
                SuggestedFileName = "yeni_dosya.txt",
                FileTypeChoices = new[]
                {
                    new FilePickerFileType("Metin Dosyaları")
                    {
                        Patterns = new[] { "*.txt" }
                    },
                    new FilePickerFileType("Tüm Dosyalar")
                    {
                        Patterns = new[] { "*.*" }
                    }
                }
            });

            if (file != null)
            {
                try
                {
                    await using var stream = await file.OpenWriteAsync();
                    using var writer = new StreamWriter(stream, Encoding.UTF8);
                    await writer.WriteAsync(content);
                    return file.Path.AbsolutePath;
                }
                catch (Exception ex)
                {
                    await ShowErrorAsync($"Dosya kaydedilemedi: {ex.Message}");
                    return null;
                }
            }

            return null;
        }

        public async Task<bool> SaveFileAsync(string filePath, string content)
        {
            try
            {
                await File.WriteAllTextAsync(filePath, content, Encoding.UTF8);
                return true;
            }
            catch (Exception ex)
            {
                await ShowErrorAsync($"Dosya kaydedilemedi: {ex.Message}");
                return false;
            }
        }

        public async Task<string?> LoadFileAsync(string filePath)
        {
            try
            {
                return await File.ReadAllTextAsync(filePath, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                await ShowErrorAsync($"Dosya açılamadı: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> PrintFileAsync(string content, string title)
        {
            // Avalonia'da yazdırma desteği sınırlı
            // Basit bir bilgi mesajı gösterelim
            if (_parentWindow != null)
            {
                var msgBox = new Window
                {
                    Title = "Yazdırma",
                    Content = new TextBlock { Text = "Yazdırma özelliği bu platformda desteklenmiyor." },
                    Width = 300,
                    Height = 150,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner
                };
                await msgBox.ShowDialog(_parentWindow);
            }
            return false;
        }

        private async Task ShowErrorAsync(string message)
        {
            if (_parentWindow != null)
            {
                var msgBox = new Window
                {
                    Title = "Hata",
                    Content = new TextBlock { Text = message },
                    Width = 400,
                    Height = 200,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner
                };
                await msgBox.ShowDialog(_parentWindow);
            }
        }
    }
}
