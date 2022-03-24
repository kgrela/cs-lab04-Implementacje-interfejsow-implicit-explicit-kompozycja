using System;
using System.Collections.Generic;
using System.Text;

namespace lab04_ex_03
{
    public class Printer : IPrinter
    {
        public IDevice.State State { get; private set; } = IDevice.State.off;
        public int Counter { get; private set; }
        public int PrintCounter { get; private set; }

        public IDevice.State GetState() => State;
        public void PowerOff()
        {
            if (State == IDevice.State.on)
            {
                State = IDevice.State.off;
                Console.WriteLine("Printing functions turned off");
            }
        }

        public void PowerOn()
        {
            if (State == IDevice.State.off)
            {
                State = IDevice.State.on;
                Counter++;
                Console.WriteLine("Printing functions turned on");
            }
        }

        public void Print(in IDocument document)
        {
            if (State == IDevice.State.on)
            {
                PrintCounter++;
                Console.WriteLine($"{DateTime.Now} Print: {document.GetFileName()}");
            }
        }
    }
}
