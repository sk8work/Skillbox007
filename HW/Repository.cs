//0#03.11.2022 0:24:20#qwrafa#15#32#10.10.2002#gfhgfjk
//1#04.11.2022 13:10:07#rgdfhd#26#200#12.12.2002#cgmhgjk
//2#03.11.2022 0:24:20#qwrafa#15#32#10.10.2002#gfhgfjk
//3#04.11.2022 13:10:07#rgdfhd#26#200#12.12.2002#cgmhgjk
//4#03.11.2022 0:24:20#qwrafa#15#32#10.10.2002#gfhgfjk
//5#04.11.2022 13:10:07#rgdfhd#26#200#12.12.2002#cgmhgjk
//6#03.11.2022 0:24:20#qwrafa#15#32#10.10.2002#gfhgfjk
//7#04.11.2022 13:10:07#rgdfhd#26#200#12.12.2002#cgmhgjk
//8#03.11.2022 0:24:20#qwrafa#15#32#10.10.2002#gfhgfjk
//9#04.11.2022 13:10:07#rgdfhd#26#200#12.12.2002#cgmhgjk
//10#03.11.2022 0:24:20#qwrafa#15#32#10.10.2002#gfhgfjk
//11#04.11.2022 13:10:07#rgdfhd#26#200#12.12.2002#cgmhgjk

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace HW
{
    class Repository
    {
        public static int ID = 0;
        public static string path = "workers.txt";

        /// <summary>
        /// Вывести всех из файла в консоль
        /// </summary>
        public void GetAllWorkers()
        {
            List<string> result = FileToArr(path);

            foreach (var stringArr in result)
            {
                string[] resStr = stringArr.Split('#');
                foreach (string item in resStr)
                {
                    Console.Write($">>{item}");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// происходит чтение из файла, возвращается Worker с запрашиваемым ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public string GetWorkerById(int ID)
        {
            string res = String.Empty;
            List<string> result = FileToArr(path);
            foreach (var item in result)
            {
                string[] toFind = item.Split('#');
                if (Convert.ToInt32(toFind[0]) == ID)
                {
                    return string.Join(" ", toFind);
                }
            }
            return "Записи с таким ID не обнаружено. Повторите запрос";
        }

        /// <summary>
        /// Удалить запись по ID
        /// </summary>
        /// <param name="ID"></param>
        public void DeleteWorker(int ID)
        {
            List<string> result = FileToArr(path);

            foreach (var item in result)
            {
                string[] toFind = item.Split('#');
                if (int.Parse(toFind[0]) == ID)
                {
                    Console.WriteLine(item);
                    result.Remove(item);
                    break;
                }
            }

            using (StreamWriter sr = new StreamWriter(path, false))
            {
                foreach (var item in result)
                {
                    sr.WriteLine(item);
                }
            }
        }

        /// <summary>
        /// Читает файл и записывает в массив
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public List<string> FileToArr(string path)
        {
            List<string> result = new List<string>();
            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    result.Add(sr.ReadLine());
                }
            }
            return result;      
        }

        /// <summary>
        /// Вычисляем нужный ID
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public int GetID(string path)
        {
            int ID = 0;
            List<string> result =  FileToArr(path);
            if (result.Count > 0)
            {
                string str = result[result.Count - 1];
                string[] parts = str.Split("#");
                ID = int.Parse(parts[0]) + 1;
            }
            
            return ID;
        }

        /// <summary>
        /// Добавляем экземпляр Worker в файл
        /// </summary>
        public void AddWorker()
        {
            string note = String.Empty;
            Worker w = new Worker();

            ID = GetID(path);

            w.DateTimeInsert = DateTime.Now;

            Console.WriteLine("Введите ФИО: ");
            w.FIO = Console.ReadLine();

            Console.WriteLine("Введите возраст: ");
            w.Age = Console.ReadLine();

            Console.WriteLine("Введите рост: ");
            w.Height = Console.ReadLine();

            Console.WriteLine("Введите дату рождения: Y.M.D");
            w.BDate = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Введите место рождения: ");
            w.BPlace = Console.ReadLine();

            Console.WriteLine();

            // дописываем нового worker в файл
            note = ID.ToString() + "#" + w.DateTimeInsert + "#" + w.FIO + "#" + w.Age + "#" + w.Height + "#" + w.BDate.ToShortDateString() + "#" + w.BPlace;

            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine(note);
            }
        }

        /// <summary>
        /// Возвращает List<string> сотрудников из промежутка дат
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public List<string> GetWorkersBetweenTwoDates(string dateFrom, string dateTo)
        {
            List<string> getAll = FileToArr(path);
            List<string> selectedArr = new List<string>();
            foreach (string item in getAll)
            {
                string[] itemToArr = item.Split('#');
                if ( (Convert.ToDateTime(itemToArr[5]) > Convert.ToDateTime(dateFrom)) && (Convert.ToDateTime(itemToArr[5]) < Convert.ToDateTime(dateTo)) )
                {
                    selectedArr.Add(item);
                }
            }
            return selectedArr;
        }

        /// <summary>
        /// Распечатываем выборку
        /// </summary>
        /// <param name="arr"></param>
        public void Printer(List<string> arr)
        {
            foreach (var item in arr)
            {
                string[] row = item.Split('#');
                foreach (string i in row)
                {
                    Console.Write($">>{i}");
                }
                Console.WriteLine();
            }
        }
    }
}
