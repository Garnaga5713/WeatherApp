using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace WeatherApp_WinForm
{
    public partial class Form1 : Form
    {

        public class TemperatureInfo
        {
            public float Pressure { get; set; }
            public float Temp { get; set; }
            public float Humidity { get; set; }
            public float Speed { get; set; }
            public float Deg { get; set; }

        }
        public class WindInfo
        {
            public float Speed { get; set; }
            public float Deg { get; set; }
           
        }

        public class WeatherInfo
        {
            public string main { get; set; }
            public string description { get; set; }
            public string Icon { get; set; }

        }
        public class Sys
        { 
        public string country { get; set; }
        }

        public class WeatherResponce
        {
            public List<WeatherInfo> weather { get; set; }
            public TemperatureInfo main { get; set; }
            public WindInfo wind { get; set; }
          public Sys sys { get; set; }
            public string Name { get; set; }
        }

     

        public Form1()
        {
            InitializeComponent();
            button1.Text = "SEARCH";
            label1.Text = "Input City";
            label2.Text = "City: ";
            label3.Text = "Weather: ";
            label4.Text = "Temperature: ";
            label5.Text = "Pressure: ";
            label6.Text = "Wind deg: ";
            label7.Text = "Wind Speed: ";
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string city_name = textBox1.Text;
                string url = "https://api.openweathermap.org/data/2.5/weather?q=" + city_name + "&units=metric&appid=9192d68bdf088f9dc720f68fd292936f";

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                string responce = "";

                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    responce = streamReader.ReadToEnd();
                }

                WeatherResponce weatherResponce = JsonConvert.DeserializeObject<WeatherResponce>(responce);


                pictureBox1.ImageLocation = "https://api.openweathermap.org/img/w/" + weatherResponce.weather[0].Icon + ".png";
                pictureBox2.ImageLocation = "http://openweathermap.org/images/flags/" + weatherResponce.sys.country.ToLower() + ".png";

                label2.Text = "City: " + weatherResponce.Name;
                label3.Text = "Weather: " + weatherResponce.weather[0].description;
                label4.Text = "Temperature: " + weatherResponce.main.Temp;
                label5.Text = "Pressure: " + weatherResponce.main.Pressure;
                label6.Text = "Wind deg: " + weatherResponce.wind.Deg;
                label7.Text = "Wind Speed: " + weatherResponce.wind.Speed;
            }
            catch (Exception q1)
            {
                MessageBox.Show("Check name of city");
            }

            textBox1.Text = "";


        }
    }
}
