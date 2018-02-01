using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Threading;
using MessageClass;


namespace FinServerClasic
{
    interface IGenerate
    {
        bool Start();
        bool Stop();
        
    }

    public class GenerateClass:IGenerate
    {

        private long setGenSpeedS = 0;
        private long ticksPerGen = 0;


        private Thread GenThread;
        private bool threadStop;


        public string [] name_list = { "AUD/CAD", "AUD/CHF", "AUD/JPY", "AUD/NZD", "AUD/USD", "BGN/RON", "CAD/CHF", "CAD/JPY", "CHF/BGN", "CHF/JPY", "CHF/RON", "CHF/TRY", "EUR/AUD", "EUR/CAD", "EUR/CHF", "EUR/CZK", "EUR/DKK", "EUR/GBP", "EUR/HKD" };

        public long MaxGenSpeedS {
            get {
                return ticksPerGen != 0 ?  TimeSpan.TicksPerSecond / ticksPerGen:TimeSpan.TicksPerSecond;
            }
        }

        public long SetGenSpeedS
        {
            get {
                return setGenSpeedS;
            }
            set {
                if (value > 0)
                {
                    if (value < MaxGenSpeedS)
                        setGenSpeedS = value;
                    else
                        setGenSpeedS = MaxGenSpeedS;
                }
                else
                {
                    setGenSpeedS = 0;
                }
            }
        }

        public delegate void NewTransaction(Transaction t);

        public event NewTransaction NewTransactionHendler;


        public GenerateClass()
        {
            GetMaxGenSpeed();

            SetGenSpeedS = 1;
        }

        public bool Start()
        {
            if (GenThread == null)
            {
                threadStop = false;
                GenThread = new Thread(GenerateThread);
                GenThread.Start();
                return true;
            }
            return false;
            
        }

        public bool Stop()
        {
            if (GenThread != null)
            {
                threadStop = true;
                if (GenThread.IsAlive) {
                    GenThread.Abort();
                    GenThread = null;
                }
            }
            return true;
        }

        long tmp;

        private void GenerateThread()//треба нормально це написати
        {

            //написано приблизно тому приблизно
            long sleep_time = (TimeSpan.TicksPerSecond) /SetGenSpeedS;
            Random rm = new Random();
            while (!threadStop)
            {
                int counts = 0;
                Transaction t = new Transaction();
                lock (name_list)
                {
                    t.name = name_list[rm.Next(0, name_list.Length - 1)];
                    t.timestamp = DateTime.Now;
                    t.price = rm.NextDouble();
                    t.volume = rm.Next();
                }
                if (NewTransactionHendler != null) NewTransactionHendler.Invoke(t);
                //Thread.Sleep(sleep_time);
                if(sleep_time!=null)Thread.Sleep(new TimeSpan((long)sleep_time));
            }
        }

        //Визначаємо приблизний час генерації одної трансакції
        private void GetMaxGenSpeed()
        {
            Stopwatch st = new Stopwatch();

            Random rm = new Random();

            int max_c = 10000;//кількість генерацій
            ticksPerGen = 0;

            Transaction tr = new Transaction();
            double tem_tick = 0;
            for (int i = 0; i < 10; i++)
            {
                st.Start();
                for (int j = 0; j < max_c; j++)
                {
                    lock (name_list)
                    {
                        tr.name = name_list[rm.Next(0, name_list.Length - 1)];
                        tr.timestamp = DateTime.Now;
                        tr.price = rm.NextDouble();
                        tr.volume = rm.Next();
                    }
                }
                st.Stop();
                tem_tick += (double)st.ElapsedTicks / max_c;
                st.Reset();
            }
            ticksPerGen = (long)tem_tick / 10;
        }
    }
}
