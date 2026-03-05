using System;
using System.Windows;
using System.Windows.Threading;

namespace TypingTrainer
{
    // Цель работы: Вариант 6. Тренажер скорописи.
    // Версия 1: Базовый функционал (текст, ввод, подсчет скорости).
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        private DateTime startTime;
        private bool isRunning = false;
        private string text = "Съешь ещё этих мягких французских булок, да выпей чаю.";

        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
        }

        private void Button_Start_Click(object sender, RoutedEventArgs e)
        {
            if (!isRunning)
            {
                TextBlock_Task.Text = text;
                TextBox_Input.Text = "";
                TextBox_Input.IsEnabled = true;
                TextBox_Input.Focus();
                startTime = DateTime.Now;
                timer.Start();
                isRunning = true;
                Button_Start.Content = "Стоп";
            }
            else
            {
                FinishTest();
            }
        }

        private void Button_Reset_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            isRunning = false;
            TextBox_Input.IsEnabled = false;
            TextBox_Input.Text = "";
            TextBlock_Task.Text = "Нажмите Старт";
            TextBlock_Result.Text = "Скорость: 0 зн/мин";
            Button_Start.Content = "Старт";
        }

        private void FinishTest()
        {
            timer.Stop();
            isRunning = false;
            TextBox_Input.IsEnabled = false;
            Button_Start.Content = "Старт";

            TimeSpan timeSpan = DateTime.Now - startTime;
            double minutes = timeSpan.TotalMinutes;
            if (minutes > 0)
            {
                int chars = TextBox_Input.Text.Length;
                int wpm = (int)(chars / minutes);
                TextBlock_Result.Text = $"Скорость: {wpm} зн/мин";
            }
        }
    }
}