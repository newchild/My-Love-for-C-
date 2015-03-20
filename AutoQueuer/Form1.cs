using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using PVPNetConnect;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoQueuer
{
    public partial class Form1 : Form
    {
        private System.Timers.Timer timer = new System.Timers.Timer(1000);
        List<Botting_account> accounts = new List<Botting_account>();
        private Dictionary<string, string> UserList = new Dictionary<string, string>();
        public Form1()
        {
            InitializeComponent();
        }

        private bool isInList(Dictionary<string,string>.KeyCollection values, string key)
        {
            foreach (var value in values)
            {
                if (key == value)
                {
                    return true;
                }
                    
            }
            return false;
        }

        private void AddAccount_Click(object sender, EventArgs e)
        {
            if (isInList(UserList.Keys, usernameBox.Text))
            {
                MessageBox.Show("User already added");
                
            }
            else
            {
                StreamWriter fileOpener = new StreamWriter("accounts.txt", true);
                UserList.Add(usernameBox.Text, passwordBox.Text);
                fileOpener.WriteLine(usernameBox.Text + "|" + passwordBox.Text);
                fileOpener.Dispose();
                MessageBox.Show("Added user successfully");
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                string line = "";
                StreamReader leser = new StreamReader("accounts.txt");
                while ((line = leser.ReadLine()) != null)
                {
                    UserList.Add(line.Split('|')[0], leser.ReadLine().Split('|')[1]);
                }
                leser.Dispose();
                
            }
            catch (Exception excepts)
            {
                StreamWriter fileOpener = new StreamWriter("accounts.txt");
                fileOpener.Dispose();
            }
        }

        private void Queue_Click(object sender, EventArgs e)
        {
            
            foreach (var user in UserList.Keys)
            {
                Botting_account account = new Botting_account(user,UserList[user]);
                accounts.Add(account);
            }
            timer.Enabled = true;
            timer.Elapsed += timer_Elapsed;
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Data.Invoke(new Action(() =>
            {
                Data.Items.Clear();
                foreach (var account in accounts)
                {
                    Data.Items.Add(account.getUserInfo());
                }
            }));
        }
    }
}
