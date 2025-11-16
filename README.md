# NotepadApp - Gelişmiş Metin Düzenleyici

Modern ve kapsamlı bir notepad uygulaması. C# ve Avalonia UI kullanılarak geliştirilmiştir. **Windows, macOS ve Linux** üzerinde çalışır.

## ✨ Özellikler

### 📁 Dosya İşlemleri
- ✅ Yeni dosya oluşturma
- ✅ Dosya açma (Ctrl+O)
- ✅ Dosya kaydetme (Ctrl+S)
- ✅ Farklı kaydetme (Ctrl+Shift+S)
- ✅ UTF-8 kodlama desteği

### ✏️ Metin Düzenleme
- ✅ Geri al / Yinele (Ctrl+Z / Ctrl+Y)
- ✅ Kes, Kopyala, Yapıştır (Ctrl+X, Ctrl+C, Ctrl+V)
- ✅ Tümünü seç (Ctrl+A)
- ✅ Tarih/Saat ekleme (F5)

### 🔍 Arama ve Değiştirme
- ✅ Bul (Ctrl+F)
- ✅ Değiştir (Ctrl+H)
- ✅ Büyük/küçük harf duyarlı arama
- ✅ Tümünü değiştir
- ✅ Satıra git (Ctrl+G)

### 🎨 Biçimlendirme
- ✅ Yazı tipi seçimi
- ✅ Yazı boyutu ayarlama
- ✅ Sözcük kaydırma (Word Wrap)
- ✅ Önizleme özelliği

### 👁️ Görünüm Özellikleri
- ✅ Yakınlaştırma/Uzaklaştırma (Ctrl+Plus/Minus)
- ✅ Varsayılan yakınlaştırma (Ctrl+0)
- ✅ Satır numaraları (VS Code tarzı)
- ✅ Durum çubuğu
- ✅ Tam ekran modu (F11)
- ✅ Modern VS Code tarzı tasarım

### 📊 Durum Bilgileri
- ✅ Satır ve sütun bilgisi
- ✅ Kelime sayısı
- ✅ Karakter sayısı
- ✅ Gerçek zamanlı güncelleme

### 🗂️ Çoklu Sekme Desteği
- ✅ Birden fazla dosya açma
- ✅ Sekme yönetimi
- ✅ Sekme kapatma
- ✅ Kaydedilmemiş değişiklik uyarıları

### ⚙️ Ayarlar
- ✅ Kullanıcı ayarlarını kaydetme
- ✅ Son açılan dosyaları hatırlama
- ✅ Yazı tipi tercihleri
- ✅ Görünüm tercihleri

## 🚀 Gereksinimler

- .NET 8.0 SDK veya üzeri
- Windows, macOS veya Linux işletim sistemi

## 📦 Kurulum

1. Projeyi klonlayın:
```bash
git clone https://github.com/DoganDlkrn/notpad.git
cd notpad
```

2. Proje dizinine gidin ve bağımlılıkları yükleyin:
```bash
dotnet restore
```

3. Projeyi derleyin:
```bash
dotnet build NotepadApp.csproj
```

4. Uygulamayı çalıştırın:
```bash
dotnet run --project NotepadApp.csproj
```

## 📁 Proje Yapısı

```
NotepadApp/
├── Models/
│   └── DocumentModel.cs          # Belge modeli
├── ViewModels/
│   └── MainViewModel.cs          # Ana görünüm modeli
├── Views/
│   ├── FindReplaceDialog.xaml    # Bul/Değiştir diyalogu
│   ├── GoToDialog.xaml           # Satıra git diyalogu
│   └── FontDialog.xaml           # Yazı tipi diyalogu
├── Services/
│   ├── FileService.cs            # Dosya işlemleri servisi
│   └── SettingsService.cs        # Ayarlar servisi
├── UserControls/
│   └── TextEditor.xaml           # Metin editörü kontrolü
├── MainWindow.xaml               # Ana pencere
├── App.xaml                      # Uygulama tanımı
└── NotepadApp.csproj             # Proje dosyası
```

## ⌨️ Klavye Kısayolları

| Kısayol | İşlem |
|---------|-------|
| Ctrl+N | Yeni dosya |
| Ctrl+O | Dosya aç |
| Ctrl+S | Kaydet |
| Ctrl+Shift+S | Farklı kaydet |
| Ctrl+Z | Geri al |
| Ctrl+Y | Yinele |
| Ctrl+X | Kes |
| Ctrl+C | Kopyala |
| Ctrl+V | Yapıştır |
| Ctrl+A | Tümünü seç |
| Ctrl+F | Bul |
| Ctrl+H | Değiştir |
| Ctrl+G | Satıra git |
| F5 | Tarih/Saat ekle |
| F11 | Tam ekran |
| Ctrl++ | Yakınlaştır |
| Ctrl+- | Uzaklaştır |
| Ctrl+0 | Varsayılan yakınlaştırma |

## 🎨 Tasarım Özellikleri

- **Modern VS Code tarzı arayüz**
- **Koyu tema menü ve araç çubuğu**
- **Beyaz metin editörü alanı**
- **Mavi aksan renkleri**
- **Emoji'li butonlar**
- **Hover efektleri**
- **Profesyonel görünüm**

## 🛠️ Teknik Detaylar

- **Framework**: .NET 8.0
- **UI Framework**: Avalonia UI 11.0.5
- **Mimari**: MVVM (Model-View-ViewModel)
- **Dil**: C#
- **Kodlama**: UTF-8
- **Platform**: Cross-platform (Windows, macOS, Linux)

## 📝 Geliştirme

Proje MVVM mimarisi kullanılarak geliştirilmiştir. Yeni özellikler eklemek için:

1. Model sınıflarını `Models/` klasörüne ekleyin
2. ViewModel sınıflarını `ViewModels/` klasörüne ekleyin
3. View dosyalarını `Views/` klasörüne ekleyin
4. Servis sınıflarını `Services/` klasörüne ekleyin

## 📄 Lisans

Bu proje eğitim amaçlı geliştirilmiştir.

## 👤 Yazar

**DoganDlkrn**

---

⭐ **Star** vererek projeyi destekleyebilirsiniz!
