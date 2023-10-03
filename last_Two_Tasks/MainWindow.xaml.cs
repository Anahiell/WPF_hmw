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

namespace last_Two_Tasks
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

        private void ApplyCommandToSelection(Action<TextRange> action)
        {
            TextRange selectedText = new TextRange(richTextBox.Selection.Start, richTextBox.Selection.End);
            action(selectedText);
        }

        private void BoldButton_Click(object sender, RoutedEventArgs e)
        {
            ApplyCommandToSelection(selectedText => selectedText.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold));
        }

        private void ItalicButton_Click(object sender, RoutedEventArgs e)
        {
            ApplyCommandToSelection(selectedText => selectedText.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Italic));
        }

        private void UnderlineButton_Click(object sender, RoutedEventArgs e)
        {
            ApplyCommandToSelection(selectedText => selectedText.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline));
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ApplyCommandToSelection(selectedText =>
            {
                selectedText.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
                selectedText.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Normal);
                selectedText.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
                selectedText.ApplyPropertyValue(TextElement.FontSizeProperty, richTextBox.FontSize);
                selectedText.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Black);
            });
        }

        private void Font15Button_Click(object sender, RoutedEventArgs e)
        {
            ApplyCommandToSelection(selectedText => selectedText.ApplyPropertyValue(TextElement.FontSizeProperty, 15.0));
        }

        private void Font30Button_Click(object sender, RoutedEventArgs e)
        {
            ApplyCommandToSelection(selectedText => selectedText.ApplyPropertyValue(TextElement.FontSizeProperty, 30.0));
        }

        private void RedColorButton_Click(object sender, RoutedEventArgs e)
        {
            ApplyCommandToSelection(selectedText => selectedText.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Red)));
        }

        private void GreenColorButton_Click(object sender, RoutedEventArgs e)
        {
            ApplyCommandToSelection(selectedText => selectedText.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Green)));
        }

        private void BlueColorButton_Click(object sender, RoutedEventArgs e)
        {
            ApplyCommandToSelection(selectedText => selectedText.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Blue)));
        }
        private void Button_Theme(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton.Name == "Theme1")
            {
                // Применение стилей для Theme 1
                Application.Current.Resources.MergedDictionaries[0] = new ResourceDictionary { Source = new Uri("Theme/Theme1.xaml", UriKind.Relative) };
            }
            else if (clickedButton.Name == "Theme2")
            {
                // Применение стилей для Theme 2
                Application.Current.Resources.MergedDictionaries[0] = new ResourceDictionary { Source = new Uri("Theme/Theme2.xaml", UriKind.Relative) };
            }
            else if (clickedButton.Name == "Theme3")
            {
                // Применение стилей для Theme 3
                Application.Current.Resources.MergedDictionaries[0] = new ResourceDictionary { Source = new Uri("Theme/Theme3.xaml", UriKind.Relative) };
            }
        }

    }
}
