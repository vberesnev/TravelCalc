using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TravelCalcBL;

namespace TravelCalc.ViewModel
{
    class TravelViewModel : INotifyPropertyChanged
    {
        private RelayCommand saveTravelCommand;
        public RelayCommand SaveTravelCommand
        {
            get
            {
                return saveTravelCommand ??
                  (saveTravelCommand = new RelayCommand(obj =>
                  {
                      travelList.AddTravel(Travel);
                      CloseAction();
                  }));
            }
        }

        private RelayCommand cancelCommand;
        public RelayCommand CancelCommand
        {
            get
            {
                return cancelCommand ??
                  (cancelCommand = new RelayCommand(obj =>
                  {
                      CloseAction();
                  }));
            }
        }

        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                  (removeCommand = new RelayCommand(obj =>
                  {
                      TravelAct act = obj as TravelAct;
                      if (act != null)
                      {
                          Travel.DeleteTravelAct(act);
                          OnPropertyChanged("Travel");
                      }
                  }));
            }
        }

        private RelayCommand updateTravelActsCommand;
        public RelayCommand UpdateTravelActsCommand
        {
            get
            {
                return updateTravelActsCommand ??
                  (updateTravelActsCommand = new RelayCommand(obj =>
                  {
                      int count = Convert.ToInt32(obj);
                      Travel.People = count;
                      Travel.UpdateTravels();
                      OnPropertyChanged("Travel");
                  }));
            }
        }

        private Travel travel;
        public Travel Travel
        {
            get { return travel; }
            set
            {
                travel = value;
                OnPropertyChanged("Travel");
            }
        }

        /// <summary>
        /// Делегат для закрытия окна (метод закрытия привязывается к нему в AccommodationWindow.xaml.cs)
        /// </summary>
        public Action CloseAction { get; set; }

        private TravelList travelList;

        public TravelViewModel(TravelList travelList)
        {
            this.travelList = travelList;
            Travel = TravelList.CurrentTravel;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
