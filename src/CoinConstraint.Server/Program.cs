using CoinConstraint.Application.Identity;
using CoinConstraint.Server.Infrastructure.Identity;
using CoinConstraint.Server.Swagger;
using CoinConstraint.Server.Util;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

if (!builder.Environment.IsDevelopment())
{
    //Configure Azure key vault
    var keyVaultName = builder.Configuration["KeyVault:VaultName"];

    if (!String.IsNullOrEmpty(keyVaultName))
    {
        var keyVaultEndpoint = $"https://{keyVaultName}.vault.azure.net/";
        var azureServiceTokenProvider = new AzureServiceTokenProvider();
        var keyVaultAuthCallback = new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback);
        var keyVaultClient = new KeyVaultClient(keyVaultAuthCallback);
        var vaultPrefixManager = new DefaultKeyVaultSecretManager();
        builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, keyVaultClient, vaultPrefixManager);
    }
}

// Add services to the container.

var connectionString = builder.Configuration["ConnectionString"];
builder.Services.AddDbContext<CoinConstraintContext>(options =>
              options.UseSqlServer(connectionString));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

builder.Services.AddControllersWithViews();

builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "CoinConstraint API", Version = "v1" });
    opt.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Scheme = "bearer"
    });
    opt.OperationFilter<AuthorizationOperationFilter>();
});
builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// execute in package manager:
//  Add-Migration CreateIdentitySchema -o Data/Migrations
//  Update-Database

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtIssuer"],
            ValidAudience = builder.Configuration["JwtAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["CoinConstraintJWTSecretKey"]))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("BudgetAuthorPolicy", policy =>
        policy.Requirements.Add(new OperationAuthorizationRequirement()));
});

builder.Services.AddTransient<IAuthorizationHandler, BudgetAuthorizationHandler>();

builder.Services.AddScoped<ICurrentUserService, ServersideUserService>();

builder.Services.AddRazorPages();
builder.Services.AddRepositores();
builder.Services.AddDataAccessServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
