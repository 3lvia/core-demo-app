
using CoreDemoApp;

namespace UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void InitWorker_IsNotNull()
        {
            var worker = new Worker(null);
            Assert.NotNull(worker);
        }
    }
}