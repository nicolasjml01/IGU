using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PracticaFinal
{
    /// <summary>
    /// Lógica de interacción para VentanaFormularioEjecuciones.xaml
    /// </summary>
    public partial class VentanaFormularioEjecuciones : Window
    {
        public Ejecuciones EjecucionFormulario { get; set; }
        private bool modificacion;

        // Constructor para añadir una nueva ejecución
        public VentanaFormularioEjecuciones(string nombreEjercicio)
        {
            this.Title = $"Añadir Ejecucion";
            InitializeComponent();
            modificacion = false;
            EjecucionFormulario = new Ejecuciones { Nombre = nombreEjercicio };
        }

        // Constructor para modificar una ejecución existente
        public VentanaFormularioEjecuciones(Ejecuciones ejecucionExistente)
        {
            this.Title = $"Modificar Ejecucion";
            InitializeComponent();
            modificacion = true;
            EjecucionFormulario = ejecucionExistente;

            // Cargar los valores existentes en los campos de texto
            RepeticionesTextBox.Text = EjecucionFormulario.Repeticiones.ToString();
            PesoTextBox.Text = EjecucionFormulario.Peso.ToString();
            FechaPicker.SelectedDate = EjecucionFormulario.Fecha;
        }

        private void Aceptar_Click(object sender, RoutedEventArgs e)
        {
            // Validar que se hayan ingresado todos los campos
            if (string.IsNullOrWhiteSpace(RepeticionesTextBox.Text) ||
                string.IsNullOrWhiteSpace(PesoTextBox.Text) ||
                !FechaPicker.SelectedDate.HasValue)
            {
                MessageBox.Show("Por favor, completa todos los campos.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Validar que Repeticiones y Peso sean números válidos
            if (!int.TryParse(RepeticionesTextBox.Text, out int repeticiones) ||
                !int.TryParse(PesoTextBox.Text, out int peso))
            {
                MessageBox.Show("Repeticiones y Peso deben ser valores numéricos.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Asignar los valores ingresados a la ejecución
            EjecucionFormulario.Repeticiones = repeticiones;
            EjecucionFormulario.Peso = peso;
            EjecucionFormulario.Fecha = FechaPicker.SelectedDate.Value;

            DialogResult = true; // Indicar que el formulario se cerrará con éxito
            Close();
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false; // Indicar que se canceló la acción
            Close();
        }
    }
}
