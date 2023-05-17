using JCeflaVServicios_Web.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static System.Net.WebRequestMethods;

using Newtonsoft.Json;
using Xamarin.Essentials;


namespace JCeflaVServicios_Web
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registros : ContentPage

    {
        private Datos dato;
        AppApi api;

        public Registros()
        {
            InitializeComponent();
            api = new AppApi();
        }

        private void btnInsertar_Clicked(object sender, EventArgs e)
        {
            try
            {

                WebClient cliente = new WebClient();
                var parametros = new System.Collections.Specialized.NameValueCollection();
               parametros.Add("nombre", txtNombre.Text);
                parametros.Add("correo", txtCorreo.Text);
                parametros.Add("contrasena", txtContrasena.Text);
                parametros.Add("nivel_acceso", txtNivel.Text);

                cliente.UploadValues("http://186.5.86.118/AppEcuador/post.php", "POST", parametros);
                DisplayAlert("Alerta", "Dato Ingresado", "Salir");


            }
            catch (Exception ex)
            {
                DisplayAlert("Alerta", ex.Message,"cerrar");
            }
            }

        private  void btnEliminar_Clicked(object sender, EventArgs e)
        {
           
        }

        private async void btnEliminar_Clicked_1(object sender, EventArgs e)
        {

            try
            {
                dato = await api.GetHighScore(Convert.ToInt32(txtID.Text));
                if (dato.id > 0)
                {
                    _ = api.DeleteHighScore(dato.id);
                }
                await DisplayAlert("Alerta", "Operação realizada com sucesso", "OK");
            }
            catch (Exception error)
            {
                await DisplayAlert("Erro", error.Message, "OK");
            }
        }

        private async void btnActualizar_Clicked(object sender, EventArgs e)
        {

            try
            {
                dato = await api.GetHighScore(Convert.ToInt32(txtID.Text));
                if (dato.id > 0)
                {
                    txtNombre.Text = dato.nombre;
                    txtCorreo.Text = dato.correo;
                    txtContrasena.Text = dato.contrasena;
                    txtNivel.Text = dato.nivel_acceso;


                }

            }
            catch (Exception error)
            {
                await DisplayAlert("Error", error.Message, "OK");
            }
        }
    }
    }
    
