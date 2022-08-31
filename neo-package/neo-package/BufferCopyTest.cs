using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace neo_package
{
    public class BufferCopyTest
    {
        private const int N = 10000;
        private readonly byte[] data;

        public BufferCopyTest()
        {
            data = new byte[N];
            new Random(42).NextBytes(data);
        }


        [Benchmark]
        public byte[] BufferCopy()
        {
            var result = new byte[N];
            Buffer.BlockCopy(data, 0, result, 0, N);
            return result;
        }



        [Benchmark]
        public byte[] ArrayCopy()
        {
            var result = new byte[N];
            Array.Copy(data, 0, result, 0, N);
            return result;
        }
    }
}
