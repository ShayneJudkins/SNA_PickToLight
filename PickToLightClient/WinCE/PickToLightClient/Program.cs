using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SNA.Mobile.PickToLightClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            ////Application.Run(new LoginLandscape());
            ////Application.Run(new LoginPortrait());
            //Application.Run(new Main());

            //using (Login login = new Login())
            //{
            //    if (login.ShowDialog() == DialogResult.OK)
            //    {
            //        //MessageBox.Show("User (" + login.UserName + ") Logged In successfully!");
            //        Application.Run(new Main(login.UserName)); //passing the userName to the constructor of Main (from Login form - property/global variable)!
            //    }
            //}

            Application.Run(new Main());
        }
    }
}