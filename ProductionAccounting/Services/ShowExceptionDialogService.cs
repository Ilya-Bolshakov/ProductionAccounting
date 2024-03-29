﻿using ProductionAccounting.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProductionAccounting.Services
{
    public class ShowExceptionDialogService : IShowExceptionDialogService
    {
        public void ShowDialog(string message, string exceptionMessage)
        {

            var dialog = MessageBox.Show(message, "Ошибка", MessageBoxButton.YesNo, MessageBoxImage.Error);
            if (dialog == MessageBoxResult.Yes)
            {
                MessageBox.Show(exceptionMessage);
            }
        }
    }
}