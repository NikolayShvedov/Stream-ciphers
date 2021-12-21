using Microsoft.VisualStudio.TestTools.UnitTesting;
using StreamCiphers_Logic;

namespace StreamCiphers_Tests
{
    [TestClass]
    public class LFSR_Tests
    {
        [TestMethod]
        public void LFSR_Logic_Initialization()
        {
            LFSR lfsr = new LFSR();

            lfsr.Init("1011", "1101");
            Assert.AreEqual("1011", lfsr.Seed);
            Assert.AreEqual("1101", lfsr.Polynomial);

        }
        [TestMethod]
        public void LFSR_Shift_Works()
        {
            LFSR lfsr = new LFSR();

            lfsr.Init("1011", "11001");

            Assert.AreEqual(2, lfsr.Shift(4));
        }
        [TestMethod]
        public void LFSR_Logic_XOR_Returns_0()
        {
            LFSR lfsr = new LFSR();

            lfsr.Init("1011", "11001");

            Assert.AreEqual(0, lfsr.XORBits(11));
            Assert.AreEqual(0, lfsr.XORBits(13));
            Assert.AreEqual(0, lfsr.XORBits(29));
        }
        [TestMethod]
        public void LFSR_Logic_XOR_Returns_1()
        {
            LFSR lfsr = new LFSR();

            lfsr.Init("1011", "11001");

            Assert.AreEqual(1, lfsr.XORBits(5));
            Assert.AreEqual(1, lfsr.XORBits(10));
        }
        [TestMethod]
        public void LSFR_Logic_Bit_Replacing_Works()
        {
            LFSR lfsr = new LFSR();

            lfsr.Init("1011", "11001");

            Assert.AreEqual(15, lfsr.ReplaceFirstBit(7, 1));
            Assert.AreEqual(0, lfsr.ReplaceFirstBit(8, 0));
            Assert.AreEqual(3, lfsr.ReplaceFirstBit(11, 0));

        }
        [TestMethod]
        public void LFSR_Logic_Engine_Works()
        {
            LFSR lfsr = new LFSR();

            lfsr.Init("1011", "11001");

            string result = lfsr.GetOutput("", -1);

            Assert.AreEqual("0110", result);

            lfsr.Init("11101", "110111");
            result = lfsr.GetOutput("", -1);
            Assert.AreEqual("11010", result);

        }
    }
}
