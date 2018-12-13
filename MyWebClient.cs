using System;
using System.Collections.Specialized;
using System.Net;
using System.Threading.Tasks;
using System.Windows;

namespace TM2
{
    internal class MyWebClient : WebClient
    {
        public readonly CookieContainer _cookieContainer = new CookieContainer();
        private readonly string _mLogin;
        private readonly string _mPassword;
        private readonly string _mUri;

        public MyWebClient(string url, string login, string password)
        {
            _mPassword = password;
            _mLogin = login;
            _mUri = url;
        }
        public async Task LogIn()
        {
            try
            {
                var data = new NameValueCollection
                {
                    {"username", _mLogin},
                    {"password", _mPassword}
                };
                await UploadValuesTaskAsync(new Uri(_mUri), data);
            }
            catch(WebException ex) {
                MessageBox.Show(ex.ToString());
            };
        }
        public async Task LogInFilebase()
        {
            try
            {
                var data = new NameValueCollection
                {
                    {"uid", _mLogin},
                    {"pwd", _mPassword}
                };
                await UploadValuesTaskAsync(new Uri(_mUri), data);
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    if (((HttpWebResponse)ex.Response).StatusCode == HttpStatusCode.NotFound)
                    {
                        MessageBox.Show("404 Error");
                    }
                }
                else if (ex.Status == WebExceptionStatus.NameResolutionFailure)
                {
                    MessageBox.Show(ex.Status.ToString());
                }
            }
        }
        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = base.GetWebRequest(address);
            if (request is HttpWebRequest)
            {
                (request as HttpWebRequest).CookieContainer = _cookieContainer;
            }
            return request;
        }
    }
}
