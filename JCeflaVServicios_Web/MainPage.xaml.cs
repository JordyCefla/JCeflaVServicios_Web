using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace JCeflaVServicios_Web
{
    public partial class MainPage : ContentPage
    {
        private const string Url = "http://186.5.86.118/AppEcuador/post.php";
        private readonly HttpClient cliente = new HttpClient();
        private ObservableCollection<JCeflaVServicios_Web.Models.Datos> post;
        public MainPage()
        {
            InitializeComponent();
            Obtener();
        }
        public async void Obtener()

        {
            var  content =await cliente.GetStringAsync(Url);
            List<JCeflaVServicios_Web.Models.Datos> post = JsonConvert.DeserializeObject<List<JCeflaVServicios_Web.Models.Datos>>(content);
            Lista.ItemsSource = post;

        }

        private void btnRegistro_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Registros());
        }
    }
}