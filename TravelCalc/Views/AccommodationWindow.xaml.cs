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
    /// Логика взаимодействия для AccommodationWindow.xaml
    /// </summary>
    public partial class AccommodationWindow : Window
    {
        public AccommodationWindow(int travelersCount)
        {
            InitializeComponent();
            AccommodationViewModel vm = new AccommodationViewModel(travelersCount);
            this.DataContext = vm;
            if (vm.CloseAction == null)
                vm.CloseAction = new Action(this.Close);
        }

        public AccommodationWindow(int travelersCount, string guid)
        {
            InitializeComponent();
            AccommodationViewModel vm = new AccommodationViewModel(travelersCount, guid);
            this.DataContext = vm;
            if (vm.CloseAction == null)
                vm.CloseAction = new Action(this.Close);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
