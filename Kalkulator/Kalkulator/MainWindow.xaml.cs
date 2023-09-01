using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kalkulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private double currentNumber = 0;
        private string currentOperator = "";

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string buttonText = button.Content.ToString();

            if (double.TryParse(buttonText, out double number))
            {
                TextBlockCurrent.Text += buttonText;
            }
            else
            {
                switch (buttonText)
                {
                    case "+":
                    case "-":
                    case "*":
                    case "/":
                        // если уже есть текущий оператор  выполн предыдущую операцию
                        if (!string.IsNullOrEmpty(currentOperator))
                        {
                            try
                            {
                                string[] parts = TextBlockCurrent.Text.Split(new char[] { '+', '-', '*', '/' });
                                if (parts.Length == 2)
                                {
                                    double num2 = double.Parse(parts[1]);
                                    double result = DoCalculation(currentNumber, num2, currentOperator);
                                    TextBlockCurrent.Text = result.ToString();
                                }
                                else
                                {
                                    MessageBox.Show("Ошибка: Некорректный формат ввода.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                    return;
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                        }
                        // сохранение текущего числа и оператора
                        currentNumber = double.Parse(TextBlockCurrent.Text);
                        currentOperator = buttonText;
                        TextBlockCurrent.Text += buttonText; // добавляем оператор в TextBlockCurrent
                        break;
                    case "=":
                        try
                        {
                            string[] parts = TextBlockCurrent.Text.Split(new char[] { '+', '-', '*', '/' });
                            if (parts.Length == 2)
                            {
                                double num2 = double.Parse(parts[1]);
                                double result = DoCalculation(currentNumber, num2, currentOperator);
                                TextBlockResult.Text = result.ToString();
                            }
                            else
                            {
                                MessageBox.Show("Ошибка: Некорректный формат ввода.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        break;
                    case "C":
                        // сброс
                        currentNumber = 0;
                        currentOperator = "";
                        TextBlockCurrent.Text = "";
                        TextBlockResult.Text = "";
                        break;
                }
            }
        }
        private double DoCalculation(double num1, double num2, string operatorStr)
        {
            switch (operatorStr)
            {
                case "+":
                    return num1 + num2;
                case "-":
                    return num1 - num2;
                case "*":
                    return num1 * num2;
                case "/":
                    if (num2 != 0)
                    {
                        return num1 / num2;
                    }
                    else
                    {
                        MessageBox.Show("Ошибка: Деление на ноль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return double.NaN; // Обработка ошибки
                    }
                default:
                    return num2;
            }

        }
      
    }
}
