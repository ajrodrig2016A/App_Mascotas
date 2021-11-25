using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace App_Mascotas.Modelo
{
    public class Cliente : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int idCliente { get; set; }
        public string numeroDocumento { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public DateTime fechaRegistro { get; set; }
        public string direccion { get; set; }
        public string email { get; set; }
        public string numeroContacto { get; set; }
        private bool _isVisible { get; set; }
        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                if (_isVisible != value)
                {
                    _isVisible = value;
                    OnPropertyChanged();
                }
            }
        }


    }
}
