using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace PracticaFinal
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Colección de ejercicios
        // MEJOR PORQUE DEJA MODIFICARLA (BORRAR)
        private ObservableCollection<Ejercicios> ejercicios;
        public ObservableCollection<Ejecuciones> ejecuciones { get; set; }
        private VentanaSecundaria ventanaSecundaria;

        public MainWindow()
        {
            InitializeComponent();
            CargarEjercicios();
            CargarEjecuciones();
            EjerciciosDataGrid.SelectionChanged += AbrirVentanaSecundaria;

        }

        // Método para abrir la ventana secundaria
        private void AbrirVentanaSecundaria(object sender, SelectionChangedEventArgs e)
        {
            // Usamos as para compropbar que seleccionamos un elemento en el datagrid de la clase Ejercicios (aunque en mi caso siempre lo es) (BORRAR)
            Ejercicios ejercicioSeleccionado = EjerciciosDataGrid.SelectedItem as Ejercicios;
            
            if (ejercicioSeleccionado != null)
            {
                // Verificamos si la ventana secundaria ya está abierta
                if (ventanaSecundaria != null && ventanaSecundaria.IsVisible)
                {
                    // Si la ventana secundaria ya está abierta, actualizamos su contenido
                    ventanaSecundaria.ActualizarContenido(ejercicioSeleccionado, ejecuciones);
                    ventanaSecundaria.Focus(); // Llevamos la ventana secundaria al frente
                }
                else
                {
                    // Si la ventana secundaria no está abierta, la creamos y mostramos
                    ventanaSecundaria = new VentanaSecundaria(ejercicioSeleccionado, ejecuciones);
                    ventanaSecundaria.Show();
                }
            }
        }

        // Método para cargar ejercicios predeterminados
        private void CargarEjercicios()
        {
            // Inicializar la colección con datos predeterminados
            ejercicios = new ObservableCollection<Ejercicios>
            {
                new Ejercicios { Nombre = "Plancha", Descripcion = "Un ejercicio isométrico para trabajar el core, especialmente los abdominales.", GruposMusculares = " Core" },
                new Ejercicios { Nombre = "Curl de Bíceps", Descripcion = "Un ejercicio simple pero efectivo para desarrollar los brazos, especialmente los bíceps.", GruposMusculares = "Brazos"},
                new Ejercicios { Nombre = "Press de banca ", Descripcion = "Este ejercicio se realiza en una máquina guiada y permite trabajar los músculos del pecho con mayor control.", GruposMusculares = "Pecho"},
                new Ejercicios { Nombre = "Jalón al pecho", Descripcion = "Un ejercicio en máquina para trabajar la espalda, especialmente el dorsal ancho.", GruposMusculares = "Espalda" },
                new Ejercicios { Nombre = "Prensa de pierna", Descripcion = "Una máquina guiada para trabajar los músculos de las piernas, especialmente los cuádriceps.", GruposMusculares = "Piernas" },
                new Ejercicios { Nombre = "Extensión de pierna", Descripcion = "Este ejercicio se enfoca en el desarrollo de los cuádriceps mediante una máquina guiada.", GruposMusculares = "Piernas" },
                new Ejercicios { Nombre = "Press de hombros ", Descripcion = "Un ejercicio para trabajar los hombros utilizando una máquina guiada.", GruposMusculares = "Brazos" },
            };

            // Enlazar datos al datagrid
            EjerciciosDataGrid.ItemsSource = ejercicios;
        }
        // Método para generar una lista de ejecuciones de ciertos ejercicios
        private void CargarEjecuciones()
        {
            ejecuciones = new ObservableCollection<Ejecuciones>
            {
                new Ejecuciones { Nombre = "Plancha", Repeticiones = 60, Peso = 0, FechayHora = new DateTime(2024, 10, 12, 12, 0, 0) },
                new Ejecuciones { Nombre = "Plancha", Repeticiones = 70, Peso = 0, FechayHora = new DateTime(2024, 10, 12, 12, 0, 0) },
                new Ejecuciones { Nombre = "Plancha", Repeticiones = 80, Peso = 0, FechayHora = new DateTime(2024, 10, 12, 12, 0, 0) },
                new Ejecuciones { Nombre = "Plancha", Repeticiones = 60, Peso = 0, FechayHora = new DateTime(2024, 10, 13, 12, 0, 0) },
                new Ejecuciones { Nombre = "Plancha", Repeticiones = 80, Peso = 0, FechayHora = new DateTime(2024, 10, 13, 12, 0, 0) },
                new Ejecuciones { Nombre = "Plancha", Repeticiones = 80, Peso = 0, FechayHora = new DateTime(2024, 10, 15, 12, 0, 0) },
                new Ejecuciones { Nombre = "Prensa de pierna", Repeticiones = 12, Peso = 100, FechayHora = new DateTime(2024, 10, 12, 12, 0, 0) },
                new Ejecuciones { Nombre = "Prensa de pierna", Repeticiones = 15, Peso = 110, FechayHora = new DateTime(2024, 10, 12, 12, 0, 0) },
                new Ejecuciones { Nombre = "Prensa de pierna", Repeticiones = 14, Peso = 115, FechayHora = new DateTime(2024, 10, 14, 12, 0, 0) },
                new Ejecuciones { Nombre = "Prensa de pierna", Repeticiones = 12, Peso = 120, FechayHora = new DateTime(2024, 10, 14, 12, 0, 0) },
                new Ejecuciones { Nombre = "Prensa de pierna", Repeticiones = 15, Peso = 125, FechayHora = new DateTime(2024, 10, 16, 12, 0, 0) },
            };
        }

        // Método para añadir un ejercicio
        // PROXIMA CONFIGURACIÓN (BORRAR)
        private void Añadir_Click(object sender, RoutedEventArgs e)
        {
            // Para los grupos musculares puedo ahcer que sea un desplegable y que pueda elegir 1 o mas grupos musculares
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
