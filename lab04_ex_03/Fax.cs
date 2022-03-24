using System;
using System.Collections.Generic;
using System.Text;

namespace lab04_ex_03
{
    public class FaxDevice : IFax
    {
        public IDevice.State State { get; private set; } = IDevice.State.off;
        public int Counter { get; private set; }
        public int FaxCounter { get; private set; }
        public List<string> RecieversList { get; private set; }
        public FaxDevice()
        {
            RecieversList = new List<string>();
        }

        public IDevice.State GetState() => State;

        public void PowerOff()
        {
            if (State == IDevice.State.on)
            {
                State = IDevice.State.off;
                Console.WriteLine("Fax's functions have been turned off");
            }
        }

        public void PowerOn()
        {
            if (State == IDevice.State.off)
            {
                State = IDevice.State.on;
                Console.WriteLine("Fax's functions have been turned on");
            }
        }

        public void Fax(string reciever, IDocument document)
        {
            if (!RecieversList.Contains(reciever))
                RecieversList.Add(reciever);
            FaxCounter++;
            Console.WriteLine($"Sending {document.GetFileName()} to {reciever}.");
        }
        public void GetRecieversList()
        {
            int len = RecieversList.Count;
            for (int i = 0; i < len; i++)
                Console.WriteLine($"{i + 1}. {RecieversList[i]}");
        }
    }
}
