using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TravelCalcBL;

namespace TravelCalc
{
    public class TravelCategorySelector : DataTemplateSelector//StyleSelector
    {
        public DataTemplate TransferClassStyle { get; set; }
        public DataTemplate AccommodationClassStyle { get; set; }
       

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            TravelAct travelAct = (TravelAct)item;

            switch (travelAct.TravelActType)
            {
                case TravelActType.Transfer:
                    return TransferClassStyle;
                case TravelActType.Accommodation:
                    return AccommodationClassStyle;
                default:
                    return null;
            }
        }
    }
}
