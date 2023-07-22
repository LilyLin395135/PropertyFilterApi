
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
            //�@��request�Ƕi�ӡA�βĤ@���N�|new�_�ӡA�᭱���ޥδX���A���|�O�P�@��
            //�ϥ�IPropertiesService�᭱��PropertiesService�N�|�@�_new�_��

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