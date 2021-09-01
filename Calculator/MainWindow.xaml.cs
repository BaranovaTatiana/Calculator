using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
        }

        private void Button_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button button)) return;
            var content = (string)button.Content;
            switch (content)
            {
                case "Clear":
                    OpacityText.Text = "";
                    MainText.Text = "0";
                    _isSignOn = false;
                    _firstNumber = null;
                    _sign = null;
                    return;
                case "+":
                case "-":
                case "/":
                case "*":
                    _firstNumber = MainText.Text;
                    _sign = content;
                    OpacityText.Text = MainText.Text + " " + content;
                    _isSignOn = true;
                    return;
                case "=":
                    {
                        var num1 = Convert.ToDouble(_firstNumber);
                        var num2 = Convert.ToDouble(MainText.Text);
                        var value = Calc(num1, num2);
                        MainText.Text = value.ToString();
                        _isResult = true;
                        return;
                    }
                case ",":
                    if (!MainText.Text.Contains(',')) MainText.Text += content;
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

        private double Calc(double num1, double num2)
        {
            var value = 0d;
            switch (_sign)
            {
                case "+":
                    value = num1 + num2;
                    break;
                case "-":
                    value = num1 - num2;
                    break;
                case "/":
                    value = num1 / num2;
                    break;
                case "*":
                    value = num1 * num2;
                    break;
            }
            return value;
        }
    }
}
