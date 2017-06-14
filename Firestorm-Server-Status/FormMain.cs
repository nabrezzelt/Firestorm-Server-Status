using AngleSharp.Parser.Html;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Firestorm_Server_Status
{
    public partial class formMain : Form
    {
        [DllImport("wininet.dll")]
        static extern InternetCookieState InternetSetCookieEx(
        string lpszURL,
        string lpszCookieName,
        string lpszCookieData,
        int dwFlags,
        int dwReserved);

        enum InternetCookieState : int
        {
            COOKIE_STATE_UNKNOWN = 0x0,
            COOKIE_STATE_ACCEPT = 0x1,
            COOKIE_STATE_PROMPT = 0x2,
            COOKIE_STATE_LEASH = 0x3,
            COOKIE_STATE_DOWNGRADE = 0x4,
            COOKIE_STATE_REJECT = 0x5,
            COOKIE_STATE_MAX = COOKIE_STATE_REJECT
        }

        public formMain()
        {
            InitializeComponent();
        }

        private void formMain_Load(object sender, EventArgs e)
        {            
            WebBrowser wb = new WebBrowser();
            wb.ScriptErrorsSuppressed = true;

            InternetSetCookieEx("https://firestorm-servers.com/", null, "teasing-legion=1", 0, 0);
            wb.Navigate("https://firestorm-servers.com/en/welcome/realms");

            wb.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(wb_DocumentCompleted);

            while (wb.ReadyState != WebBrowserReadyState.Complete)
                Application.DoEvents();
                
            wb.Dispose();

           
        }        

        private void wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            List<Realm> realmList = new List<Realm>();

            WebBrowser wb = (WebBrowser)sender;            

            HtmlParser parser = new HtmlParser();
            var document = parser.Parse(wb.Document.Body.InnerHtml);

            var realms = document.QuerySelectorAll(".realm");

            foreach (var realm in realms)
            {                
                Realm.RealmStatus status = Realm.RealmStatus.Offline;
                string onlineSince = "offline";
                Realm.RealmType type = Realm.RealmType.None;

                if(realm.QuerySelector(".status").ClassList.Contains("online"))
                {
                    status = Realm.RealmStatus.Online;
                    onlineSince = realm.QuerySelector(".status").InnerHtml.Replace("online since ", "");
                }
                else if (realm.QuerySelector(".status").ClassList.Contains("offline"))
                {
                    status = Realm.RealmStatus.Offline;
                }

                if(realm.QuerySelector(".type").InnerHtml.Contains("PvP"))
                {
                    type = Realm.RealmType.PvP;
                }
                else if (realm.QuerySelector(".type").InnerHtml.Contains("PvE"))
                {
                    type = Realm.RealmType.PvE;
                }

                string rate = realm.QuerySelector(".rate").InnerHtml;
                string name = realm.QuerySelector(".name").InnerHtml;

                int players = 0;
                Int32.TryParse(realm.QuerySelector(".players").InnerHtml.Replace(" <span>connected players </span>", ""), out players);

                string imgURL = realm.QuerySelector("img").GetAttribute("src");                

                realmList.Add(new Realm(status, onlineSince, rate, name, players, imgURL, type));
                Console.WriteLine(imgURL);
            }

            DrawRealmStatus(realmList);
        }

        private void DrawRealmStatus(List<Realm> realms)
        {
            const int HEIGHT = 377;
            const int WIDTH = 380;

            int currentWidth;
            foreach (var item in collection)
            {

            }
        }

        
    }
}
