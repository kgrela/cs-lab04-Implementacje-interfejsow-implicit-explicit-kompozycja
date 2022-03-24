using NUnit.Framework;
using System;
using System.IO;
using lab04_ex_03;

namespace lab04_tests
{
    [TestFixture]
    public class Tests3
    {

        public class ConsoleRedirectionToStringWriter : IDisposable
        {
            private StringWriter stringWriter;
            private TextWriter originalOutput;

            public ConsoleRedirectionToStringWriter()
            {
                stringWriter = new StringWriter();
                originalOutput = Console.Out;
                Console.SetOut(stringWriter);
            }

            public string GetOutput()
            {
                return stringWriter.ToString();
            }

            public void Dispose()
            {
                Console.SetOut(originalOutput);
                stringWriter.Dispose();
            }
        }

        [Test]
        public void Copier_GetState_StateOff()
        {
            var copier = new Copier();
            copier.PowerOff();

            Assert.AreEqual(IDevice.State.off, copier.GetState());
        }

        [Test]
        public void Copier_GetState_StateOn()
        {
            var copier = new Copier();
            copier.PowerOn();

            Assert.AreEqual(IDevice.State.on, copier.GetState());
        }

        [Test]
        public void Copier_Print_DeviceOn()
        {
            var copier = new Copier();
            copier.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                copier.Print(doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [Test]
        public void Copier_Print_DeviceOff()
        {
            var copier = new Copier();
            copier.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                copier.Print(doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [Test]
        public void Copier_Scan_DeviceOff()
        {
            var copier = new Copier();
            copier.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                copier.Scan(IDocument.FormatType.PDF);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [Test]
        public void Copier_Scan_DeviceOn()
        {
            var copier = new Copier();
            copier.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                copier.Scan(IDocument.FormatType.PDF);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [Test]
        public void Copier_Scan_FormatTypeDocument()
        {
            var copier = new Copier();
            copier.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                copier.Scan(IDocument.FormatType.JPG);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".jpg"));

                copier.Scan(IDocument.FormatType.TXT);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".txt"));

                copier.Scan(IDocument.FormatType.PDF);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".pdf"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [Test]
        public void Copier_ScanAndPrint_DeviceOn()
        {
            var copier = new Copier();
            copier.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                copier.ScanAndPrint(IDocument.FormatType.PDF);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [Test]
        public void Copier_ScanAndPrint_DeviceOff()
        {
            var copier = new Copier();
            copier.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                copier.ScanAndPrint(IDocument.FormatType.PDF);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [Test]
        public void Copier_PrintCounter()
        {
            var copier = new Copier();
            copier.PowerOn();

            IDocument doc1 = new PDFDocument("aaa.pdf");
            copier.Print(doc1);
            IDocument doc2 = new TextDocument("aaa.txt");
            copier.Print(doc2);
            IDocument doc3 = new ImageDocument("aaa.jpg");
            copier.Print(doc3);

            copier.PowerOff();
            copier.Print(doc3);
            copier.Scan(IDocument.FormatType.PDF);
            copier.PowerOn();

            copier.ScanAndPrint(IDocument.FormatType.JPG);
            copier.ScanAndPrint(IDocument.FormatType.TXT);

            Assert.AreEqual(5, copier.PrintCounter);
        }

        [Test]
        public void Copier_ScanCounter()
        {
            var copier = new Copier();
            copier.PowerOn();

            IDocument doc1;
            copier.Scan(IDocument.FormatType.PDF);
            IDocument doc2;
            copier.Scan(IDocument.FormatType.JPG);

            IDocument doc3 = new ImageDocument("aaa.jpg");
            copier.Print(doc3);

            copier.PowerOff();
            copier.Print(doc3);
            copier.Scan(IDocument.FormatType.PDF);
            copier.PowerOn();

            copier.ScanAndPrint(IDocument.FormatType.TXT);
            copier.ScanAndPrint(IDocument.FormatType.PDF);

            Assert.AreEqual(4, copier.ScanCounter);
        }

        [Test]
        public void Copier_PowerOnCounter()
        {
            var copier = new Copier();
            copier.PowerOn();
            copier.PowerOn();
            copier.PowerOn();

            IDocument doc1;
            copier.Scan(IDocument.FormatType.JPG);
            IDocument doc2;
            copier.Scan(IDocument.FormatType.PDF);

            copier.PowerOff();
            copier.PowerOff();
            copier.PowerOff();
            copier.PowerOn();

            IDocument doc3 = new ImageDocument("aaa.jpg");
            copier.Print(doc3);

            copier.PowerOff();
            copier.Print(doc3);
            copier.Scan(IDocument.FormatType.PDF);
            copier.PowerOn();

            copier.ScanAndPrint(IDocument.FormatType.TXT);
            copier.ScanAndPrint(IDocument.FormatType.JPG);

            Assert.AreEqual(3, copier.Counter);
        }
        [Test]
        public void MultidimensionalDevice_GetState_StateOff()
        {
            var fax = new MultidimensionalDevice();
            fax.PowerOff();

            Assert.AreEqual(IDevice.State.off, fax.GetState());
        }
        [Test]
        public void MultidimensionalDevice_GetState_StateOn()
        {
            var fax = new MultidimensionalDevice();
            fax.PowerOn();

            Assert.AreEqual(IDevice.State.on, fax.GetState());
        }

        [Test]
        public void MultidimensionalDevice_Print_DeviceOn()
        {
            var fax = new MultidimensionalDevice();
            fax.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                fax.Print(doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [Test]
        public void MultidimensionalDevice_Print_DeviceOff()
        {
            var fax = new MultidimensionalDevice();
            fax.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                fax.Print(doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [Test]
        public void MultidimensionalDevice_Scan_DeviceOff()
        {
            var fax = new MultidimensionalDevice();
            fax.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                fax.Scan(IDocument.FormatType.JPG);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [Test]
        public void MultidimensionalDevice_Scan_DeviceOn()
        {
            var fax = new MultidimensionalDevice();
            fax.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                fax.Scan(IDocument.FormatType.JPG);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [Test]
        public void MultidimensionalDevice_Scan_FormatTypeDocument()
        {
            var fax = new MultidimensionalDevice();
            fax.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                fax.Scan(IDocument.FormatType.JPG);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".jpg"));

                fax.Scan(IDocument.FormatType.TXT);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".txt"));

                fax.Scan(IDocument.FormatType.PDF);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".pdf"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [Test]
        public void MultidimensionalDevice_ScanAndPrint_DeviceOn()
        {
            var fax = new MultidimensionalDevice();
            fax.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                fax.ScanAndPrint(IDocument.FormatType.JPG);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [Test]
        public void MultidimensionalDevice_ScanAndPrint_DeviceOff()
        {
            var fax = new MultidimensionalDevice();
            fax.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                fax.ScanAndPrint(IDocument.FormatType.JPG);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [Test]
        public void MultidimensionalDevice_PrintCounter()
        {
            var fax = new MultidimensionalDevice();
            fax.PowerOn();

            IDocument doc1 = new PDFDocument("aaa.pdf");
            fax.Print(doc1);
            IDocument doc2 = new TextDocument("aaa.txt");
            fax.Print(doc2);
            IDocument doc3 = new ImageDocument("aaa.jpg");
            fax.Print(doc3);

            fax.PowerOff();
            fax.Print(doc3);
            fax.Scan(IDocument.FormatType.JPG);
            fax.PowerOn();

            fax.ScanAndPrint(IDocument.FormatType.TXT);
            fax.ScanAndPrint(IDocument.FormatType.TXT);
            Assert.AreEqual(5, fax.PrintCounter);
        }

        [Test]
        public void MultidimensionalDevice_ScanCounter()
        {
            var fax = new MultidimensionalDevice();
            fax.PowerOn();

            IDocument doc1;
            fax.Scan(IDocument.FormatType.TXT);
            IDocument doc2;
            fax.Scan(IDocument.FormatType.TXT);

            IDocument doc3 = new ImageDocument("aaa.jpg");
            fax.Print(doc3);

            fax.PowerOff();
            fax.Print(doc3);
            fax.Scan(IDocument.FormatType.TXT);
            fax.PowerOn();

            fax.ScanAndPrint(IDocument.FormatType.TXT);
            fax.ScanAndPrint(IDocument.FormatType.TXT);

            Assert.AreEqual(4, fax.ScanCounter);
        }

        [Test]
        public void MultidimensionalDevice_PowerOnCounter()
        {
            var fax = new MultidimensionalDevice();
            fax.PowerOn();
            fax.PowerOn();
            fax.PowerOn();

            IDocument doc1;
            fax.Scan(IDocument.FormatType.TXT);
            IDocument doc2;
            fax.Scan(IDocument.FormatType.TXT);

            fax.PowerOff();
            fax.PowerOff();
            fax.PowerOff();
            fax.PowerOn();

            IDocument doc3 = new ImageDocument("aaa.jpg");
            fax.Print(doc3);

            fax.PowerOff();
            fax.Print(doc3);
            fax.Scan(IDocument.FormatType.TXT);
            fax.PowerOn();

            fax.ScanAndPrint(IDocument.FormatType.TXT);
            fax.ScanAndPrint(IDocument.FormatType.TXT);

            Assert.AreEqual(3, fax.Counter);
        }
        [Test]
        public void MultidimensionalDevice_Adding_Recievers_On_Recievers_List()
        {
            var multi = new MultidimensionalDevice();
            multi.PowerOn();
            multi.FaxDocument("testReciever1", IDocument.FormatType.TXT);
            multi.FaxDocument("testReciever2", IDocument.FormatType.TXT);
            multi.FaxDocument("testReciever3", IDocument.FormatType.TXT);
            multi.FaxDocument("testReciever4", IDocument.FormatType.TXT);
            Assert.AreEqual(4, multi._Fax.RecieversList.Count);
        }
        [Test]
        public void MultidimensionalDevice_NotDoubling_Recievers_On_Recievers_List()
        {
            var multi = new MultidimensionalDevice();
            multi.PowerOn();
            multi.FaxDocument("testReciever", IDocument.FormatType.TXT);
            multi.FaxDocument("testReciever", IDocument.FormatType.TXT);
            multi.FaxDocument("testReciever", IDocument.FormatType.TXT);
            Assert.AreEqual(1, multi._Fax.RecieversList.Count);
        }
        
        [Test]
        public void MultidimensionalDevice_Correct_FaxCounter()
        {
            var multi = new MultidimensionalDevice();
            multi.PowerOn();
            multi.FaxDocument("testReciever", IDocument.FormatType.TXT);
            multi.FaxDocument("testReciever", IDocument.FormatType.TXT);
            multi.FaxDocument("testReciever", IDocument.FormatType.TXT);
            multi.FaxDocument("testReciever", IDocument.FormatType.TXT);
            multi.FaxDocument("testReciever", IDocument.FormatType.TXT);
            Assert.AreEqual(5, multi.FaxCounter);
        }

        [Test]
        public void MultidimensionalDevice_Fax_DeviceOff()
        {
            var multi = new MultidimensionalDevice();
            multi.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                multi.FaxDocument("testReciever", IDocument.FormatType.TXT);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Sending"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }
        [Test]
        public void MultidimensionalDevice_Fax_DeviceOn()
        {
            var multi = new MultidimensionalDevice();
            multi.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                multi.FaxDocument("testReciever", IDocument.FormatType.TXT);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Sending"));

            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

    }
}
