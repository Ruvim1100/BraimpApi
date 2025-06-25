using AutoMapper;
using Braimp.Application.Abstraction;
using Braimp.Application.Features.Courses.Queries.GetCourseList;
using Braimp.Application.Mapping;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Braimp.Tests.Integration.Helpers;
public static class TestHelpers
{
    public static IMapper CreateMapper()
    {
        var services = new ServiceCollection();
        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile(new AssemblyMappingProfile(typeof(GetCourseListQuery).Assembly));
            cfg.AddProfile(new AssemblyMappingProfile(typeof(IBraimpDbContext).Assembly));
        });
        return services.BuildServiceProvider().GetRequiredService<IMapper>();
    }

    public static IMediator CreateMediator(IBraimpDbContext dbContext, IMapper mapper)
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(GetCourseListQueryHandler).Assembly);
        });
        services.AddSingleton(dbContext);
        services.AddSingleton(mapper);
        var currentUserMock = new Mock<ICurrentUserService>();
        currentUserMock.Setup(u => u.UserId).Returns(Guid.NewGuid());
        services.AddSingleton(currentUserMock.Object);
        return services.BuildServiceProvider().GetRequiredService<IMediator>();
    }
}
