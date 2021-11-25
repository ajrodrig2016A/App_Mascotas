using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions;
using SQLiteNetExtensions.Attributes;

namespace App_Mascotas.Modelo
{
    public class Mascota
    {
        [PrimaryKey,AutoIncrement]
        public int idMascota { get; set; }


        [ForeignKey(typeof(Raza))]
        public int idRaza {  get; set; }

        [ManyToOne]
        public Raza raza {  get; set; }


        [ForeignKey(typeof(Cliente))]
        public int idCliente { get; set; }

        [ManyToOne]
        public Cliente cliente { get; set; }

        public string nombre { get; set; }

        public DateTime fechaNac { get; set; }

        public string genero { get; set; }
        public string esterilizado { get; set; }
        public string color { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Cita> Citas { get; set; }




    }
}
