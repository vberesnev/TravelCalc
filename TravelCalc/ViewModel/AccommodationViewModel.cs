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
    class AccommodationViewModel
    {

        private RelayCommand checkOutChangeCommand { get; set; }
        public RelayCommand CheckOutChangeCommand
        {
            get
            {
                return checkOutChangeCommand ??
                  (checkOutChangeCommand = new RelayCommand(obj =>
                  {
                      Accommodation.CheckOut.AddHours(12);
                  }));
            }
        }

        private RelayCommand addAccommodationCommand { get; set; }
        public RelayCommand AddAccommodationCommand
        {
            get
            {
                return addAccommodationCommand ??
                  (addAccommodationCommand = new RelayCommand(obj =>
                  {
                      if (CheckForm())
                      {
                          if (Accommodation.Id == null)
                          {
                              TravelList.CurrentTravel.AddAccommodation(Accommodation);
                          }
                          else
                          {
                              TravelList.CurrentTravel.EditAccommodation(Accommodation);
                          }
                          CloseAction();
                      }
                  }));
            }
        }

        private Accommodation accommodation;
        public Accommodation Accommodation
        {
            get { return accommodation; }
            set
            {
                accommodation = value;
                OnPropertyChanged("Accommodation");
            }
        }

        /// <summary>
        /// Словарь мест проживаний с картинками
        /// </summary>
        public Dictionary<string, string> AccommodationDict
        {
            get
            {
                return new Dictionary<string, string>
                {
                    ["/Resources/appartments/hotel.png"] = "Гостиница",
                    ["/Resources/appartments/hostel.png"] = "Хостел",
                    ["/Resources/appartments/appartment.png"] = "Аппартаменты",
                    ["/Resources/appartments/house.png"] = "Дом",
                };
            }
        }

        /// <summary>
        /// Делегат для закрытия окна (метод закрытия привязывается к нему в AccommodationWindow.xaml.cs)
        /// </summary>
        public Action CloseAction { get; set; }

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
        /// Конструктор для нового проживания
        /// </summary>
        /// <param name="travelersCount">количество путешественников</param>
        public AccommodationViewModel(int travelersCount)
        {
            Accommodation = new Accommodation(travelersCount);
            Accommodation.CheckIn = DateTime.Today.Date;
            Accommodation.CheckOut = DateTime.Today.Date.AddDays(1);
            TravelerCount = travelersCount;
        }

        /// <summary>
        /// Конструктор для существующего проживания, для редактирования
        /// </summary>
        /// <param name="travelersCount">количество путешественников</param>
        /// <param name="guid">id записи проживания</param>
        public AccommodationViewModel(int travelersCount, string guid)
        {
            Accommodation = TravelList.CurrentTravel.Accommodation.FirstOrDefault(x => x.Id == guid);
            TravelerCount = travelersCount;
        }

        /// <summary>
        /// Проверка формы на валидность
        /// </summary>
        /// <returns></returns>
        private bool CheckForm()
        {
            if (string.IsNullOrWhiteSpace(Accommodation.Name))
                return false;
            if (Accommodation.CheckIn > Accommodation.CheckOut)
                return false;
            if (string.IsNullOrEmpty(Accommodation.ImagePath))
                return false;
            decimal member;
            if (!decimal.TryParse(Accommodation.TotalPrice.ToString(), out member))
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
