using Autoservice.DAL.Entities;
using Autoservice.DAL.Data;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoservice.DAL.Seeders
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {
            if (context.Clients.Any()) return;

            var serviceTypes = new[] 
            {
                 "Engine Repair",
                 "Oil Change",
                 "Tire Replacement",
                 "Brake Service",
                 "Paint Job",
                 "Battery Replacement"
            };

            var rnd = new Random();

            var clients = new Faker<Client>()
                .RuleFor(c => c.FullName, f => f.Name.FullName())
                .Generate(10);

            var cars = new Faker<Car>()
                .RuleFor(c => c.Brand, f => f.Vehicle.Manufacturer())
                .RuleFor(c => c.Year, f => f.Date.Past(10).Year)
                .Generate(10);

            var employees = new Faker<Employee>()
                .RuleFor(e => e.FullName, f => f.Name.FullName())
                .RuleFor(e => e.Position, f => f.Name.JobTitle())
                .Generate(5);

            context.Employees.AddRange(employees);
            context.SaveChanges();

            var services = new Faker<Service>()
                .RuleFor(s => s.Type, f => f.PickRandom(serviceTypes))
                .RuleFor(s => s.Price, f => f.Random.Decimal(100, 500))
                .RuleFor(s => s.EmployeeId, f => f.PickRandom(employees).EmployeeId)
                .Generate(10);

            context.Clients.AddRange(clients);
            context.Cars.AddRange(cars);
            context.Services.AddRange(services);
            context.SaveChanges();

            var records = new Faker<Record>()
                .RuleFor(r => r.ClientId, f => f.PickRandom(clients).ClientId)
                .RuleFor(r => r.CarId, f => f.PickRandom(cars).CarId)
                .RuleFor(r => r.ServiceId, f => f.PickRandom(services).ServiceId)
                .RuleFor(r => r.Date, f => f.Date.Recent(30))
                .Generate(20);

            context.Records.AddRange(records);
            context.SaveChanges();
        }
    }
}
