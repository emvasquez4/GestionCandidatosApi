using GestionCandidatosApi.ConexionDB;
using GestionCandidatosApi.Modelos;
using GestionCandidatosApi.Services;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Context>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("ConexionSql"),
        sqlOptions => sqlOptions.EnableRetryOnFailure());
});

builder.Services.AddTransient<IUsuariosService, UsuariosService>();
builder.Services.AddTransient<IVacantesService, VacantesService>();

//builder.Services.AddTransient<ICandidatosService, CandidatosService>();
//builder.Services.AddTransient<IEntrevistasService, EntrevistasService>();
//builder.Services.AddTransient<IPermisoService, PermisoService>();
//builder.Services.AddTransient<IPuestosService, PuestosService>();
//builder.Services.AddTransient<IRolesService, RolesService>();

//builder.Services.AddTransient<IRoles_PermisosService, RolesPermisosService>();
//builder.Services.AddTransient<IUsuarios_RolesService, Usuarios_RolesService>();
//builder.Services.AddTransient<IMenus_MenusService, MenusService>();

builder.Services.Configure<EncryptionSettings>(builder.Configuration.GetSection("EncryptionSettings"));
builder.Services.AddTransient<IUsuariosService, UsuariosService>();
builder.Services.AddSingleton<EncryptionService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
using (var SCOPE = app.Services.CreateScope())
{
    var context = SCOPE.ServiceProvider.GetRequiredService<Context>();
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
