using Movies.BL.Interface;
using Movies.BL.Repository;
using Movies.DAL.Database;
using Movies.BL.Mapper;

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(name: "v1", info: new OpenApiInfo
    {
        Version = "v1",
        Title = "TestMoviesProject",
        Description = "DevCreed Project",
        TermsOfService = new Uri(uriString: "https://docs.microsoft.com"),
        Contact = new OpenApiContact
        {
            Name = "Abdullah",
            Email = "Abdullahyousr94@gmail.com",
            Url = new Uri(uriString: "https://docs.microsoft.com")
        },
        License = new OpenApiLicense
        {
            Name = "My license",
            Url = new Uri(uriString: "https://docs.microsoft.com")
        }
    });
    options.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\""
    });
    options.AddSecurityRequirement(securityRequirement: new OpenApiSecurityRequirement
    {
       {
           new OpenApiSecurityScheme
           {
               Reference = new OpenApiReference
               {
                   Type = ReferenceType.SecurityScheme,
                   Id = "Bearer"
               },
               Name = "Bearer",
               In = ParameterLocation.Header,
           },
           new List<string>()
       }
    });
});

// Enhancement ConnectionString
var connectionString = builder.Configuration.GetConnectionString("DevCreedConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options => 
options.UseSqlServer(connectionString));

builder.Services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));

builder.Services.AddScoped<IGenreRep,GenreRep>();
builder.Services.AddScoped<IMovieRep,MovieRep>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
