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
        public ObservableCollection<Ejecuciones> Ejecuciones { get; set; }
        public VentanaSecundaria(Ejercicios ejercicioSeleccionado, ObservableCollection<Ejecuciones> ejecuciones)
        {
            InitializeComponent();
            TituloVentana(ejercicioSeleccionado.Nombre);
            // Filtrar las ejecuciones según el ejercicio seleccionado
            Ejecuciones = new ObservableCollection<Ejecuciones>(
                ejecuciones.Where(e => e.Nombre == ejercicioSeleccionado.Nombre)
            );
            // Asignar la lista filtrada al DataGrid
            EjecucionesDataGrid.ItemsSource = Ejecuciones;
        }

        private void TituloVentana(string nombre)
        {
            this.Title = $"Detalles del ejercicio: {nombre}";
        }

        // Funcion utilizada para cambiar entre uno u otro ejercicio seleccionado
        public void ActualizarContenido(Ejercicios ejercicioSeleccionado, ObservableCollection<Ejecuciones> ejecuciones) 
        { 
            TituloVentana(ejercicioSeleccionado.Nombre);
            // Filtrar y actualizar las ejecuciones por nombre del ejercicio seleccionado
            Ejecuciones = new ObservableCollection<Ejecuciones>(
                ejecuciones.Where(e => e.Nombre == ejercicioSeleccionado.Nombre)
            );

            EjecucionesDataGrid.ItemsSource = Ejecuciones;
            EjecucionesDataGrid.Items.Refresh();
        }

        // Método para añadir
        // PROXIMA CONFIGURACIÓN (BORRAR)
        // Método para añadir una nueva ejecución
        private void AñadirEjecucion_Click(object sender, RoutedEventArgs e)
        {
            // Crear una nueva instancia de VentanaFormularioEjecuciones en modo "añadir"
            var ventanaFormulario = new VentanaFormularioEjecuciones(this.Title.Replace("Detalles del ejercicio: ", ""));

            // Mostrar la ventana y comprobar si el usuario confirmó la acción
            if (ventanaFormulario.ShowDialog() == true)
            {
                // Acceder a la ejecución creada a través de la propiedad EjecucionFormulario
                Ejecuciones nuevaEjecucion = ventanaFormulario.EjecucionFormulario;

                // Agregar la nueva ejecución a la colección observable
                Ejecuciones.Add(nuevaEjecucion);
            }
        }

        // Método para modificar una ejecución seleccionada
        private void ModificarEjecucion_Click(object sender, RoutedEventArgs e)
        {
            // Obtener la ejecución seleccionada del DataGrid
            var ejecucionSeleccionada = EjecucionesDataGrid.SelectedItem as Ejecuciones;

            if (ejecucionSeleccionada == null)
            {
                MessageBox.Show("Selecciona una ejecución para modificar.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Crear una nueva instancia de VentanaFormularioEjecuciones en modo "modificar"
            var ventanaFormulario = new VentanaFormularioEjecuciones(ejecucionSeleccionada);

            // Mostrar la ventana y comprobar si el usuario confirmó la acción
            if (ventanaFormulario.ShowDialog() == true)
            {
                // Los cambios ya estarán reflejados en ejecucionSeleccionada
                EjecucionesDataGrid.Items.Refresh(); // Refrescar el DataGrid para ver los cambios
            }
        }

        // Método para eliminar una ejecución seleccionada
        private void EliminarEjecucion_Click(object sender, RoutedEventArgs e)
        {
            var ejecucionSeleccionada = EjecucionesDataGrid.SelectedItem as Ejecuciones;

            if (ejecucionSeleccionada != null)
            {
                if (MessageBox.Show("¿Estás seguro de querer eliminar esta ejecución?", "Eliminar", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Ejecuciones.Remove(ejecucionSeleccionada);
                    MessageBox.Show("Ejecución eliminada.");
                }
            }
            else
            {
                MessageBox.Show("Selecciona una ejecución para eliminar.");
            }
        }

    }
}
