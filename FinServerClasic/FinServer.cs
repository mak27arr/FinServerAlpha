using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using MessageClass;



namespace FinServerClasic
{
    class FinServer
    {
        public FinServer(int port = 8080)
        {
            this.port = port; 
        }

        public delegate void NewTransaction(Transaction t);

        public event NewTransaction NewTransactionEventHendler;

        public delegate void EveMess(string s);

        public event EveMess ServEventHendler;

        public string[] AllFinTool;

        TcpListener Listener;

        private int port;

        private bool serverStop = true;

        public void StartAsync()
        {
            Thread Thread = new Thread(ServerThread);
            Thread.Start();
        }

        private void ServerThread()
        {
            Listener = new TcpListener(IPAddress.Any, port);
            this.ServerEventMessage("Server start on port : " + port);
            Listener.Start();
            serverStop = false;
            while (!serverStop)
            {
                if (Listener.Pending())
                {
                    TcpClient Client = Listener.AcceptTcpClient();
                    Thread Thread = new Thread(new ParameterizedThreadStart(Cli_Process));
                    Thread.Start(Client);
                    this.ServerEventMessage("New client conected from ip: " + ((IPEndPoint)Client.Client.RemoteEndPoint).Address);
                }
                Thread.Sleep(10);
                //TcpClient t_client = await Listener.AcceptTcpClientAsync();
                //Task c_task = Cli_Process(t_client);
                //this.ServerEventMessage("New client conected from ip: " + ((IPEndPoint)t_client.Client.RemoteEndPoint).Address);
            }
        }

        public bool Stop()
        {
            serverStop = true;
            return true;
        }

        private void Cli_Process(object client)
        {
            ClientProcesed c_class = new ClientProcesed((TcpClient)client, AllFinTool);
            NewTransactionEventHendler += c_class.NewTransactionProcesed;
            c_class.ProcesedMessageAsync();
            NewTransactionEventHendler -= c_class.NewTransactionProcesed;
            ServerEventMessage("Client disconected.");
        }

        private void ServerEventMessage(string mess)
        {
            if (ServEventHendler != null) ServEventHendler.Invoke(mess);
        }

        public void NewInIncuminTransaction(Transaction t)
        {
            if (NewTransactionEventHendler != null) NewTransactionEventHendler.Invoke(t);
        }

    }

    class ClientProcesed
    {
        private TcpClient client;

        private string[] FinTranSubsc;

        private string[] FinTranAllList;

        bool close_conection = false;

        public ClientProcesed(TcpClient client, string[] FinTranAllList)
        {
            this.client = client;
            FinTranSubsc = new string[0];

            this.FinTranAllList = FinTranAllList;

        }

        public void ProcesedMessageAsync()
        {
            Message_FC mess = new Message_FC();
            while (!close_conection)
            {
                mess = GetMessageAsync();
                if (mess == null)
                {
                    if(client==null || !client.Connected)close_conection = true;
                }
                else
                {
                    switch (mess.type)
                    {
                        case MESSAGE_TYPE.EXIT://відключення клієнта
                            close_conection = true;
                            break;
                        case MESSAGE_TYPE.RECVEST_FIN_TRANS_LIST://запит списку 
                            close_conection = !SendMessage(new Message_FC(MESSAGE_TYPE.FIN_TRANS_LIST, FinTranAllList));
                            break;
                        case MESSAGE_TYPE.FIN_SUBSC_LIST://підписка на трансакції
                            FinTranSubsc = mess.mess as string[];
                            break;
                    }
                }
            }
            client.Close();
        }

        protected Message_FC GetMessageAsync()
        {
            Message_FC mess = new Message_FC();
            try
            {
                NetworkStream stream = client.GetStream();
                byte[] messSize = new byte[sizeof(Int32)];

                stream.Read(messSize, 0, messSize.Length);

                byte[] mess_byte = new byte[BitConverter.ToInt32(messSize, 0)];
                stream.Read(mess_byte, 0, mess_byte.Length);

                mess = mess.fromByteArray(mess_byte);
            }
            catch (Exception ex)
            {
                return null;
            }

            return mess;
            //throw new NotImplementedException();
        }

        protected bool SendMessage(Message_FC mess)
        {
            try
            {
                NetworkStream stream = client.GetStream();

                byte[] mess_byte = mess.toByteArray();

                stream.Write(BitConverter.GetBytes(mess_byte.Length), 0, sizeof(Int32));

                stream.Write(mess_byte, 0, mess_byte.Length);
            }
            catch(Exception ex)
            {
                return false;
            }
            return true;
            //throw new NotImplementedException();
        }

        public void NewTransactionProcesed(Transaction t)
        {
            if (FinTranSubsc.Contains(t.name))
            {
                close_conection = !SendMessage(new Message_FC(MESSAGE_TYPE.FIN_NEW_TRANSACTION, t));
            }
        }
    }
}
