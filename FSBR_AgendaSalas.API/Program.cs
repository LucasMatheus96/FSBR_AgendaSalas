using FSBR_AgendaSalas.Application.Interfaces;
using FSBR_AgendaSalas.Application.Services;
using FSBR_AgendaSalas.Domain.Repositories;
using FSBR_AgendaSalas.Infrastructure.Repositories;  

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços ao contêiner
builder.Services.AddControllers();

// Registrar os serviços de aplicação (Services)
builder.Services.AddScoped<IReservaService, ReservaService>();
builder.Services.AddScoped<ISalaService, SalaService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

// Registrar os repositórios (Repositories)
builder.Services.AddScoped<IReservaRepository, ReservaRepository>();
builder.Services.AddScoped<ISalaRepository, SalaRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlServer(connectionString));

// Configurar Swagger para documentação da API (opcional, mas recomendado para testes)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuração do middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();  // Mapear as controllers para que as rotas sejam reconhecidas

app.Run();
