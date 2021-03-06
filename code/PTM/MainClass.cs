using System;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using PTM.Util;
using PTM.Framework;
using PTM.Framework.Helpers;
using PTM.View;
using PTM.View.Forms;
using Timer=System.Timers.Timer;
[assembly : CLSCompliant(false)]

namespace PTM
{
	/// <summary>
	/// Application Starter class
	/// </summary>
	internal sealed class MainClass
	{
		/// <summary>
		/// Boolean value to store single instance configuration 
		/// </summary>
		//private static bool runSingleInstance = true;

		/// <summary>
		/// MemoryMappedFile to share between instances
		/// </summary>
		internal static MemoryMappedFile sharedMemory;


		/// <summary>
		/// MainClass constructor
		/// </summary>
		private MainClass()
		{
		} //MainClass

		[STAThread]
		/// <summary>
			/// Main Method, Application access point
			/// </summary>
		private static void Main()
		{
			try
			{
                //if (runSingleInstance)
                //{
                //    RunSingleInstance();
                //}
                //else
                //{
					Launch();
				//} //if-else
			}
			catch(Exception ex)
			{
				Logger.WriteException(ex);
                ExceptionMessageForm exForm = new ExceptionMessageForm(ex);
			    exForm.ShowDialog();
			    //MessageBox.Show(ex.Message + "\n" + ex.StackTrace , "PTM", MessageBoxButtons.OK, MessageBoxIcon.Error);
			    throw;
			}
		} //Main

        ///// <summary>
        ///// Validates that no other application is runnin.
        ///// </summary>
        //private static void RunSingleInstance()
        //{
        //    // Used to check if we can create a new mutex
        //    bool newMutexCreated = false;
        //    // The name of the mutex is to be prefixed with Local\ to make sure that its is created in the per-session namespace, not in the global namespace.
        //    string mutexName = "Local\\" + Assembly.GetExecutingAssembly().GetName().Name;

        //    Mutex mutex = null;
        //    try
        //    {
        //        // Create a new mutex object with a unique name
        //        mutex = new Mutex(false, mutexName, out newMutexCreated);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message + "\n\n" + ex.StackTrace + "\n\n" + "Application Exiting...", "Exception thrown",
        //                        MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        Application.Exit();
        //    } //try-catch

        //    // When the mutex is created for the first time we run the program since it is the first instance.
        //    if (newMutexCreated)
        //    {
        //        //Create the Shared Memory to store the window handle. This memory is shared between processes
        //        lock (typeof (MainForm))
        //        {
        //            sharedMemory = MemoryMappedFile.CreateMMF(
        //                "Local\\sharedMemoryPTM",
        //                MemoryMappedFile.FileAccess.ReadWrite, 8);
        //        } //lock
        //        Launch();
        //    }
        //    else
        //        // If the mutex already exists, no need to launch a new instance of the program because a previous instance is running .
        //    {
        //        try
        //        {
        //            // Get the Program's main window handle, which was previously stored in shared memory.
        //            IntPtr mainWindowHandle = IntPtr.Zero;
        //            lock (typeof (MainForm))
        //            {
        //                mainWindowHandle = MemoryMappedFile.ReadHandle(
        //                    "Local\\sharedMemoryPTM");
        //            } //lock
        //            if (mainWindowHandle != IntPtr.Zero)
        //            {
        //                if (ViewHelper.IsIconic(mainWindowHandle))
        //                {
        //                    // Restore the Window 
        //                    ViewHelper.ShowWindow(mainWindowHandle, ViewHelper.SW_SHOWNORMAL);
        //                }
        //                else
        //                {
        //                    // Restore the Window 
        //                    ViewHelper.ShowWindow(mainWindowHandle, ViewHelper.SW_RESTORE);
        //                } //if-else
        //                ViewHelper.UpdateWindow(mainWindowHandle);
        //            } //if
        //            return;
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message + "\n\n" + ex.StackTrace + "\n\n" + "Application Exiting...", "Exception thrown");
        //        } //try-catch

        //        // Tell the garbage collector to keep the Mutex alive until the code 
        //        //execution reaches this point, ie. normally when the program is exiting.
        //        GC.KeepAlive(mutex);
        //        // Release the Mutex 
        //        try
        //        {
        //            mutex.ReleaseMutex();
        //        }
        //        catch (ApplicationException ex)
        //        {
        //            MessageBox.Show(ex.Message + "\n\n" + ex.StackTrace, "Exception thrown");
        //            GC.Collect();
        //        } //try-catch
        //    } //if-else
        //} //RunSingleInstance

		private static SplashForm splash;// = new SplashForm();

		internal static void Launch()
		{
			Application.CurrentCulture = CultureInfo.InvariantCulture;
            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
		    splash = new SplashForm();
			splash.Show();
			Timer timer = new Timer();
			//Configure this timer to restart each second ( 1000 millis)
			timer.Interval = 1000;
			timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
			timer.Start();
			//splash.Refresh();
			
			//Application.DoEvents();
			Application.DoEvents();
			MainModule.Initialize("data");
			splash.SetLoadProgress(50);
			splash.Refresh();
			Application.DoEvents();

			UpdaterHelper.UpdateInfo info;
			info = UpdaterHelper.CheckFromUpdates();
			Application.DoEvents();
			if (info.UpdateAvailable)
			{
				UpdateForm updateForm;
				updateForm = new UpdateForm(info);
				updateForm.ShowDialog();
				Application.DoEvents();
			}

			splash.SetLoadProgress(60);
			splash.Refresh();
			Application.DoEvents();
			IconsManager.Initialize();
			Application.DoEvents();
			MainForm main = new MainForm();
            //if (runSingleInstance)
            //{
            //    main.HandleCreated += new EventHandler(main_HandleCreated);
            //}
			GC.Collect();
            timer.Stop();
            timer.Close();
			splash.SetLoadProgress(100);
			splash.Refresh();
			Application.DoEvents();
			splash.Close();
			splash = null;
			Application.DoEvents();
            
			Application.Run(main);
		} //Launch

		private static void timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			splash.AddProgress(5);
			Application.DoEvents();
		} //timer_Elapsed


        //private static void main_HandleCreated(object sender, EventArgs e)
        //{
        //    IntPtr mainWindowHandle = ((MainForm) sender).Handle;
        //    try
        //    {
        //        lock (sender)
        //        {
        //            //Write the handle to the Shared Memory 
        //            sharedMemory.WriteHandle(mainWindowHandle);
        //        } //lock
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message + "\n\n" + ex.StackTrace + "\n\n" +
        //                        "Application Exiting...", "Exception thrown",
        //                        MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        Application.Exit();
        //    } //try-catch
        //} //main_HandleCreated
	} //end of class
} //enf of namespace