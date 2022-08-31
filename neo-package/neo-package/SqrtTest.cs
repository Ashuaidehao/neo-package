using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace neo_package
{
    public class SqrtTest
    {

        [Benchmark]
        public void TestSqrtDivision()
        {
            SqrtDivision(1000);
        }

        [Benchmark]
        public void TestSqrtBit()
        {
            SqrtBit(1000);
        }


        public BigInteger SqrtDivision(BigInteger value)
        {
            if (value < 0) throw new InvalidOperationException("value can not be negative");
            if (value.IsZero) return BigInteger.Zero;
            if (value < 4) return BigInteger.One;

            var z = value;
            var x = value / 2 + 1;
            while (x < z)
            {
                z = x;
                x = (value / x + x) / 2;
            }

            return z;
        }


        public BigInteger SqrtBit(BigInteger value)
        {
            if (value < 0) throw new InvalidOperationException("value can not be negative");
            if (value.IsZero) return BigInteger.Zero;
            if (value < 4) return BigInteger.One;

            var z = value;
            var x = BigInteger.One << (int)(((value - 1).GetBitLength() + 1) >> 1); 
            while (x < z)
            {
                z = x;
                x = (value / x + x) / 2;
            }

            return z;
        }
    }
}
