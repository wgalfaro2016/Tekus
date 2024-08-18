using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PruebaTecnicaTekus.Commands.Providers;
using PruebaTecnicaTekus.Commands.ProviderServices;
using PruebaTecnicaTekus.Data;
using PruebaTecnicaTekus.Models;
using PruebaTecnicaTekus.Queries.CustomProviderField;
using PruebaTecnicaTekus.Queries.Providers;
using PruebaTecnicaTekus.Queries.ProvidersServices;
using PruebaTecnicaTekus.Repositories.CustomProviderFields;
using PruebaTecnicaTekus.Repositories.Providers;
using PruebaTecnicaTekus.Repositories.ProviderServices;
using PruebaTecnicaTekus.Repositories.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header
        
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddDbContext<TekusContext>(
    opts => opts.UseSqlServer(
        builder.Configuration.GetConnectionString("TekusDb"),
        options => options.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null)
    )
);

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
    typeof(GetProviderByIdQuery).Assembly,
    typeof(GetProvidersQuery).Assembly,
    typeof(CreateProviderCommand).Assembly,
    typeof(UpdateProviderCommand).Assembly,
    typeof(GetProviderServicesQuery).Assembly,
    typeof(GetProviderServiceByIdQuery).Assembly,
    typeof(CreateProviderServiceCommand).Assembly,
    typeof(UpdateProviderServiceCommand).Assembly,
    typeof(GetCustomProviderFieldByIdQuery).Assembly
));

// JWT Authentication setup
var key = Encoding.ASCII.GetBytes("11223344556677889900112233445566");
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddAuthorization();
builder.Services.AddTransient<ICustomProviderFieldRepository, CustomProviderFieldRepository>();
builder.Services.AddTransient<IServiceRepository, ServiceRepository>();
builder.Services.AddTransient<IProvidersRepository, ProvidersRepository>();
builder.Services.AddTransient<IProviderServicesRepository, ProviderServicesRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
