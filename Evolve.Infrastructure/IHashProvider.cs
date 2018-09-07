﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve.Infrastructure
{
    public interface IHashProvider
    {
        string GenerateHash(string hashObject);
    }
}
