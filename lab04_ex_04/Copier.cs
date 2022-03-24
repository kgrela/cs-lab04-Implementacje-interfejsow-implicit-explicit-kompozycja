using System;
using System.Collections.Generic;
using System.Text;

namespace lab04_ex_04
{
    public class Copier : IPrinter, IScanner
    {
        protected IDevice.State state = IDevice.State.off;
        public int Counter { get; private set; }
        public IDevice.State GetState()
        {
            if (state == IDevice.State.off)
                return IDevice.State.off;
            var pState = IPrinter.PrinterState;
            var sState = IScanner.ScannerState;
            if (pState == IDevice.State.standby && sState == IDevice.State.standby)
                return IDevice.State.standby;
            return IDevice.State.on;
        }


        void IDevice.SetState(IDevice.State state)
        {
            if (state == IDevice.State.on && this.state == IDevice.State.off)
                Counter++;
            this.state = state;
            IPrinter.PrinterState = state;
            IScanner.ScannerState = state;
        }
        public void StandbyOn()
        {
            this.state = IDevice.State.standby;
            IPrinter.PrinterState = IDevice.State.standby;
            IScanner.ScannerState = IDevice.State.standby;
        }
        public void StandbyOff()
        {
            this.state = IDevice.State.on;
            IPrinter.PrinterState = IDevice.State.on;
            IScanner.ScannerState = IDevice.State.on;
        }
        public void GetCounter()
        {
            Console.WriteLine($"Copier counter: {Counter}");
            Console.WriteLine($"Scan counter: {IScanner.ScanCounter}");
            Console.WriteLine($"Print counter: {IPrinter.PrintCounter}");
        }
    }
}
