using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TravelCalcBL
{
    public class Travel: ICloneable
    {
        public string Id { get; private set; }
        public string Name { get; set; }
        public int People { get; set; }
        public List<Transfer> Transfers { get; set; }
        public List<Accommodation> Accommodation { get; set; }
        /// <summary>
        /// Список всех активностей в путешествии (трансферов и проживаний) для вывода в один общий список
        /// </summary>
        [JsonIgnore]
        public List<TravelAct> Acts
        {
            get
            {
                var acts = new List<TravelAct>();
                foreach (var transfer in Transfers)
                {
                    TravelAct act = new TravelAct(TravelActType.Transfer,
                                                  transfer.Id,
                                                  $"{transfer.Departure} -> {transfer.Destination}",
                                                  transfer.Begin, transfer.Finish,
                                                  transfer.TotalPrice,
                                                  transfer.ImagePath);
                    acts.Add(act);
                }
                foreach (var accommodation in Accommodation)
                {
                    TravelAct act = new TravelAct(TravelActType.Accommodation,
                                                  accommodation.Id,
                                                  accommodation.Name,
                                                  accommodation.CheckIn, accommodation.CheckOut,
                                                  accommodation.TotalPrice,
                                                  accommodation.ImagePath);
                    acts.Add(act);
                }

                return acts.OrderBy(x => x.Begin).ToList();
            }
        }
        [JsonIgnore]
        public decimal TotalPrice
        {
            get
            {
                return Acts.Sum(x => x.TotalPrice);
            }
        }
        [JsonIgnore]
        public string Information
        {
            get
            {
                if (Acts.Count == 0)
                    return "Добавьте трансфер или проживание в путешествие";
                return $"Трансферов: {Transfers.Count} на {Transfers.Sum(x => x.TotalPrice)} руб.\r\nПроживаний: {Accommodation.Count} на {Accommodation.Sum(x => x.TotalPrice)} руб.";
            }
        }
        [JsonIgnore]
        public string TimeSpan
        {
            get
            {
                if (Acts.Count == 0) return "Период не указан";
                return $"{Acts.First().Begin.ToString("dd MMM")} - {Acts.Last().End.ToString("dd MMM yyyy")}";
            }
        }

        public Travel()
        {
            Id = Guid.NewGuid().ToString();
            People = 1;
            Transfers = new List<Transfer>();
            Accommodation = new List<Accommodation>();
        }
        /// <summary>
        /// Метод для обновления количества людей в списке трансферов и проживаний для пересчета стоимости (применяется, если на форме Travel менять количество путешественников)
        /// </summary>
        public void UpdateTravels()
        {
            foreach (var transfer in Transfers)
            {
                transfer.TravelersCount = People;
            }
            foreach (var accommodation in Accommodation)
            {
                accommodation.TravelersCount = People;
            }
        }

        /// <summary>
        /// Удалить TravelAct вместе с сопутствующим ему трансфером или проживанием
        /// </summary>
        /// <param name="act"></param>
        public void DeleteTravelAct(TravelAct act)
        {
            if (act.TravelActType == TravelActType.Transfer)
            {
                Transfer transfer = this.Transfers.FirstOrDefault(x => x.Id == act.Id);
                this.Transfers.Remove(transfer);
            }
            else
            {
                Accommodation accommodation = this.Accommodation.FirstOrDefault(x => x.Id == act.Id);
                this.Accommodation.Remove(accommodation);
            }
        }

        public void AddTransfer(Transfer transfer)
        {
            if (transfer.Id == null)
            {
                transfer.SetId();
            }
            Transfers.Add(transfer);
        }

        public void EditTransfer(Transfer transfer)
        {
            var editTransfer = Transfers.FirstOrDefault(x => x.Id == transfer.Id);
            editTransfer = transfer;
        }

        public  void AddAccommodation(Accommodation accommodation)
        {
            if (accommodation.Id == null)
            {
                accommodation.SetId();
            }
            Accommodation.Add(accommodation);
        }

        public  void EditAccommodation(Accommodation accommodation)
        {
            var editAccommodation = Accommodation.FirstOrDefault(x => x.Id == accommodation.Id);
            editAccommodation = accommodation;
        }

        /// <summary>
        /// Полное клонирование объекта путешествия, нужно для сохранения
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            Travel travel = new Travel() { Id = this.Id, Name = this.Name, People = this.People };
            travel.Transfers = new List<Transfer>();
            travel.Accommodation = new List<Accommodation>();
            foreach (var transfer in this.Transfers)
            {
                travel.Transfers.Add(transfer.Clone() as Transfer);
            }
            foreach (var accommodation in this.Accommodation)
            {
                travel.Accommodation.Add(accommodation.Clone() as Accommodation);
            }
            return travel;
        }

        public void Copy(Travel travel)
        {
            this.Name = travel.Name;
            this.People = travel.People;
            this.Transfers = travel.Transfers;
            this.Accommodation = travel.Accommodation;
        }
    }
}
