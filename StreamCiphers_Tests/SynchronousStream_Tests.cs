using Microsoft.VisualStudio.TestTools.UnitTesting;
using StreamCiphers_Logic;
namespace StreamCiphers_Tests
{
    [TestClass]
    public class SynchronousStream_Tests
    {
        [TestMethod]
        public void SynchronousStream_Logic_Initialization_Works()
        {
            SynchronousStream cipher = new SynchronousStream();

            cipher.Init("1011", "11001");

            Assert.AreEqual("1011", cipher.Seed);
            Assert.AreEqual("11001", cipher.Polynomial);
        }
 
    }
}
