#  GezGörTürkiye1

Türkiye'deki şehirler ve mekanlar için rota planlayıcı web uygulaması.

## 📋 Proje Özellikleri

- **Şehir ve Mekan Yönetimi**
  - Yeni şehir ve mekan ekleyebilir, mevcut kayıtları güncelleyebilir veya silebilirsiniz.
  - Mekan bilgileri: Ad, Kategori, Açıklama, Şehir, Harita Konumu.

- **🗺 Rota Planlama**
  - **Sistem Rota Önerisi:** Mekanları konumlarına göre en yakın olandan başlatır.
  - **Manuel Rota:** Kullanıcı, mekan sıralamasını kendi belirleyebilir.

- **Harita Desteği (Leaflet.js)**
  - Mekanlar harita üzerinde gösterilir.
  - Rota çizgileri dinamik olarak çizilir.

- **🌗 Modern Arayüz**
  - Dark Mode destekli, mobil uyumlu (Responsive).
  - Bootstrap 5 ve FontAwesome ikonları kullanıldı.

---

## Teknolojiler

- **Backend:** ASP.NET Core MVC
- **Frontend:** HTML, CSS, Bootstrap, Leaflet.js
- **Veritabanı:** SQL Server
- **ORM:** Entity Framework Core

---

##  Klasör Yapısı

- `Controllers` → İş mantığı ve yönlendirme.
- `DataAccess` → DbContext ve veritabanı erişimi.
- `Entities` → Mekan ve Şehir sınıfları.
- `Views` → Razor View dosyaları (UI).
- `wwwroot` → CSS, JS, ve harici kaynaklar.

---

## ✨ Gelecek Geliştirmeler
- Kullanıcı giriş sistemi.
- Canlı etkinlik verileri entegrasyonu.
- Fotoğraf ve yorum ekleme özellikleri.

