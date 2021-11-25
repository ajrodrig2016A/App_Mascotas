using App_Mascotas.Modelo;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using System.IO;
using App_Mascotas.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(SqlClient))]

namespace App_Mascotas.Droid
{
    class SqlClient : Database
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var documenPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documenPath, "uisrael.db3");
            return new SQLiteAsyncConnection(path);
        }
    }
}