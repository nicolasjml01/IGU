using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PracticaFinal
{
    /// <summary>
    /// Lógica de interacción para VentanaSecundaria.xaml
    /// </summary>
    public partial class VentanaSecundaria : Window
    {
        // Referencia a la lista general de ejecuciones
        private ObservableCollection<Ejecuciones> ejecuciones;
        public ObservableCollection<Ejecuciones> ejecucionesFiltradas { get; set; }
        public VentanaSecundaria(Ejercicios ejercicioSeleccionado, ObservableCollection<Ejecuciones> ejecuciones)
        {
            InitializeComponent();

            // Duplicamos los datos de ejecuciones
            this.ejecuciones = ejecuciones;

            // Filtrar las ejecuciones relacionadas con el ejercicio seleccionado
            ejecucionesFiltradas = new ObservableCollection<Ejecuciones>(
                ejecuciones.Where(e => e.Nombre == ejercicioSeleccionado.Nombre)
            // Funciona como un for, obtiene la lista de las ejecuciones del ejercicio seleccionado (BORRAR)
            // ejecucionesFiltradas convierte la lsita del where en una ObservableCollection (BORRAR)
            );

            // Asignar la lista filtrada al DataGrid
            EjecucionesDataGrid.ItemsSource = ejecucionesFiltradas;

            // Configurar el título de la ventana
            TituloVentana(ejercicioSeleccionado.Nombre);

            // Dibujar el gráfico
            GraficoCanvas.SizeChanged += GraficoCanvas_SizeChanged;
        }

        private void TituloVentana(string nombre)
        {
            this.Title = $"Detalles del ejercicio: {nombre}";
        }

        // Funcion utilizada para cambiar entre uno u otro ejercicio seleccionado
        public void ActualizarContenido(Ejercicios ejercicioSeleccionado, ObservableCollection<Ejecuciones> ejecuciones)
        {
            // Actualizamos el título de la ventana
            TituloVentana(ejercicioSeleccionado.Nombre);

            // Limpiamos la colección actual
            ejecucionesFiltradas.Clear();

            // Filtramos las ejecuciones relacionadas con el ejercicio seleccionado y las agregamos
            foreach (var ejecucion in ejecuciones.Where(e => e.Nombre == ejercicioSeleccionado.Nombre))
            {
                ejecucionesFiltradas.Add(ejecucion);
            }

            // Dibujar el gráfico
            GraficoCanvas.SizeChanged += GraficoCanvas_SizeChanged;
        }

        // Método para añadir
        // PROXIMA CONFIGURACIÓN (BORRAR)
        // Método para añadir una nueva ejecución
        private void AñadirEjecucion_Click(object sender, RoutedEventArgs e)
        {
            // Crear una nueva instancia de VentanaFormularioEjecuciones
            var ventanaFormulario = new VentanaFormularioEjecuciones(this.Title.Replace("Detalles del ejercicio: ", ""));

            // Mostrar la ventana y comprobar si el usuario confirmó la acción
            if (ventanaFormulario.ShowDialog() == true)
            {
                // Obtener la nueva ejecución del formulario
                var nuevaEjecucion = ventanaFormulario.NuevaEjecucion;

                // Añadir la nueva ejecución a la lista filtrada de ejecuciones
                ejecucionesFiltradas.Add(nuevaEjecucion);

                // Añadir la nueva ejecución a la lista general de ejecuciones
                ejecuciones.Add(nuevaEjecucion);

                // Dibujar el gráfico
                GraficoCanvas.SizeChanged += GraficoCanvas_SizeChanged;

                MessageBox.Show("Ejecución añadida correctamente.");
            }
            else MessageBox.Show("ERROR, no se añadio ninguna ejecucion");
        }

        // Método para eliminar una ejecución seleccionada
        private void EliminarEjecucion_Click(object sender, RoutedEventArgs e)
        {
            var ejecucionSeleccionada = EjecucionesDataGrid.SelectedItem as Ejecuciones;

            if (ejecucionSeleccionada != null)
            {
                if (MessageBox.Show("¿Estás seguro de querer eliminar esta ejecución?", "Eliminar", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    ejecucionesFiltradas.Remove(ejecucionSeleccionada);
                    // Añadir la nueva ejecución a la lista general de ejecuciones
                    ejecuciones.Remove(ejecucionSeleccionada);
                    // Dibujar el gráfico
                    GraficoCanvas.SizeChanged += GraficoCanvas_SizeChanged;

                    MessageBox.Show("Ejecución eliminada.");
                }
            }
            else
            {
                MessageBox.Show("Selecciona una ejecución para eliminar.");
            }
        }


        // Ventana de Gráficos
        private void GraficoCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            

            // Funcion de dibujar
            DibujarGrafico();
        }
        private void DibujarGrafico() 
        {
            // Limpiamos el canvas
            GraficoCanvas.Children.Clear();

            // Ordenar los datos por fecha
            var datosOrdenados = ejecucionesFiltradas.OrderBy(e => e.FechayHora).ToList();
            if (!datosOrdenados.Any()) return;

            // Dimensiones del gráfico
            double anchoCanvas = GraficoCanvas.ActualWidth;
            double altoCanvas = GraficoCanvas.ActualHeight;
            double margen = 50;

            // Canvas suficientemente grande
            double anchoTotal = (datosOrdenados.Count * 60) + margen * 2;  // 60px de ancho por cada punto (BORRAR)
            GraficoCanvas.Width = Math.Max(anchoCanvas, anchoTotal);  // Ajustar el ancho del canvas si es necesario (BORRAR)

            // Valores máximos
            int maxReps = datosOrdenados.Max(d => d.Repeticiones);
            int maxPeso = datosOrdenados.Max(d => d.Peso);

            // Escalas
            double escalaReps = (altoCanvas - 2 * margen) / maxReps;
            double escalaPeso = (altoCanvas - 2 * margen) / maxPeso;
            double anchoBarra = 60;  // Ancho fijo por barra (BORRAR)

            // Dibujar ejes
            DibujarEjes(margen, anchoCanvas, altoCanvas);

            // Dibujar las barras de repeticiones (llamada aquí)
            DibujarBarras(margen, altoCanvas, datosOrdenados);

        }

        private void DibujarEjes(double margen, double anchoCanvas, double altoCanvas)
        {
            // Eje X
            Line ejeX = new Line
            {
                X1 = margen,
                Y1 = altoCanvas - margen,
                X2 = anchoCanvas - margen,
                Y2 = altoCanvas - margen,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };
            GraficoCanvas.Children.Add(ejeX);

            // Añadir las etiquetas del eje X (Fechas de las ejecuciones)
            double espacioX = (anchoCanvas - 2 * margen) / ejecucionesFiltradas.Count; // Distancia entre cada barra
            for (int i = 0; i < ejecucionesFiltradas.Count; i++)
            {
                var fecha = ejecucionesFiltradas[i].FechayHora.Date.ToString("dd/MM/yyyy");
                double xPos = margen + (i * espacioX);

                // Crear la etiqueta de la fecha
                TextBlock textoFecha = new TextBlock
                {
                    Text = fecha,
                    FontSize = 10,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Top
                };
                Canvas.SetLeft(textoFecha, xPos - 25); // Centrar texto en la barra
                Canvas.SetTop(textoFecha, altoCanvas - margen + 5); // Colocar debajo del eje X
                GraficoCanvas.Children.Add(textoFecha);
            }

            // Eje Y (reps)
            Line ejeYReps = new Line
            {
                X1 = margen,
                Y1 = margen,
                X2 = margen,
                Y2 = altoCanvas - margen,
                Stroke = Brushes.Red,
                StrokeThickness = 2
            };
            GraficoCanvas.Children.Add(ejeYReps);

            // Eje Y (peso)
            Line ejeYPeso = new Line
            {
                X1 = anchoCanvas - margen,
                Y1 = margen,
                X2 = anchoCanvas - margen,
                Y2 = altoCanvas - margen,
                Stroke = Brushes.Blue,
                StrokeThickness = 2
            };
            GraficoCanvas.Children.Add(ejeYPeso);

        }

        private void DibujarBarras(double margen, double altoCanvas, List<Ejecuciones> datosOrdenados)
        {
            double anchoBarra = 60;  // Ancho fijo de la barra
            double espacioEntreBarras = 10;  // Espacio entre las barras (ajustable)

            for (int i = 0; i < datosOrdenados.Count; i++)
            {
                var ejecucion = datosOrdenados[i];

                // Calcular la posición en el eje X (basado en el índice y el espacio entre barras)
                double x = margen + (i * (anchoBarra + espacioEntreBarras));

                // Calcular la altura de la barra (proporcional a las repeticiones)
                double alturaBarra = ejecucion.Repeticiones * (altoCanvas - 2 * margen) / datosOrdenados.Max(d => d.Repeticiones);

                // Dibujar la barra
                Rectangle barra = new Rectangle
                {
                    Width = anchoBarra,
                    Height = alturaBarra,
                    Fill = Brushes.Red, // Color de la barra
                    Margin = new Thickness(x, altoCanvas - margen - alturaBarra, 0, 0)  // Ajustar la posición
                };
                GraficoCanvas.Children.Add(barra);
            }
        }

    }
}
