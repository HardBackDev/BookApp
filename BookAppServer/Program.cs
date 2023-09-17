using BookAppServer.Extensions;
using BookAppServer.Repositories;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.ConfigureControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<RepositoryContext>(opts => opts.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection")));
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.ConfigureIISIntegration();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddServicesToContainer();
builder.Services.ConfigureCors();
builder.Services.AddResponseCaching();
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 104857600;
});

var app = builder.Build();
app.MigrateDB();

app.UseCors("CorsPolicy");
app.ConfigureExceptionHandler();

app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();

app.UseAuthentication();

app.MapControllers();

app.UseResponseCaching();

app.Run();

