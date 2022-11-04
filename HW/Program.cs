using System;

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
                        string deleteChoice = String.Empty;
                        Console.WriteLine("Введите ID для удаления сотрудника. q - для выхода в меню.");
                        deleteChoice = Console.ReadLine();
                        if (deleteChoice == "q")
                        {
                            break;
                        }
                        r.DeleteWorker(Convert.ToInt32(deleteChoice));
                        break;
                    case '3':
                        r.GetAllWorkers();
                        break;
                    case '4':
                        string IDChoice = String.Empty;
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
