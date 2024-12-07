using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaFinal
{
    // Clase utilizada para almacenar la informacion de cada ejercicio
    public class Ejercicios : INotifyPropertyChanged
    {
        private string nombre;
        private string descripcion;
        private string gruposMusculares;

        // Colección de ejecuciones asociadas a este ejercicio
        public ObservableCollection<Ejecuciones> Ejecuciones { get; set; } = new ObservableCollection<Ejecuciones>();


        public string Nombre
        {
            get { return nombre; }
            set
            {
                if (nombre != value)
                {
                    nombre = value;
                    OnPropertyChanged(nameof(Nombre));
                }
            }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set
            {
                if (descripcion != value)
                {
                    descripcion = value;
                    OnPropertyChanged(nameof(Descripcion));
                }
            }
        }

        public string GruposMusculares
        {
            get { return gruposMusculares; }
            set
            {
                if (gruposMusculares != value)
                {
                    gruposMusculares = value;
                    OnPropertyChanged(nameof(GruposMusculares));
                }
            }
        }

        // Implementación de INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            // Llama al evento PropertyChanged cuando una propiedad cambia
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
