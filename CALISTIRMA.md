# NotepadApp - Çalıştırma Talimatları

## Windows'ta Çalıştırma

### Yöntem 1: Terminal/Command Prompt ile

1. **Windows Terminal veya Command Prompt'u açın**

2. **Proje klasörüne gidin:**
   ```cmd
   cd C:\Users\KullaniciAdi\Desktop\notepad
   ```
   (Kendi klasör yolunuzu yazın)

3. **Projeyi derleyin:**
   ```cmd
   dotnet build
   ```

4. **Uygulamayı çalıştırın:**
   ```cmd
   dotnet run
   ```

### Yöntem 2: Visual Studio ile

1. **Visual Studio 2022'yi açın**

2. **Dosya > Aç > Proje/Çözüm** menüsünden `NotepadApp.csproj` dosyasını seçin

3. **F5 tuşuna basın** veya **Çalıştır** butonuna tıklayın

### Yöntem 3: Visual Studio Code ile

1. **Visual Studio Code'u açın**

2. **File > Open Folder** ile proje klasörünü açın

3. **Terminal** açın (Ctrl + `)

4. **Şu komutları çalıştırın:**
   ```bash
   dotnet restore
   dotnet build
   dotnet run
   ```

## İlk Çalıştırma

Uygulama ilk açıldığında:
- Boş bir sekme ile başlar
- Menü çubuğu ve araç çubuğu görünür
- Durum çubuğu alt kısımda yer alır

## Hızlı Test

1. **Yeni dosya:** `Ctrl+N` veya menüden **Dosya > Yeni**
2. **Metin yazın:** Herhangi bir metin yazabilirsiniz
3. **Kaydet:** `Ctrl+S` ile dosyayı kaydedin
4. **Bul:** `Ctrl+F` ile arama yapın
5. **Yakınlaştır:** `Ctrl++` ile yakınlaştırın

## Sorun Giderme

### "dotnet komutu bulunamadı" hatası
- .NET 8.0 SDK'nın yüklü olduğundan emin olun
- Terminal'i yeniden başlatın
- PATH değişkenini kontrol edin

### Derleme hataları
- `dotnet restore` komutunu çalıştırın
- Visual Studio'da **Build > Rebuild Solution** yapın

### Uygulama açılmıyor
- Windows sürümünüzün güncel olduğundan emin olun
- .NET 8.0 Runtime'ın yüklü olduğundan emin olun

## Çalıştırılabilir Dosya Oluşturma

Uygulamayı .exe dosyası olarak derlemek için:

```cmd
dotnet publish -c Release -r win-x64 --self-contained
```

Bu komut `bin/Release/net8.0-windows/win-x64/publish/` klasöründe çalıştırılabilir dosya oluşturur.


