using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kata2_StringCalc.Interfaces;

namespace Kata2_StringCalc
{
    public class Calc : ICalc
    {
        public int Add(params int[] numbers)
        {
            int answer = 0;
            foreach (int num in numbers)
            {
                answer = answer + num;
            }
            return answer;
        }
    }
}
