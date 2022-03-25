using System;
using System.Threading;

namespace lab04_ex_05
{
    public interface IDevice
    {
        enum State { on, off, standby };

        State GetState();
        void PowerOn() => SetState(State.on);
        void PowerOff() => SetState(State.off);
        void StandbyOn() => SetState(State.standby);
        void StandbyOff() => SetState(State.on);
        abstract protected void SetState(State state);
        int Counter { get; }
    }


    public interface IPrinter : IDevice
    {
        public static IDevice.State PrinterState = IDevice.State.off;
        public static int PrintCounter;
        public static void Print(in IDocument document)
        {
            if (PrinterState == IDevice.State.on || PrinterState == IDevice.State.standby)
            {
                PrinterState = IDevice.State.on;
                if (PrintCounter != 0 && PrintCounter % 3 == 0)
                {
                    Console.WriteLine("Printer is cooling down, wait 2 sec until next print");
                    Thread.Sleep(2000);
                }
                Console.WriteLine($"{DateTime.Now} Print: {document.GetFileName()}");
                PrintCounter++;
                PrinterState = IDevice.State.standby;
            }

        }
        public new State GetState() => PrinterState;
        public new State SetState(IDevice.State state) => PrinterState = state;
    }

    public interface IScanner : IDevice
    {
        public static IDevice.State ScannerState = IDevice.State.off;
        public static int ScanCounter;
        public new State SetState(IDevice.State state) => ScannerState = state;
        public new State GetState() => ScannerState;
        public static void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.JPG)
        {
            document = null;
            if (ScannerState == IDevice.State.on || ScannerState == IDevice.State.standby)
            {
                ScannerState = IDevice.State.on;
                if (ScanCounter != 0 && ScanCounter % 2 == 0)
                {
                    Console.WriteLine("Scanner is cooling down, wait 2 sec until next scan");
                    Thread.Sleep(2000);
                }

                string scanType = "";
                switch (formatType)
                {
                    case IDocument.FormatType.JPG:
                        scanType = $"ImageScan{ScanCounter}.jpg";
                        document = new ImageDocument(scanType);
                        break;
                    case IDocument.FormatType.PDF:
                        scanType = $"PDFScan{ScanCounter}.pdf";
                        document = new PDFDocument(scanType);
                        break;
                    case IDocument.FormatType.TXT:
                        scanType = $"TextScan{ScanCounter}.txt";
                        document = new TextDocument(scanType);
                        break;
                }
                Console.WriteLine($"{DateTime.Now} Scan: {scanType}");
                ScanCounter++;
                ScannerState = IDevice.State.standby;
            }

        }
    }
}
