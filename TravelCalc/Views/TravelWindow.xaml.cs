using System;
using System.Drawing;
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
using TravelCalc.Views;
using TravelCalcBL;
using Themes = Xceed.Wpf.Toolkit.Themes;

namespace TravelCalc.Views
{
    /// <summary>
    /// Логика взаимодействия для CreateTravelWindow.xaml
    /// </summary>
    public partial class TravelWindow : Window
    {
        public TravelWindow(TravelList travelList)
        {
            InitializeComponent();
            TravelViewModel vm = new TravelViewModel(travelList);
            DataContext = vm;
            if (vm.CloseAction == null)
            {
                vm.CloseAction = new Action(this.Close);
            }
        }

        private void AddTransfer_Click(object sender, RoutedEventArgs e)
        {
            TransferWindow window = new TransferWindow(Convert.ToInt32(travelersCount.Text));
            window.Owner = this;
            window.ShowDialog();
            ((TravelViewModel)this.DataContext).OnPropertyChanged("Travel");
        }

        private void AddAccommandation_Click(object sender, RoutedEventArgs e)
        {
            AccommodationWindow window = new AccommodationWindow(Convert.ToInt32(travelersCount.Text));
            window.Owner = this;
            window.ShowDialog();
            ((TravelViewModel)this.DataContext).OnPropertyChanged("Travel");

        }

        private void EditTravelActButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            TravelAct act = button.DataContext as TravelAct;
            if (act.TravelActType == TravelActType.Transfer)
            {
                TransferWindow window = new TransferWindow(Convert.ToInt32(travelersCount.Text), act.Id);
                window.Owner = this;
                window.ShowDialog();
            }
            else
            {
                AccommodationWindow window = new AccommodationWindow(Convert.ToInt32(travelersCount.Text), act.Id);
                window.Owner = this;
                window.ShowDialog();
            }
            ((TravelViewModel)this.DataContext).OnPropertyChanged("Travel");
        }
    }
}
