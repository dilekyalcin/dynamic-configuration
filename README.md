# Dynamic Configuration

Bu repo, hem **backend** (.NET 8/C#) hem de **frontend** (Next.js/React) projelerini içeren dinamik bir konfigürasyon yönetim sistemidir.

## Genel Bakış

- **Backend:** .NET 8 ile yazılmış, farklı servisler ve API'ler içerir.
- **Frontend:** Next.js tabanlı modern bir kullanıcı arayüzü sunar.

## Kurulum

### Gereksinimler

- Node.js (v18+ önerilir)
- .NET 8 SDK

### Backend

Her bir backend servisi için ilgili dizine girip aşağıdaki adımları uygulayın:

```bash
cd backend/Services/ConfigurationReader/ConfigurationReaderAPI
dotnet restore
dotnet build
dotnet run
```

> Diğer backend servisleri için de benzer şekilde ilgili dizinlere girip aynı komutları çalıştırabilirsiniz.

### Frontend

```bash
cd frontend
npm install
npm run dev
```

## Kullanım

- Backend servisleri varsayılan olarak `http://localhost:5264` veya ilgili portlarda çalışır.
- Frontend arayüzü varsayılan olarak `http://localhost:3000` adresinde çalışır.

## Dizin Yapısı

```plaintext
dynamic-configuration/
  backend/
    Services/
      ConfigurationReader/
      ConfigurationService/
      Service-A/
      Service-B/
  frontend/
    src/
    public/
    ...
```
