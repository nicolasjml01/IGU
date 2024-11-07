using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PracticaFinal
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Colección de ejercicios
        private ObservableCollection<Ejercicios> ejercicios;
        public MainWindow()
        {
            InitializeComponent();

            // Inicializar la colección con datos predeterminados
            ejercicios = new ObservableCollection<Ejercicios>
            {
                new Ejercicios { Nombre = "Plancha", Descripcion = "Un ejercicio isométrico para trabajar el core, especialmente los abdominales.", GruposMusculares = " Core" },
                new Ejercicios { Nombre = "Curl de Bíceps", Descripcion = "Un ejercicio simple pero efectivo para desarrollar los brazos, especialmente los bíceps.", GruposMusculares = "Brazos"},
                new Ejercicios { Nombre = "Press de banca ", Descripcion = "Este ejercicio se realiza en una máquina guiada y permite trabajar los músculos del pecho con mayor control.", GruposMusculares = "Pecho"},
                new Ejercicios { Nombre = "Jalón al pecho", Descripcion = "Un ejercicio en máquina para trabajar la espalda, especialmente el dorsal ancho.", GruposMusculares = "Espalda" },
                new Ejercicios { Nombre = "Prensa de pierna", Descripcion = "Una máquina guiada para trabajar los músculos de las piernas, especialmente los cuádriceps.", GruposMusculares = "Piernas" },
                new Ejercicios { Nombre = "Extensión de pierna", Descripcion = "Este ejercicio se enfoca en el desarrollo de los cuádriceps mediante una máquina guiada.", GruposMusculares = "Piernas" },
                new Ejercicios { Nombre = "Press de hombros ", Descripcion = "Un ejercicio para trabajar los hombros utilizando una máquina guiada.", GruposMusculares = "Brazos" }
            };

            // Enlazar datos al datagrid
            EjerciciosDataGrid.ItemsSource = ejercicios;

        }

        // Método para añadir un ejercicio
        private void Añadir_Click(object sender, RoutedEventArgs e)
        {
            Ejercicios nuevoEjercicio = new Ejercicios { Nombre = "Nuevo Ejercicio", Descripcion = "Descripción...", GruposMusculares = "Grupos..." };
            ejercicios.Add(nuevoEjercicio);
        }

        // Método para modificar el ejercicio seleccionado
        private void Modificar_Click(object sender, RoutedEventArgs e)
        {
            Ejercicios ejercicioSeleccionado = EjerciciosDataGrid.SelectedItem as Ejercicios;
            if (ejercicioSeleccionado != null)
            {
                // Ejemplo de cambio; en una aplicación real podrías abrir una ventana para editar el ejercicio
                ejercicioSeleccionado.Nombre = "Ejercicio Modificado";
                ejercicioSeleccionado.Descripcion = "Descripción modificada";
                ejercicioSeleccionado.GruposMusculares = "Grupos modificados";
                EjerciciosDataGrid.Items.Refresh(); // Actualiza el DataGrid
            }
            else
            {
                MessageBox.Show("Selecciona un ejercicio para modificar.");
            }
        }

        // Método para eliminar el ejercicio seleccionado
        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            Ejercicios ejercicioSeleccionado = EjerciciosDataGrid.SelectedItem as Ejercicios;
            if (ejercicioSeleccionado != null)
            {
                if ((MessageBox.Show("¿Estas seguro de querer eliminar este ejercicio?", "Eliminar", MessageBoxButton.YesNo) == MessageBoxResult.Yes))
                    ejercicios.Remove(ejercicioSeleccionado);
            }
            else
            {
                MessageBox.Show("Selecciona un ejercicio para eliminar.");
            }
        }
    }
}
