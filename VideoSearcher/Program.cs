
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;

namespace VideoSearcher
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "VideoSearcher API",
                    Description = "Team ¹7 App API",
                }); ;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            app.UseSwagger();
            app.UseSwaggerUI();
            //}

            app.UseAuthorization();



            //static host
            app.UseStaticFiles();
            app.MapGet("/", () => Results.Content(File.ReadAllText("./wwwroot/index.html"), "text/html"));

            //api

            app.MapGet("/api/searchtips", (string search) =>
            {
                if (search == "0") return new List<string>();
                return Enumerable.Range(1, 10).Select(v => v.ToString());
            })
            .WithName("SearchTips")
            .WithOpenApi();

            string[] uris = { 
                "https://i.ytimg.com/vi/6iseNlvH2_s/hq720.jpg?sqp=-oaymwE2COgCEMoBSFXyq4qpAygIARUAAIhCGAFwAcABBvABAfgB_gmAAtAFigIMCAAQARhyIFkoKjAP&rs=AOn4CLCUIHvej2p-ph80h1oCeDBRsoYgbQ",
                "https://i.ytimg.com/vi/SUw7R1gBWR4/hq720.jpg?sqp=-oaymwEcCOgCEMoBSFXyq4qpAw4IARUAAIhCGAFwAcABBg==&rs=AOn4CLClcFNQgNqM71U4zfW7mxr6MNT7dQ",
                "https://i.ytimg.com/vi/eweLbt5PPNQ/hq720.jpg?sqp=-oaymwEcCOgCEMoBSFXyq4qpAw4IARUAAIhCGAFwAcABBg==&rs=AOn4CLD_g8ZvTVws5oxeZUngS_AYttkhBQ",
                "https://i.ytimg.com/vi/3qU90_SJe3g/hq720.jpg?sqp=-oaymwEcCOgCEMoBSFXyq4qpAw4IARUAAIhCGAFwAcABBg==&rs=AOn4CLBa1I6PWIVUQ6cHymq9qYpdzKnsnw",
                "https://i.ytimg.com/vi/d5f7OhLQF4I/hqdefault.jpg?sqp=-oaymwEcCOADEI4CSFXyq4qpAw4IARUAAIhCGAFwAcABBg==&rs=AOn4CLDcnF36Nrr_hp-rnBtwEkyLVcjPPw",
                "https://i.ytimg.com/vi/5T0eW4f98PQ/hqdefault.jpg?sqp=-oaymwEcCOADEI4CSFXyq4qpAw4IARUAAIhCGAFwAcABBg==&rs=AOn4CLAyn9LyxDHH0bilLNBEJLeDeuN-4Q",
                "https://i.ytimg.com/vi/_EFfjRgxynU/hq720.jpg?sqp=-oaymwEcCOgCEMoBSFXyq4qpAw4IARUAAIhCGAFwAcABBg==&rs=AOn4CLAisbJ4R9rVbOctjEEqlZaVWJpfiw",
                
            };

            app.MapGet("/api/videos", (string search) =>
            {
                if (search == "0") return new List<Video>();
                return Enumerable.Range(1, 10).Select(v => new Video() { Id = v , Title = v.ToString(), Thumbnail = uris[Random.Shared.Next(0, uris.Length)], Channel = v.ToString(), Date = new DateOnly(2023, 12, v) });
            })
           .WithName("Videos")
           .WithOpenApi();


            if (app.Environment.IsDevelopment())
            {
                app.Urls.Add("http://*:3000");
                app.Urls.Add("http://*:80");
            }

            app.Run();
        }
    }
}
