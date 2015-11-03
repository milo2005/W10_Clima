using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Clima.Net;
using Windows.Data.Json;

// La plantilla de elemento Página en blanco está documentada en http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Clima
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page, HttpConnection.IHttpRta
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            HttpConnection http = new HttpConnection(this);
            http.requestByGet("https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text%3D%22popayan%2C%20co%22)&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys");

        }

        public void setRta(string rta)
        {
            JsonObject json = JsonObject.Parse(rta);
            JsonObject query = json["query"].GetObject();
            JsonObject results = query["results"].GetObject();
            JsonObject channel = results["channel"].GetObject();
            JsonObject atmosphere = channel["atmosphere"].GetObject();

            humedad.Text = atmosphere["humidity"].GetString();
            presion.Text = atmosphere["pressure"].GetString();

            JsonObject item = channel["item"].GetObject();
            JsonObject condition = item["condition"].GetObject();

            pronostico.Text = condition["text"].GetString();
            temperatura.Text = condition["temp"].GetString();





        }
    }
}
