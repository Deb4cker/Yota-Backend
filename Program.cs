using Microsoft.EntityFrameworkCore;
using Yota_backend.ApplicationDbContext;
using Yota_backend.ApplicationDbContext.Interface;
using Yota_backend.Mapper;
using Yota_backend.Services;
using Yota_backend.Services.Interface;

var builder = WebApplication.CreateBuilder(args);
ConfigureServices(builder);

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");
app.MapControllers();
app.UseAuthorization();
app.Run();
return;

//***********************************************************************************************//

static void ConfigureServices(WebApplicationBuilder builder)
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var dbString = builder.Configuration.GetConnectionString("Psql");
    builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(dbString));

    builder.Services.AddScoped<IAppDbContext, AppDbContext>();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAllOrigins",
            builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod()
                    .AllowAnyHeader();
            });
    });

    builder.Services.AddAutoMapper(typeof(MapProfile));
    builder.Services.AddScoped<IPlaylistService, PlaylistService>();
    builder.Services.AddScoped<IAlbumService, AlbumService>();
    builder.Services.AddScoped<IArtistService, ArtistService>();
    builder.Services.AddScoped<IGenreService, GenreService>();
    builder.Services.AddScoped<ITrackService, TrackService>();
    builder.Services.AddScoped<ICryptographyService, CryptographyService>();
}
