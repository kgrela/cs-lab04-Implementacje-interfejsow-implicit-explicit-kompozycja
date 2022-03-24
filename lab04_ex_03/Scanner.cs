using System;
using System.Collections.Generic;
using System.Text;

namespace lab04_ex_03
{
    public class Scanner : IScanner
    {
        public IDevice.State State { get; private set; } = IDevice.State.off;
        public int Counter { get; private set; }
        public int ScanCounter { get; private set; }

        public IDevice.State GetState() => State;

        public void PowerOff()
        {
            if (State == IDevice.State.on)
            {
                State = IDevice.State.off;
                Console.WriteLine("Scanning functions turned off");
            }
        }

        public void PowerOn()
        {
            if (State == IDevice.State.off)
            {
                State = IDevice.State.on;
                Counter++;
                Console.WriteLine("Scanning functions turned on");
            }
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.JPG)
        {
            document = null;
            if (State == IDevice.State.on)
            {
                string scanType = "";
                switch (formatType)
                {
                    case IDocument.FormatType.JPG:
                        scanType = $"ImageScan{++ScanCounter}.jpg";
                        document = new ImageDocument(scanType);
                        break;
                    case IDocument.FormatType.PDF:
                        scanType = $"PDFScan{++ScanCounter}.pdf";
                        document = new PDFDocument(scanType);
                        break;
                    case IDocument.FormatType.TXT:
                        scanType = $"TextScan{++ScanCounter}.txt";
                        document = new TextDocument(scanType);
                        break;
                }
                Console.WriteLine($"{DateTime.Now} Scan: {scanType}");
            }
        }
    }
}
