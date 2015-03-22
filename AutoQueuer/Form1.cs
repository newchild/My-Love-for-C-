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
		private bool isStarted = false;
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

        

        private void Form1_Load(object sender, EventArgs e)
        {
			
            this.Text = "Autoqueuer by newchild. Thank you maufeat for volibot ^^";
            try
            {


                var lines = File.ReadAllLines("accounts.txt");
                foreach (var line in lines)
                {
                    UserList.Add(line.Split('|')[0], line.Split('|')[1]);
                }
                    
                
                
            }
            catch (Exception excepts)
            {
                StreamWriter fileOpener = new StreamWriter("accounts.txt");
                MessageBox.Show("Please fill the accounts.txt");
                fileOpener.Dispose();
            }
        }

        private void Queue_Click(object sender, EventArgs e)
		{

			if (!isStarted)
			{
				isStarted = !isStarted;
				foreach (var user in UserList.Keys)
				{
					Botting_account account = new Botting_account(user, UserList[user]);
					accounts.Add(account);
				}
				timer.Enabled = true;
				timer.Elapsed += timer_Elapsed;
			}
			else
			{
				MessageBox.Show("Bots already started :S");
			}
			
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
