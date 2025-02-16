using MassTransit;
using Microsoft.EntityFrameworkCore;
using Newsletter.Analytics.Api.Consumers.Articles;
using Newsletter.Analytics.Api.Contexts;
using Newsletter.Analytics.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add services to the container.
builder.Services.AddScoped<AnalyticRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var messageBrokerConfig = builder.Configuration.GetSection("MessageBroker");
builder.Services.AddMassTransit(bus =>
{
    bus.AddConsumer<ArticleRated>();

    bus.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(messageBrokerConfig["Host"]!,
        host =>
        {
            host.Username(messageBrokerConfig["Username"]!);
            host.Password(messageBrokerConfig["Password"]!);
        });

        cfg.ReceiveEndpoint("article-rated-queue", e =>
        {
            e.ConfigureConsumer<ArticleRated>(context);
        });
    });
});


var app = builder.Build();

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
