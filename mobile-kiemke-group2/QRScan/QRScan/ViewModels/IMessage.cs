using System;
using System.Collections.Generic;
using System.Text;

namespace QRScan.ViewModels
{
    public interface IMessage
    {
        void Longtime(string message);
        void Shorttime(string message);
    }
}
