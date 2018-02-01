using System;
using MessageClass;
using System.Threading.Tasks;
using System.Threading;

namespace FinServerClasic
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello from FinServer!");

            //генератор трансакцій
            GenerateClass g = new GenerateClass();
            //покищо незвжди коректно непрацює
            Show("Max possible generation speed: " + g.MaxGenSpeedS +" T/s");
            g.SetGenSpeedS = 1000;
            //g.NewTransactionHendler += Show;
            g.Start();

            Console.WriteLine("Enter DB server name:");
            string db_server = Console.ReadLine();
            if(db_server == "") db_server = "KIVIT01\\TESTBASE";
            Console.WriteLine("Enter DB user:");
            string db_user = Console.ReadLine();
            if (db_user == "") db_user = "FinUser";
            Console.WriteLine("Enter DB user pass:");
            string db_upass = Console.ReadLine();
            if (db_upass == "") db_upass = "Qwerty11";
            Console.WriteLine("Enter DB name:");
            string db_base = Console.ReadLine();
            if (db_base == "") db_base = "FinLog";

            //запис в БД
            DataBase_MSSQL db = new DataBase_MSSQL(db_server, db_user, db_upass, db_base);
            //запис в БД нових трансакції
            g.NewTransactionHendler += db.NewInTransaction;
            db.NewDBIventHandler += Show;
            db.StartDB(); ;

            Console.WriteLine("Enter port for server:");
            string serv_port = Console.ReadLine();
            int s_port;

            //опрацювання клієнтів
            FinServer f;
            if (int.TryParse(serv_port, out s_port))
                f = new FinServer(s_port);
            else
                f = new FinServer();

            //помилки та події сервера
            f.ServEventHendler += Show;
            //отримуємо нові трансакції від генератора
            g.NewTransactionHendler += f.NewInIncuminTransaction;
            //список фінансовиї інструментів для передачі клієнтам
            f.AllFinTool = g.name_list;
            //запускаємо сервер
            f.StartAsync();

            //щоб незакрилась програма
            Console.ReadKey();

            g.Stop();

            db.StopDB();
            f.Stop();

        }

        static void Show(string t)
        {
            Console.WriteLine(t.ToString());
        }

        static void Show(Transaction t)
        {
            Console.WriteLine(t.ToString());
        }



    }
}
