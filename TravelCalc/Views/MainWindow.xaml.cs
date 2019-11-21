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
using TravelCalc.ViewModel;
using TravelCalc.Views;
using TravelCalcBL;

namespace TravelCalc
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainViewModel vm = new MainViewModel();
            this.DataContext = vm;
            if (vm.OpenWindowAction == null)
                vm.OpenWindowAction = new Action<TravelList>(ShowTravelWindow);
        }

        private void ShowTravelWindow(TravelList travelList)
        {
            TravelWindow window = new TravelWindow(travelList);
            window.Owner = this;
            this.Hide();
            window.ShowDialog();
            this.Show();
        }

        private void InformationButton_Click(object sender, RoutedEventArgs e)
        {
            InformationWindow IW = new InformationWindow();
            IW.Owner = this;
            this.Hide();
            IW.ShowDialog();
            this.Show();
        }
    }
}
