Sistema de Controle de Reservas de Salas - FSBR_AgendaSalas
Descrição do Projeto
O FSBR_AgendaSalas é um sistema completo para controle de reservas de salas de reunião. Ele é composto por dois componentes principais: uma API Web e um Frontend MVC. O sistema é construído utilizando ASP.NET Core, Entity Framework Core, e Docker para containerização. A aplicação permite que os usuários façam reservas de salas, com validação de disponibilidade, envio de e-mails de confirmação, e manipulação de cancelamentos. O sistema também inclui funcionalidades de gestão de usuários e salas.

Funcionalidades
Gerenciamento de Salas: Cadastrar, listar e editar salas disponíveis.

Gestão de Reservas: Criar, editar, visualizar e cancelar reservas de salas.

Envio de E-mails: Envio automático de e-mails para confirmação de reservas.

Validação de Conflitos de Horários: Impede reservas em horários já ocupados.

Restrição de Cancelamento: Restrições baseadas no tempo restante para o evento.

Autenticação de Usuários: Controle de acesso através de autenticação no sistema.

Tecnologias Utilizadas
ASP.NET Core: Framework utilizado para o desenvolvimento da API e frontend MVC.

Entity Framework Core: ORM utilizado para interação com o banco de dados.

SQL Server: Banco de dados utilizado para armazenar informações de reservas, salas e usuários.

Docker: Containerização da aplicação para facilitar a implantação e execução em diferentes ambientes.

SMTP (SendGrid ou outro): Para o envio de e-mails de confirmação de reservas.

Estrutura do Projeto
O sistema é composto por três principais partes:

FSBR_AgendaSalas.API - API Backend para gerenciamento das reservas, usuários e salas.

FSBR_AgendaSalas.MVC - Frontend baseado em ASP.NET Core MVC com Razor Views para interação com o usuário.

Banco de Dados (SQL Server) - Armazena dados relacionados a usuários, salas e reservas.

Pré-Requisitos
Antes de rodar o projeto, é necessário ter as seguintes ferramentas instaladas:

Docker: Para rodar a aplicação em containers.

.NET 8.0 SDK: Para compilar e rodar a aplicação localmente.

Visual Studio ou VS Code: IDE recomendada para desenvolvimento.

SQL Server: Para rodar o banco de dados localmente ou na nuvem.

SMTP (opcional): Para configuração de envio de e-mails de confirmação.

Instalação e Execução
Passo 1: Clonando o Repositório
Clone o repositório para o seu ambiente local:

bash
Copiar
Editar
git clone https://github.com/seu-usuario/FSBR_AgendaSalas.git
cd FSBR_AgendaSalas
Passo 2: Configuração do Docker
Dockerfile para a API (Backend)

O Dockerfile da API está localizado em FSBR_AgendaSalas.API/Dockerfile. Ele é responsável por compilar a aplicação backend e gerar a imagem do Docker.

dockerfile
Copiar
Editar
# Usar a imagem base do .NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Usar a imagem do SDK do .NET para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["FSBR_AgendaSalas.API/FSBR_AgendaSalas.API.csproj", "FSBR_AgendaSalas.API/"]
RUN dotnet restore "FSBR_AgendaSalas.API/FSBR_AgendaSalas.API.csproj"
COPY . .
WORKDIR "/src/FSBR_AgendaSalas.API"
RUN dotnet build "FSBR_AgendaSalas.API.csproj" -c Release -o /app/build

# Publicar a aplicação
FROM build AS publish
RUN dotnet publish "FSBR_AgendaSalas.API.csproj" -c Release -o /app/publish

# Gerar a imagem final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FSBR_AgendaSalas.API.dll"]
Dockerfile para o MVC (Frontend)

O Dockerfile do frontend está localizado em FSBR_AgendaSalas.MVC/Dockerfile.

dockerfile
Copiar
Editar
# Usar a imagem base do .NET (ASP.NET Core 8.0)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Usar a imagem do SDK do .NET para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["FSBR_AgendaSalas.MVC/FSBR_AgendaSalas.MVC.csproj", "FSBR_AgendaSalas.MVC/"]
RUN dotnet restore "FSBR_AgendaSalas.MVC/FSBR_AgendaSalas.MVC.csproj"
COPY . .
WORKDIR "/src/FSBR_AgendaSalas.MVC"
RUN dotnet build "FSBR_AgendaSalas.MVC.csproj" -c Release -o /app/build

# Publicar a aplicação
FROM build AS publish
RUN dotnet publish "FSBR_AgendaSalas.MVC.csproj" -c Release -o /app/publish

# Gerar a imagem final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FSBR_AgendaSalas.MVC.dll"]
Docker Compose (orquestração dos containers)

Crie o arquivo docker-compose.yml para orquestrar o ambiente com os containers da API, MVC e banco de dados:

yaml
Copiar
Editar
version: '3.4'

services:
  webapi:
    image: fsbr_agendasalas_api
    build:
      context: .
      dockerfile: FSBR_AgendaSalas.API/Dockerfile
    ports:
      - "5000:80"
    depends_on:
      - sqlserver
    environment:
      - ASPNETCORE_ENVIRONMENT=Production
      - ConnectionStrings__DefaultConnection=Server=sqlserver;Database=AgendaSalas;User=sa;Password=Password@1234

  mvc:
    image: fsbr_agendasalas_mvc
    build:
      context: .
      dockerfile: FSBR_AgendaSalas.MVC/Dockerfile
    ports:
      - "5001:80"
    depends_on:
      - webapi

  sqlserver:
    image: mcr.microsoft.com/mssql/server:2022-latest
    environment:
      - SA_PASSWORD=Password@1234
      - ACCEPT_EULA=Y
    ports:
      - "1433:1433"
    networks:
      - default
Passo 3: Executando a Aplicação com Docker
Execute o comando abaixo para buildar e subir os containers:

bash
Copiar
Editar
docker-compose up --build
Isso criará e iniciará:

O container da API na porta 5000.

O container do MVC na porta 5001.

O SQL Server na porta 1433.

Passo 4: Acessando a Aplicação
Frontend (MVC): Acesse a aplicação no seu navegador através de http://localhost:5001.

API (Backend): A API estará disponível em http://localhost:5000.

Considerações Finais
Este sistema foi desenvolvido para ser fácil de implantar e escalável. Utilizando Docker para containerização, você pode facilmente replicar o ambiente de produção localmente. O uso de ASP.NET Core MVC no frontend e Web API no backend segue as melhores práticas de desenvolvimento, garantindo performance e flexibilidade.

Contribuições
Se você deseja contribuir com melhorias ou novas funcionalidades para este projeto, siga as etapas abaixo:

Faça um fork deste repositório.

Crie uma branch com sua feature (git checkout -b feature/nome-da-feature).

Faça as modificações desejadas e crie um commit (git commit -m 'Adiciona nova funcionalidade').

Push para a branch (git push origin feature/nome-da-feature).

Abra um pull request.

Licença
Este projeto está licenciado sob a MIT License - consulte o arquivo LICENSE para mais informações.

