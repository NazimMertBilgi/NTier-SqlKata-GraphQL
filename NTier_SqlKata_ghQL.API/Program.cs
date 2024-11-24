using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using NTier_SqlKata_ghQL.Core.Classes.AppSettings;
using NTier_SqlKata_ghQL.Business.Abstract; // if you haven't run npm run generate yet, put this part in the comment line. ( e�er hen�z npm run generate i�lemi ger�ekle�tirmediysen bu k�sm� yorum sat�r�na al. )
using NTier_SqlKata_ghQL.Business.Concrete; // if you haven't run npm run generate yet, put this part in the comment line. ( e�er hen�z npm run generate i�lemi ger�ekle�tirmediysen bu k�sm� yorum sat�r�na al. )
using NTier_SqlKata_ghQL.DataAccess.Abstract; // if you haven't run npm run generate yet, put this part in the comment line. ( e�er hen�z npm run generate i�lemi ger�ekle�tirmediysen bu k�sm� yorum sat�r�na al. )
using NTier_SqlKata_ghQL.DataAccess.Concrete.SqlKata; // if you haven't run npm run generate yet, put this part in the comment line. ( e�er hen�z npm run generate i�lemi ger�ekle�tirmediysen bu k�sm� yorum sat�r�na al. )
using NTier_SqlKata_ghQL.Entities.Concrete; // if you haven't run npm run generate yet, put this part in the comment line. ( e�er hen�z npm run generate i�lemi ger�ekle�tirmediysen bu k�sm� yorum sat�r�na al. )
using SqlKata.Compilers;
using SqlKata.Execution;
using System.Text;
using System.Text.Json.Serialization;
using GraphQL.MicrosoftDI;
using GraphQL.Types;
using NTier_SqlKata_ghQL.API.Schema;
using GraphQL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGraphQL(options => options.ConfigureExecution((opt, next) =>
{
    opt.EnableMetrics = true;
    return next(opt);
}).AddSystemTextJson());

// Add services to the container.

builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

//
builder.Services.Configure<FormOptions>(options =>
{
    options.ValueCountLimit = int.MaxValue;
});
//
var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);
var appSettings = appSettingsSection.Get<AppSettings>();
var key = Encoding.ASCII.GetBytes(appSettings.Secret);
builder.Services.AddAuthentication(scheme =>
{
    scheme.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    scheme.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };

    options.Events = new JwtBearerEvents
    {
        OnTokenValidated = ctx =>
        {
            return Task.CompletedTask;
        },
        OnAuthenticationFailed = ctx =>
        {
            ctx.NoResult();
            ctx.Response.StatusCode = 500;
            ctx.Response.ContentType = "text/plain";
            ctx.Response.WriteAsync(ctx.Exception.ToString()).Wait();
            return Task.CompletedTask;
        },
        OnChallenge = ctx =>
        {
            return Task.CompletedTask;
        },
        OnMessageReceived = ctx =>
        {
            return Task.CompletedTask;
        }
    };
});
//

builder.Services.AddTransient<QueryFactory>((e) =>
{
    var connection = new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection"));
    var compiler = new SqlServerCompiler();
    return new QueryFactory(connection, compiler);
});
builder.Services.AddTransient<XQuery>((e) =>
{
    var connection = new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection"));
    var compiler = new SqlServerCompiler();
    return new XQuery(connection, compiler);
});
//
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseGraphQLAltair();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseGraphQL<ISchema>();

app.MapControllers();

app.Run();
