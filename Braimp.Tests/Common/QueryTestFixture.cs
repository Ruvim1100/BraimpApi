using AutoMapper;
using Braimp.Application.Abstraction;
using Braimp.Application.Common.Mapping;
using Braimp.Infrastructure;

namespace Braimp.Tests.Common
{
    public class QueryTestFixture : IDisposable
    {
        public BraimpDbContext Context;
        public IUnitOfWork UnitOfWork;
        public IMapper Mapper;

        public QueryTestFixture()
        {
            Context = BraimpContextFactory.Create();
            UnitOfWork = Context;
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AssemblyMappingProfile(
                    typeof(IBraimpDbContext).Assembly));
            });
            Mapper = configurationProvider.CreateMapper();
        }

        public void Dispose()
        {
            BraimpContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}
