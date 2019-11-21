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
using System.Windows.Shapes;
using TravelCalc.ViewModel;

namespace TravelCalc.Views
{
    /// <summary>
    /// Логика взаимодействия для CreateTransferWindow.xaml
    /// </summary>
    public partial class TransferWindow : Window
    {
        public TransferWindow(int travelersCount)
        {
            InitializeComponent();
            TransferViewModel vm = new TransferViewModel(travelersCount);
            this.DataContext = vm;
            if (vm.CloseAction == null)
                vm.CloseAction = new Action(this.Close);
        }

        public TransferWindow(int travelersCount, string guid)
        {
            InitializeComponent();
            TransferViewModel vm = new TransferViewModel(travelersCount, guid);
            this.DataContext = vm;
            if (vm.CloseAction == null)
                vm.CloseAction = new Action(this.Close);
        }

        private void ChangeDepAndDest_Click(object sender, RoutedEventArgs e)
        {
            string temp = DepartureTextBox.Text;
            DepartureTextBox.Text = DestinationTextBox.Text;
            DestinationTextBox.Text = temp;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
