using GestionCandidatosApi.ConexionDB;
using GestionCandidatosApi.Modelos;
using GestionCandidatosApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using GestionCandidatosApi.Services.Utilidades;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy => policy.WithOrigins("*")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
    });
builder.Services.AddControllers();


builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IUsuariosService, UsuariosService>();
builder.Services.AddTransient<IVacantesService, VacantesService>();

builder.Services.Configure<EncryptionSettings>(builder.Configuration.GetSection("EncryptionSettings"));
builder.Services.AddTransient<IUsuariosService, UsuariosService>();
builder.Services.AddSingleton<EncryptionService>();

var encryptionService = builder.Services.BuildServiceProvider().GetRequiredService<EncryptionService>();


// Lee la cadena de conexión encriptada desde la configuración
var encryptedConnectionString = builder.Configuration.GetConnectionString("ConexionSql");

// Desencripta la cadena de conexión utilizando el servicio
var decryptedConnectionString = encryptionService.Decrypt(encryptedConnectionString);

builder.Services.AddDbContext<Context>(options =>
{
    options.UseMySQL(decryptedConnectionString,
        sqlOptions => sqlOptions.EnableRetryOnFailure());
});
builder.Services.AddSingleton<Utilidades>();

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]!))
    };
});



//builder.Services.AddTransient<ICandidatosService, CandidatosService>();
//builder.Services.AddTransient<IEntrevistasService, EntrevistasService>();
//builder.Services.AddTransient<IPermisoService, PermisoService>();
//builder.Services.AddTransient<IPuestosService, PuestosService>();
//builder.Services.AddTransient<IRolesService, RolesService>();

//builder.Services.AddTransient<IRoles_PermisosService, RolesPermisosService>();
//builder.Services.AddTransient<IUsuarios_RolesService, Usuarios_RolesService>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Configura el middleware para usar CORS

using (var SCOPE = app.Services.CreateScope())
{
    var context = SCOPE.ServiceProvider.GetRequiredService<Context>();
}
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}



app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin");
app.UseAuthorization();

app.MapControllers();

app.Run();
