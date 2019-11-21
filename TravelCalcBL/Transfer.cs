using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TravelCalcBL
{
    public class Transfer: ICloneable
    {       
        public DateTime Begin { get; set; }
        public DateTime Finish { get; set; }
        public string Id { get; set; }
        public string Departure { get; set; }
        public string Destination { get; set; }

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
        public bool PriceForAll => !PricePerOneMan;
        public int TravelersCount { get; set; }
        public decimal TransportPrice { get; set; }
        public decimal FoodPrice { get; set; }
        public string ImagePath { get; set; }
        [JsonIgnore]
        public decimal TotalPrice
        {
            get
            {
                if (PricePerOneMan)
                    return (TransportPrice + FoodPrice) * TravelersCount;
                return TransportPrice + FoodPrice;
            } 
        }

        public Transfer()
        {
        }

        public Transfer(int travelersCount)
        {
            TravelersCount = travelersCount;
            PricePerOneMan = true;
        }

        public void SetId()
        {
            Id = Guid.NewGuid().ToString();
        }

        public object Clone()
        {
            return new Transfer()
            {
                Id = this.Id,
                Begin = this.Begin,
                Finish = this.Finish,
                Departure = this.Departure,
                Destination = this.Destination,
                PricePerOneMan = this.PricePerOneMan,
                TransportPrice = this.TransportPrice,
                FoodPrice = this.FoodPrice,
                ImagePath = this.ImagePath,
                TravelersCount = this.TravelersCount
            };
        }
    }
}
