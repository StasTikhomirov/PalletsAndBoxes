using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pallets_and_Boxes
{
    public class Box
    {
        public int id { get; set; }
        public double width { get; set; }

        public double height { get; set; }

        public double length { get; set; }

        public double weight { get; set; }

        public double volume { get; set; }

        public DateTime storageLife { get; set; }

        /// <summary>
        /// Задает объем коробки
        /// </summary>
        public void SetVolume()
        {
            volume = height * width * length;
        }
    }
}
