using App_Mascotas.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using System.Collections.ObjectModel;

namespace App_Mascotas.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarMascota : ContentPage
    {
        private SQLiteAsyncConnection con;
        private ObservableCollection<Cliente> tablaCliente;
        private ObservableCollection<Raza> tablaRaza;

        private int validacion = 0;
        public string texto;
        public int id;
        IEnumerable<Mascota> ResultadoUpdate;
        IEnumerable<Mascota> ResultadoDelete;

        public RegistrarMascota(Mascota mascota)
        {
            InitializeComponent();
            con = DependencyService.Get<Database>().GetConnection();
            int aux = 0;

            cargarPicker();
            if (mascota.idMascota > -1)
            {

                consultaCliente2(mascota.idCliente);
                consultaRaza2(mascota.idRaza);
                txtNombre.Text = mascota.nombre;
                dtFecha.Date = mascota.fechaNac;
                txtFecha.Text = mascota.fechaNac.ToString();
                if(mascota.genero == "Masculino")
                {
                    cmbGenero.SelectedIndex=0;
                }
                else if (mascota.genero == "Femenino")
                {
                    cmbGenero.SelectedIndex=1;
                }

                if (mascota.esterilizado == "Si")
                {
                    cmbster.SelectedIndex = 0;
                }
                else
                {
                    cmbster.SelectedIndex = 1;
                }
                txtColor.Text = mascota.color;
                id = mascota.idMascota;
                validacion = 1;
                btnAgregar.Text = "Modificar";
                btnEliminar.IsVisible = true;
                aux = mascota.idCliente;
            }
            else
            {
                consultaCliente();
                consultaRaza();
                btnAgregar.Text = "Agregar";
                btnEliminar.IsVisible = false;
            }
        }

        public void cargarPicker()
        {
            List<gen> genero = new List<gen>();
            genero.Add(new gen() { id = 0, name = "Masculino" });
            genero.Add(new gen() { id = 1, name = "Femenino" });
            cmbGenero.Title = "Seleccione Genero";
            cmbGenero.ItemsSource = (System.Collections.IList)genero;


            List<gen> ester = new List<gen>();
            ester.Add(new gen() { id = 0, name = "Si" });
            ester.Add(new gen() { id = 1, name = "No" });
            cmbster.Title = "Esterilizado?";
            cmbster.ItemsSource = (System.Collections.IList)ester;

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

        public async void consultaRaza()
        {
            var documentPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "uisrael.db3");
            var db = new SQLiteConnection(documentPath);
            var registro = await con.Table<Raza>().ToListAsync();
            tablaRaza = new ObservableCollection<Raza>(registro);

            cmbRaza.Title = "Seleccione Raza";
            cmbRaza.ItemsSource = (System.Collections.IList)tablaRaza;

        }
        public async void consultaRaza2(int id)
        {
            var documentPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "uisrael.db3");
            var db = new SQLiteConnection(documentPath);
            var registro = await con.Table<Raza>().ToListAsync();
            tablaRaza = new ObservableCollection<Raza>(registro);

            cmbRaza.ItemsSource = (System.Collections.IList)tablaRaza;
            cmbRaza.SelectedIndex = id;

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
        
        public static IEnumerable<Mascota> DELETE(SQLiteConnection db, int id)
        {
            return db.Query<Mascota>("DELETE FROM Mascota where idMascota = ?", id);
        }

        public static IEnumerable<Mascota> UPDATE(SQLiteConnection db, int idRaza, int idCliente, string nombre,
                                                    DateTime fecha, string genero, string esteri, string color, int id)
        {
            return db.Query<Mascota>("UPDATE Mascota SET idRaza = ?, idCliente = ?, nombre = ?, fechaNac = ?," +
                "genero = ?, esterilizado = ?, color = ? where idMascota = ?", idRaza, idCliente, nombre, fecha, genero, esteri, color, id);
        }

        private void btnAgregar_Clicked(object sender, EventArgs e)
        {
            if (validacion == 1)
            {
                var documentPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(documentPath);
                int idCliente=cmbCliente.SelectedIndex;
                int idRaza=cmbRaza.SelectedIndex;
                
                string genero=cmbGenero.SelectedItem.ToString();
                ResultadoUpdate = UPDATE(db, idRaza, idCliente, txtNombre.Text, Convert.ToDateTime(txtFecha.Text), genero, cmbster.SelectedItem.ToString(), txtColor.Text, id);
                DisplayAlert("Alerta", "Datos Actualizados", "Ok");
                db.Close();
                Navigation.PopAsync();
                Navigation.PopAsync();
                Navigation.PushAsync(new ViewMascota());
            }
            else
            {
                try
                {
                    int idCliente = cmbCliente.SelectedIndex;
                    int idRaza = cmbRaza.SelectedIndex;
                    string genero = cmbGenero.SelectedItem.ToString();
                    var Registros = new Mascota { idRaza = idRaza, idCliente = idCliente, nombre = txtNombre.Text, fechaNac = Convert.ToDateTime(txtFecha.Text), genero = genero, esterilizado = cmbster.SelectedItem.ToString(), color = txtColor.Text };
                    con.InsertAsync(Registros);
                    DisplayAlert("Alerta", "Dato Ingresado", "ok");
                    txtNombre.Text = "";
                    txtFecha.Text = "";
                    txtColor.Text = "";
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
                Navigation.PushAsync(new ViewMascota());
            }
            catch (Exception ex)
            {

                DisplayAlert("Alerta", "Error: " + ex.Message, "ok");
            }
        }

        private void dtFecha_DateSelected(object sender, DateChangedEventArgs e)
        {
            txtFecha.Text = dtFecha.Date.ToString("MMM dd, yyyy");
        }
    }
}