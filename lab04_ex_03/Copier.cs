using System;
using System.Collections.Generic;
using System.Text;

namespace lab04_ex_03
{
    public class Copier : BaseDevice
    {
        public int PrintCounter { get; protected set; }
        public int ScanCounter { get; protected set; }
        public new int Counter { get; protected set; }
        protected readonly Printer _Printer;
        protected readonly Scanner _Scanner;
        public Copier()
        {
            _Printer = new Printer();
            _Scanner = new Scanner();
        }
        public new void PowerOn()
        {
            if (state == IDevice.State.off)
            {
                Counter++;
                base.PowerOn();
            }
        }
        public new void PowerOff()
        {
            if (state == IDevice.State.on)
                base.PowerOff();
        }
        public void Scan(IDocument.FormatType formatType)
        {
            if (state == IDevice.State.on)
            {
                _Scanner.PowerOn();
                _Scanner.Scan(out IDocument document, formatType);
                ScanCounter = _Scanner.ScanCounter;
                _Scanner.PowerOff();
            }

        }
        public void Print(IDocument document)
        {
            if (state == IDevice.State.on)
            {
                _Printer.PowerOn();
                _Printer.Print(document);
                PrintCounter = _Printer.PrintCounter;
                _Printer.PowerOff();
            }
        }
        public void ScanAndPrint(IDocument.FormatType formatType)
        {
            if (state == IDevice.State.on)
            {
                _Scanner.PowerOn();
                _Scanner.Scan(out IDocument document, formatType);
                ScanCounter = _Scanner.ScanCounter;
                _Printer.PowerOn();
                _Printer.Print(document);
                PrintCounter = _Printer.PrintCounter;
                _Scanner.PowerOff();
                _Printer.PowerOff();
            }
        }
    }
}
