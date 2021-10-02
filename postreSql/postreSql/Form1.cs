using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace postreSql
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        NpgsqlConnection connection = new NpgsqlConnection("server=localhost;port=5432; user ID=postgres; password=12345; Database=deneme");

        private void Form1_Load(object sender, EventArgs e)
        {
            String query = "Select * from kisiler";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, connection);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];

        }

        private void button1_Click(object sender, EventArgs e)
        {
            connection.Open();
            String query = "Insert into kisiler (ad,soyad) values(@p1,@p2)";
            NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@p1", textBox1.Text);
            cmd.Parameters.AddWithValue("@p2", textBox2.Text);
            cmd.ExecuteNonQuery();
            connection.Close();

        }
    }
}
