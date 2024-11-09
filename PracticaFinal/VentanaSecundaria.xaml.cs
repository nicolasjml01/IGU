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
using System.Windows.Shapes;

namespace PracticaFinal
{
    /// <summary>
    /// Lógica de interacción para VentanaSecundaria.xaml
    /// </summary>
    public partial class VentanaSecundaria : Window
    {
        public ObservableCollection<Ejecuciones> Ejecuciones;
        public VentanaSecundaria(Ejercicios ejercicioSeleccionado)
        {
            InitializeComponent();
            TituloVentana(ejercicioSeleccionado.Nombre);
            DataContext = ejercicioSeleccionado;

            // Cargar los datos predeterminados
            EjecucionesDataGrid.ItemsSource = Ejecuciones;
        }

        private void TituloVentana(string nombre)
        {
            this.Title = $"Detalles del ejercicio: {nombre}";
        }

        public void ActualizarContenido(Ejercicios nuevoEjercicio) 
        { 
            DataContext = nuevoEjercicio;
            TituloVentana(nuevoEjercicio.Nombre);
        }
        private void CargarEjecuciones(ObservableCollection<Ejecuciones> Ejecuciones)
        {
            
        }

        // Método para añadir un ejercicio
        // PROXIMA CONFIGURACIÓN (BORRAR)
        private void Añadir_Click(object sender, RoutedEventArgs e)
        {
            // Para los grupos musculares puedo ahcer que sea un desplegable y que pueda elegir 1 o mas grupos musculares
            Ejercicios nuevoEjercicio = new Ejercicios { Nombre = "Nuevo Ejercicio", Descripcion = "Descripción...", GruposMusculares = "Grupos..." };
            ejecuciones.Add(nuevoEjercicio);
        }

        // Método para modificar el ejercicio seleccionado
        private void Modificar_Click(object sender, RoutedEventArgs e)
        {
            Ejercicios ejercicioSeleccionado = EjecucionesDataGrid.SelectedItem as Ejercicios;
            if (ejercicioSeleccionado != null)
            {
                // Ejemplo de cambio; en una aplicación real podrías abrir una ventana para editar el ejercicio
                ejercicioSeleccionado.Nombre = "Ejercicio Modificado";
                ejercicioSeleccionado.Descripcion = "Descripción modificada";
                ejercicioSeleccionado.GruposMusculares = "Grupos modificados";
                EjecucionesDataGrid.Items.Refresh(); // Actualiza el DataGrid
            }
            else
            {
                MessageBox.Show("Selecciona un ejercicio para modificar.");
            }
        }

        // Método para eliminar el ejercicio seleccionado
        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            Ejercicios ejercicioSeleccionado = EjecucionesDataGrid.SelectedItem as Ejercicios;
            if (ejercicioSeleccionado != null)
            {
                if ((MessageBox.Show("¿Estas seguro de querer eliminar este ejercicio?", "Eliminar", MessageBoxButton.YesNo) == MessageBoxResult.Yes))
                    ejecuciones.Remove(ejercicioSeleccionado);
            }
            else
            {
                MessageBox.Show("Selecciona un ejercicio para eliminar.");
            }
        }
    }
}
