using Braimp.Persistence;

namespace Braimp.Tests.Common
{
    public abstract class TestCommandBase : IDisposable
    {
        protected readonly BraimpDbContext context;

        public TestCommandBase()
        {
            context = BraimpContextFactory.Create();
        }

        public void Dispose()
        {
            BraimpContextFactory.Destroy(context);
        }
    }
}
