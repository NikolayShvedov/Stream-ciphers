using Microsoft.Win32;
using StreamCiphers_Logic;
using System.Windows;
using System.Windows.Controls;

namespace StreamCiphers
{
    public partial class MainWindow : Window
    {
        LFSR lfsr = new LFSR();
        SynchronousStream synchronous = new SynchronousStream();
        ICipher _cipher;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void generator_checked(object sender, RoutedEventArgs e)
        {
            fileTB.IsEnabled = false;
            openFileBTN.IsEnabled = false;
            modeGB.IsEnabled = false;
            wayGB.IsEnabled = false;

            _cipher = lfsr;
        }

        private void way_checked(object sender, RoutedEventArgs e)
        {
            var _radiobutton = sender as RadioButton;
            if (_radiobutton.Name == "text")
            {
                fileTB.IsEnabled = false;
                openFileBTN.IsEnabled = false;
                polynomialTB.IsEnabled = true;
            }
            else
            {
                fileTB.IsEnabled = true;
                openFileBTN.IsEnabled = true;
                polynomialTB.IsEnabled = false;
            }
        }

        private void encryption_checked(object sender, RoutedEventArgs e)
        {
            fileTB.IsEnabled = true;
            openFileBTN.IsEnabled = true;

            var _radiobutton = sender as RadioButton;
            if (_radiobutton.Name == "ex2")
            {
                wayGB.IsEnabled = true;
                modeGB.IsEnabled = true;
                fileTB.IsEnabled = false;
                openFileBTN.IsEnabled = false;
                _cipher = synchronous;
            }
            else
            {
                wayGB.IsEnabled = false;
                modeGB.IsEnabled = false;
                fileTB.IsEnabled = false;
                openFileBTN.IsEnabled = false;

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ex1.IsChecked = true;
            text.IsChecked = true;
            encrypt.IsChecked = true;
            fileTB.IsEnabled = false;
            wayGB.IsEnabled = false;
            openFileBTN.IsEnabled = false;

        }

        private void runButton_Click(object sender, RoutedEventArgs e)
        {
            var _seed = seedTB.Text;
            var _polynomial = polynomialTB.Text;
            var _fileName = fileTB.Text;
            int _mode = 0;
            if (encrypt.IsChecked == true) _mode = 0;
            if (decrypt.IsChecked == true) _mode = 1;

            int _way = 0;
            if (text.IsChecked == true) _way = 0;
            if (file.IsChecked == true) _way = 1;

            _cipher.Init(_seed, _polynomial);
            var result = _cipher.GetOutput(_fileName, _mode, _way);
            outputTB.Text = result;
        }

        private void openFileBTN_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == true)
            {
                fileTB.Text = fileDialog.FileName;
            }
        }
    }
}
