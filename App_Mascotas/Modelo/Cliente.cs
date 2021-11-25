using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace App_Mascotas.Modelo
{
    public class Cliente
    {
        [PrimaryKey, AutoIncrement]
        public int idCliente { get; set; }
        [MaxLength(15)]
        public string cedula {  get; set; }
        [MaxLength(50)]
        public string nombres { get; set; }
        [MaxLength(50)]
        public string apellidos { get; set; }
        [MaxLength(50)]
        public DateTime fechaRegistro { get; set; }
        [MaxLength(50)]
        public string direccion { get; set; }
        [MaxLength(50)]
        public string email { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Mascota> Mascotas { get; set; }


        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Cita> Citas { get; set; }


    }
}
