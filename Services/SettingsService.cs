using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace NotepadApp.Services
{
    public class SettingsService
    {
        private readonly string _settingsPath;
        private readonly string _recentFilesPath;

        public SettingsService()
        {
            var appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "NotepadApp");
            Directory.CreateDirectory(appDataPath);
            _settingsPath = Path.Combine(appDataPath, "settings.json");
            _recentFilesPath = Path.Combine(appDataPath, "recentfiles.json");
        }

        public AppSettings LoadSettings()
        {
            try
            {
                if (File.Exists(_settingsPath))
                {
                    var json = File.ReadAllText(_settingsPath);
                    return JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
                }
            }
            catch { }

            return new AppSettings();
        }

        public void SaveSettings(AppSettings settings)
        {
            try
            {
                var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_settingsPath, json);
            }
            catch { }
        }

        public List<string> LoadRecentFiles()
        {
            try
            {
                if (File.Exists(_recentFilesPath))
                {
                    var json = File.ReadAllText(_recentFilesPath);
                    return JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
                }
            }
            catch { }

            return new List<string>();
        }

        public void SaveRecentFiles(List<string> recentFiles)
        {
            try
            {
                // Son 10 dosyayı sakla
                var limitedList = recentFiles.Count > 10 ? recentFiles.GetRange(0, 10) : recentFiles;
                var json = JsonSerializer.Serialize(limitedList, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_recentFilesPath, json);
            }
            catch { }
        }
    }

    public class AppSettings
    {
        public string FontFamily { get; set; } = "Consolas";
        public double FontSize { get; set; } = 14;
        public bool WordWrap { get; set; } = true;
        public bool ShowLineNumbers { get; set; } = true;
        public bool DarkMode { get; set; } = false;
        public double ZoomLevel { get; set; } = 100;
        public bool AutoSave { get; set; } = false;
        public int AutoSaveInterval { get; set; } = 5; // dakika
    }
}


