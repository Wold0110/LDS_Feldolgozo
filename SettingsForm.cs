using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LDS_Feldolgozo
{
    public partial class SettingsForm : Form
    {
        public static int threadNum = 1;
        public static void Init()
        {
            //betöltése a config fájlnak
            try
            {
                string[] lines = File.ReadAllLines("settings.cfg");

                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    string param = line.Split('=')[0];
                    string value = line.Split('=')[1];
                    switch (param)
                    {
                        case "threadnum":
                            threadNum= Convert.ToInt32(value);
                            break;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Hibás config fájl!");
            }
        }
        public SettingsForm()
        {
            InitializeComponent();
            threadNumInput.Value = threadNum;
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            string settings = "";
            //settings to save
            settings += "threadnum=" + threadNum;

            File.WriteAllText("settings.cfg", settings);
            this.Close();
        }

        private void threadNumInput_ValueChanged(object sender, EventArgs e)
        {
            int tmp = Convert.ToInt32(Math.Truncate(threadNumInput.Value));
            if(tmp > 0)
            {
                threadNum = tmp;
            }
            else
            {
                MessageBox.Show("A szálak számának nullánál nagyobb egész számnak kell lennie!");
            }
        }
    }
}
