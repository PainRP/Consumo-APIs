using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using ConsumoApiTarea.Clases;

namespace ConsumoApiTarea
{
    public partial class Form1 : Form
    {
        private HttpClient client = new HttpClient();

        public Form1()
        {
            InitializeComponent();
        }

        private async void btnConsulta_Click(object sender, EventArgs e)
        {
            string pokemonName = textBoxPoke.Text.ToLower();
            string url = $"https://pokeapi.co/api/v2/pokemon/{pokemonName}";
            string tipos = "";


            try
            {
                string response = await client.GetStringAsync(url);
                JObject data = JObject.Parse(response);

                foreach (var type in data["types"])
                {
                    tipos += type["type"]["name"].ToString() + ", ";
                }

                dataGridView1.Columns.Clear();
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Add("Nombre", "Nombre");
                dataGridView1.Columns.Add("Altura", "Altura");
                dataGridView1.Columns.Add("Peso", "Peso");
                dataGridView1.Columns.Add("Tipo", "Tipo");
                dataGridView1.Rows.Add(data["name"].ToString(), data["height"].ToString(), data["weight"].ToString(), tipos.ToString().Remove(tipos.Length - 2));

                string imagenUrl = data["sprites"]["front_shiny"].ToString();
                pictureBox1.Load(imagenUrl);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado: {ex.Message}");
            }
        }

        private async void btnExportar_Click(object sender, EventArgs e)
        {
            string pokemonName = textBoxPoke.Text.ToLower();
            string url = $"https://pokeapi.co/api/v2/pokemon/{pokemonName}";
            string tipos = "";

            try
            {
                string response = await client.GetStringAsync(url);
                JObject data = JObject.Parse(response);

                foreach (var type in data["types"])
                {
                    tipos += type["type"]["name"].ToString() + ", ";
                }

                string nombre = data["name"].ToString();
                string altura = data["height"].ToString();
                string peso = data["weight"].ToString();

                Class1 exportador = new Class1();
                exportador.Exportar(nombre, altura, peso, tipos.Remove(tipos.Length - 2));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado: {ex.Message}");
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            textBoxPoke.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            pictureBox1.Image = null;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            string url = $"https://api.open-meteo.com/v1/forecast?latitude=14.2917&longitude=-89.8958&current_weather=true&timezone=auto";

            try
            {
                string response = await client.GetStringAsync(url);
                JObject data = JObject.Parse(response);
                string temperatura = data["current_weather"]["temperature"].ToString();
                lblTemp.Text = $"Temp: {temperatura}°C";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener el tiempo actual: {ex.Message}");
            }
        }
    }
}
