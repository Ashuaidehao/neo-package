using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Neo;
using Neo.VM;

namespace neo_package
{
    public class EngineBenchmarkTest
    {
        //[Benchmark]
        public void Test()
        {
            var script = "c2104d11c0114d11c0560100199d60114d114d12c05312c0584a24f3455145".HexToBytes();
            var engine = new ExecutionEngine();
            engine.LoadScript(script);
            engine.Execute();
        }



        public void Test2()
        {
            var script = "5601c24a4a01020060589d6011c05824fa11c0cf0260cc050060589d6010ce5824fa10ce45".HexToBytes();
            var engine = new ExecutionEngine();
            engine.LoadScript(script);
            engine.Execute();
        }

        /// <summary>
        /// Out of Memory Clone
        /// </summary>
        public void Test3()
        {
            var script = "5601c501fe0360589d604a12c0db415824f7cd45".HexToBytes();
            var engine = new ExecutionEngine();
            engine.LoadScript(script);
            engine.Execute();
        }

        /// <summary>
        /// Out of Memory Equal
        /// </summary>
        public void Test3_1()
        {
            var script = "560112c501fe0160589d604a12c0db415824f7509d4a102aec4597".HexToBytes();
            var engine = new ExecutionEngine();
            engine.LoadScript(script);
            engine.Execute();
        }

        /// <summary>
        /// Out of Memory Equal
        /// </summary>
        public void Test3_1_1()
        {
            var script = "560112c501010060589d604a12c0db415824f7509d4a102aec4597".HexToBytes();
            var engine = new ExecutionEngine();
            engine.LoadScript(script);
            engine.Execute();
        }



        /// <summary>
        /// json
        /// </summary>
        public void Test4()
        {
            var script = "56010c015b0c135b5b5b5b5b5b5b5b5b5d5d5d5d5d5d5d5d5d2c1f60589d604a8b5824fa0c135b5b5b5b5b5b5b5b5b5d5d5d5d5d5d5d5d5d2c1e60589d604a8b5824fa02ffbf04008d8b8b0c015d8b11c01f0c0f6a736f6e446573657269616c697a650c14c0ef39cee0e4e925c6c2a06a79e1440dd86fceac41627d5b5245".HexToBytes();
            var engine = new ExecutionEngine();
            engine.LoadScript(script);
            engine.Execute();
        }

        [Benchmark]
        public void Test1()
        {
            var a = 1;
            a++;
        }
    }
}
