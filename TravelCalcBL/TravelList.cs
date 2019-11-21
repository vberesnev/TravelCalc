using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TravelCalcBL
{
    /// <summary>
    /// Класс списка всех путешествий
    /// </summary>
    public class TravelList : INotifyPropertyChanged
    {
        public ObservableCollection<Travel> TravelsList { get; set; }
        [JsonIgnore]
        public static Travel CurrentTravel { get; private set; }

        public void SetCurrentTravel(Travel travel)
        {
            CurrentTravel = travel;
        }

        public void AddTravel(Travel travel)
        {
            Travel newTravel = TravelsList.FirstOrDefault(x => x.Id == travel.Id);
            if (newTravel == null)
                TravelsList.Add(travel);
            else
            {
                newTravel.Copy(travel);
            }
        }

        public void RemoveTravel(Travel travel)
        {
            TravelsList.Remove(travel);
        }

        /// <summary>
        /// При создании экземпляра класса происходит десериализация из json-файла сохранения
        /// </summary>
        /// <param name="path">Путь к json-файлу сохранения</param>
        public TravelList(string path)
        {
            CurrentTravel = new Travel();
            string data = File.ReadAllText(path, Encoding.UTF8);
            TravelsList = JsonConvert.DeserializeObject<ObservableCollection<Travel>>(data);
        }

        /// <summary>
        /// Сериализация всех путешествий в json-файла сохранения
        /// </summary>
        /// <param name="path">Путь к json-файлу сохранения</param>
        public void Save(string path)
        {
            string json = JsonConvert.SerializeObject(TravelsList, Formatting.Indented);
            File.WriteAllText(path, json, Encoding.UTF8);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }



    }
}
