
using Autoservice.DAL.Data;
using Autoservice.DAL.Repositories.Interfaces;
using Autoservice.DAL.Repositories;
using Autoservice.DAL.Seeders;
using Microsoft.EntityFrameworkCore;
using Autoservice.DAL.UOW;
using Autoservice.BLL.Automapper;
using Autoservice.BLL.Services.Interfaces;
using Autoservice.BLL.Services;
using FluentValidation;
using Autoservice.BLL.Validator;
using Autoservice.DAL.Entities;
using FluentValidation.AspNetCore;
using Autoservice.BLL.DTO;

namespace Autoservice.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IClientRepository, ClientRepository>();
            builder.Services.AddScoped<ICarRepository, CarRepository>();
            builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IRecordRepository, RecordRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IClientService, ClientService>();
            builder.Services.AddScoped<ICarService, CarService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IServiceService, ServiceService>();
            builder.Services.AddScoped<IRecordService, RecordService>();

            builder.Services.AddAutoMapper(typeof(MappingProfile));


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddScoped<IValidator<ClientDto>, ClientDtoValidator>();
            builder.Services.AddScoped<IValidator<CarDto>, CarDtoValidator>();
            builder.Services.AddScoped<IValidator<EmployeeDto>, EmployeeDtoValidator>();
            builder.Services.AddScoped<IValidator<ServiceDto>, ServiceDtoValidator>();
            builder.Services.AddScoped<IValidator<RecordDto>, RecordDtoValidator>();


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

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<AppDbContext>();
                DbInitializer.Seed(context);
            }

            app.Run();
        }
    }
}
