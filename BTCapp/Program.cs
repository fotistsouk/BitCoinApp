using BTCapp.Contracts;
using BTCapp.Application;
using BTCapp.Domain;
using BTCapp.Infrastructure;
using MediatR;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BTCDBContext>();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IPriceRepository, PriceRepository>();
builder.Services.AddDbContext<BTCDBContext>();
builder.Services.AddControllers( );
builder.Services.AddScoped<IApplicationService, ApplicationService>();
builder.Services.AddScoped<IUpdateBtcService, UpdateBtcService>();
builder.Services.AddScoped<HttpClient>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "BTCApi", Version = "v1" });
});
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BTCApi v1"));


app.UseCors(x => x
                   .AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader()
           );
app.Run();
