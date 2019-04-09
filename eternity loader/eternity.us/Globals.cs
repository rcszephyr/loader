using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace eternity.us
{
    class Stuff
    {
        public static string username = "";//leave it blank
        public static string expires = "";//leave it blank
        public static int programid;//leave it blank
        public static int user_id;//leave it blank
        public static int specialint;//leave it blank
        public static string key = "your shitty authed code goes here";
    }

    class names
    {
        public static bool offline = false;
        public static string csgo = "csgo";
        public static string path = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) + "\\agaviaga.dll";
        public static string path2 = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) + "\\agaviaga2.dll";
        public static string pastebin = webString("https://pastebin.com/raw/V99wdBVp");// dll normal
        public static string pastebin2 = webString("https://pastebin.com/raw/V99wdBVp");// dll alpha-beta, whatever.
        public static string snd = @"C:\Users\" + Environment.UserName + @"\AppData\Roaming\pSound.wav";
        public static string status = webString("https://pastebin.com/raw/MgwjbBHW");
        public static string online = "On";
        public static string offline2 = "Off";
        public static string dll = string.Empty;

        public static string webString(string url)
        {
            string htmlString = string.Empty;
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    try
                    {
                        webClient.Headers["User-Agent"] = "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.2.15) Gecko/20110303 Firefox/3.6.15";
                        WebProxy myProxy = new WebProxy();
                        myProxy.IsBypassed(new Uri(url));
                        webClient.Proxy = myProxy;
                        htmlString = webClient.DownloadString(url);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    finally
                    {
                        webClient.Dispose();
                    }
                }
            }
            catch (WebException wex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally { }
            return htmlString.Replace("\r", string.Empty).Replace("\n", string.Empty).Replace("\t", string.Empty);
        }
    }
}
