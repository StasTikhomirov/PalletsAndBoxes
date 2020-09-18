using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pallets_and_Boxes
{
    public class Pallet
    {
        public int id { get; set; }
        public double width { get; set; }

        public double height { get; set; }

        public double length { get; set; }

        public double weight { get; set; }

        public double volume { get; set; }

        public List<Box> Boxes {get;set;}

        public DateTime storageLife { get; set; }

        //---------------------------------------------------------------------------------
        /// <summary>
        /// Задает срок хранения паллета и общий вес
        /// </summary>
        /// <param name="palletsList">Список паллетов</param>
        public void SetWeightVolumeAndStorageLife()
        {
            weight = 30; // вес пустого паллета
            volume = height * width * length;

            foreach (var box in Boxes)
            {
                // если указана дата производства, тогда добавляем 100 дней
                if ((DateTime.Now.Date - box.storageLife ).TotalDays< 100 && box.storageLife < DateTime.Now.Date)
                {
                    box.storageLife = box.storageLife.AddDays(100);
                }
                // вес коробки добавляем к весу паллета
                weight += box.weight;
                box.SetVolume();
                volume += box.volume;
            }
            var minDate = Boxes.Min(box => box.storageLife);
            storageLife = minDate;
        }            


    //---------------------------------------------------------------------------------

    }
    //---------------------------------------------------------------------------------

}
