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
    /// Lógica de interacción para VentanaFormularioEjercicios.xaml
    /// </summary>
    public partial class VentanaFormularioEjercicios : Window
    {
        public Ejercicios nuevoEjercicio { get; private set; }
        private Ejercicios ejercicioEditado;

        // Contructor para crear un nuevo ejercicio
        public VentanaFormularioEjercicios()
        {
            InitializeComponent();
            // Esto indica que estamos creando un nuevo ejercicio
            ejercicioEditado = null;
        }

        // Constructor para modificar un ejercicio existente
        public VentanaFormularioEjercicios(Ejercicios ejercicio)
        {
            InitializeComponent();
            ejercicioEditado = ejercicio;

            // Llenar los controles con los datos del ejercicio
            NombreTextBox.Text = ejercicio.Nombre;
            DescripcionTextBox.Text = ejercicio.Descripcion;
            GruposMuscularesComboBox.SelectedItem = ejercicio.GruposMusculares; 
        }

        // Evento cuando se hace clic en "Aceptar"
        private void Aceptar_Click(object sender, RoutedEventArgs e)
        {
            // Validar que los campos no estén vacíos
            if (string.IsNullOrEmpty(NombreTextBox.Text) ||
                string.IsNullOrEmpty(DescripcionTextBox.Text) ||
                GruposMuscularesComboBox.SelectedItem == null)
            {
                MessageBox.Show("Por favor, rellene todos los campos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Si estamos editando un ejercicio, actualizamos el ejercicio original
            if (ejercicioEditado != null)
            {
                ejercicioEditado.Nombre = NombreTextBox.Text;
                ejercicioEditado.Descripcion = DescripcionTextBox.Text;

                // Obtener el valor seleccionado en el ComboBox
                var grupoSeleccionado = GruposMuscularesComboBox.SelectedItem as ComboBoxItem;
                ejercicioEditado.GruposMusculares = grupoSeleccionado?.Content.ToString();
            }
            else
            {
                // Obtener los valores de los controles
                string nombre = NombreTextBox.Text;
                string descripcion = DescripcionTextBox.Text;

                // Obtener el valor seleccionado en el ComboBox
                var grupoSeleccionado = GruposMuscularesComboBox.SelectedItem as ComboBoxItem;
                string grupoMuscular = grupoSeleccionado?.Content.ToString();

                // Crear el nuevo objeto Ejercicio
                nuevoEjercicio = new Ejercicios
                {
                    Nombre = nombre,
                    Descripcion = descripcion,
                    GruposMusculares = grupoMuscular
                };
            }

            // Cerrar la ventana y devolver la nueva ejecución
            this.DialogResult = true;
            this.Close();
        }

        // Evento cuando se hace clic en "Cancelar"
        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            // Cerrar la ventana sin realizar ninguna acción
            this.DialogResult = false;
            this.Close();
        }
    }
}
