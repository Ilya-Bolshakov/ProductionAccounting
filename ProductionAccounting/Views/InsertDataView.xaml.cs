using System;
using System.Windows.Controls;
using System.Windows.Data;

namespace ProductionAccounting.Views
{
    /// <summary>
    /// Логика взаимодействия для InsertDataView.xaml
    /// </summary>
    public partial class InsertDataView : UserControl
    {
        public InsertDataView()
        {
            InitializeComponent();
        }

        private void ComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)e.OriginalSource;
            if (tb.SelectionStart != 0)
            {
                CB.SelectedItem = null;
            }
            if (tb.SelectionStart == 0 && CB.SelectedItem == null)
            {
                CB.IsDropDownOpen = false; 
            }

            CB.IsDropDownOpen = true;
            if (CB.SelectedItem == null)
            {
                CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(CB.ItemsSource);
                cv.Filter = s => (s.ToString()).IndexOf(CB.Text, StringComparison.CurrentCultureIgnoreCase) >= 0;
            }
        }

        private void ComboBoxEmployees_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)e.OriginalSource;
            if (tb.SelectionStart != 0)
            {
                CB_Employee.SelectedItem = null;
            }
            if (tb.SelectionStart == 0 && CB_Employee.SelectedItem == null)
            {
                CB_Employee.IsDropDownOpen = false;
            }

            CB_Employee.IsDropDownOpen = true;
            if (CB_Employee.SelectedItem == null)
            {
                CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(CB_Employee.ItemsSource);
                cv.Filter = s => (s.ToString()).IndexOf(CB_Employee.Text, StringComparison.CurrentCultureIgnoreCase) >= 0;
            }
        }
    }
}
