using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace App_Mascotas.Modelo
{
    public class Cita
    {
        [PrimaryKey,AutoIncrement]
        public int idCita { get; set; }
        [ForeignKey(typeof(Cliente))]
        public int idCliente { get; set; }

        [ManyToOne]
        public Cliente cliente { get; set; }

        [ForeignKey(typeof(Mascota))]
        public int idMascota { get; set; }

        [ManyToOne]
        public Mascota mascota { get; set; }

        public DateTime fecha { get; set; }
        public string diagonistico { get; set; }


    }
}
