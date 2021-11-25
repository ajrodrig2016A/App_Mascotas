using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace App_Mascotas.Modelo
{
    public interface Database
    {
        SQLiteAsyncConnection GetConnection();
    }
}
