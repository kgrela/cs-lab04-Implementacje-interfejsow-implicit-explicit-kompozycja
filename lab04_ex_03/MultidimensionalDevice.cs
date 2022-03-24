using System;
using System.Collections.Generic;
using System.Text;

namespace lab04_ex_03
{
    public class MultidimensionalDevice : Copier
    {
        public int FaxCounter => _Fax.FaxCounter;
        public readonly FaxDevice _Fax;
        public MultidimensionalDevice()
        {
            _Fax = new FaxDevice();
        }
        public void FaxDocument(string reciever, IDocument.FormatType formatType)
        {
            if (string.IsNullOrEmpty(reciever))
                throw new ArgumentNullException();
            if (state == IDevice.State.on)
            {
                _Scanner.PowerOn();
                _Scanner.Scan(out IDocument document, formatType);
                ScanCounter = _Scanner.ScanCounter;
                _Scanner.PowerOff();
                _Fax.PowerOn();
                _Fax.Fax(reciever, document);
                _Fax.PowerOff();

            }
        }
    }
}
