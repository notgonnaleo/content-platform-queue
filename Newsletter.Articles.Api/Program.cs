using MassTransit;
using Microsoft.EntityFrameworkCore;
using Newsletter.Articles.Api.Contexts;
using Newsletter.Articles.Api.Repositories;
using Newsletter.Articles.Api.Consumers.Analytics;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add services to the container.
builder.Services.AddScoped<ArticleRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var messageBrokerConfig = builder.Configuration.GetSection("MessageBroker");
builder.Services.AddMassTransit(bus =>
{
    bus.AddConsumer<ArticleInteractionsTotal>();
    bus.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(messageBrokerConfig["Host"]!,
        host =>
        {
            host.Username(messageBrokerConfig["Username"]!);
            host.Password(messageBrokerConfig["Password"]!);

            cfg.ReceiveEndpoint("article-interactions-total-queue", e =>
            {
                e.ConfigureConsumer<ArticleInteractionsTotal>(context);
            });
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
