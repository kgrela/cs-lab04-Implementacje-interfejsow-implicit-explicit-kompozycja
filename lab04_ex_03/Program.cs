using System;

namespace lab04_ex_03
{
    class Program
    {
        static void Main(string[] args)
        {
            Copier x = new Copier();
            Console.WriteLine($"copier counter: {x.Counter}");
            Console.WriteLine($"print counter: {x.PrintCounter}");
            Console.WriteLine(x.GetState());

            x.PowerOn();
            Console.WriteLine(x.GetState());

            IDocument doc1 = new PDFDocument("doc1.pdf");
            x.Print(doc1);

            x.Scan(IDocument.FormatType.TXT);
            x.Scan(IDocument.FormatType.JPG);

            Console.WriteLine($"copier counter: {x.Counter}");
            Console.WriteLine($"print counter: {x.PrintCounter}");
            Console.WriteLine($"scan counter: {x.ScanCounter}");

            x.ScanAndPrint(IDocument.FormatType.PDF);

            Console.WriteLine($"print counter: {x.PrintCounter}");
            Console.WriteLine($"scan counter: {x.ScanCounter}\n");

            MultidimensionalDevice multi = new MultidimensionalDevice();
            Console.WriteLine(multi.GetState());

            multi.PowerOn();
            Console.WriteLine(multi.GetState());

            Console.WriteLine($"multi print counter: {multi.PrintCounter}");
            Console.WriteLine($"multi scan counter: {multi.ScanCounter}");

            multi.Print(doc1);
            multi.Scan(IDocument.FormatType.TXT);
            multi.Scan(IDocument.FormatType.JPG);

            Console.WriteLine($"multi print counter: {multi.PrintCounter}");
            Console.WriteLine($"multi scan counter: {multi.ScanCounter}");
            Console.WriteLine("sending fax .txt document to testRecievier");
            multi.FaxDocument("testReciever", IDocument.FormatType.TXT);
            Console.WriteLine("sending fax .pdf document to testRecievier2");
            multi.FaxDocument("testReciever2", IDocument.FormatType.PDF);

            Console.WriteLine($"multi counter: {multi.Counter}");
            Console.WriteLine($"multi print counter: {multi.PrintCounter}");
            Console.WriteLine($"multi scan counter: {multi.ScanCounter}");
            Console.WriteLine($"multi fax counter: {multi.FaxCounter}");
        }
    }
}
