using System;
using System.Windows;

namespace PracticaFinal
{
    /// <summary>
    /// Lógica de interacción para VentanaFormularioEjecuciones.xaml
    /// </summary>
    public partial class VentanaFormularioEjecuciones : Window
    {
        public Ejecuciones NuevaEjecucion { get; set; }
        private string nombreEjercicio;

        // Constructor para añadir una nueva ejecución
        public VentanaFormularioEjecuciones(string nombreEjercicio)
        {
            this.Title = $"Añadir Ejecucion";
            InitializeComponent();
            this.nombreEjercicio = nombreEjercicio;
        }

        private void Aceptar_Click(object sender, RoutedEventArgs e)
        {
            // Validar que se hayan ingresado todos los campos
            if (string.IsNullOrWhiteSpace(RepeticionesTextBox.Text) ||
                string.IsNullOrWhiteSpace(PesoTextBox.Text) ||
                !FechaPicker.SelectedDate.HasValue ||
                string.IsNullOrWhiteSpace(HoraTextBox.Text))
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

            // Validar que la hora sea válida
            if (!TimeSpan.TryParse(HoraTextBox.Text, out TimeSpan hora))
            {
                MessageBox.Show("La hora debe estar en un formato válido (HH:mm).", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Crear una nueva instancia de Ejecuciones con los datos ingresados
            NuevaEjecucion = new Ejecuciones
            {
                Nombre = nombreEjercicio,
                // Asignar los valores ingresados a la ejecución
                Repeticiones = repeticiones,
                Peso = peso,
                FechayHora = FechaPicker.SelectedDate.Value
            };

            // Confirmar la acción
            DialogResult = true;
            Close();
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false; // Indicar que se canceló la acción
            Close();
        }
    }
}
