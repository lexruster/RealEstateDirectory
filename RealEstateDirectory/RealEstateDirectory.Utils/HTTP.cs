using System;
using System.IO;
using System.Net;
using System.Text;

namespace RealEstateDirectory.Utils
{
    public class HTTP
    {
        protected bool Proxy;
        protected bool Credent;
        protected string ProxyUrl;
        protected Stream RequestStream;
        protected StreamReader Reader;
        protected HttpWebRequest Request;
        protected HttpWebResponse Response;
        protected NetworkCredential ProxyCredential;
        protected NetworkCredential WebCredential;

        public HTTP ConfigureProxy(string proxyUrl, string userName = "", string password = "")
        {
            Proxy = true;
            if (!String.IsNullOrEmpty(userName))
            {
                ProxyCredential = new NetworkCredential(userName, password);
            }
            ProxyUrl = proxyUrl;
            return this;
        }

        public HTTP ConfigureCredentials(string userName, string password)
        {
            Credent = true;
            WebCredential = new NetworkCredential(userName, password);
            return this;
        }


        /// <summary>
        /// Делает запрос с данными на сервер и получает ответ
        /// </summary>
        /// <param name="requestUrl">URL для запроса</param>
        /// <param name="requestData">Данные для запроса</param>
        /// <param name="timeout">Время ожидания в милисекундах</param>
        /// <param name="method">Метод -post - по умолчанию</param>
        /// <returns>Строка с ответом сервера</returns>
        /// <exception cref="Exception">Бросается при возникновении проблем при связи с сервером. 
        /// InnerException = исключение, брошенное объектом, в котором возникли проблемы</exception>
        /// <exception cref="Exception">Бросается при передаче null-значения для любого из входных параметров.</exception>
        public string TalkToServer(string requestUrl, string requestData, int timeout, string method = "POST")
        {
            string responseData;
            if (String.IsNullOrEmpty(requestUrl))
                throw new Exception("Пустой урл");
            RequestStream = null;
            Reader = null;
            try
            {

                //Подготавливаем запрос и посылаем его
                Request = (HttpWebRequest)WebRequest.Create(requestUrl);
                if (Proxy)
                {
                    var myProx = new WebProxy();
                    var myUrl = new Uri(ProxyUrl);
                    myProx.Address = myUrl;
                    if (ProxyCredential != null)
                    {
                        myProx.Credentials = ProxyCredential;
                    }
                    Request.Proxy = myProx;
                }
                else
                {
                    Request.Proxy = null;
                }
                if (Credent)
                {
                    Request.PreAuthenticate = true;
                    Request.Credentials = WebCredential;
                }
                else
                {
                    Request.Credentials = CredentialCache.DefaultCredentials;
                }

                Request.Headers.Add("X-Requested-With", "XMLHttpRequest");
                Request.ContentType = "application/x-www-form-urlencoded";
                if (timeout == 0)
                    timeout = 120000;
                Request.Timeout = timeout;
                Request.ContentLength = 0;
                Request.Accept = "text/xml";
                Request.KeepAlive = true;
                Request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; ru; rv:1.9.2.10) Gecko/20100914 Firefox/3.6.10";
                Request.AllowAutoRedirect = true;
                Request.Referer = "http://mysite.ru/";
                var cookieCont = new CookieContainer();
                Request.CookieContainer = cookieCont;
                Request.Method = method;

                //Формируем запрос
                byte[] bytes = Encoding.UTF8.GetBytes(requestData);
                Request.ContentLength = bytes.Length;
                RequestStream = Request.GetRequestStream();
                RequestStream.Write(bytes, 0, bytes.Length);
                // Получаем ответ
                Response = (HttpWebResponse)Request.GetResponse();
                Reader = new StreamReader(Response.GetResponseStream());
                responseData = Reader.ReadToEnd();
            }
            catch (Exception e)
            {
                throw new Exception("Ошибка связи с сервером", e);
            }
            finally
            {
                if (RequestStream != null) RequestStream.Close();
                if (Reader != null) Reader.Close();
            }

