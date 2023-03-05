using System;
using System.Windows.Controls;
using System.Windows.Data;

namespace ProductionAccounting.Views
{
    /// <summary>
    /// Логика взаимодействия для CalculateSalaryView.xaml
    /// </summary>
    public partial class CalculateSalaryView : UserControl
    {
        public CalculateSalaryView()
        {
            InitializeComponent();
        }

        //private void CB_Employee_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    var tb = (TextBox)e.OriginalSource;
        //    if (tb.SelectionStart != 0)
        //    {
        //        CB_Employee.SelectedItem = null;
        //    }
        //    if (tb.SelectionStart == 0 && CB_Employee.SelectedItem == null)
        //    {
        //        CB_Employee.IsDropDownOpen = false;
        //    }

        //    CB_Employee.IsDropDownOpen = true;
        //    if (CB_Employee.SelectedItem == null)
        //    {
        //        CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(CB_Employee.ItemsSource);
        //        cv.Filter = s => (s.ToString()).IndexOf(CB_Employee.Text, StringComparison.CurrentCultureIgnoreCase) >= 0;
        //    }
        //}
    }
}
