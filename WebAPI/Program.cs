using WebAPI.Data.Favorites;
using WebAPI.Data.Actors;
using WebAPI.Data.Movies;
using WebAPI.Data.User;
using WebAPI.Data.FavouriteActor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IMovieService, MovieService>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IFavoriteMovieService, FavoriteMovieService>();
builder.Services.AddSingleton<IActorService, ActorService>();
builder.Services.AddSingleton<IFavouriteActorService, FavouriteActorService>();

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseCors("corsapp");
app.UseAuthorization();

app.UseRouting(); 
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});


app.MapControllers();

app.Run();
