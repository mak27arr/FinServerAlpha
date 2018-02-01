using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace MessageClass
{
    public enum MESSAGE_TYPE
    {
        OPEN = 1,
        EXIT = 2,
        RECVEST_FIN_TRANS_LIST = 3,
        FIN_TRANS_LIST = 4,
        FIN_SUBSC_LIST = 5,
        FIN_NEW_TRANSACTION = 6,
    }

    [Serializable]
    public class Message_FC
    {
        public Message_FC()
        {

        }

        public Message_FC(MESSAGE_TYPE mt, object data)
        {
            type = mt;
            mess = data;
        }

        public MESSAGE_TYPE type;

        public object mess;
    }

    public static class Message_FC_Extension
    {
        public static byte[] toByteArray(this Message_FC str)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, str);
                return ms.ToArray();
            }
        }

        public static Message_FC fromByteArray(this Message_FC str, byte[] in_data)
        {
            using (var mem_stream = new MemoryStream())
            {
                var binForm = new BinaryFormatter();
                mem_stream.Write(in_data, 0, in_data.Length);
                mem_stream.Seek(0, SeekOrigin.Begin);
                var obj = binForm.Deserialize(mem_stream);
                return obj as Message_FC;
            }
        }
    }

    [Serializable]
    public struct Transaction
    {
        public string name { get; set; }
        public DateTime timestamp { get; set; }
        public double price { get; set; }
        public long volume { get; set; }

        public Transaction(string name, DateTime timestamp, double price, long volume)
        {
            this.name = name;
            this.timestamp = timestamp;
            this.price = price;
            this.volume = volume;
        }

        public override string ToString()
        {
            return "{ NAME:" + this.name + ", TIME:" + this.timestamp.ToString("yyyy-MM-ddThh:mm:ss") + ", PRICE:" + price + ", VOLUME:" + volume + "}";
        }
    }



}
