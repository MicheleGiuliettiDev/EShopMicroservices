var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// COnfigure the HTTP request pipeline.

app.Run();
