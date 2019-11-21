using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TravelCalcBL;

namespace TravelCalc.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        //Строка с файлом сохранения
        private string SAVE_PATH = Path.Combine(System.Environment.CurrentDirectory, "data.json");

        private RelayCommand removeTravelCommand;
        public RelayCommand RemoveTravelCommand
        {
            get
            {
                return removeTravelCommand ??
                  (removeTravelCommand = new RelayCommand(obj =>
                  {
                      Travel travel = obj as Travel;
                      if (travel != null)
                      {
                          TravelListItem.RemoveTravel(travel);
                          SetInfoTextZIndex();
                          Save();
                      }
                  }));
            }
        }


        private RelayCommand addTravelCommand;
        public RelayCommand AddTravelCommand
        {
            get
            {
                return addTravelCommand ??
                  (addTravelCommand = new RelayCommand(obj =>
                  {
                      OpenWindowAction(TravelListItem);
                      this.TravelListItem.SetCurrentTravel(new Travel());
                      SetInfoTextZIndex();
                      Save();
                  }));
            }
        }

        private RelayCommand editTravelCommand;
        public RelayCommand EditTravelCommand
        {
            get
            {
                return editTravelCommand ??
                  (editTravelCommand = new RelayCommand(obj =>
                  {
                      Travel travel = obj as Travel;
                      if (travel != null)
                      {
                          this.TravelListItem.SetCurrentTravel(travel);
                          OpenWindowAction(TravelListItem);
                          this.TravelListItem.SetCurrentTravel(new Travel());
                          ListOfTravel = null;
                          ListOfTravel = TravelListItem.TravelsList;
                          Save();
                      }
                  }));
            }
        }

        private TravelList travelListItem;
        public TravelList TravelListItem
        {
            get { return travelListItem; }
            set
            {
                travelListItem = value;
                OnPropertyChanged("TravelListItem");
            }
        }

        /// <summary>
        /// Делегат для открытия нового окна, метод открытия привязывается к нему в MainWindow.xaml.cs
        /// </summary>
        public Action<TravelList> OpenWindowAction { get; set; }

        private ObservableCollection<Travel> listOfTravel;
        public ObservableCollection<Travel> ListOfTravel
        {
            get { return listOfTravel; }
            set
            {
                listOfTravel = value;
                OnPropertyChanged("ListOfTravel");
            }
        }

        //Z индекс для строки "Нет путешествий"
        private int infoTextZIndex;
        public int InfoTextZIndex
        {
            get { return infoTextZIndex; }
            set
            {
                infoTextZIndex = value;
                OnPropertyChanged("InfoTextZIndex");
            }
        }

        /// <summary>
        /// Метод установки значения Z индекса строки "Нет путешествий" (если путешествие есть, строку вниз)
        /// </summary>
        private void SetInfoTextZIndex()
        {
            if (TravelListItem.TravelsList.Count > 0)
                InfoTextZIndex = 0;
            else
                InfoTextZIndex = 100;
        }

        public MainViewModel()
        {
            TravelListItem = new TravelList(SAVE_PATH);
            ListOfTravel = TravelListItem.TravelsList;
            SetInfoTextZIndex();
        }

        /// <summary>
        /// Сохранить список путешествий
        /// </summary>
        private void Save()
        {
            TravelListItem.Save(SAVE_PATH);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
