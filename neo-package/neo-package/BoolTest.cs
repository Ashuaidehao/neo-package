using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace neo_package
{
    [MarkdownExporter, AsciiDocExporter, HtmlExporter, CsvExporter, RPlotExporter]
    public class BoolTest
    {
        //[Params(true, false)]
        public bool a;
        //[Params(true, false)]
        public bool b;

        //[Benchmark]
        public void And()
        {
            if (a && b)
            {

            }
        }

        //[Benchmark]
        public void AndBit()
        {
            if (a & b)
            {

            }
        }

        //[Benchmark]
        public void Or()
        {
            if (a || b)
            {

            }
        }

        //[Benchmark]
        public void OrBit()
        {
            if (a | b)
            {

            }
        }


        [Benchmark]
        public void AndTest()
        {
            And(true, true);
            And(true, false);
            And(false, true);
            And(false, false);
        }

        [Benchmark]
        public void AndBitTest()
        {
            AndBit(true, true);
            AndBit(true, false);
            AndBit(false, true);
            AndBit(false, false);
        }


        [Benchmark]
        public void OrTest()
        {
            Or(true, true);
            Or(true, false);
            Or(false, true);
            Or(false, false);
        }

        [Benchmark]
        public void OrBitTest()
        {
            OrBit(true, true);
            OrBit(true, false);
            OrBit(false, true);
            OrBit(false, false);
        }


        private bool And(bool x, bool y)
        {
            return x && y;
        }


        private bool AndBit(bool x, bool y)
        {
            return x & y;
        }


        private bool Or(bool x, bool y)
        {
            return x || y;
        }


        private bool OrBit(bool x, bool y)
        {
            return x | y;
        }
    }
}
