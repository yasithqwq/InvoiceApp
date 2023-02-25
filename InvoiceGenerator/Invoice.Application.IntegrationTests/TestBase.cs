using NUnit.Framework;
using System.Threading.Tasks;

namespace Invoice.Application.IntegrationTests
{
    public class TestBase
    {
        [SetUp]
        public async Task TestSetUp()
        {
            //await ResetState();
        }
    }
}
