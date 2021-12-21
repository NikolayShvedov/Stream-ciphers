using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace StreamCiphers_Logic
{
    public class SynchronousStream : ICipher
    {
        private LFSR _lfsr;
        private List<string> _output;
        public List<string> Bytes { get; set; }

        public SynchronousStream()
        {
            _lfsr = new LFSR();
            Bytes = new List<string>();
            _output = new List<string>();
        }
        public string Polynomial { get; set; }
        public string Seed { get; set; }
        public IEnumerable<byte> CipherText { get; set; }

        public string GetOutput(string _fileName, int _mode, int _way)
        {
            if (_way == 0)
            {
                if (_mode == 0)
                {
                    var encoding = Encoding.GetEncoding(1251);

                    var plainText = encoding.GetBytes(Polynomial);
                    var key = encoding.GetBytes(Seed);

                    var nonce = new byte[4];
                    using (var randomGenerator = new RNGCryptoServiceProvider())
                        randomGenerator.GetBytes(nonce);

                    var cipherText =
                    nonce.Concat(
                        XorCounterModeEncryptDecrypt(key, nonce, plainText)
                    );
                    CipherText = cipherText;
                    return Encoding.Default.GetString(cipherText.ToArray());
                }
                else
                {
                    var encoding = Encoding.GetEncoding(1251);
                    var key = encoding.GetBytes(Seed);

                    var decrypted = XorCounterModeEncryptDecrypt(
                    keyBytes: key,
                    nonceBytes: CipherText.Take(4).ToArray(),
                    data: CipherText.Skip(4));
                    var decryptedStr = encoding.GetString(decrypted.ToArray());
                    return decryptedStr;
                }
            }
            else
            {
                string _lfsrResults = _lfsr.GetOutput(null, 0, 0);
                string[] _lfsrResultTokens = _lfsrResults.Split('\n');
                string _lfsrResult = "";
                for (int i = 0; i < _lfsrResultTokens.Length; i++)
                {
                    _lfsrResult += _lfsrResultTokens[0];
                }
                string _result = "";
                ReadBytesFromFile(_fileName);
                int pos = 0;
                foreach (string input in Bytes)
                {
                    _result = "";
                    for (int i = 0; i < 8; i++)
                    {
                        _result += (Convert.ToInt32(input[i]) ^ Convert.ToInt32(_lfsrResult[pos++]));
                        if (pos == _lfsrResult.Length)
                        {
                            pos = 0;
                        }
                    }
                    _output.Add(_result);
                }

                WriteBytesToFile(GetOutputFileName(_fileName));
                return _result;
            }
        }
        public void ReadBytesFromFile(string fileName)
        {
            byte[] fileBytes = File.ReadAllBytes(fileName);

            foreach (byte b in fileBytes)
            {
                Bytes.Add(Convert.ToString(b, 2).PadLeft(8, '0'));
            }
        }

        public void Init(string _seed, string _polynomial)
        {
            Seed = _seed;
            Polynomial = _polynomial;
            Bytes.Clear();
            _lfsr.Init(Seed, Polynomial);
            _output.Clear();

        }
        private byte[] GetOutputBytes()
        {
            List<byte> byteList = new List<byte>();

            foreach (string stream in _output)
            {
                byteList.Add(Convert.ToByte(stream, 2));
            }
            return byteList.ToArray();
        }
        public string GetOutputFileName(string inputFileName)
        {
            string[] parts = inputFileName.Split('.');
            parts[parts.Count() - 2] += "_out";

            return String.Join(".", parts);
        }

        public void WriteBytesToFile(string filename)
        {
            File.WriteAllBytes(filename, GetOutputBytes());
        }

        private static IEnumerable<byte> XorCounterModeEncryptDecrypt(byte[] keyBytes, byte[] nonceBytes, IEnumerable<byte> data)
        {
            if (keyBytes == null) throw new ArgumentNullException(nameof(keyBytes));
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (nonceBytes == null) throw new ArgumentNullException(nameof(nonceBytes));
            if (nonceBytes.Length < 4) throw new ArgumentOutOfRangeException(nameof(nonceBytes));

            int roundIndex = 0;
            byte[] roundGamma = null;
            int gammaIndex = 0;
            foreach (var d in data)
            {
                if (gammaIndex == 0)
                {
                    // create gamma

                    // create counter block: Nonce + Counter
                    // another way: Nonce XOR Counter (has some constraints)
                    var counterBlock = nonceBytes.Concat(BitConverter.GetBytes(roundIndex)).ToArray();
                    using (var hmacSHA = new HMACSHA512(keyBytes))
                        roundGamma = hmacSHA.ComputeHash(counterBlock);

                }

                yield return (byte)(d ^ roundGamma[gammaIndex]);

                if (gammaIndex < roundGamma.Length - 1)
                    gammaIndex++;
                else
                {
                    gammaIndex = 0;
                    roundIndex++;
                }
            } // foreach
        }
    }
}
