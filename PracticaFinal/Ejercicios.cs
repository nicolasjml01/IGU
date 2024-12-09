using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaFinal
{
    public class Ejercicios : INotifyPropertyChanged
    {
        private string nombre;
        private string descripcion;
        private string gruposMusculares;

        // Evento para notificar cuando el nombre cambie
        public event Action<string, string> NombreCambiado;


        public string Nombre
        {
            get { return nombre; }
            set
            {
                if (nombre != value)
                {
                    string nombreAnterior = nombre;
                    nombre = value;
                    OnPropertyChanged(nameof(Nombre));

                    // Notificar que el nombre cambió
                    NombreCambiado?.Invoke(nombreAnterior, nombre);
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
