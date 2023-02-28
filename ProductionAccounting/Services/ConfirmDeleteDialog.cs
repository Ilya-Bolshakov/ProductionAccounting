using ProductionAccounting.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProductionAccounting.Services
{
    public class ConfirmDeleteDialog : IConfirmDeleteDialog
    {
        public bool ShowConfirmDeleteDialog(string confirmMessage)
        {
            var dialog = MessageBox.Show(confirmMessage, "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            return dialog == MessageBoxResult.Yes;
        }
    }
}
