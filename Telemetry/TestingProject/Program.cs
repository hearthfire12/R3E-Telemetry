namespace TestingProject
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows.Forms;
    using View;
    using Timer = System.Threading.Timer;

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string [] args)
        {
            var p = Process.GetProcessesByName("RRRE64")[0];
            var dc = new DriverConfiguration
            {
                KeyAccelerate = 1,
                KeyDecelerate = 2,
                KeyShiftUp = 3,
                KeyShiftDown = 4,
            };

            var d = new Driver(p, dc);
            d.Accelerate();
            
            //var stream = new BinaryReader(view);
            //var buffer = stream.ReadBytes(Marshal.SizeOf(typeof(Shared)));
            //var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            //var data = (Shared) Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(Shared));
            //handle.Free();



            return;

            if (args.Length > 0)
            {
                switch (args[0])
                {
                    case "Subscriber":
                        RunInCmdMode.Subscriber(args[1], int.Parse(args[2]));
                        break;
                    case "Publisher":
                        RunInCmdMode.Publisher(int.Parse(args[1]));
                        break;
                    default:
                        Console.WriteLine(@"Not supported");
                        break;
                }

                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Subscribers());
        }
    }
}
