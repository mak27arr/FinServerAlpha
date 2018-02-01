using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MessageClass;

namespace FinClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void conect_but_Click(object sender, EventArgs e)
        {
            FinClient f = new FinClient(System.Net.IPAddress.Parse(ip_tB.Text),int.Parse(port_tB.Text));
            f.ClientEventHendler += NewEvent;
            f.NewTransEventHendler += NewTransaction;
            f.Start();
        }


        private void NewEvent(string s)
        {
            if (log_tB.Text.Length + s.Length < log_tB.MaxLength)
                log_tB.Text += s + Environment.NewLine;
            else {
                var lines = System.Text.RegularExpressions.Regex.Split(log_tB.Text, "\r\n|\r|\n").Skip(1);               
                log_tB.Text = string.Join(Environment.NewLine, lines.ToArray()) + Environment.NewLine + s;
            }
            log_tB.Select(log_tB.Text.Length, 0);
        }

        private void NewTransaction(Transaction t)
        {
            trans_dGV.Rows.Add(t.name,t.timestamp,t.price,t.volume);
        }
        
        private void ip_tB_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
