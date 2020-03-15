﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ITelnetClient
    {
        void Connect(string ip, int port);
        void Write(string command);
        string Read();
        void Disconnect();
    }
}
