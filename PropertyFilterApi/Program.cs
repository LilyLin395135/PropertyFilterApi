
using PropertyFilterApi.Controllers;
using static PropertyFilterApi.Controllers.PropertiesController;

namespace PropertyFilterApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //builder.Services.AddScoped<IPropertiesService, PropertiesService>();
            //一個request傳進來，用第一次就會new起來，後面不管用幾次，都會是同一個
            //使用IPropertiesService後面的PropertiesService就會一起new起來

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}