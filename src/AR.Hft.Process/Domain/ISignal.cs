﻿using System;

namespace AR.Hft.Process.Domain
{
    public interface ISignal
    {
        Assessment Assess(string symbol);
    }
}