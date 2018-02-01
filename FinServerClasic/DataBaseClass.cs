using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MessageClass;
//using System.Data.OleDb;
using MessageClass;
using System.Data.SqlClient;
using System.Collections.Concurrent;

namespace FinServerClasic
{

    abstract class DataBaseOLE
    {
        Thread Thread;
        private bool rec_stop = false;
        private ConcurrentQueue<Transaction> queue = new ConcurrentQueue<Transaction>();

        public delegate void NewIvent(string s);
        public event NewIvent NewDBIventHandler; 

        protected SqlConnection conn;
        protected string connectionStr;

        public void StartDB()
        {
            Thread Thread = new Thread(ThreadFunc);
            Thread.Start();
        }

        public void StopDB()
        {
            rec_stop = true;
            if (Thread != null) Thread.Abort();
        }

        private void ThreadFunc()
        {
            if (Conect())
            {
                while (!rec_stop)
                {
                    while (queue.Count > 0)
                    {
                        Transaction t = new Transaction();
                        if(queue.TryDequeue(out t))
                            LogTransaction(t);
                    }
                    Thread.Sleep(1);//не саме геніальне рішення але потік якось треба призупиняти на час
                }
                Close();
            }
        }

        protected virtual bool Close()
        {
            try
            {
                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                if (NewDBIventHandler != null)
                    NewDBIventHandler.Invoke("Error conect: "+ex.ToString());
                return false;
            }
            finally
            {
            }
        }

        protected virtual bool Conect()
        {
            conn = new SqlConnection();
            conn.ConnectionString = connectionStr;
            try
            {
                conn.Open();
                return true;
            }
            catch (Exception ex)
            {

                this.Close();
                if (NewDBIventHandler != null)
                    NewDBIventHandler.Invoke("Error close conect: " + ex.ToString());
                return false;
            }
            finally
            {
            }
        }

        protected virtual void LogTransaction(Transaction t)
        {
            throw new NotImplementedException();
        }

        public void NewInTransaction(Transaction t)
        {
            if (queue != null) queue.Enqueue(t);
        }
    }

    class DataBase_MSSQL : DataBaseOLE
    {
        public string db_server;
        public string db_UserName;
        public string db_UserPass;
        public string db_database = "FinLog";
        public string db_table { get { return "tb_FinTransaction"; } private set { } }

        public DataBase_MSSQL(string db_serverIP,string db_UserName,string db_UserPass,string db_database)
        {
            this.db_server = db_serverIP;
            this.db_UserName = db_UserName;
            this.db_UserPass = db_UserPass;
            this.db_database = db_database;
            //connectionStr = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data source= " + db_server + "; Initial Catalog= " + db_database + "; User Id= " + db_UserName + "; Password=" + db_UserPass;
            connectionStr = "user id=" + db_UserName + ";password=" + db_UserPass + ";server=" + db_serverIP + ";Trusted_Connection=yes;database=" + db_database + "; connection timeout=30";
        }

        ~DataBase_MSSQL()
        {
            this.Close();
        }

        protected override void LogTransaction(Transaction t)
        {
            if (conn.State == System.Data.ConnectionState.Open)
            {
                string sql = "INSERT INTO tb_FinTransactionLog (tool_name,timestamp,price,volume) VALUES('" + t.name + "','" + t.timestamp.ToString("yyyy-MM-dd hh:mm:ss") + "'," + t.price.ToString().Replace(',', '.') + "," + t.volume + ")";
                using (SqlCommand cmd = new SqlCommand(sql))
                {
                    cmd.Connection = conn;
                    cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
