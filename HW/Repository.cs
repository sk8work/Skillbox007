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

        public List<Worker> allWorkers = new List<Worker>();

        /// <summary>
        /// Проверяет, существует ли файл
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private bool IsFileExist(string path)
        {
            return File.Exists(path);
        }

        /// <summary>
        /// Проверяет, не пустой ли файл
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private bool IsFileNotEmpty(string path)
        {
            bool flag = false;
            using (StreamReader sr = new StreamReader(path))
            {
                if (sr.ReadToEnd() != string.Empty)
                {
                    flag = true;
                }
            }
            return flag;
        }

        /// <summary>
        /// Вывести всех из файла в консоль
        /// </summary>
        public List<Worker> GetAllWorkers()
        {
            if (IsFileExist(path) && IsFileNotEmpty(path))
            {
                List<string> result = FileToArr(path);
                for (int i = 0; i < result.Count; i++)
                {
                    string[] str    = result[i].Split('#');
                    Worker w        = new Worker();

                    w.Id                = str[0];
                    w.DateTimeInsert    = DateTime.Parse(str[1]);
                    w.FIO               = str[2];
                    w.Age               = str[3];
                    w.Height            = str[4];
                    w.BDate             = DateTime.Parse(str[5]);
                    w.BPlace            = str[6];

                    this.allWorkers.Add(w);

                }
            }
            return allWorkers;
        }

        /// <summary>
        /// происходит чтение из файла, возвращается Worker с запрашиваемым ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public string GetWorkerById(int ID)
        {
            List<Worker> allWorkers = GetAllWorkers();
            string note = "Записи с таким ID не найдено. Повторите запрос..";
            foreach (Worker item in allWorkers)
            {
                if (item.Id == ID.ToString())
                {
                    note = $"{item.Id} >> {item.DateTimeInsert} >> {item.FIO} >> {item.Age} >> {item.Height} >> {item.BDate.ToShortDateString()} >> {item.BPlace}";
                    return note;
                }
            }
            return note;
        }

        /// <summary>
        /// Удалить запись по ID
        /// </summary>
        /// <param name="ID"></param>
        public void DeleteWorker(int ID)
        {
            List<Worker> allWorkers = GetAllWorkers();
            allWorkers.RemoveAll(w => w.Id == ID.ToString());

            using (StreamWriter sr = new StreamWriter(path, false))
            {
                string note;
                foreach (var w in allWorkers)
                {
                    note = w.Id.ToString() + "#" + w.DateTimeInsert + "#" + w.FIO + "#" + w.Age + "#" + w.Height + "#" + w.BDate.ToShortDateString() + "#" + w.BPlace;
                    sr.WriteLine(note);
                }
            }
        }

        /// <summary>
        /// Читает файл и записывает в массив
        /// </summary>
        /// <param name = "" ></ param >
        /// < returns ></ returns >
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
            List<Worker> allWorkers = GetAllWorkers();
            int ID = int.Parse(allWorkers[allWorkers.Count - 1].Id) + 1;
            List<string> result =  FileToArr(path);
            return ID;
        }

        /// <summary>
        /// Добавляем экземпляр Worker в файл
        /// </summary>
        public void AddWorker()
        {
            string note;
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
        public List<Worker> GetWorkersBetweenTwoDates(string dateFrom, string dateTo)
        {
            List<Worker> allWorkers = GetAllWorkers();
            List<Worker> selectedArr = new List<Worker>();
            foreach (var item in allWorkers)
            {
                if ((item.BDate > Convert.ToDateTime(dateFrom)) && (item.BDate < Convert.ToDateTime(dateTo)))
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
        public void Printer(List<Worker> workers)
        {
            foreach (var item in workers)
            {
                Console.WriteLine($"{item.Id} >> {item.DateTimeInsert} >> {item.FIO} >> {item.Age} >> {item.Height} >> {item.BDate.ToShortDateString()} >> {item.BPlace}");
            }
        }

        /// <summary>
        /// Возвращает отсортированный список по Id
        /// </summary>
        /// <returns></returns>
        public List<Worker> SortByID()
        {
            List<Worker> allWorkers = GetAllWorkers();
            List<Worker> newWorkers = new List<Worker>();
            newWorkers = allWorkers.OrderBy(w => w.Id).ToList<Worker>();
            return newWorkers;
        }

        /// <summary>
        /// Возвращает отсортированный список по FIO
        /// </summary>
        /// <returns></returns>
        public List<Worker> SortByFIO()
        {
            List<Worker> allWorkers = GetAllWorkers();
            List<Worker> newWorkers = new List<Worker>();
            newWorkers = allWorkers.OrderBy(w => w.FIO).ToList<Worker>();
            return newWorkers;
        }

        /// <summary>
        /// Возвращает отсортированный список по Дате рождения
        /// </summary>
        /// <returns></returns>
        public List<Worker> SortByBDate()
        {
            List<Worker> allWorkers = GetAllWorkers();
            List<Worker> newWorkers = new List<Worker>();
            newWorkers = allWorkers.OrderBy(w => w.BDate).ToList<Worker>();
            return newWorkers;
        }

        /// <summary>
        /// Возвращает отсортированный список по месту рождения
        /// </summary>
        /// <returns></returns>
        public List<Worker> SortByBPlace()
        {
            List<Worker> allWorkers = GetAllWorkers();
            List<Worker> newWorkers = new List<Worker>();
            newWorkers = allWorkers.OrderBy(w => w.BPlace).ToList<Worker>();
            return newWorkers;
        }
    }
}