            return responseData;

        }
        /// <summary>
        /// Загрузка файла на сервер
        /// </summary>
        /// <param name="requestUrl">Сервер (скрипт)</param>
        /// <param name="fileName">Имя файла</param>
        /// <returns>Ответ сервера</returns>
        public string UploadFile(string requestUrl, string fileName)
        {
            var myWeb = new WebClient();
            if (Proxy)
            {
                var myProx = new WebProxy();
                var myUrl = new Uri(ProxyUrl);
                myProx.Address = myUrl;
                if (ProxyCredential != null)
                {
                    myProx.Credentials = ProxyCredential;
                }
                myWeb.Proxy = myProx;
            }
            else
            {
                myWeb.Proxy = null;
            }
            myWeb.Credentials = Credent ? WebCredential : CredentialCache.DefaultCredentials;
            // Получаем ответ
            byte[] bt = myWeb.UploadFile(requestUrl, fileName);
            string responseData = Encoding.UTF8.GetString(bt);
            return responseData;
        }


        /// <summary>
        /// Делает запрос с данными на сервер и получает ответ
        /// </summary>
        /// <param name="requestUrl">URL для запроса</param>
        /// <param name="timeout">Время ожидания в милисекундах</param>
        /// <param name="enc"></param>
        /// <returns>Строка с ответом сервера</returns>
        /// <exception cref="Exception">Бросается при возникновении проблем при связи с сервером. 
        /// InnerException = исключение, брошенное объектом, в котором возникли проблемы</exception>
        /// <exception cref="Exception">Бросается при передаче null-значения для любого из входных параметров.</exception>
        public string ReadFromServer(string requestUrl, int timeout, Encoding enc = null)
        {
            string responseData;
            if (String.IsNullOrEmpty(requestUrl))
                throw new Exception("Пустой урл");

            RequestStream = null;
            Reader = null;

            try
            {
                //Подготавливаем запрос и посылаем его
                Request = (HttpWebRequest)WebRequest.Create(requestUrl);
                if (Proxy)
                {
                    var myProx = new WebProxy();
                    var myUrl = new Uri(ProxyUrl);
                    myProx.Address = myUrl;
                    if (ProxyCredential != null)
                    {
                        myProx.Credentials = ProxyCredential;
                    }
                    Request.Proxy = myProx;
                }
                else
                {
                    Request.Proxy = null;
                }
                if (Credent)
                {
                    Request.PreAuthenticate = true;
                    Request.Credentials = WebCredential;
                }
                else
                {
                    Request.Credentials = CredentialCache.DefaultCredentials;
                }
                Request.Headers.Add("X-Requested-With", "XMLHttpRequest");
                Request.ContentType = "application/x-www-form-urlencoded";
                if (timeout == 0)
                    timeout = 120000;
                Request.Timeout = timeout;
                Request.ContentLength = 0;
                Request.Accept = "text/xml";
                Request.KeepAlive = true;
                Request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; ru; rv:1.9.2.10) Gecko/20100914 Firefox/3.6.10";
                Request.AllowAutoRedirect = true;
                Request.Referer = "http://mysite.ru/";
                var cookieCont = new CookieContainer();
                Request.CookieContainer = cookieCont;

                // Получаем ответ
                Response = (HttpWebResponse)Request.GetResponse();
                Reader = enc == null ? new StreamReader(Response.GetResponseStream(), Encoding.UTF8) : new StreamReader(Response.GetResponseStream(), enc);
                responseData = Reader.ReadToEnd();
            }
            catch (Exception e)
            {
                throw new Exception("Ошибка связи с сервером", e);
            }
            finally
            {
                if (RequestStream != null) RequestStream.Close();
                if (Reader != null) Reader.Close();
            }

            return responseData;
        }
    }
}