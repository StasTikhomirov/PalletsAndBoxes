using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows;
using Newtonsoft.Json.Linq;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Serialization;

namespace Pallets_and_Boxes
{
    class Program
    {
        static void Main(string[] args)
        {
            //Получение данных из JSON
            var palletsList = File.Exists("JSON/pallets.json") ? JsonConvert.DeserializeObject<List<Pallet>>(File.ReadAllText("JSON/pallets.json")) : null;
            if (palletsList == null)
            {
                Console.WriteLine("Файл JSON не найден");
                return;
            }

            
            foreach (var pal in palletsList)
            {
                pal.SetWeightVolumeAndStorageLife();
            }         
            
            Console.WriteLine("Сгруппированный список");            
            
            var grouppedResult = palletsList.OrderBy(pallet => pallet.storageLife.Month)
                                    .ThenBy(pallet => pallet.weight)
                                    .GroupBy(pallet => pallet.storageLife.Month)
                                    .ToList();


            foreach (var group in grouppedResult)
            {
                Console.WriteLine("Код группы: " + group.Key);
                foreach (var pallet in group)
                {
                    Console.WriteLine("Id паллета: " + pallet.id);
                    Console.WriteLine("Срок годности: " + pallet.storageLife);
                    Console.WriteLine("Вес: " + pallet.weight);
                    group.OrderBy(pallet => pallet.weight);
                }
                Console.WriteLine("");
            }



            List<Pallet> outputList = new List<Pallet>();

            for (int i = grouppedResult.Count() - 1; i>0; i--)
            {
                try
                {
                    for (int y = grouppedResult[i].Count() - 1; y >= 0; y--)
                    {
                        if (outputList.Count < 3)
                        {
                            Pallet pallet = grouppedResult[i].ElementAt(y);
                            outputList.Add(pallet);
                        }
                    }

                }
                catch 
                { 
                    Console.WriteLine("Месяца + " + i + "Нет в списке");                    
                }

                if (outputList.Count == 3)
                {
                    break;
                }
            }

            Console.WriteLine("");
            Console.WriteLine("-------------------------------");
            Console.WriteLine("3 паллеты c наибольшим сроком годности ");

            outputList.Sort((x, y) => x.volume.CompareTo(y.volume));

            foreach (var pallet in outputList)
            {
                Console.WriteLine("Id паллета: " + pallet.id);
                Console.WriteLine("Срок годности: " + pallet.storageLife);
                Console.WriteLine("Объем: " + pallet.volume);               
            }

            Console.ReadLine();
            
        }
        
    }
}
