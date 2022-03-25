using System;

namespace lab04_ex_05
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Copier copier = new Copier();
            Console.WriteLine($"Current copier state: {copier.GetState()}");

            copier.PowerOn();
            Console.WriteLine($"Current copier state: {copier.GetState()}");

            var doc = new PDFDocument("doc.pdf");
            copier._Printer.Print(doc);
            copier._Printer.Print(doc);
            copier._Printer.Print(doc);
            copier._Printer.Print(doc);

            copier._Scanner.Scan();
            copier._Scanner.Scan();
            copier._Scanner.Scan();

            copier.GetCounter();
            Console.WriteLine($"Current copier state: {copier.GetState()}");

            copier.PowerOff();
            Console.WriteLine($"Current copier state: {copier.GetState()}");
        }
    }
}
