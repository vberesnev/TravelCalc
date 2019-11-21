using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelCalcBL
{
    /// <summary>
    /// Класс действия в путешествии, не сохраняется, объекты выводятся из Transfers и Accommodations в классе Travel
    /// </summary>
    public class TravelAct
    {
        public TravelActType TravelActType { get; }
        public string Id { get; }
        public string Name { get; }
        public DateTime Begin { get; }
        public DateTime End { get;  }
        public decimal TotalPrice { get; }
        public string ImagePath { get; }

        public TravelAct(TravelActType type,  string guid,  string name, DateTime begin, DateTime end, decimal totalPrice, string imagePath)
        {
            TravelActType = type;
            Id = guid;
            Name = name;
            Begin = begin;
            End = end;
            TotalPrice = totalPrice;
            ImagePath = imagePath;
        }

        public string GetTime
        {
            get
            {
                if (TravelActType == TravelActType.Transfer)
                {
                    if (Begin.Date == End.Date)
                    {
                        return $"{Begin.ToString("d MMM HH:mm")} - {End.ToString("HH:mm")}";
                    }
                    else
                    {
                        return $"{Begin.ToString("d MMM HH:mm")} - {End.ToString("d MMM HH:mm")}";
                    }
                }
                else
                {
                    return $"{Begin.ToString("d MMM")} - {End.ToString("d MMM")}. Ночей: {(int)(End - Begin).TotalDays+1}";
                }
            }
        }
    }
}
