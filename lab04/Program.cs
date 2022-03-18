using System;

namespace lab04
{
    class Program
    {
        static void Main(string[] args)
        {
            var xerox = new Copier();
            xerox.PowerOn();
            IDocument doc1 = new PDFDocument("aaa.pdf");
            xerox.Print(in doc1);

            IDocument doc2;
            xerox.Scan(out doc2, IDocument.FormatType.JPG);

            xerox.ScanAndPrint();
            System.Console.WriteLine(xerox.Counter);
            System.Console.WriteLine(xerox.PrintCounter);
            System.Console.WriteLine(xerox.ScanCounter);

            IFax faxox = new MultifunctionalDevice();
            faxox.PowerOn();
            faxox.SendFax(new ImageDocument("ZDJECIE.JPG"), "+123456789");
            faxox.ReceiveFax(out IDocument doc, IDocument.FormatType.JPG);
            faxox.PowerOff();
        }
    }
}
