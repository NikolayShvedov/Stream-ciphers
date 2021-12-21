using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StreamCiphers_Logic;

namespace StreamCiphers_Tests
{
    [TestClass]
    public class AutokeyCiphertext_Tests
    {
        [TestMethod]
        public void AutokeyCiphertext_Logic_Engine_Works()
        {
            AutokeyCiphertext autokey = new AutokeyCiphertext();

            //autokey.Init("1011", "1001", "1100");
            autokey.Init("1011", "11001");
            List<string> streams = new List<string>();
            streams.Add("1100");
            autokey.Bytes = streams;
            //autokey.Encrypt();
            string result = autokey.GetOutput("", 0);

            Assert.AreEqual("1110", result);
        }

        [TestMethod]
        public void AutokeyCiphertext_Logic_Engine_Works_2()
        {
            AutokeyCiphertext autokey = new AutokeyCiphertext();

            //autokey.Init("1011", "1001", "11001101");
            autokey.Init("1011", "11001");
            List<string> streams = new List<string>();
            streams.Add("11001101");
            autokey.Bytes = streams;
            //autokey.Encrypt();
            string result = autokey.GetOutput("", 0);

            Assert.AreEqual("11100010", result);
        }

        [TestMethod]
        public void AutokeyCiphertext_Logic_Engine_Works_3()
        {
            AutokeyCiphertext autokey = new AutokeyCiphertext();

            //autokey.Init("1110", "0101", "10011101");
            autokey.Init("11101010", "110100011");
            List<string> streams = new List<string>();
            streams.Add("10011101");
            autokey.Bytes = streams;
            //autokey.Encrypt();
            string result = autokey.GetOutput("", 0);

            Assert.AreEqual("00000111", result);
        }

        [TestMethod]
        public void AutokeyCiphertext_Logic_Engine_Works_4()
        {
            AutokeyCiphertext autokey = new AutokeyCiphertext();

            //autokey.Init("0010", "1011", "1111");
            autokey.Init("11101010", "110100011");
            List<string> streams = new List<string>();
            streams.Add("01110010");
            autokey.Bytes = streams;
            //autokey.Encrypt();
            string result = autokey.GetOutput("", 0);

            Assert.AreEqual("10101010", result);
        }

        [TestMethod]
        public void AutokeyCiphertext_Logic_Engine_Works_5()
        {
            AutokeyCiphertext autokey = new AutokeyCiphertext();

            //autokey.Init("0010", "1011", "1111");
            autokey.Init("11101010", "110100011");
            List<string> streams = new List<string>();
            streams.Add("10011101");
            streams.Add("01110010");
            autokey.Bytes = streams;
            string result = autokey.GetOutput("", 0);

            Assert.AreEqual("0000011110101010", result);
        }

        [TestMethod]
        public void AutokeyCiphertext_Logic_Engine_Works_6()
        {
            AutokeyCiphertext autokey = new AutokeyCiphertext();

            //autokey.Init("1011", "1001", "1100");
            autokey.Init("1011", "11001");
            List<string> streams = new List<string>();
            streams.Add("1110");
            autokey.Bytes = streams;
            //autokey.Encrypt();
            string result = autokey.GetOutput("", 1);

            Assert.AreEqual("1100", result);
        }
        [TestMethod]
        public void AutokeyCiphertext_Logic_Engine_Works_7()
        {
            AutokeyCiphertext autokey = new AutokeyCiphertext();

            //autokey.Init("1011", "1001", "11001101");
            autokey.Init("1011", "11001");
            List<string> streams = new List<string>();
            streams.Add("11100010");
            autokey.Bytes = streams;
            //autokey.Encrypt();
            string result = autokey.GetOutput("", 1);
            
            Assert.AreEqual("11001101", result);
        }

        [TestMethod]
        public void AutokeyCiphertext_Logic_Engine_Works_8()
        {
            AutokeyCiphertext autokey = new AutokeyCiphertext();

            //autokey.Init("1110", "0101", "10011101");
            autokey.Init("11101010", "110100011");
            List<string> streams = new List<string>();
            streams.Add("00000111");
            autokey.Bytes = streams;
            //autokey.Encrypt();
            string result = autokey.GetOutput("", 1);
            
            Assert.AreEqual("10011101", result);
        }

        [TestMethod]
        public void AutokeyCiphertext_Logic_Engine_Works_9()
        {
            AutokeyCiphertext autokey = new AutokeyCiphertext();

            //autokey.Init("0010", "1011", "1111");
            autokey.Init("11101010", "110100011");
            List<string> streams = new List<string>();
            streams.Add("10101010");
            autokey.Bytes = streams;
            //autokey.Encrypt();
            string result = autokey.GetOutput("", 1);
            
            Assert.AreEqual("01110010", result);
        }

        [TestMethod]
        public void AutokeyCiphertext_Logic_Engine_Works_10()
        {
            AutokeyCiphertext autokey = new AutokeyCiphertext();

            //autokey.Init("0010", "1011", "1111");
            autokey.Init("11101010", "110100011");
            List<string> streams = new List<string>();
            streams.Add("00000111");
            streams.Add("10101010");
            autokey.Bytes = streams;
            string result = autokey.GetOutput("", 1);
            
            Assert.AreEqual("1001110101110010", result);
        }

        [TestMethod]
        public void AutokeyCiphertext_Output_File_Name()
        {
            AutokeyCiphertext autokey = new AutokeyCiphertext();

            string outputFileName = autokey.GetOutputFileName("H:\\Downloads\\test3.bin");

            Assert.AreEqual("H:\\Downloads\\test3_out.bin", outputFileName);
        }

        /*
        [TestMethod]
        public void AutokeyCiphertext_Logic_Engine_Works_File()
        {
            AutokeyCiphertext autokey = new AutokeyCiphertext();

            //autokey.Init("1110", "0101", "10011101");
            autokey.Init("11101010", "10100011");
            autokey.ReadBytesFromFile("test3.bin");
            autokey.Encrypt();
            autokey.WriteBytesToFile("output3.bin");
            string result = autokey.GetOutput();

            Assert.AreEqual("00100110", result);
        }
        */
    }
}
