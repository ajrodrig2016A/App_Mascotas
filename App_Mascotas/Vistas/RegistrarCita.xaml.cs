using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using App_Mascotas.Modelo;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using System.IO;

namespace App_Mascotas.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarCita : ContentPage
    {
        private SQLiteAsyncConnection con;
        private ObservableCollection<Cliente> tablaCliente;
        private ObservableCollection<Mascota> tablaMascota;

        private int validacion = 0;
        public string texto;
        public int id;
        IEnumerable<Cita> ResultadoUpdate;
        IEnumerable<Cita> ResultadoDelete;
        public RegistrarCita(Cita cita)
        {
            InitializeComponent();
            con = DependencyService.Get<Database>().GetConnection();
            int aux = 0;


            if (cita.idCita > -1)
            {

                consultaCliente2(cita.idCliente);
                consultaMascota2(cita.idMascota);
                dtFecha.Date = cita.fecha;
                txtFecha.Text = cita.fecha.ToString();
                txtDiagnostico.Text = cita.diagonistico;
                id = cita.idCita;
                validacion = 1;
                btnAgregar.Text = "Modificar";
                btnEliminar.IsVisible = true;
                //aux = mascota.idCliente;
            }
            else
            {
                consultaCliente();
                consultaMascota();
                btnAgregar.Text = "Agregar";
                btnEliminar.IsVisible = false;
            }
        }

        public async void consultaCliente()
        {
            var documentPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "uisrael.db3");
            var db = new SQLiteConnection(documentPath);
            var registro = await con.Table<Cliente>().ToListAsync();
            tablaCliente = new ObservableCollection<Cliente>(registro);

            cmbCliente.Title = "Seleccione Cliente";
            cmbCliente.ItemsSource = (System.Collections.IList)tablaCliente;

        }

        public async void consultaCliente2(int id)
        {
            var documentPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "uisrael.db3");
            var db = new SQLiteConnection(documentPath);
            var registro = await con.Table<Cliente>().ToListAsync();
            tablaCliente = new ObservableCollection<Cliente>(registro);

            cmbCliente.ItemsSource = (System.Collections.IList)tablaCliente;
            cmbCliente.SelectedIndex = id;

        }

        public async void consultaMascota()
        {
            var documentPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "uisrael.db3");
            var db = new SQLiteConnection(documentPath);
            var registro = await con.Table<Mascota>().ToListAsync();
            tablaMascota = new ObservableCollection<Mascota>(registro);

            cmbMascota.Title = "Seleccione Mascota";
            cmbMascota.ItemsSource = (System.Collections.IList)tablaMascota;

        }

        public async void consultaMascota2(int id)
        {
            var documentPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "uisrael.db3");
            var db = new SQLiteConnection(documentPath);
            var registro = await con.Table<Mascota>().ToListAsync();
            tablaMascota = new ObservableCollection<Mascota>(registro);

            cmbMascota.ItemsSource = (System.Collections.IList)tablaMascota;
            cmbMascota.SelectedIndex = id;

        }

        public static IEnumerable<Cita> DELETE(SQLiteConnection db, int id)
        {
            return db.Query<Cita>("DELETE FROM Cita where idCita = ?", id);
        }

        public static IEnumerable<Cita> UPDATE(SQLiteConnection db, int idCliente, int idMascota,
                                                    DateTime fecha, string diagnos, int id)
        {
            return db.Query<Cita>("UPDATE Cita SET idCliente = ?, idMascota = ?, fecha = ?," +
                "diagonistico = ? where idCita = ?", idCliente, idMascota, fecha, diagnos, id);
        }

        private void dtFecha_DateSelected(object sender, DateChangedEventArgs e)
        {
            txtFecha.Text = dtFecha.Date.ToString("MMM dd, yyyy");
        }

        private void btnAgregar_Clicked(object sender, EventArgs e)
        {
            if (validacion == 1)
            {
                var documentPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(documentPath);
                int idCliente = cmbCliente.SelectedIndex;
                int idMascota = cmbMascota.SelectedIndex;
                ResultadoUpdate = UPDATE(db, idCliente, idMascota, dtFecha.Date, txtDiagnostico.Text, id);
                DisplayAlert("Alerta", "Datos Actualizados", "Ok");
                db.Close();
                Navigation.PopAsync();
                Navigation.PopAsync();
                Navigation.PushAsync(new ViewCita());
            }
            else
            {
                try
                {
                    int idCliente = cmbCliente.SelectedIndex;
                    int idMascota = cmbMascota.SelectedIndex;
                    var Registros = new Cita { idCliente = idCliente, idMascota = idMascota, fecha = dtFecha.Date, diagonistico = txtDiagnostico.Text };
                    con.InsertAsync(Registros);
                    DisplayAlert("Alerta", "Dato Ingresado", "ok");
                    
                    txtFecha.Text = "";
                    txtDiagnostico.Text = "";
                }
                catch (Exception ex)
                {
                    DisplayAlert("Alerta", "Error: " + ex.Message, "ok");

                }
            }
        }

        private void btnEliminar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var documentPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(documentPath);
                ResultadoDelete = DELETE(db, id);
                DisplayAlert("Alerta", "Datos Eliminados", "Ok");
                db.Close();
                Navigation.PopAsync();
                Navigation.PopAsync();
                Navigation.PushAsync(new ViewCita());
            }
            catch (Exception ex)
            {

                DisplayAlert("Alerta", "Error: " + ex.Message, "ok");
            }
        }
    }
}