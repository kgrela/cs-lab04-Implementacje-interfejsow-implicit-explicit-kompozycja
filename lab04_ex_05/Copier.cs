using System;
using System.Collections.Generic;
using System.Text;

namespace lab04_ex_05
{
    public class Copier
    {
        protected IDevice.State state = IDevice.State.off;
        public int Counter { get; private set; }
        public Scanner _Scanner;
        public Printer _Printer;
        public Copier()
        {
            _Scanner = new Scanner();
            _Printer = new Printer();
        }
        public IDevice.State GetState()
        {
            if (state == IDevice.State.off)
                return state;
            var pState = _Printer.GetState();
            var sState = _Scanner.GetState();
            if (pState == IDevice.State.standby && sState == IDevice.State.standby)
            {
                state = IDevice.State.standby;
                return state;
            }
            else
            {
                state = IDevice.State.on;
                return state;
            }
        }
        public void PowerOn() => SetState(IDevice.State.on);
        public void PowerOff() => SetState(IDevice.State.off);
        public void SetState(IDevice.State state)
        {
            if (state == IDevice.State.on && this.state == IDevice.State.off)
                Counter++;
            this.state = state;
            _Printer.SetState(state);
            _Scanner.SetState(state);
        }
        public void StandbyOn()
        {
            this.state = IDevice.State.standby;
            _Printer.SetState(IDevice.State.standby);
            _Scanner.SetState(IDevice.State.standby);
        }
        public void StandbyOff()
        {
            this.state = IDevice.State.on;
            _Printer.SetState(IDevice.State.on);
            _Scanner.SetState(IDevice.State.on);
        }
        public void GetCounter()
        {
            Console.WriteLine($"Copier counter: {Counter}");
            Console.WriteLine($"Scan counter: {_Scanner.Counter}");
            Console.WriteLine($"Print counter: {_Printer.Counter}");
        }
    }
}
