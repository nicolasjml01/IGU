using PracticaFinal;
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

            // Si el Canvas ya tiene dimensiones válidas, dibuja el gráfico
            if (GraficoCanvas.ActualWidth > 0 && GraficoCanvas.ActualHeight > 0)
            {
                DibujarGrafico();
            }
            // Seleccionamos la fecha de la ejecucion seleccionada
            EjecucionesDataGrid.SelectionChanged += CargarDatosFecha;
        }
        // Seleccion de fecha en ventana secundaria
        private void CargarDatosFecha(object sender, SelectionChangedEventArgs e)
        {
            // Obtenemos el objeto seleccionado del DataGrid
            Ejecuciones ejecucionSeleccionada = EjecucionesDataGrid.SelectedItem as Ejecuciones;

            // Validamos que no sea nulo
            if (ejecucionSeleccionada != null) 
            {
                // Cogemos solo la fecha
                DateTime fecha = ejecucionSeleccionada.FechayHora.Date;

                // Llamamos a la ventana principal para pasar la fecha seleccionada
                MainWindow ventanaPrincipal = Application.Current.MainWindow as MainWindow;

                if (ventanaPrincipal != null)
                {
                    ventanaPrincipal.ActualizarFechaDesdeVentanaSecundaria(fecha);
                }
            }
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

            // Si el Canvas ya tiene dimensiones válidas, dibuja el gráfico
            if (GraficoCanvas.ActualWidth > 0 && GraficoCanvas.ActualHeight > 0)
            {
                DibujarGrafico();
            }
        }

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

                // Si el Canvas ya tiene dimensiones válidas, dibuja el gráfico
                if (GraficoCanvas.ActualWidth > 0 && GraficoCanvas.ActualHeight > 0)
                {
                    DibujarGrafico();
                }

                // Recargar los datos para la fecha de la nueva ejecución
                CargarDatosFechaAñadidaEliminar(nuevaEjecucion.FechayHora.Date);

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
                    ejecuciones.Remove(ejecucionSeleccionada);

                    // Si el Canvas ya tiene dimensiones válidas, dibuja el gráfico
                    if (GraficoCanvas.ActualWidth > 0 && GraficoCanvas.ActualHeight > 0)
                    {
                        DibujarGrafico();
                    }

                    // Recargar los datos para la fecha de la ejecución eliminada
                    CargarDatosFechaAñadidaEliminar(ejecucionSeleccionada.FechayHora.Date);
                    MessageBox.Show("Ejecución eliminada.");
                }
            }
            else
            {
                MessageBox.Show("Selecciona una ejecución para eliminar.");
            }
        }

        // Recalcular la ventana Daily Insigth con la fecha añadida o eliminada
        private void CargarDatosFechaAñadidaEliminar(DateTime fecha)
        {
            MainWindow ventanaPrincipal = Application.Current.MainWindow as MainWindow;

            if (ventanaPrincipal != null)
            {
                // Pasar la fecha a la ventana principal
                ventanaPrincipal.ActualizarFechaDesdeVentanaSecundaria(fecha);
            }
        }

        // Actualizacion del tamaño
        private void GraficoCanvas_TamañoCambiado(object sender, SizeChangedEventArgs e)
        {
            DibujarGrafico();
        }

        // Ventana de Gráficos
        private void DibujarGrafico()
        {
            // Limpiamos el Canvas
            GraficoCanvas.Children.Clear();

            // Ordenar los datos por fecha
            var datosOrdenados = ejecucionesFiltradas.OrderBy(e => e.FechayHora).ToList();
            if (!datosOrdenados.Any()) return;

            // Dimensiones del gráfico
            double altoCanvas = GraficoCanvas.ActualHeight;
            double margen = 50;
            double anchoBarra = 30;

            // Agrupar datos por fecha
            var gruposPorFecha = datosOrdenados.GroupBy(e => e.FechayHora.Date).OrderBy(g => g.Key).ToList();

            // Calcular el espacio total requerido
            int maxBarrasPorGrupo = gruposPorFecha.Max(g => g.Count());
            double espacioEntreGrupos = 20; // Espacio mínimo entre grupos
            double anchoRequerido = margen * 2 + gruposPorFecha.Count * maxBarrasPorGrupo * anchoBarra +
                                    (gruposPorFecha.Count - 1) * espacioEntreGrupos;

            // Ajustar el ancho del Canvas dinámicamente
            GraficoCanvas.Width = Math.Max(GraficoCanvas.ActualWidth, anchoRequerido);

            // Valores máximos
            int maxReps = datosOrdenados.Max(e => e.Repeticiones);
            int maxPeso = datosOrdenados.Max(e => e.Peso);

            // Dibujar ejes
            DibujarEjes(margen, GraficoCanvas.Width, altoCanvas, maxReps, maxPeso);

            // Dibujar barras
            DibujarBarras(datosOrdenados, margen, GraficoCanvas.Width, altoCanvas, maxReps, maxPeso);

            // Dibujar etiquetas del eje X
            DibujarEtiquetasX(datosOrdenados, margen, altoCanvas);
        }

        private void DibujarEjes(double margen, double anchoCanvas, double altoCanvas, int maxReps, int maxPeso)
        {
            // Dimensiones para calcular escalas
            double altoDisponible = altoCanvas - 2 * margen;
            int numLineas = 10; // Número de divisiones en las escalas

            // Dibujar el eje X
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

            // Dibujar el eje Y Repeticiones
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

            // Escala y etiquetas para el eje Y izquierdo
            for (int i = 0; i <= numLineas; i++)
            {
                double y = altoCanvas - margen - (altoDisponible / numLineas) * i;
                double valor = maxReps * i / numLineas;

                // Etiqueta de la escala
                TextBlock etiquetaReps = new TextBlock
                {
                    Text = valor.ToString("0"),
                    Foreground = Brushes.Red
                };
                Canvas.SetLeft(etiquetaReps, margen - 40);
                Canvas.SetTop(etiquetaReps, y - 10);
                GraficoCanvas.Children.Add(etiquetaReps);

                // Línea guía opcional (BORRAR?)
                Line lineaGuia = new Line
                {
                    X1 = margen,
                    Y1 = y,
                    X2 = anchoCanvas - margen,
                    Y2 = y,
                    Stroke = Brushes.Gray,
                    StrokeDashArray = new DoubleCollection { 2, 2 }
                };
                GraficoCanvas.Children.Add(lineaGuia);
            }

            // Dibujar el eje Y Pesos
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

            // Escala y etiquetas para el eje Y derecho
            for (int i = 0; i <= numLineas; i++)
            {
                double y = altoCanvas - margen - (altoDisponible / numLineas) * i;
                double valor = maxPeso * i / numLineas;

                // Etiqueta de la escala
                TextBlock etiquetaPeso = new TextBlock
                {
                    Text = valor.ToString("0"),
                    Foreground = Brushes.Blue
                };
                Canvas.SetLeft(etiquetaPeso, anchoCanvas - margen + 10);
                Canvas.SetTop(etiquetaPeso, y - 10);
                GraficoCanvas.Children.Add(etiquetaPeso);
            }

            // Etiqueta para el eje Y izquierdo (Repeticiones)
            TextBlock etiquetaEjeYReps = new TextBlock
            {
                Text = "Repeticiones",
                FontSize = 12,
                Foreground = Brushes.Red
            };
            Canvas.SetLeft(etiquetaEjeYReps, margen - 50);
            Canvas.SetTop(etiquetaEjeYReps, margen / 2);
            GraficoCanvas.Children.Add(etiquetaEjeYReps);

            // Etiqueta para el eje Y derecho (Peso en kg)
            TextBlock etiquetaEjeYPeso = new TextBlock
            {
                Text = "Peso (kg)",
                FontSize = 12,
                Foreground = Brushes.Blue
            };
            Canvas.SetLeft(etiquetaEjeYPeso, anchoCanvas - margen + 10);
            Canvas.SetTop(etiquetaEjeYPeso, margen / 2);
            GraficoCanvas.Children.Add(etiquetaEjeYPeso);
        }

        private void DibujarBarras(List<Ejecuciones> datosOrdenados, double margen, double anchoCanvas, double altoCanvas, int maxReps, int maxPeso)
        {
            double anchoBarra = 30;          // Ancho de cada barra
            double altoDisponible = altoCanvas - 2 * margen;

            // Agrupar ejecuciones por fecha
            var gruposPorFecha = datosOrdenados
                .GroupBy(e => e.FechayHora.Date)
                .OrderBy(g => g.Key)
                .ToList();

            // Calcular el espacio necesario para cada grupo
            int totalGrupos = gruposPorFecha.Count;
            int maxBarrasPorGrupo = gruposPorFecha.Max(g => g.Count());
            double anchoNecesarioPorGrupo = maxBarrasPorGrupo * anchoBarra;

            // Calcular espacio dinámico entre grupos
            double espacioEntreGrupos = (anchoCanvas - 2 * margen - totalGrupos * anchoNecesarioPorGrupo) /
                                         Math.Max(1, totalGrupos - 1);

            // Evitar que el espacio sea negativo
            espacioEntreGrupos = Math.Max(10, espacioEntreGrupos);

            // Lista de puntos para la línea
            List<Point> puntosLinea = new List<Point>();

            for (int i = 0; i < totalGrupos; i++)
            {
                var grupo = gruposPorFecha[i];
                var ejecuciones = grupo.ToList();

                // Posición base X para el grupo
                double xBase = margen + i * (anchoNecesarioPorGrupo + espacioEntreGrupos);

                for (int j = 0; j < ejecuciones.Count; j++)
                {
                    var ejecucion = ejecuciones[j];

                    // Altura de las barras
                    double alturaReps = altoDisponible * ejecucion.Repeticiones / maxReps;
                    double alturaPeso = altoDisponible * ejecucion.Peso / maxPeso;

                    // Posición de la barra
                    double xBarra = xBase + j * anchoBarra;

                    // Dibujar barra de repeticiones
                    Rectangle barraReps = new Rectangle
                    {
                        Width = anchoBarra,
                        Height = alturaReps,
                        Fill = Brushes.Red
                    };
                    Canvas.SetLeft(barraReps, xBarra);
                    Canvas.SetTop(barraReps, altoCanvas - margen - alturaReps);
                    GraficoCanvas.Children.Add(barraReps);

                    // Calcular punto del gráfico según el peso
                    double xPunto = xBarra + anchoBarra / 2;  // Centrar el punto sobre la barra
                    double yPunto = altoCanvas - margen - alturaPeso; // Ajustar la posición Y según el peso

                    // Dibujar un punto en el gráfico (un círculo pequeño)
                    Ellipse punto = new Ellipse
                    {
                        Width = 8, // Tamaño del punto
                        Height = 8,
                        Fill = Brushes.Blue // Color del punto
                    };
                    Canvas.SetLeft(punto, xPunto - 4); // Centrar el punto sobre la coordenada X
                    Canvas.SetTop(punto, yPunto - 4);  // Centrar el punto sobre la coordenada Y
                    GraficoCanvas.Children.Add(punto);

                    // Guardar el punto para la línea
                    puntosLinea.Add(new Point(xPunto, yPunto));
                }
            }

            // Dibujar la línea conectando los puntos
            Polyline linea = new Polyline
            {
                Stroke = Brushes.Blue,      // Color de la línea
                StrokeThickness = 2,        // Grosor de la línea
                StrokeDashArray = new DoubleCollection() { 5, 3 }  // Define el patrón de la línea discontinua
            };

            linea.Points = new PointCollection(puntosLinea);
            GraficoCanvas.Children.Add(linea);
        }


        private void DibujarEtiquetasX(List<Ejecuciones> datosOrdenados, double margen, double altoCanvas)
        {
            // Obtener fechas únicas ordenadas
            var fechasUnicas = datosOrdenados
                .Select(e => e.FechayHora.Date)
                .Distinct()
                .OrderBy(fecha => fecha)
                .ToList();

            // Calcular espacio entre grupos de acuerdo con el ancho del Canvas
            double anchoCanvas = GraficoCanvas.Width;
            double anchoBarra = 30; // Ancho de cada barra
            var gruposPorFecha = datosOrdenados
                .GroupBy(e => e.FechayHora.Date)
                .OrderBy(g => g.Key)
                .ToList();

            int maxBarrasPorGrupo = gruposPorFecha.Max(g => g.Count());
            double espacioEntreGrupos = (anchoCanvas - 2 * margen - maxBarrasPorGrupo * anchoBarra * gruposPorFecha.Count) /
                                         (gruposPorFecha.Count - 1);

            espacioEntreGrupos = Math.Max(10, espacioEntreGrupos); // Asegurar un mínimo espacio

            // Dibujar etiquetas para cada fecha
            for (int i = 0; i < fechasUnicas.Count; i++)
            {
                var fecha = fechasUnicas[i];

                // Calcular la posición X de la etiqueta (igual al cálculo base de las barras)
                double xBase = margen + i * (anchoBarra * maxBarrasPorGrupo + espacioEntreGrupos);

                // Etiqueta de la fecha
                TextBlock etiquetaFecha = new TextBlock
                {
                    Text = fecha.ToString("dd/MM/yyyy"),
                    FontSize = 10,
                    Foreground = Brushes.Black,
                    TextAlignment = TextAlignment.Center
                };

                // Ajustar posición
                Canvas.SetLeft(etiquetaFecha, xBase - anchoBarra / 2); // Centrar la etiqueta
                Canvas.SetTop(etiquetaFecha, altoCanvas - 30);        // Debajo del eje X
                GraficoCanvas.Children.Add(etiquetaFecha);
            }
        }











    }
}

