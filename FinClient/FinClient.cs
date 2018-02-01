using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;
using MessageClass;

namespace FinClient
{
    class FinClient
    {
        private TcpClient client;

        private IPAddress serv_addr;
        private int port;

        private List<string> FinTranSubsc;

        public delegate void EveMess(string s);
        public event EveMess ClientEventHendler;

        public delegate void NewTransaction(Transaction m);
        public event NewTransaction NewTransEventHendler;

        public FinClient(IPAddress ip,int port)
        {
            serv_addr = ip;
            this.port = port;
        }

        public void Start()
        {
            client = new TcpClient();
            client.Connect(new IPEndPoint(serv_addr, port));
            ProcesedMessageAsync();

            //Thread Thread = new Thread(ProcesedMessage);
            //Thread.Start();
        }

        protected async Task ProcesedMessageAsync()
        {
            SendMessage(new Message_FC(MESSAGE_TYPE.RECVEST_FIN_TRANS_LIST, null));
            bool close_conection = false;
            Message_FC mess = new Message_FC();
            while (!close_conection)
            {
                mess = await GetMessage();
                switch (mess.type)
                {
                    case MESSAGE_TYPE.EXIT://відключення клієнта
                        close_conection = true;
                        break;
                    case MESSAGE_TYPE.FIN_NEW_TRANSACTION:
                        Transaction t = (Transaction)mess.mess;
                        if (NewTransEventHendler != null) NewTransEventHendler.Invoke(t);
                        break;
                    case MESSAGE_TYPE.FIN_TRANS_LIST:
                        string[] trans_tool = mess.mess as string[];
                        if (trans_tool != null)
                        {
                            if (ClientEventHendler != null) ClientEventHendler.Invoke("Get tool : " + string.Join(", ", trans_tool));
                            Random r = new Random();
                            int count = r.Next(1, trans_tool.Length - 1);
                            string[] sub_trans_tool = new string[count];
                            for (int i = 0; i < count; i++)
                            {
                                int ind;
                                do
                                    ind = r.Next(0, trans_tool.Length - 1);
                                while (sub_trans_tool.Contains(trans_tool[ind]));
                                sub_trans_tool[i] = trans_tool[ind];
                            }
                            SendMessage(new Message_FC(MESSAGE_TYPE.FIN_SUBSC_LIST, sub_trans_tool));
                            if (ClientEventHendler != null) ClientEventHendler.Invoke("Subscribe tool : " + string.Join(", ", sub_trans_tool));
                        }
                        break;
                }
            }
            client.Close();
        }

        protected async Task<Message_FC> GetMessage()
        {
            NetworkStream stream = client.GetStream();
            byte[] messSize = new byte[sizeof(Int32)];
            await stream.ReadAsync(messSize, 0, messSize.Length);

            byte[] mess_byte = new byte[BitConverter.ToInt32(messSize, 0)];
            await stream.ReadAsync(mess_byte, 0, mess_byte.Length);

            Message_FC mess = new Message_FC();
            mess = mess.fromByteArray(mess_byte);

            return mess;
            //throw new NotImplementedException();
        }

        protected bool SendMessage(Message_FC mess)
        {
            NetworkStream stream = client.GetStream();

            byte[] mess_byte = mess.toByteArray();

            stream.Write(BitConverter.GetBytes(mess_byte.Length), 0, sizeof(Int32));

            stream.Write(mess_byte, 0, mess_byte.Length);

            return true;
            //throw new NotImplementedException();
        }

    }
}
