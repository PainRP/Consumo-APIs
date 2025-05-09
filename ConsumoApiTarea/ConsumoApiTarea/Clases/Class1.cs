using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;


namespace ConsumoApiTarea.Clases
{
    public class Class1
    {
        public string nombre { get; set; }
        public string altura { get; set; }
        public string peso { get; set; }
        public string tipo { get; set; }

        public void Exportar(string name, string height, string weight, string type)
        {
            List<Class1> listaPoke = new List<Class1>();
            listaPoke.Add(new Class1 { nombre = name, altura = height, peso = weight, tipo = type });

            try
            {
                string json = JsonConvert.SerializeObject(listaPoke.ToArray());
                string ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), $"pokemon_{name}.json");
                File.WriteAllText(ruta, json);
                MessageBox.Show($"Datos exportados correctamente a {ruta}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al exportar: {ex.Message}");
            }
        }
    }
}
    
 
