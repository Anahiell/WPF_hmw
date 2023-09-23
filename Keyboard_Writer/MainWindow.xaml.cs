using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Threading;


namespace Keyboard_Writer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Button> KeyboardButtons { get; set; }
        private StringBuilder typedText = new StringBuilder();
        private DispatcherTimer timer; // для таймера
        private int errorCount; // для счетчика ошибок
        private DateTime startTime; // для расчета скорости печати
        private bool trainingMode = false; // флаг режима тренировки
        private DateTime lastKeyPressTime;
        private List<double> keyPressIntervals = new List<double>();
        private double typingSpeed = 0.0;
        public MainWindow()
        {
            InitializeComponent();
            KeyboardButtons = new ObservableCollection<Button>();
            FindButtonsInGrid(grid1);

            // Инициализация таймера
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;

            // Начальные значения
            TypingSpeedTextBlock.Text = "Typing Speed: 0 wpm";
            ErrorCountTextBlock.Text = "Errors: 0";
            TimerTextBlock.Text = "Timer: 0";
            TrainingModeCheckBox.IsChecked = false; //  режим тренировки выключен

        }
        private void UpdateTypingSpeed()
        {
            if (keyPressIntervals.Count > 0)
            {
                double totalInterval = keyPressIntervals.Sum();
                double averageInterval = totalInterval / keyPressIntervals.Count;

                if (averageInterval > 0)
                {
                    typingSpeed = 60.0 / averageInterval; // 60 секунд в минуте
                    TypingSpeedTextBlock.Text = $"Typing Speed: {typingSpeed:F2} wpm";
                }
            }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeSpan elapsedTime = DateTime.Now - startTime;
            TimerTextBlock.Text = $"Timer: {elapsedTime:mm\\:ss}"; 

            // Обновление скорости печатания
           UpdateTypingSpeed();
        }
        private void FindButtonsInGrid(DependencyObject container)
        {
            int count = VisualTreeHelper.GetChildrenCount(container);
            for (int i = 0; i < count; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(container, i);

                if (child is Button button)
                {
                    KeyboardButtons.Add(button);
                }
                FindButtonsInGrid(child);
            }
        }
        private void KeyboardButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (sender is Button button)
            {
                char keyPressed = button.Content.ToString()[0];

                typedText.Append(keyPressed);

                TextRow.Text = typedText.ToString();
            }
        }
        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            DateTime currentKeyPressTime = DateTime.Now;

            if (lastKeyPressTime != DateTime.MinValue)
            {
                double interval = (currentKeyPressTime - lastKeyPressTime).TotalSeconds;
                keyPressIntervals.Add(interval);
            }

            lastKeyPressTime = currentKeyPressTime;
            HandleKeyPress(e.Key);
        }

        private void HandleKeyPress(Key key)
        {
            // обработка спец клавиш
            switch (key)
            {
                case Key.Back:
                    if (typedText.Length > 0)
                    {
                        typedText.Remove(typedText.Length - 1, 1);
                        TextRow.Text = typedText.ToString();
                    }
                    break;
                case Key.Space:
                    typedText.Append(" ");
                    break;
                case Key.LeftAlt:
                case Key.RightAlt:
                    // обработка альт
                    break;
                case Key.LeftCtrl:
                case Key.RightCtrl:
                    //обработка ктр
                    break;
            }
            Button targetButton = KeyboardButtons.FirstOrDefault(button => button.Content.ToString() == key.ToString().ToLower());

            //если найдена кнопка, вызываем для неё событие Click
            if (targetButton != null)
            {
                RoutedEventArgs newEventArgs = new RoutedEventArgs(Button.ClickEvent, targetButton);
                targetButton.RaiseEvent(newEventArgs);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (timer.IsEnabled)
            {
                timer.Stop();
                StopButton.IsEnabled = false; // задизейблим кнопку "Stop"
            }
            else
            {
                timer.Start();
                startTime = DateTime.Now;
                StopButton.IsEnabled = true; // активируем кнопку "Stop"
            }
        }
        private void TrainingModeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            trainingMode = TrainingModeCheckBox.IsChecked ?? false;

            //  останавливаем таймер
            if (trainingMode)
            {
                timer.Stop();
            }
            else
            {
                // возобновляем таймер
                timer.Start();
                startTime = DateTime.Now;
            }
        }
    }
}
