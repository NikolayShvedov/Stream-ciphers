using System;

namespace StreamCiphers_Logic
{
    public class LFSR : ICipher
    {
        public string Polynomial { get; set; }
        public string Seed { get; set; }

        public void Init(string _seed, string _polynomial)
        {
            Polynomial = _polynomial;
            Seed = _seed;
        }
        public int XORBits(int _base)
        {
            string _baseString = Convert.ToString(_base, 2);
            _baseString = _baseString.PadLeft(Polynomial.Length, '0');
            int bit = -1;
            for (int j = 0; j < Polynomial.Length; j++)
            {
                if (Polynomial[j] == '1')
                {
                    if (bit == -1)
                    {
                        bit = Convert.ToInt32(_baseString[j]);
                    }
                    else
                    {
                        int _currentBitValue = Convert.ToInt32(_baseString[j]);
                        bit = (bit ^ _currentBitValue);
                    }

                }
            }
            return (bit & 1);
        }
        public int ReplaceFirstBit(int _base, int bit)
        {
            string _baseString = Convert.ToString(_base, 2);

            _baseString = _baseString.PadLeft(Seed.Length, '0');
            _baseString = bit.ToString() + _baseString.Substring(1);

            return Convert.ToInt32(_baseString, 2);
        }
        public int Shift(int _base)
        {
            return (_base >> 1);
        }
        public string GetOutput(string _fileName, int _mode, int way)
        {
            string result = "";
            string ciphered = Seed;
            for (int i = 0; i < Seed.Length; i++)
            {
                int cipheredInt = Convert.ToInt32(ciphered, 2);

                int bit = XORBits(cipheredInt);

                cipheredInt = Shift(cipheredInt);

                cipheredInt = ReplaceFirstBit(cipheredInt, bit);

                ciphered = Convert.ToString(cipheredInt, 2).PadLeft(Seed.Length, '0');
                result += ciphered + "\n";
            }

            return result;
        }

        public void ReadFile(string _fileName)
        {

        }
    }
}
