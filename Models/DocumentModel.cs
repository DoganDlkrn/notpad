using System;
using System.IO;

namespace NotepadApp.Models
{
    public class DocumentModel
    {
        public string FilePath { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string FileName => string.IsNullOrEmpty(FilePath) ? "Yeni Dosya" : Path.GetFileName(FilePath);
        public bool IsModified { get; set; } = false;
        public DateTime LastModified { get; set; } = DateTime.Now;
        public string DisplayName => IsModified ? FileName + " *" : FileName;

        public bool HasPath => !string.IsNullOrEmpty(FilePath);
    }
}


