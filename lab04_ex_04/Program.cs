using System;

namespace lab04_ex_04
{
    class Program
    {
        static void Main(string[] args)
        {
            Copier x = new Copier();
            Console.WriteLine($"Current copier state: {x.GetState()}");

            ((IDevice)x).PowerOn();
            Console.WriteLine($"Current copier state: {x.GetState()}");


            var doc = new PDFDocument("doc.pdf");
            ((IPrinter)x).Print(doc);
            ((IPrinter)x).Print(doc);
            ((IPrinter)x).Print(doc);

            // cool down
            ((IPrinter)x).Print(doc);
            ((IPrinter)x).Print(doc);

            IDocument doc1;
            IDocument doc2;
            IDocument doc3;

            ((IScanner)x).Scan(out doc1);
            ((IScanner)x).Scan(out doc2);

            // cool down
            ((IScanner)x).Scan(out doc3);
            x.GetCounter();

            Console.WriteLine($"Current copier state: {x.GetState()}");

            ((IDevice)x).PowerOff();
            Console.WriteLine($"Current copier state: {x.GetState()}");
        }
    }
}
