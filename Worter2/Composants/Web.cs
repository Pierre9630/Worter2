using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Web;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;

namespace Worter2
{
     class Web
    {
        public void WebBrowse()
        {
             
            var prs = new ProcessStartInfo("MicrosoftEdge.exe");
            prs.Arguments = "192.168.1.52:8080";
            Process.Start(prs);
            //_ = Process.GetProcesses("MicrosoftEdge.exe");

        }
        
        //private static bool completed = false;
        //private static WebBrowser wb;
        //[STAThread]
        //static void Main(string[] args)
        //{
        //    wb = new WebBrowser();
        //    wb.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(wb_DocumentCompleted);
        //    wb.Navigate("http://www.google.com");
        //    while (!completed)
        //    {
        //        Application.DoEvents();
        //        Thread.Sleep(100);
        //    }
        //    Console.Write("\n\nDone with it!\n\n");
        //    Application.Run(form);
        //}


        //static void wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        //{
        //    Console.WriteLine(wb.Document.Body.InnerHtml);
        //    completed = true;
        //}
    }
}
