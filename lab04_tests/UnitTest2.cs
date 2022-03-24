using NUnit.Framework;
using System;
using System.IO;
using lab04;

namespace lab04_tests
{
    public class Tests2
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


        [TestFixture]
        public class UnitTestFax
        {

            [Test]
            public void Fax_GetState_StateOff()
            {
                var fax = new MultifunctionalDevice();
                fax.PowerOff();

                Assert.AreEqual(IDevice.State.off, fax.GetState());
            }

            [Test]
            public void Fax_GetState_StateOn()
            {
                var fax = new MultifunctionalDevice();
                fax.PowerOn();

                Assert.AreEqual(IDevice.State.on, fax.GetState());
            }

            [Test]
            public void Fax_Print_DeviceOn()
            {
                var fax = new MultifunctionalDevice();
                fax.PowerOn();

                var currentConsoleOut = Console.Out;
                currentConsoleOut.Flush();
                using (var consoleOutput = new ConsoleRedirectionToStringWriter())
                {
                    IDocument doc1 = new PDFDocument("aaa.pdf");
                    fax.Print(in doc1);
                    Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
                }
                Assert.AreEqual(currentConsoleOut, Console.Out);
            }

            [Test]
            public void Fax()
            {
                var fax = new MultifunctionalDevice();
                fax.PowerOff();

                var currentConsoleOut = Console.Out;
                currentConsoleOut.Flush();
                using (var consoleOutput = new ConsoleRedirectionToStringWriter())
                {
                    IDocument doc1 = new PDFDocument("aaa.pdf");
                    fax.Print(in doc1);
                    Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
                }
                Assert.AreEqual(currentConsoleOut, Console.Out);
            }

            [Test]
            public void Fax_Scan_DeviceOff()
            {
                var fax = new MultifunctionalDevice();
                fax.PowerOff();

                var currentConsoleOut = Console.Out;
                currentConsoleOut.Flush();
                using (var consoleOutput = new ConsoleRedirectionToStringWriter())
                {
                    IDocument doc1;
                    fax.Scan(out doc1);
                    Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
                }
                Assert.AreEqual(currentConsoleOut, Console.Out);
            }

            [Test]
            public void Fax_Scan_DeviceOn()
            {
                var fax = new MultifunctionalDevice();
                fax.PowerOn();

                var currentConsoleOut = Console.Out;
                currentConsoleOut.Flush();
                using (var consoleOutput = new ConsoleRedirectionToStringWriter())
                {
                    IDocument doc1;
                    fax.Scan(out doc1);
                    Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                }
                Assert.AreEqual(currentConsoleOut, Console.Out);
            }

            [Test]
            public void Fax_Scan_FormatTypeDocument()
            {
                var fax = new MultifunctionalDevice();
                fax.PowerOn();

                var currentConsoleOut = Console.Out;
                currentConsoleOut.Flush();
                using (var consoleOutput = new ConsoleRedirectionToStringWriter())
                {
                    IDocument doc1;
                    fax.Scan(out doc1, formatType: IDocument.FormatType.JPG);
                    Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                    Assert.IsTrue(consoleOutput.GetOutput().Contains(".jpg"));

                    fax.Scan(out doc1, formatType: IDocument.FormatType.TXT);
                    Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                    Assert.IsTrue(consoleOutput.GetOutput().Contains(".txt"));

                    fax.Scan(out doc1, formatType: IDocument.FormatType.PDF);
                    Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                    Assert.IsTrue(consoleOutput.GetOutput().Contains(".pdf"));
                }
                Assert.AreEqual(currentConsoleOut, Console.Out);
            }

            [Test]
            public void Fax_ScanAndPrint_DeviceOn()
            {
                var fax = new MultifunctionalDevice();
                fax.PowerOn();

                var currentConsoleOut = Console.Out;
                currentConsoleOut.Flush();
                using (var consoleOutput = new ConsoleRedirectionToStringWriter())
                {
                    fax.ScanAndPrint();
                    Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                    Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
                }
                Assert.AreEqual(currentConsoleOut, Console.Out);
            }

            [Test]
            public void Fax_ScanAndPrint_DeviceOff()
            {
                var fax = new MultifunctionalDevice();
                fax.PowerOff();

                var currentConsoleOut = Console.Out;
                currentConsoleOut.Flush();
                using (var consoleOutput = new ConsoleRedirectionToStringWriter())
                {
                    fax.ScanAndPrint();
                    Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
                    Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
                }
                Assert.AreEqual(currentConsoleOut, Console.Out);
            }

            [Test]
            public void Fax_PrintCounter()
            {
                var fax = new MultifunctionalDevice();
                fax.PowerOn();

                IDocument doc1 = new PDFDocument("aaa.pdf");
                fax.Print(in doc1);
                IDocument doc2 = new TextDocument("aaa.txt");
                fax.Print(in doc2);
                IDocument doc3 = new ImageDocument("aaa.jpg");
                fax.Print(in doc3);

                fax.PowerOff();
                fax.Print(in doc3);
                fax.Scan(out doc1);
                fax.PowerOn();

                fax.ScanAndPrint();
                fax.ScanAndPrint();

                Assert.AreEqual(5, fax.PrintCounter);
            }

            [Test]
            public void Fax_ScanCounter()
            {
                var fax = new MultifunctionalDevice();
                fax.PowerOn();

                IDocument doc1;
                fax.Scan(out doc1);
                IDocument doc2;
                fax.Scan(out doc2);

                IDocument doc3 = new ImageDocument("aaa.jpg");
                fax.Print(in doc3);

                fax.PowerOff();
                fax.Print(in doc3);
                fax.Scan(out doc1);
                fax.PowerOn();

                fax.ScanAndPrint();
                fax.ScanAndPrint();

                Assert.AreEqual(4, fax.ScanCounter);
            }

            [Test]
            public void Fax_PowerOnCounter()
            {
                var fax = new MultifunctionalDevice();
                fax.PowerOn();
                fax.PowerOn();
                fax.PowerOn();

                IDocument doc1;
                fax.Scan(out doc1);
                IDocument doc2;
                fax.Scan(out doc2);

                fax.PowerOff();
                fax.PowerOff();
                fax.PowerOff();
                fax.PowerOn();

                IDocument doc3 = new ImageDocument("aaa.jpg");
                fax.Print(in doc3);

                fax.PowerOff();
                fax.Print(in doc3);
                fax.Scan(out doc1);
                fax.PowerOn();

                fax.ScanAndPrint();
                fax.ScanAndPrint();

                Assert.AreEqual(3, fax.Counter);
            }

        }
    }
}