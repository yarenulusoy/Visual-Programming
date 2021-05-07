//Yasin Ertune - Girilen sayilari buyukten kucuge siralama
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(textBox1.Text);
            textBox1.Text = "";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int piece = listBox1.Items.Count;
            int temp = 0;

            if (comboBox1.SelectedIndex == 0) // comboBox1.Text =="BÜYÜKTEN KÜÇÜĞE"
            {
                for (int i = 0; i < piece; i++)
                {
                    for (int a = 0; a < piece; a++)
                    {
                        if (Convert.ToInt32(listBox1.Items[i]) > Convert.ToInt32(listBox1.Items[a]))
                        {               
                            temp = Convert.ToInt32(listBox1.Items[i]);
                            listBox1.Items[i] = Convert.ToInt32(listBox1.Items[a]);
                            listBox1.Items[a] = temp;
                        }
                    }
                }
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                for (int i = 0; i < piece; i++)
                {
                    for (int a = 0; a < piece; a++)
                    {
                        if (Convert.ToInt32(listBox1.Items[i]) < Convert.ToInt32(listBox1.Items[a]))
                        {
                            temp = Convert.ToInt32(listBox1.Items[i]);
                            listBox1.Items[i] = Convert.ToInt32(listBox1.Items[a]);
                            listBox1.Items[a] = temp;
                        }
                    }
                }
            }
        }
    }
}
