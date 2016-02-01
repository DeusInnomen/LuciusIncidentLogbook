using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace KitchenGeeks
{
    internal static class Program
    {
        private static bool HasClosed;

        /// <summary>
        ///     The main filename for this Assembly, regardless of what the executed filename was.
        /// </summary>
        public static string BaseFilename
        {
            get
            {
                string BaseName = Assembly.GetExecutingAssembly().FullName;
                BaseName = BaseName.Substring(0, BaseName.IndexOf(",")) + ".exe";
                return BaseName;
            }
        }

        /// <summary>
        ///     The directory where this application is running.
        /// </summary>
        public static string BasePath
        {
            get { return Path.GetDirectoryName(Application.ExecutablePath); }
        }

        /// <summary>
        ///     The Product Version of this Assembly.
        /// </summary>
        public static Version Version
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version; }
        }

        /// <summary>
        ///     The Product Version as a string, excluding the Revision value.
        /// </summary>
        public static string VersionString
        {
            get { return Version.Major.ToString() + "." + Version.Minor.ToString() + "." + Version.Build.ToString(); }
        }

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
#if(!DEBUG)
            Application.ThreadException += UnhandledExceptionsCatch;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
#endif

            // Check for an update command in the command line arguments.
            if (args.Length != 0)
            {
                int oldPID = 0;
                string oldVersion = "";
                switch (args[0])
                {
                    case "/update":
                        if (args.Length != 3) break;
                        try
                        {
                            oldPID = Convert.ToInt32(args[1]);
                            oldVersion = args[2];
                            try
                            {
                                Process p = Process.GetProcessById(oldPID);
                                DateTime hardStop = DateTime.Now.AddSeconds(2);
                                while (!p.HasExited && DateTime.Now < hardStop)
                                    Thread.Sleep(100);
                                if (!p.HasExited) p.Kill();
                            }
                            catch
                            {
                            }
                            string target = Path.Combine(BasePath, BaseFilename);
                            File.Delete(target);
                            File.Copy(Application.ExecutablePath, target);
                            Process.Start(target, "/flush " + Process.GetCurrentProcess().Id.ToString() + " " +
                                                  oldVersion + " " + Path.GetFileName(Application.ExecutablePath));
                            return;
                        }
                        catch
                        {
                            // Do nothing, just exit out as we're basically ignoring the command line here.
                        }
                        break;

                    case "/flush":
                        if (args.Length != 4) break;
                        try
                        {
                            oldPID = Convert.ToInt32(args[1]);
                            oldVersion = args[2];
                            var oldVer = new Version(oldVersion);
                            string updaterName = args[3];
                            try
                            {
                                Process p = Process.GetProcessById(oldPID);
                                DateTime hardStop = DateTime.Now.AddSeconds(2);
                                while (!p.HasExited && DateTime.Now < hardStop)
                                    Thread.Sleep(100);
                                if (!p.HasExited) p.Kill();
                            }
                            catch
                            {
                            }
                            File.Delete(Path.Combine(BasePath, updaterName));

                            // Perform any upgrades to the data files, if necessary.
                            Upgrades.PerformUpgrades(new Version(oldVersion));

                            var dialog = new frmUpdated(oldVer);
                            dialog.ShowDialog();
                            dialog.Close();
                        }
                        catch
                        {
                            // Do nothing, just exit out as we're basically ignoring the command line here.
                        }
                        break;
                }
            }

            var splash = new frmSplash();
            splash.FormClosed += SplashClosed;
            splash.Show();

            DateTime done = DateTime.Now.AddSeconds(3);
            while (done > DateTime.Now && !HasClosed)
            {
                Thread.Sleep(50);
                Application.DoEvents();
            }
            splash.Close();

            Application.Run(new FrmManager());
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            using (var dialog = new frmCrash((Exception) e.ExceptionObject))
            {
                if (dialog.ShowDialog() == DialogResult.Retry)
                    Process.Start(Application.ExecutablePath);
            }
            Application.Exit();
        }

        private static void UnhandledExceptionsCatch(object sender, ThreadExceptionEventArgs e)
        {
            using (var dialog = new frmCrash(e.Exception))
            {
                if (dialog.ShowDialog() == DialogResult.Retry)
                    Process.Start(Application.ExecutablePath);
            }
            Application.Exit();
        }

        private static void SplashClosed(object sender, FormClosedEventArgs e)
        {
            HasClosed = true;
        }
    }
}