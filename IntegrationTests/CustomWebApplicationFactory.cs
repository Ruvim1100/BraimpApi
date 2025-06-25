//using Braimp.Application.Abstraction;
//using Braimp.Domain.Entities.Courses;
//using Braimp.Domain.Entities.Courses.Enums;
//using Braimp.Infrastructure;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Mvc.Testing;
//using Microsoft.AspNetCore.TestHost;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.DependencyInjection.Extensions;

//namespace IntegrationTests;
//public class CustomWebApplicationFactory : WebApplicationFactory<Program>
//{
//    protected override void ConfigureWebHost(IWebHostBuilder builder)
//    {
//        builder.ConfigureServices(services =>
//        {
//            services.RemoveAll(typeof(BraimpDbContext));
//            services.RemoveAll(typeof(DbContextOptions<BraimpDbContext>));

//            services.AddDbContext<BraimpDbContext>(options =>
//            {
//                options.UseInMemoryDatabase($"TestDb_{Guid.NewGuid()}");
//            });

//            services.RemoveAll(typeof(ICurrentUserService));
//            services.AddSingleton<ICurrentUserService, TestCurrentUserService>();

//            var sp = services.BuildServiceProvider();

//            using var scope = sp.CreateScope();
//            var scopedServices = scope.ServiceProvider;
//            var db = scopedServices.GetRequiredService<BraimpDbContext>();

//            db.Database.EnsureCreated();

//            if (!db.Courses.Any())
//            {
//                db.Courses.Add(new Course
//                {
//                    Id = Guid.Parse("07EBFD0E-AB52-462F-AB96-8E31C689B7FC"),
//                    Title = "Seeded Course",
//                    Description = "Seeded Description",
//                    GradingSystem = GradingSystem.PointsOutOf10,
//                    CourseCategoryId = Guid.NewGuid(),
//                });
//                db.SaveChanges();
//            }
//        });
//    }
//}

