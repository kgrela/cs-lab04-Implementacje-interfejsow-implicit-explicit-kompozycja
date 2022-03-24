﻿using System;
using System.Collections.Generic;
using System.Text;

namespace lab04_ex_03
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
        void Fax(string reciever, IDocument document);
    }
}
