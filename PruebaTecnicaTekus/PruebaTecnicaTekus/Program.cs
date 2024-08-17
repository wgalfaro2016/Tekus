using Microsoft.EntityFrameworkCore;
using PruebaTecnicaTekus.Commands.Providers;
using PruebaTecnicaTekus.Commands.ProviderServices;
using PruebaTecnicaTekus.Data;
using PruebaTecnicaTekus.Queries.Providers;
using PruebaTecnicaTekus.Queries.ProvidersServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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
    typeof(UpdateProviderServiceCommand).Assembly
));

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
