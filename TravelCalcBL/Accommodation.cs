using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TravelCalcBL
{
    /// <summary>
    /// Класс проживания
    /// </summary>
    public class Accommodation: ICloneable
    {
        public string Name { get; set; }
        public string Id { get; set; }
        private DateTime checkIn;
        public DateTime CheckIn
        {
            get { return checkIn; }
            set
            {
                if (value.Date == checkIn.Date)
                    checkIn = value;
                else
                    checkIn = value.AddHours(14);
            }
        }
        private DateTime checkOut;
        public DateTime CheckOut
        {
            get { return checkOut; }
            set
            {
                if (value.Date == checkOut.Date)
                    checkOut = value;
                else
                    checkOut = value.AddHours(12);
            }
        }
        public string HotelType {get; set;}

        //Считать стоимость за одного человека (если false - общая стоимость за всех людей)
        private bool pricePerOneMan;
        public bool PricePerOneMan
        {
            get
            {
                if (TravelersCount == 1)
                    return true;
                return pricePerOneMan;
            }
            set
            {
                pricePerOneMan = value;
            }
        }
        public bool PriceForAllTravelers => !PricePerOneMan;

        //Считать стоимость за один день проживания (если false - общая стоимость за все дни)
        public bool PricePerOneDay{ get; set; }
        public bool PriceForAllDays => !PricePerOneDay;
        public int TravelersCount { get; set; }
        public decimal Price { get; set; }
        public string Nutrion { get; set; }
        public decimal FoodPrice { get; set; }
        public string ImagePath { get; set; }
        [JsonIgnore]
        public decimal TotalPrice
        {
            get
            {
                decimal price = Price + FoodPrice;

                if (PricePerOneMan)
                    price = price * TravelersCount;

                if (PricePerOneDay)
                    price = ((int)((CheckOut - CheckIn).TotalDays)+1) * price;

                return price;
            }
        }

        public Accommodation() { }

        public Accommodation(int travelersCount)
        {
            TravelersCount = travelersCount;
            PricePerOneDay = true;
            PricePerOneMan = true;
        }

        public void SetId()
        {
            Id = Guid.NewGuid().ToString();
        }

        public object Clone()
        {
            return new Accommodation()
            {
                Id = this.Id,
                CheckIn = this.CheckIn,
                CheckOut = this.CheckOut,
                Name = this.Name,
                HotelType = this.HotelType,
                PricePerOneMan = this.PricePerOneMan,
                PricePerOneDay = this.PricePerOneDay,   
                TravelersCount = this.TravelersCount,
                Price = this.Price,
                Nutrion = this.Nutrion,
                FoodPrice = this.FoodPrice,
                ImagePath = this.ImagePath
            };
        }
    }
}
