using System;
using System.Collections.Generic;
using System.Text;

namespace lab04
{
    public interface IDevice
    {
        enum State { on, off };

        void PowerOn(); 
        void PowerOff(); 
        State GetState(); 

        int Counter { get; }  
    }

    public abstract class BaseDevice : IDevice
    {
        protected IDevice.State state = IDevice.State.off;
        public IDevice.State GetState() => state;

        public void PowerOff()
        {
            state = IDevice.State.off;
            Console.WriteLine("... Device is off !");
        }

        public void PowerOn()
        {
            state = IDevice.State.on;
            Console.WriteLine("Device is on ...");
        }

        public int Counter { get; private set; } = 0;
    }

    public interface IPrinter : IDevice
    {
        /// <summary>
        /// Dokument jest drukowany, jeśli urządzenie włączone. W przeciwnym przypadku nic się nie wykonuje
        /// </summary>
        /// <param name="document">obiekt typu IDocument, różny od `null`</param>
        void Print(in IDocument document);
    }

    public interface IScanner : IDevice
    {
        // dokument jest skanowany, jeśli urządzenie włączone
        // w przeciwnym przypadku nic się dzieje
        void Scan(out IDocument document, IDocument.FormatType formatType);
    }

    public interface IFax : IDevice
    {
        void SendFax(IDocument document, string receiver);
        void ReceiveFax(out IDocument document, IDocument.FormatType formatType);
    }

    public class Copier : IPrinter, IScanner
    {
        public int Counter { get; set; }
        public int PrintCounter { get; set; }
        public int ScanCounter { get; set; }

        private IDevice.State state = IDevice.State.off;

        public IDevice.State GetState()
        {
            return state;
        }

        public void PowerOff()
        {
            state = IDevice.State.off;
        }

        public void PowerOn()
        {
            if (state == IDevice.State.off)
            {
                state = IDevice.State.on;
                Counter++;
            }

        }

        public void Print(in IDocument document)
        {
            if (state == IDevice.State.off)
            {
                return;
            }

            Console.WriteLine($"{DateTime.Now} Print: {document.GetFileName()}");
            PrintCounter++;
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.JPG)
        {
            if (state == IDevice.State.off)
            {
                document = null;
                return;
            }

            ScanCounter++;
            string filename = "";
            switch (formatType)
            {
                case IDocument.FormatType.TXT:
                    filename = "TextScan" + ScanCounter + ".txt";
                    document = new TextDocument(filename);
                    break;
                case IDocument.FormatType.PDF:
                    filename = "PDFScan" + ScanCounter + ".pdf";
                    document = new PDFDocument(filename);
                    break;
                case IDocument.FormatType.JPG:
                    filename = "ImageScan" + ScanCounter + ".jpg";
                    document = new ImageDocument(filename);
                    break;
                default:
                    throw new NotImplementedException();
            }

            Console.WriteLine($"{DateTime.Now} Scan: {filename}");
        }

        public void ScanAndPrint()
        {
            Scan(out IDocument doc, IDocument.FormatType.JPG);
            Print(doc);
        }
    }

    public class MultifunctionalDevice : IPrinter, IScanner, IFax
    {
        private IFax.State state = IFax.State.off;
        public int FaxCounter { get; set; }
        public int Counter { get; set; }
        public int PrintCounter { get; set; }
        public int ScanCounter { get; set; }

        public IDevice.State GetState()
        {
            return state;
        }

        public void PowerOff()
        {
            state = IFax.State.off;
        }

        public void PowerOn()
        {
            if (state == IFax.State.off)
            {
                state = IFax.State.on;
                Counter++;
            }
        }

        public void ReceiveFax(out IDocument document, IDocument.FormatType formatType)
        {
            if (state == IFax.State.off)
            {
                document = null;
                return;
            }

            string filename = "";
            switch (formatType)
            {
                case IDocument.FormatType.TXT:
                    filename = "TextScan" + FaxCounter + ".txt";
                    document = new TextDocument(filename);
                    break;
                case IDocument.FormatType.PDF:
                    filename = "PDFScan" + FaxCounter + ".pdf";
                    document = new PDFDocument(filename);
                    break;
                case IDocument.FormatType.JPG:
                    filename = "ImageScan" + FaxCounter + ".jpg";
                    document = new ImageDocument(filename);
                    break;
                default:
                    throw new NotImplementedException();
            }
            Console.WriteLine($"{DateTime.Now} Received: {filename}");
        }

        public void SendFax(IDocument document, string receiver)
        {
            if (state == IDevice.State.off)
            {
                return;
            }

            Console.WriteLine($"{DateTime.Now} Send: {document.GetFileName()} to {receiver}");
            FaxCounter++;
        }

        public void Print(in IDocument document)
        {
            if (state == IDevice.State.off)
            {
                return;
            }

            Console.WriteLine($"{DateTime.Now} Print: {document.GetFileName()}");
            PrintCounter++;
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.JPG)
        {
            if (state == IDevice.State.off)
            {
                document = null;
                return;
            }

            ScanCounter++;
            string filename = "";
            switch (formatType)
            {
                case IDocument.FormatType.TXT:
                    filename = "TextScan" + ScanCounter + ".txt";
                    document = new TextDocument(filename);
                    break;
                case IDocument.FormatType.PDF:
                    filename = "PDFScan" + ScanCounter + ".pdf";
                    document = new PDFDocument(filename);
                    break;
                case IDocument.FormatType.JPG:
                    filename = "ImageScan" + ScanCounter + ".jpg";
                    document = new ImageDocument(filename);
                    break;
                default:
                    throw new NotImplementedException();
            }

            Console.WriteLine($"{DateTime.Now} Scan: {filename}");
        }

        public void ScanAndPrint()
        {
            Scan(out IDocument doc, IDocument.FormatType.JPG);
            Print(doc);
        }
    }
}
