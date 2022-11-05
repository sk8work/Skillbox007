using System;
using System.Collections.Generic;

namespace HW
{
    class Program
    {
        /// <summary>
        /// Delay
        /// </summary>
        static void Delay()
        {
            Console.ReadKey();
        }

        /// <summary>
        /// Выводит меню
        /// </summary>
        static void PrintMenu()
        {
            Console.WriteLine("Выберите действие: ");
            Console.WriteLine("1 - Добавить сотрудника");
            Console.WriteLine("2 - Удалить сотрудника");
            Console.WriteLine("3 - Вывести список сотрудников");
            Console.WriteLine("4 - Вывести сотрудника по ID");
            Console.WriteLine("5 - Вывести список сотрудников из промужутка дат (по дате рождения)");
            Console.WriteLine("6 - Отсортировать по ID");
            Console.WriteLine("7 - Отсортировать по FIO");
            Console.WriteLine("8 - Отсортировать по дате рождения");
            Console.WriteLine("9 - Отсортировать по месту рождения");
            Console.WriteLine("q - Выйти из программы");
        }


        /// <summary>
        /// Start
        /// </summary>
        static void Start()
        {
            bool start = true;
            do
            {
                PrintMenu();
                char select = ' ';
                select = Convert.ToChar(Console.ReadLine());

                Repository r = new Repository();
                switch (select)
                {
                    case '1':
                        r.AddWorker();
                        break;
                    case '2':
                        string deleteChoice;
                        Console.WriteLine("Введите ID для удаления сотрудника. q - для выхода в меню.");
                        deleteChoice = Console.ReadLine();
                        if (deleteChoice == "q")
                        {
                            break;
                        }
                        r.DeleteWorker(Convert.ToInt32(deleteChoice));
                        break;
                    case '3':
                        List<Worker> getAllWorkers = r.GetAllWorkers();
                        r.Printer(getAllWorkers);
                        break;
                    case '4':
                        string IDChoice;
                        Console.WriteLine("Введите ID для вывода сотрудника на экран. q - для выхода в меню.");
                        IDChoice = Console.ReadLine();
                        if (IDChoice == "q")
                        {
                            break;
                        }
                        string result = r.GetWorkerById(Convert.ToInt32(IDChoice));
                        Console.WriteLine(result);
                        break;
                    case '5':
                        string dateFrom;
                        string dateTo;
                        Console.WriteLine("Введите начальную дату.");
                        dateFrom = Convert.ToDateTime(Console.ReadLine()).ToShortDateString();
                        Console.WriteLine("Введите конечную дату.");
                        dateTo = Convert.ToDateTime(Console.ReadLine()).ToShortDateString();
                        r.Printer(r.GetWorkersBetweenTwoDates(dateFrom, dateTo));
                        break;
                    case '6':
                        List<Worker> getSortByID = r.SortByID();
                        r.Printer(getSortByID);
                        break;
                    case '7':
                        List<Worker> getSortFIO = r.SortByFIO();
                        r.Printer(getSortFIO);
                        break;
                    case '8':
                        List<Worker> getSortBDate = r.SortByBDate();
                        r.Printer(getSortBDate);
                        break;
                    case '9':
                        List<Worker> getSortBPlace = r.SortByBPlace();
                        r.Printer(getSortBPlace);
                        break;
                    case 'q':
                        start = false;
                        Console.WriteLine("Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Сделайте выбор из пунктов меню");
                        break;
                }
            } while (start);

        }

        static void Main(string[] args)
        { 
            Start();

            Delay();
        }
    }
}
