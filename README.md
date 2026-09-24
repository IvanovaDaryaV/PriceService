# Pricing Service

Учебный backend-сервис для работы с ценами товаров.

Проект создан для практики backend-разработки на .NET и изучения взаимодействия PostgreSQL, Redis, Kafka и gRPC.

## Используемые технологии

* C#
* .NET 10
* ASP.NET Core Web API
* Entity Framework Core
* PostgreSQL
* Redis
* Apache Kafka
* gRPC
* Docker

## Реализованная на данный момент функциональность

* создание товара, получение товара по id, изменение стоимости товара по id;
* хранение данных в PostgreSQL;
* EF Core migrations:
* cache-aside при получении товара по id, update cache при обновлении цены.

## Архитектура

```text
Client
   |
   v
ASP.NET Core API
   |
   +------> PostgreSQL
   |
   +------> Redis
   |
   +------> Kafka
```

## Локальный запуск

### Требования

* .NET 10 SDK
* Docker

### Инфраструктура

Start PostgreSQL, Redis and Kafka:

```bash
docker compose up -d
```

### БД

Apply EF Core migrations:

```bash
dotnet ef database update
```

### Приложение

```bash
dotnet run
```

Строки подключения и остальные секреты храняться с использованием .NET User Secrets и не добавляются в репозиторий.

## Цели проекта

Цель проекта практика в:

* REST API разработке;
* PostgreSQL, EF Core;
* кэширование в Redis;
* асинхронные сообщения через Kafka;
* service-to-service взаимодействие с gRPC;
* тестирование и базовая backend-архитектура.
