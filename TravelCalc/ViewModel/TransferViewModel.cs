using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TravelCalcBL;

namespace TravelCalc.ViewModel
{
    class TransferViewModel : INotifyPropertyChanged
    {

        private RelayCommand addTransferCommand;
        public RelayCommand AddTransferCommand
        {
            get
            {
                return addTransferCommand ??
                  (addTransferCommand = new RelayCommand(obj =>
                  {
                      if (CheckForm())
                      {
                          if (Transfer.Id == null)
                          {
                              TravelList.CurrentTravel.AddTransfer(Transfer);
                          }
                          else
                          {
                              TravelList.CurrentTravel.EditTransfer(Transfer);
                          }                          
                          CloseAction();
                      }
                  }));
            }
        }

        private Transfer transfer;
        public Transfer Transfer
        {
            get { return transfer; }
            set
            {
                transfer = value;
                OnPropertyChanged("Transfer");
            }
        }

        /// <summary>
        /// Словарь транспорта с картинками
        /// </summary>
        public Dictionary<string, string> TransportDict
        {
            get
            {
                return new Dictionary<string, string>
                {
                    ["/Resources/transport/airplane.png"] = "Самолет",
                    ["/Resources/transport/train.png"] = "Поезд",
                    ["/Resources/transport/bus.png"] = "Автобус",
                    ["/Resources/transport/taxi.png"] = "Такси",
                    ["/Resources/transport/car.png"] = "Автомобиль",
                    ["/Resources/transport/boat.png"] = "Корабль"
                };
            }
        }

        /// <summary>
        /// Делегат для закрытия окна (метод закрытия привязывается к нему в AccommodationWindow.xaml.cs)
        /// </summary>
        public Action CloseAction { get; set; }

        /// <summary>
        /// Если путешественников больше 1, появляется строка с возможностью выбора оплаты за одного или за всех сразу
        /// </summary>
        private int travelerCount;
        public int TravelerCount
        {
            get { return travelerCount; }
            set
            {
                travelerCount = value;
                OnPropertyChanged("TravelerCount");
            }
        }

        /// <summary>
        /// Метод для вывода строки для radioButton выбора оплаты за нескольких путешественников
        /// </summary>
        public string TravelerCountString => $"За {TravelerCount}-х чел.";


        /// <summary>
        /// Если путешественников больше 1, появляется строка с возможностью выбора оплаты за одного или за всех сразу
        /// </summary>
        public string RowHeight
        {
            get
            {
                if (TravelerCount > 1)
                    return "Auto";
                else
                    return "0";
            }
        }

        /// <summary>
        /// Конструктор для нового трансфера
        /// </summary>
        /// <param name="travelersCount">количество путешественников</param>
        public TransferViewModel(int travelersCount)
        {
            Transfer = new Transfer(travelersCount);
            Transfer.Begin = DateTime.Today;
            Transfer.Finish = DateTime.Today;
            TravelerCount = travelersCount;
        }

        /// <summary>
        /// Конструктор для существующего трансфера, для редактирования
        /// </summary>
        /// <param name="travelersCount">количество путешественников</param>
        /// <param name="guid">id записи проживания</param>
        public TransferViewModel(int travelersCount, string guid)
        {
            Transfer = TravelList.CurrentTravel.Transfers.FirstOrDefault(x => x.Id == guid);
            Transfer.TravelersCount = travelersCount;
            TravelerCount = travelersCount;
        }

        /// <summary>
        /// Проверка формы на валидность
        /// </summary>
        /// <returns></returns>
        private bool CheckForm()
        {
            if (string.IsNullOrWhiteSpace(Transfer.Departure) || string.IsNullOrWhiteSpace(Transfer.Destination))
                return false;
            if (Transfer.Begin > Transfer.Finish)
                return false;
            if (string.IsNullOrEmpty(Transfer.ImagePath))
                return false;
            decimal member;
            if (!decimal.TryParse(Transfer.TransportPrice.ToString(), out member))
                return false;
            return true;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
