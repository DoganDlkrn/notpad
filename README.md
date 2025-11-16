# NotepadApp - Gelişmiş Metin Düzenleyici

Modern ve kapsamlı bir notepad uygulaması. C# ve WPF kullanılarak geliştirilmiştir.

## Özellikler

### 📁 Dosya İşlemleri
- ✅ Yeni dosya oluşturma
- ✅ Dosya açma (Ctrl+O)
- ✅ Dosya kaydetme (Ctrl+S)
- ✅ Farklı kaydetme (Ctrl+Shift+S)
- ✅ Yazdırma (Ctrl+P)
- ✅ Son açılan dosyalar listesi
- ✅ UTF-8 kodlama desteği

### ✏️ Metin Düzenleme
- ✅ Geri al / Yinele (Ctrl+Z / Ctrl+Y)
- ✅ Kes, Kopyala, Yapıştır (Ctrl+X, Ctrl+C, Ctrl+V)
- ✅ Tümünü seç (Ctrl+A)
- ✅ Sil (Del)
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
- ✅ Satır numaraları
- ✅ Durum çubuğu
- ✅ Karanlık mod
- ✅ Tam ekran modu (F11)

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

## Gereksinimler

- .NET 8.0 SDK veya üzeri
- Windows işletim sistemi (WPF gereksinimi)

## Kurulum

1. Projeyi klonlayın veya indirin
2. Terminal/Command Prompt'ta proje dizinine gidin
3. Aşağıdaki komutu çalıştırın:

```bash
dotnet restore
dotnet build
dotnet run
```

## Proje Yapısı

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
├── Styles/
│   └── AppStyles.xaml            # Uygulama stilleri
├── MainWindow.xaml               # Ana pencere
├── App.xaml                      # Uygulama tanımı
└── NotepadApp.csproj             # Proje dosyası
```

## Kullanım

### Temel İşlemler

1. **Yeni Dosya**: Menüden `Dosya > Yeni` veya `Ctrl+N`
2. **Dosya Aç**: Menüden `Dosya > Aç...` veya `Ctrl+O`
3. **Kaydet**: Menüden `Dosya > Kaydet` veya `Ctrl+S`
4. **Bul**: Menüden `Düzenle > Bul...` veya `Ctrl+F`
5. **Yakınlaştır**: Menüden `Görünüm > Yakınlaştır` veya `Ctrl++`

### Klavye Kısayolları

| Kısayol | İşlem |
|---------|-------|
| Ctrl+N | Yeni dosya |
| Ctrl+O | Dosya aç |
| Ctrl+S | Kaydet |
| Ctrl+Shift+S | Farklı kaydet |
| Ctrl+P | Yazdır |
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

## Teknik Detaylar

- **Framework**: .NET 8.0
- **UI Framework**: WPF (Windows Presentation Foundation)
- **Mimari**: MVVM (Model-View-ViewModel)
- **Dil**: C#
- **Kodlama**: UTF-8

## Geliştirme

Proje MVVM mimarisi kullanılarak geliştirilmiştir. Yeni özellikler eklemek için:

1. Model sınıflarını `Models/` klasörüne ekleyin
2. ViewModel sınıflarını `ViewModels/` klasörüne ekleyin
3. View dosyalarını `Views/` klasörüne ekleyin
4. Servis sınıflarını `Services/` klasörüne ekleyin

## Lisans

Bu proje eğitim amaçlı geliştirilmiştir.

## Yazar

Proje ödevi için geliştirilmiştir.

---

**Not**: Bu uygulama Windows işletim sistemi için tasarlanmıştır ve WPF framework'ü gerektirir.


