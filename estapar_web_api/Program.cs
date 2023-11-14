using Microsoft.EntityFrameworkCore;

using estapar_web_api;

var builder = WebApplication.CreateBuilder(args);

var port = 8080;
builder.Configuration["Urls"] = $"http://localhost:{port}";

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<EstaparDbContext>(optionsBuilder => optionsBuilder.UseSqlServer("Name=ConnectionStrings:ConnectToEstaparDB"));
builder.Services.AddTransient<DatabaseSeeder>();
builder.Services.AddTransient<CommomService>();
builder.Services.AddTransient<CarroService>();
builder.Services.AddTransient<FechamentoService>();
builder.Services.AddTransient<TempoMedioService>();

var app = builder.Build();

if (args.Length == 1 && args[0].ToLower() == "seeddata")
    SeedData(app);

void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
    if (scopedFactory == null) {
        Console.WriteLine("não foi possível criar o scoped Factory");
        return;
    }
    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<DatabaseSeeder>();
        service?.SeedData();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();