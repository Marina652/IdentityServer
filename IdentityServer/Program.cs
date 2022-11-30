using IdentityServer;
using System.Reflection;
using User.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddIdentityServer()
       .AddDeveloperSigningCredential()        //This is for dev only scenarios when you don’t have a certificate to use.
       .AddInMemoryApiScopes(Config.ApiScopes)
       .AddInMemoryClients(Config.Clients);

//builder.Services.AddIdentityServer()
//            //.AddAspNetIdentity<Account>()
//            .AddInMemoryApiResources(Config.ApiResources)
//            .AddInMemoryClients(Config.Clients)
//            .AddInMemoryIdentityResources(Config.IdentityResources)
//            .AddInMemoryApiScopes(Config.ApiScopes)
//            .AddDeveloperSigningCredential();

//builder.Services.AddCors(optionss =>
//{
//    optionss.AddPolicy("CorsPolicy", builder =>
//    builder.AllowAnyOrigin()
//    .AllowAnyMethod()
//    .AllowAnyHeader());
//});

builder.Services.AddSwaggerGen();

builder.Services.AddApplication(builder.Configuration);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDeveloperExceptionPage();


app.UseHttpsRedirection();

app.UseIdentityServer();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
