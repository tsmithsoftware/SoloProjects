using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kata2_StringCalc.Interfaces
{
    public interface ICalc
    {
        int Add(params int[] numbers);
    }
}
