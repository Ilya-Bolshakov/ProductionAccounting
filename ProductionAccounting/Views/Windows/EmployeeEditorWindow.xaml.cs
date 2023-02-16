﻿using System.Windows;

namespace ProductionAccounting.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для EmployeeEditorWindow.xaml
    /// </summary>
    public partial class EmployeeEditorWindow : Window
    {
        public EmployeeEditorWindow()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            App.CurrentWindow.Close();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
                DragMove();
        }
    }
}
