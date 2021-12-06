using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CalcLibrary;

namespace Calculator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _isSignOn;  // знак нажат
        private bool _isResult;
        private string _firstNumber;
        private string _sign; // знак
        public MainWindow()
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        }

        private void Button_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button button)) return;
            var content = (string)button.Content;
            switch (content)
            {
                case "Clear":
                    Clear();
                    return;
                case "√":
                    SqrtAsync();
                    return;
                case "+": case "-": case "/": case "*":
                    AddSign(content);
                    return;
                case "=":
                    СalculationAsync();
                    return;
                case ",":
                    if (!MainText.Text.Contains(',')) MainText.Text += content;
                    return;
                case "(": case ")":
                    MessageBox.Show("В процессе разработки");
                    _isSignOn = true;
                    return;
            }
            if (MainText.Text == "0" || _isSignOn || _isResult)
            {
                MainText.Text = content;
                _isSignOn = false;
                _isResult = false;
            }
            else MainText.Text += content;
        }

        private void Clear()
        {
            OpacityText.Text = "";
            MainText.Text = "0";
            _isSignOn = false;
            _firstNumber = null;
            _sign = null;
        }

        private void AddSign(string content)
        {
            _firstNumber = MainText.Text;
            _sign = content;
            OpacityText.Text = MainText.Text + " " + content;
            _isSignOn = true;
        }

        private async void SqrtAsync()
        {
            double num = double.Parse(MainText.Text.Replace(',', '.'));
            if (num < 0)
            {
                MessageBox.Show("Неверный ввод!");
                return;
            }
            GridCalc.Visibility = Visibility.Visible;
            
            MainText.Text = (await AsyncCalc.Sqrt(num)).ToString();
            GridCalc.Visibility = Visibility.Collapsed;
        }
        private async void СalculationAsync()
        {
            var num1 = double.Parse(_firstNumber.Replace(',','.'));
            var num2 = double.Parse(MainText.Text.Replace(',', '.'));
            GridCalc.Visibility = Visibility.Visible;
            double value;
            switch (_sign)
            {
                case "+":
                    value = await AsyncCalc.Sum(num1, num2);
                    break;
                case "-":
                    value = await AsyncCalc.Diff(num1, num2);
                    break;
                case "/":
                    if ((int)num2 < 0)
                    {
                        GridCalc.Visibility = Visibility.Collapsed;
                        MessageBox.Show("Деление на 0 невозможно");
                        return;
                    }
                    value = await AsyncCalc.Divide(num1, num2);
                    break;
                case "*":
                    value = await AsyncCalc.Multiply(num1, num2);
                    break;
                default:
                    throw new Exception("Неверный знак");
            }
            MainText.Text = value.ToString();
            GridCalc.Visibility = Visibility.Collapsed;
            _isResult = true;
        }
    }

}
