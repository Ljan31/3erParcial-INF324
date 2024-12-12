using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalisisImagenes
{
    public partial class Form1 : Form
    {
        
        // Diccionario para guardar los rangos de colores para diferentes cultivos
        Dictionary<string, RangoColor> cultivosColores;


        public Form1()
        {
            InitializeComponent();
            // Inicializar el diccionario con los rangos de colores
            cultivosColores = new Dictionary<string, RangoColor>
            {
                { "Maíz", new RangoColor(150, 255, 200, 255, 0, 100) },  // Rango de colores típicos de maíz
                { "Trigo", new RangoColor(180, 255, 150, 255, 0, 100) }, // Rango de colores típicos de trigo
                { "Soja", new RangoColor(0, 150, 100, 255, 0, 100) },    // Rango de colores típicos de soja
                { "Arroz", new RangoColor(100, 200, 100, 255, 0, 100) }   // Rango de colores típicos de arroz
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Imagenes JPG|*.jpg|Imagenes PNG|*.png";
            openFileDialog1.ShowDialog();
            Bitmap bmp = new Bitmap(openFileDialog1.FileName);
            pictureBox1.Image = bmp;
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            if (comboBox1.SelectedItem == null)
            {
                label2.Text = "Debe seleccionar un cultivo."; // Mensaje de error
                label2.ForeColor = Color.Red; // Opcional: cambiar color del texto a rojo
            }else{
                string cadena = comboBox1.SelectedItem.ToString(); // Capturar el valor seleccionado

                label2.Text = "";
                Bitmap bmp = new Bitmap(pictureBox1.Image);
                Bitmap bmp2 = new Bitmap(bmp.Width, bmp.Height);

                // Iterar sobre todos los píxeles de la imagen
                for (int i = 0; i < bmp.Width; i++)
                {
                    for (int j = 0; j < bmp.Height; j++)
                    {
                        Color c = bmp.GetPixel(i, j);
                        string cultivoDetectado = "No detectado";

                        // Verificar qué cultivo es el más cercano en color
                        foreach (var cultivo in cultivosColores)
                        {
                            var rango = cultivo.Value;
                            if (c.R >= rango.MinR && c.R <= rango.MaxR &&
                                c.G >= rango.MinG && c.G <= rango.MaxG &&
                                c.B >= rango.MinB && c.B <= rango.MaxB)
                            {
                                cultivoDetectado = cultivo.Key;
                                break;
                            }
                        }

                        // Marcar el píxel de acuerdo al cultivo detectado
                        if (cultivoDetectado == "Maíz" && cultivoDetectado == cadena)
                        {
                            bmp2.SetPixel(i, j, Color.Yellow);  // Maíz en color amarillo
                        }
                        else if (cultivoDetectado == "Trigo" && cultivoDetectado == cadena)
                        {
                            bmp2.SetPixel(i, j, Color.Blue);  // Trigo en color marrón claro
                        }
                        else if (cultivoDetectado == "Soja" && cultivoDetectado == cadena)
                        {
                            bmp2.SetPixel(i, j, Color.Red);  // Soja en color verde
                        }
                        else if (cultivoDetectado == "Arroz" && cultivoDetectado == cadena)
                        {
                            bmp2.SetPixel(i, j, Color.LightGreen);  // Arroz en color verde claro
                        }
                        else
                        {
                            bmp2.SetPixel(i, j, Color.FromArgb(c.R, c.G, c.B));  // No se detecta cultivo, mantener color original
                        }
                    }
                }

                // Mostrar la imagen resultante con los cultivos clasificados por colores
                pictureBox2.Image = bmp2;
            }
            
        }
    }
    public class RangoColor
    {
        public int MinR { get; set; }
        public int MaxR { get; set; }
        public int MinG { get; set; }
        public int MaxG { get; set; }
        public int MinB { get; set; }
        public int MaxB { get; set; }

        // Constructor
        public RangoColor(int minR, int maxR, int minG, int maxG, int minB, int maxB)
        {
            MinR = minR;
            MaxR = maxR;
            MinG = minG;
            MaxG = maxG;
            MinB = minB;
            MaxB = maxB;
        }
    }

}
