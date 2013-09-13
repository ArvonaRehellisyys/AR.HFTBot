using System;
using System.Collections.Generic;

namespace AR.Hft.Process.Domain
{
    public interface ISignal
    {
        Assessment Assess();
    }
}