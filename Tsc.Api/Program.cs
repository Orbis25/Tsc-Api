
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//configurations
builder.Services.AddDataBaseConfiguration(builder.Configuration);
builder.Services.AddAutomapperConfiguration();
builder.Services.AddHttpContextAccessor();
builder.Services.AddServices();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddConfigurations(builder.Configuration);
builder.Services.AddJwtConfiguration(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.PoblateDatabase(app.Environment.ContentRootPath);

app.UseCors("AnyOrigin");

app.UseAuthentication();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
