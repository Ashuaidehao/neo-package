using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neo.VM;

namespace neo_package
{
    record OpCodeInfo
    {
        public byte Index { get; set; }
        public string Name { get; set; }
        public OperandSizeAttribute Operand { get; set; }
    }


}
