using AutoMapper;
using Braimp.Application.Common.Mapping;
using Braimp.Application.Interfaces;
using Braimp.Persistence;

namespace Braimp.Tests.Common
{
    public class QueryTestFixture : IDisposable
    {
        public BraimpDbContext Context;
        public IMapper Mapper;

        public QueryTestFixture()
        {
            Context = BraimpContextFactory.Create();
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
