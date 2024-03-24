using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace firebase_db
{
    
    public partial class Form1 : Form
    {
        //config
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "nEEJflPbOlPH0ANP4PPqdYvLEwTnXlmY9Fp7ROMO",
            BasePath = "https://fir-c569c-default-rtdb.asia-southeast1.firebasedatabase.app/"
        };

        IFirebaseClient client;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //config
            client = new FireSharp.FirebaseClient(config);
            if (client != null)
            {
                MessageBox.Show("Connection is established!");
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            //data to insert
            var data = new Data
            {
                Id = textBox1.Text,
                Name = textBox2.Text,
                Age = textBox3.Text,
                Address = textBox4.Text
            };

            //insert data into path Information/[data.Id]
            SetResponse response = await client.SetTaskAsync("Information/" + textBox1.Text, data);
            MessageBox.Show("Data is inserted!");
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            //select data from path Information/[Id]
            FirebaseResponse response = await client.GetTaskAsync("Information/" + textBox5.Text);
            //get data
            Data data = response.ResultAs<Data>();
            //show data
            textBox1.Text = data.Id;
            textBox2.Text = data.Name;
            textBox3.Text = data.Age;
            textBox4.Text = data.Address;
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            //data to update
            var data = new Data
            {
                Id = textBox1.Text,
                Name = textBox2.Text,
                Age = textBox3.Text,
                Address = textBox4.Text
            };

            //update data into path Information/[data.Id]
            FirebaseResponse response = await client.UpdateTaskAsync("Information/" + textBox1.Text, data);
            MessageBox.Show("Data is updated!");
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            FirebaseResponse response = await client.DeleteTaskAsync("Information/" + textBox6.Text);
            MessageBox.Show("Data is deleted");
        }
    }
}
