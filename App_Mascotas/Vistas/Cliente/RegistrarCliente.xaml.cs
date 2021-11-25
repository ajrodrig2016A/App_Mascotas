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
using System.Net;

namespace App_Mascotas.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarCliente : ContentPage
    {
        public RegistrarCliente()
        {
            InitializeComponent();
        }

        //private void dtFecha_DateSelected(object sender, DateChangedEventArgs e)
        //{
        //    txtFecha.Text = dtFecha.Date.ToString("MMM dd, yyyy");
        //}

        private void btnAgregar_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtNumeroDocumento.Text) && !String.IsNullOrEmpty(txtNombre.Text) && !String.IsNullOrEmpty(txtApellido.Text) && !String.IsNullOrEmpty(dtpFechaRegistro.Date.ToString()) && !String.IsNullOrEmpty(txtDireccion.Text) && !String.IsNullOrEmpty(txtEmail.Text) && !String.IsNullOrEmpty(txtNumeroContacto.Text))
                {
                    WebClient cliente = new WebClient();
                    var parametros = new System.Collections.Specialized.NameValueCollection();
                    //parametros.Add("idCliente", Convert.ToInt32(txtIdCliente.Text)); --Se utiliza un campo PK autoincrementable para codigo de tabla cliente
                    parametros.Add("numeroDocumento", txtNumeroDocumento.Text);
                    parametros.Add("nombres", txtNombre.Text);
                    parametros.Add("apellidos", txtApellido.Text);
                    parametros.Add("fechaRegistro", Convert.ToString(dtpFechaRegistro.Date.Year) + '-' + Convert.ToString(dtpFechaRegistro.Date.Month) + '-' + Convert.ToString(dtpFechaRegistro.Date.Day));
                    parametros.Add("direccion", txtDireccion.Text);
                    parametros.Add("email", txtEmail.Text);
                    parametros.Add("numeroContacto", txtNumeroContacto.Text);
                    cliente.UploadValues("http://192.168.200.7/doctorvetWebApi/postCliente.php", "POST", parametros);
                    DisplayAlert("Alerta", "Ingreso correcto", "OK");
                    limpiarCampos();
                }
                else{
                    DisplayAlert("Administracion de Clientes", "Favor ingrese datos faltantes", "OK");
                }

            }
            catch (Exception ex)
            {
                DisplayAlert("Alerta", ex.Message, "OK");
            }
        }
        private void btnRegresar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewCliente());
        }
        private void limpiarCampos()
        {
            txtNumeroDocumento.Text = String.Empty;
            txtNombre.Text = String.Empty;
            txtApellido.Text = String.Empty;
            //dtpFechaRegistro.SetValue(DatePicker.DateProperty, String.Empty);
            txtDireccion.Text = String.Empty; ;
            txtEmail.Text = String.Empty; ;
            txtNumeroContacto.Text = String.Empty;
        }

    }

}
