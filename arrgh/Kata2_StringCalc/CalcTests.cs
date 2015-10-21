using System;
using Kata2_StringCalc.Interfaces;
using Moq;
using Xunit;

namespace Kata2_StringCalc
{
    public class CalcTests
    {
        [Fact]
        public void MethodTakesOneParameter()
        {
            var calc = new Calc();
            Assert.True(calc.Add(1) == 1);
        }

        [Fact]
        public void MethodTakesMultipleParameter()
        {
            var calc = new Calc();
            Assert.True(calc.Add(1,2,3,4) == 10);
        }
    }
}
