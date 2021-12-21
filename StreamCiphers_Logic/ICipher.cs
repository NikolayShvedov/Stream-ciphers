namespace StreamCiphers_Logic
{
    public interface ICipher
    {
        void Init(string _seed, string _polynomial);
        string GetOutput(string _fileName, int _mode, int _way);
    }
}
