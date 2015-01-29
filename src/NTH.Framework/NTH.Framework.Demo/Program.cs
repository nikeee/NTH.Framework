using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using NTH.Framework.Storage;
using NTH.Framework.Windows.Mail;

namespace NTH.Framework.Demo
{
    static class Program
    {
        /// <summary>The main entry point for the application.</summary>
        [STAThread]
        static void Main()
        {

            var mail = new MapiMail();
            mail.Subject = "Some Subject";
            mail.MessageBody = "This is the message content";
            mail.Recipients.Add("somebody@example.com");
            mail.BccRecipients.Add("secretSomebody@example.com");
            mail.SendPopup();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }
    }
}
