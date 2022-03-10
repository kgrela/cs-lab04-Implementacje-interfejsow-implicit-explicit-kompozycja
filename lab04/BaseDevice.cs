using System;
using System.Collections.Generic;
using System.Text;

namespace lab04
{
    abstract class BaseDevice
    {
        public static int state;
        public static int Counter { get; set; }

        public static int GetState()
        {
            return state;
        }

        public static void PowerOff()
        {
            state = 0;
        }


        public static void PowerOn()
        {
            state = 1;
        }

    }

    class Copier : BaseDevice
    {
        public static int PrintCounter { get; set; }
        public static int ScanCounter { get; set; }

        public static void Print(string fileName)
        {
            DateTime now = DateTime.Now;
            Console.WriteLine(now+" Print: "+fileName);
        }

        public static void Scan(string type)
        {
            DateTime now = DateTime.Now;
            Console.WriteLine(now + " Scan: "); // TODO: format pliku z innej klasy
        }

        public static void ScanAndPrint()
        {
            Scan("jpg");
            Print("ImageScan" + ScanCounter + ".jpg");
        }
    }
}
