using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace App_Mascotas.Modelo
{
    public class Raza
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        [MaxLength(50)]
        public string nombre { get; set; }
        [MaxLength(25)]
        public string caracteristicas { get; set; }


        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Mascota> Mascotas {  get; set; }
    }
}
