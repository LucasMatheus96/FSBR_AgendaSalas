using FSBR_AgendaSalas.Application.Interfaces;
using FSBR_AgendaSalas.Application.Services;
using FSBR_AgendaSalas.Domain.Repositories;
using FSBR_AgendaSalas.Infrastructure.Repositories;  

var builder = WebApplication.CreateBuilder(args);

// Adicionar servi�os ao cont�iner
builder.Services.AddControllers();

// Registrar os servi�os de aplica��o (Services)
builder.Services.AddScoped<IReservaService, ReservaService>();
builder.Services.AddScoped<ISalaService, SalaService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

// Registrar os reposit�rios (Repositories)
builder.Services.AddScoped<IReservaRepository, ReservaRepository>();
builder.Services.AddScoped<ISalaRepository, SalaRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlServer(connectionString));

// Configurar Swagger para documenta��o da API (opcional, mas recomendado para testes)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configura��o do middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();  // Mapear as controllers para que as rotas sejam reconhecidas

app.Run();
