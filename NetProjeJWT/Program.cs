using Microsoft.AspNetCore.Authorization;
using NetProjeJWT.Users;
using NetProjeJWT;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddService(builder.Configuration);
builder.Services.AddScoped<IAuthorizationHandler, OverAgeRequirementHandler>();

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"),
        x => { x.MigrationsAssembly(typeof(RepositoryAssembly).Assembly.GetName().Name); });
});


builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("Over18AgePolicy", x => { x.AddRequirements(new OverAgeRequirement() { Age = 18 }); });

    x.AddPolicy("UpdatePolicy", y => { y.RequireClaim("update", "true"); });
});

var app = builder.Build();

await app.SeedIdentityData();

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
