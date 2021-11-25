using System;
using System.Collections.Generic;
using System.Text;

namespace App_Mascotas.Modelo
{
    public class gen
    {
        public int id { get; set; }
        public String name {  get; set;}

        public override string ToString()
        {
            return this.name;
        }
    }
}
