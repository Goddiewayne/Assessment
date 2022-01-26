using Microsoft.EntityFrameworkCore;
using Zedcrest.Data;
using Zedcrest.Data.Repository;
using Zedcrest.Api.Controllers;
using Zedcrest.Api.AutoMapper;
using Zedcrest.Data.RabbitQueue;
using Zedcrest.Service.EmailService;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 
var connectionString = builder.Configuration.GetConnectionString("SQLDBConnection");
builder.Services.AddDbContext<ApplicationDbContext>(
        options => options.UseSqlServer(connectionString,
            providerOptions => providerOptions.EnableRetryOnFailure()));

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddAutoMapper(typeof(UserController));
builder.Services.AddTransient(typeof(IRepository<>), typeof(GenericRepository<>));
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddMediatR(typeof(GetAllUsersQuery).Assembly);
// Rabbit Dependency injection
builder.Services.AddSingleton(rh => RabbitHutch.CreateBus("localhost"));

var emailConfig = builder.Configuration
       .GetSection("EmailConfiguration")
       .Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);
builder.Services.AddSingleton<IEmailConfiguration>(emailConfig);
builder.Services.AddTransient<IEmailService, EmailService>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
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
