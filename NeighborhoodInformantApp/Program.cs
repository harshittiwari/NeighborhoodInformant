using NeighborhoodInformantApp.DataMgr;
using NeighborhoodInformantApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeighborhoodInformantApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // string line;

            // Read the file and display it line by line.
            //System.IO.StreamReader file =
            //   new System.IO.StreamReader(@"D:\UIC\CS 440 - SE1\coding project\GIT\Code\NeighborhoodInformant\NeighborhoodInformantApp\Resources\Kmlmetrastations.kml");
            // bool isWrite = true;
            // while ((line = file.ReadLine()) != null)
            // {
            //     if (line.Contains("description"))
            //         isWrite = !isWrite;
            //     else if (isWrite)
            //         using (System.IO.StreamWriter file1 =
            // new System.IO.StreamWriter(@"D:\UIC\CS 440 - SE1\coding project\GIT\Code\NeighborhoodInformant\NeighborhoodInformantApp\Resources\Kmlmetrastations1.kml", true))
            //         {
            //             file1.WriteLine(line);
            //         }
            // }

            // file.Close();

            //Connect to DB 


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            int i = 0;

            bool isLoop = true;
            while (isLoop)
            {
                User user = null;

                DataAccess.SetConnection();
                Thread oThread = new Thread(new ThreadStart(DataMgr.DataMgr.SetupDB));
                oThread.Start();

                try
                {
                    Login login = new Login();
                    while (!login.IsLoginSuccessful && !(login.IsNewLoginReq || login.IsForgotPassword))
                        Application.Run(login);

                    user = login.user;

                    if (login.IsNewLoginReq)
                    {
                        NewLogin nLogin = new NewLogin();
                        Application.Run(nLogin);
                        user = nLogin.user;
                    }
                    else if (login.IsForgotPassword)
                    {
                        NewLogin nLogin = new NewLogin(true);
                        Application.Run(nLogin);
                        user = nLogin.user;
                    }
                    else
                        isLoop = false;

                    if (oThread.IsAlive)
                    {
                        MessageBox.Show("We are doing initial setup. Application will open in few seconds after loading the data.");
                    }

                    oThread.Join();
                }
                catch (Exception)
                {
                    //MessageBox.Show(ex.Message);
                }
                if (user != null)
                {
                    if (string.IsNullOrEmpty(user.UserName))
                    {
                        i++;
                        if (i > 3)
                        {
                            MessageBox.Show("Error Starting Application");
                            Application.Exit();
                            isLoop = false;
                        }
                        MessageBox.Show("Error loading user. Please try again.");
                        continue;
                    }
                    isLoop = false;
                    DataMgr.DataMgr.user = user;
                    Application.Run(new Form1());
                }
                else
                {
                    MessageBox.Show("Error Starting Application");
                    Application.Exit();
                    isLoop = false;
                }
            }
        }
    }
}
