using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;
using Windows.Web.Http.Headers;

namespace Clima.Net
{
    public class HttpConnection
    {
        public interface IHttpRta {
            void setRta(string rta);
        }

        HttpClient client;
        IHttpRta httpRta;

        public HttpConnection(IHttpRta httpRta) {
            client = new HttpClient();
            this.httpRta = httpRta;
        }

        public async void requestByGet(string url) {
            Uri uri = new Uri(url);
            HttpResponseMessage response = await client.GetAsync(uri);
            string rta = await response.Content.ReadAsStringAsync();
            httpRta.setRta(rta);
        }

        public async void requestByPostJSON(string url, string json) {
            Uri uri = new Uri(url);
            HttpStringContent content = new HttpStringContent(json);

            HttpMediaTypeHeaderValue contentType = new HttpMediaTypeHeaderValue("application/json");
            content.Headers.ContentType = contentType;

            HttpResponseMessage response = await client.PostAsync(uri, content);
            string rta = await response.Content.ReadAsStringAsync();
            httpRta.setRta(rta);
        }

        public async void requestByPostForm(string url, List<KeyValuePair<String, String>> form) {
            Uri uri = new Uri(url);
            HttpFormUrlEncodedContent content = new HttpFormUrlEncodedContent(form);

            HttpMediaTypeHeaderValue contentType = 
                new HttpMediaTypeHeaderValue("application/x-www-form-urlencoded");
            content.Headers.ContentType = contentType;

            HttpResponseMessage response = await client.PostAsync(uri, content);
            string rta = await response.Content.ReadAsStringAsync();
            httpRta.setRta(rta);
        }

    }
}
