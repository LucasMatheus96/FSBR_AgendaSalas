# FSBR Agenda de Salas

Projeto completo de **controle de reservas de salas** utilizando **ASP.NET Core Web API**, **MVC (Razor)**, **Entity Framework Core** e **Clean Architecture**. O sistema permite o gerenciamento de usuários, salas e reservas, com regras de negócio como prevenção de conflitos de horário e envio de e-mails de confirmação.

---

## 📁 Estrutura do Projeto

### 📦 Projeto `FSBR_AgendaSalas.API`

> Responsável por expor a Web API.

```
FSBR_AgendaSalas.API
│
├── Properties
├── Controllers
│   ├── ReservaController.cs
│   ├── SalaController.cs
│   └── UsuarioController.cs
├── DTOs
└── Program.cs
```

### 📦 Projeto `FSBR_AgendaSalas.Application`

> Contém as regras de negócio e os serviços da aplicação.

```
FSBR_AgendaSalas.Application
│
├── Configuration
├── DTOs
├── Interfaces
├── Services
└── Utils
```

### 📦 Projeto `FSBR_AgendaSalas.Domain`

> Define as entidades, contratos de repositórios e serviços, além das regras compartilhadas.

```
FSBR_AgendaSalas.Domain
│
├── Entities
├── Repositories
├── Services
└── Shared
```

### 📦 Projeto `FSBR_AgendaSalas.Infrastructure`

> Responsável pelo acesso ao banco de dados e persistência dos dados.

```
FSBR_AgendaSalas.Infrastructure
│
├── DTOs
├── Migrations
├── Persistence
├── Repositories
└── Services
```

### 📦 Projeto `FSBR_AgendaSalas.MVC`

> Interface web da aplicação utilizando ASP.NET Core MVC com Razor Views.

```
FSBR_AgendaSalas.MVC
│
├── Properties
├── Controllers
├── Models
├── ViewModels
└── Views
```

---

## 🔌 Endpoints da API

### 🗓️ Reservas

- `GET /api/Reserva` — Listar todas as reservas  
- `GET /api/Reserva/{id}` — Obter detalhes de uma reserva  
- `POST /api/Reserva` — Criar uma nova reserva  
- `PUT /api/Reserva/{id}` — Atualizar uma reserva existente  
- `DELETE /api/Reserva/{id}` — Cancelar uma reserva  

### 🏢 Salas

- `GET /api/Sala` — Listar todas as salas  
- `GET /api/Sala/{id}` — Obter detalhes de uma sala  
- `POST /api/Sala` — Cadastrar uma nova sala  
- `PUT /api/Sala/{id}` — Atualizar uma sala existente  
- `DELETE /api/Sala/{id}` — Remover uma sala  

### 👤 Usuários

- `GET /api/Usuario` — Listar todos os usuários  
- `GET /api/Usuario/{id}` — Obter detalhes de um usuário  
- `POST /api/Usuario` — Cadastrar um novo usuário  
- `PUT /api/Usuario/{id}` — Atualizar um usuário existente  
- `DELETE /api/Usuario/{id}` — Remover um usuário  

---

## ⚙️ Funcionalidades

- CRUD completo de Reservas, Salas e Usuários  
- Validação de conflitos de horário em reservas  
- Envio de e-mail de confirmação de reserva  
- Separação em camadas seguindo o padrão Clean Architecture  
- Repositórios com Entity Framework Core  
- Validações centralizadas e tratativas de erro  

---

## 🚀 Como Executar

1. **Restore dos pacotes:**

```bash
dotnet restore
```

2. **Aplicar Migrations e atualizar o banco:**

```bash
dotnet ef migrations add InitialCreate -p FSBR_AgendaSalas.Infrastructure -s FSBR_AgendaSalas.API
dotnet ef database update -p FSBR_AgendaSalas.Infrastructure -s FSBR_AgendaSalas.API
```

3. **Rodar o projeto:**

Execute os projetos `FSBR_AgendaSalas.API` e `FSBR_AgendaSalas.MVC`.

---

## 🛠️ Tecnologias Utilizadas

- .NET 8
- ASP.NET Core Web API
- ASP.NET Core MVC (Razor)
- Entity Framework Core
- SQL Server
- AutoMapper
- FluentValidation
- Clean Architecture
- Injeção de Dependência
- Swagger (Swashbuckle)

---

## 📄 Licença

Este projeto está licenciado sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.