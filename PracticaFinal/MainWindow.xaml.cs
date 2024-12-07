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
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Colección de ejercicios y ejecuciones
        private ObservableCollection<Ejercicios> ejercicios;
        public ObservableCollection<Ejecuciones> ejecuciones { get; set; }
        private VentanaSecundaria ventanaSecundaria;

        // Valor para cargar la ventana DailyInsigth
        public DateTime? FechaSeleccionada { get; private set; } = null; // ? permite que tenga valor nulo


        public MainWindow()
        {
            InitializeComponent();
            CargarEjercicios();
            CargarEjecuciones();
            // Ventana Daily Insigth
            VenatanaDailyInsigthNeutro();
            // Ventana Secundaria
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
                new Ejercicios { Nombre = "Plancha", Descripcion = "Un ejercicio isométrico para trabajar el core, especialmente los abdominales.", GruposMusculares = "Core" },
                new Ejercicios { Nombre = "Curl de Bíceps", Descripcion = "Un ejercicio simple pero efectivo para desarrollar los brazos, especialmente los bíceps.", GruposMusculares = "Brazos"},
                new Ejercicios { Nombre = "Press de banca", Descripcion = "Este ejercicio se realiza en una máquina guiada y permite trabajar los músculos del pecho con mayor control.", GruposMusculares = "Pecho"},
                new Ejercicios { Nombre = "Jalón al pecho", Descripcion = "Un ejercicio en máquina para trabajar la espalda, especialmente el dorsal ancho.", GruposMusculares = "Espalda" },
                new Ejercicios { Nombre = "Prensa de pierna", Descripcion = "Una máquina guiada para trabajar los músculos de las piernas, especialmente los cuádriceps.", GruposMusculares = "Piernas" },
                new Ejercicios { Nombre = "Extensión de pierna", Descripcion = "Este ejercicio se enfoca en el desarrollo de los cuádriceps mediante una máquina guiada.", GruposMusculares = "Piernas" },
                new Ejercicios { Nombre = "Press de hombros", Descripcion = "Un ejercicio para trabajar los hombros utilizando una máquina guiada.", GruposMusculares = "Brazos" },
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
                new Ejecuciones { Nombre = "Plancha", Repeticiones = 30, Peso = 10, FechayHora = new DateTime(2024, 10, 15, 12, 30, 0) },
                new Ejecuciones { Nombre = "Prensa de pierna", Repeticiones = 12, Peso = 100, FechayHora = new DateTime(2024, 10, 12, 12, 0, 0) },
                new Ejecuciones { Nombre = "Prensa de pierna", Repeticiones = 15, Peso = 110, FechayHora = new DateTime(2024, 10, 12, 12, 0, 0) },
                new Ejecuciones { Nombre = "Prensa de pierna", Repeticiones = 14, Peso = 115, FechayHora = new DateTime(2024, 10, 14, 12, 0, 0) },
                new Ejecuciones { Nombre = "Prensa de pierna", Repeticiones = 12, Peso = 120, FechayHora = new DateTime(2024, 10, 14, 12, 0, 0) },
                new Ejecuciones { Nombre = "Prensa de pierna", Repeticiones = 15, Peso = 125, FechayHora = new DateTime(2024, 10, 16, 12, 0, 0) },
                new Ejecuciones { Nombre = "Curl de Bíceps", Repeticiones = 12, Peso = 100, FechayHora = new DateTime(2024, 10, 12, 12, 0, 0) },
                new Ejecuciones { Nombre = "Curl de Bíceps", Repeticiones = 15, Peso = 110, FechayHora = new DateTime(2024, 10, 12, 12, 0, 0) },
                new Ejecuciones { Nombre = "Curl de Bíceps", Repeticiones = 14, Peso = 115, FechayHora = new DateTime(2024, 10, 14, 12, 0, 0) },
                new Ejecuciones { Nombre = "Curl de Bíceps", Repeticiones = 12, Peso = 120, FechayHora = new DateTime(2024, 10, 14, 12, 0, 0) },
                new Ejecuciones { Nombre = "Curl de Bíceps", Repeticiones = 15, Peso = 125, FechayHora = new DateTime(2024, 10, 16, 12, 0, 0) },
                new Ejecuciones { Nombre = "Press de hombros", Repeticiones = 12, Peso = 100, FechayHora = new DateTime(2024, 10, 12, 12, 0, 0) },
                new Ejecuciones { Nombre = "Press de hombros", Repeticiones = 15, Peso = 110, FechayHora = new DateTime(2024, 10, 12, 12, 0, 0) },
                new Ejecuciones { Nombre = "Press de hombros", Repeticiones = 14, Peso = 115, FechayHora = new DateTime(2024, 10, 14, 12, 0, 0) },
                new Ejecuciones { Nombre = "Press de hombros", Repeticiones = 12, Peso = 120, FechayHora = new DateTime(2024, 10, 14, 12, 0, 0) },
                new Ejecuciones { Nombre = "Press de hombros", Repeticiones = 15, Peso = 125, FechayHora = new DateTime(2024, 10, 16, 12, 0, 0) },
                new Ejecuciones { Nombre = "Press de banca", Repeticiones = 12, Peso = 100, FechayHora = new DateTime(2024, 10, 12, 12, 0, 0) },
                new Ejecuciones { Nombre = "Press de banca", Repeticiones = 15, Peso = 110, FechayHora = new DateTime(2024, 10, 12, 12, 0, 0) },
                new Ejecuciones { Nombre = "Press de banca", Repeticiones = 14, Peso = 115, FechayHora = new DateTime(2024, 10, 14, 12, 0, 0) },
                new Ejecuciones { Nombre = "Press de banca", Repeticiones = 12, Peso = 120, FechayHora = new DateTime(2024, 10, 14, 12, 0, 0) },
                new Ejecuciones { Nombre = "Press de banca", Repeticiones = 15, Peso = 125, FechayHora = new DateTime(2024, 10, 16, 12, 0, 0) },
                new Ejecuciones { Nombre = "Jalón al pecho", Repeticiones = 12, Peso = 100, FechayHora = new DateTime(2024, 10, 12, 12, 0, 0) },
                new Ejecuciones { Nombre = "Jalón al pecho", Repeticiones = 15, Peso = 110, FechayHora = new DateTime(2024, 10, 12, 12, 0, 0) },
                new Ejecuciones { Nombre = "Jalón al pecho", Repeticiones = 14, Peso = 115, FechayHora = new DateTime(2024, 10, 14, 12, 0, 0) },
                new Ejecuciones { Nombre = "Jalón al pecho", Repeticiones = 12, Peso = 120, FechayHora = new DateTime(2024, 10, 14, 12, 0, 0) },
                new Ejecuciones { Nombre = "Jalón al pecho", Repeticiones = 15, Peso = 125, FechayHora = new DateTime(2024, 10, 16, 12, 0, 0) },

            };
        }

        // Método para añadir un ejercicio
        // PROXIMA CONFIGURACIÓN (BORRAR)
        private void Añadir_Click(object sender, RoutedEventArgs e)
        {
            // Crear una nueva instancia de VentanaFormularioEjercicios
            var ventanaFormulario = new VentanaFormularioEjercicios();

            // Mostrar la ventana y comprobar si el usuario confirmó la acción
            if (ventanaFormulario.ShowDialog() == true)
            {
                // Obtener la nueva ejecución del formulario
                var nuevoEjercicio = ventanaFormulario.nuevoEjercicio;
                ejercicios.Add(nuevoEjercicio);

                MessageBox.Show("Ejercicio añadido correctamente.");
            }
            else
            {
                MessageBox.Show("No se añadió ningún ejercicio.");
            }
        }

        // Método para modificar el ejercicio seleccionado
        private void Modificar_Click(object sender, RoutedEventArgs e)
        {
            // Obtener el ejercicio seleccionado
            Ejercicios ejercicioSeleccionado = EjerciciosDataGrid.SelectedItem as Ejercicios;

            if (ejercicioSeleccionado != null)
            {
                // Abrir la ventana para modificar el ejercicio
                var ventanaFormulario = new VentanaFormularioEjercicios(ejercicioSeleccionado);

                // Mostrar la ventana y comprobar si el usuario confirmó la acción
                if (ventanaFormulario.ShowDialog() == true)
                {
                    // Aquí puedes realizar alguna acción con el ejercicio modificado, por ejemplo, actualizar la lista
                    MessageBox.Show("Ejecución modificada correctamente.");
                }
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



        // Ventana Daily Insigth
        private void VenatanaDailyInsigthNeutro()
        {
            GraficoCanvas.Children.Clear(); // Limpiar cualquier contenido previo
            EstadoNeutroText.Text = "No hay datos para esta fecha.";
            GraficoCanvas.Children.Add(EstadoNeutroText);
        }

        // Evento para el DatePicker
        private void FechaInsigth_CambioFecha(object sender, SelectionChangedEventArgs e)
        {
            if (FechaInsigth.SelectedDate.HasValue)
            {
                DateTime fechaSeleccionada = FechaInsigth.SelectedDate.Value;
                ValidarDatosParaFecha(fechaSeleccionada);
            }
        }
        // Método para recibir una fecha desde la ventana secundaria
        public void ActualizarFechaDesdeVentanaSecundaria(DateTime fechaSeleccionada)
        {
            FechaInsigth.SelectedDate = fechaSeleccionada; // Actualiza el DatePicker
            ValidarDatosParaFecha(fechaSeleccionada);
        }

        // Validamos si hay datos para une fecha especifica
        private void ValidarDatosParaFecha(DateTime fechaSeleccionada)
        {
            var datosFiltrados = ejecuciones.Where(x => x.FechayHora.Date == fechaSeleccionada.Date).ToList();

            if (datosFiltrados.Count == 0)
            {
                // Estado neutro si no hay datos
                VenatanaDailyInsigthNeutro();
            }
            else
            {
                // Cargar gráfico si hay datos (lógica futura)
                MostrarGrafico(datosFiltrados);
            }
        }
        // Gráfico de datos a cargar
        private void MostrarGrafico(List<Ejecuciones> datosFiltrados)
        {
            GraficoCanvas.Children.Clear(); // Limpia el Canvas

            // Diccionario para almacenar las repeticiones por grupo muscular
            Dictionary<string, int> contadores = new Dictionary<string, int>
            {
                { "Core", 0 },
                { "Pecho", 0 },
                { "Brazos", 0 },
                { "Piernas", 0 },
                { "Espalda", 0 }
            };

            // Iteramos sobre las ejecucionesFiltradas
            foreach (var ejecucion in datosFiltrados)
            {
                // Encontrar el ejercicio correspondiente a la ejecución
                Ejercicios ejercicio = ObtenerEjercicioPorNombre(ejecucion.Nombre);

                // Si encontramos el ejercicio, sumamos las repeticiones al grupo muscular correspondiente
                if (ejercicio != null && contadores.ContainsKey(ejercicio.GruposMusculares))
                {
                    contadores[ejercicio.GruposMusculares] += ejecucion.Repeticiones;
                }
            }
            // Cargamos los ejes para el grafico
            CargarEjesGrafico(contadores);
            // Cargamos los valores al grafico
            CargarValoresGrafico(contadores);
        }

        // Obtenemos el ejercicio por el nombre
        private Ejercicios ObtenerEjercicioPorNombre(string nombreEjecucion)
        {
            return ejercicios.FirstOrDefault(ej => ej.Nombre == nombreEjecucion);
        }
        // Cargamos los ejes del grafico
        private void CargarEjesGrafico(Dictionary<string, int> contadores)
        {
            // Ajustamos el ángulo inicial
            double[] angulos = new double[5];
            double anguloInicio = -Math.PI / 2; // Esto coloca el primer eje hacia arriba (BORRAR)

            for (int i = 0; i < 5; i++)
            {
                angulos[i] = anguloInicio + 2 * Math.PI * i / 5;  // 2π / 5 para dividir el círculo en 5 partes iguales
            }

            // Establecemos el centro del gráfico y el radio
            double radio = 150;
            double centerX = GraficoCanvas.Width / 2;
            double centerY = GraficoCanvas.Height / 2;

            // Dibujar los ejes en el gráfico
            for (int i = 0; i < 5; i++)
            {
                double x = centerX + radio * Math.Cos(angulos[i]);
                double y = centerY + radio * Math.Sin(angulos[i]);

                // Dibujamos el eje (línea desde el centro hasta el borde)
                Line eje = new Line
                {
                    X1 = centerX,
                    Y1 = centerY,
                    X2 = x,
                    Y2 = y,
                    Stroke = Brushes.Black
                };
                GraficoCanvas.Children.Add(eje);

                // Dibujar el nombre del grupo muscular en el borde del gráfico
                TextBlock nombreGrupo = new TextBlock
                {
                    Text = contadores.Keys.ElementAt(i),
                    FontSize = 14,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Canvas.SetLeft(nombreGrupo, x + 5);
                Canvas.SetTop(nombreGrupo, y - 10);
                GraficoCanvas.Children.Add(nombreGrupo);
            }
        }
        // Cargamos los valores del grafico
        private void CargarValoresGrafico(Dictionary<string, int> contadores)
        {
            double[] angulos = new double[5];
            double anguloInicio = -Math.PI / 2; // Primer eje hacia arriba
            for (int i = 0; i < 5; i++)
            {
                angulos[i] = anguloInicio + 2 * Math.PI * i / 5;
            }

            double radioMax = 150; // Radio del gráfico
            double centerX = GraficoCanvas.Width / 2;
            double centerY = GraficoCanvas.Height / 2;

            PointCollection puntos = new PointCollection(); // Colección para los puntos del polígono
            int index = 0;

            foreach (var contador in contadores)
            {
                double valor = Math.Min(contador.Value, 100); // Limitar a 100
                double radioActual = (valor / 100.0) * radioMax;

                double xPunto = centerX + radioActual * Math.Cos(angulos[index]);
                double yPunto = centerY + radioActual * Math.Sin(angulos[index]);

                // Añadir el punto a la colección de puntos del polígono
                puntos.Add(new Point(xPunto, yPunto));

                // Dibujar marcador en el punto
                Ellipse marcador = new Ellipse
                {
                    Width = 6,
                    Height = 6,
                    Fill = Brushes.Red
                };
                Canvas.SetLeft(marcador, xPunto - 3);
                Canvas.SetTop(marcador, yPunto - 3);
                GraficoCanvas.Children.Add(marcador);

                index++;
            }

            // Crear el polígono y rellenarlo con color
            Polygon poligono = new Polygon
            {
                Points = puntos, // Asignar los puntos calculados
                Stroke = Brushes.Blue, // Color del contorno del polígono
                StrokeThickness = 2, // Grosor del contorno
                Fill = new SolidColorBrush(Color.FromArgb(128, 173, 216, 230)) // Azul clarito semitransparente
            };

            // Añadir el polígono al Canvas
            GraficoCanvas.Children.Add(poligono);
        }
    }
}