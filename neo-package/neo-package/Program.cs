using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Neo.SmartContract;
using Neo.SmartContract.Native;
using Neo.VM;

namespace neo_package
{
    class Program
    {

        struct MyStruct
        {
            public string Name;
        }
        static void Main(string[] args)
        {
            Console.WriteLine($"{NativeContract.NameService.Hash}");


            var p = new MyStruct() { Name = "S" };
            var p2 = new MyStruct() { Name = "S" };

            var a = p.Equals(p2);
            using (var engine = new ExecutionEngine())
            using (var sb = new ScriptBuilder())
            {
                sb.EmitPush(0);
                sb.Emit(OpCode.NEWARRAY);
                sb.Emit(OpCode.ASSERT);


                engine.LoadScript(sb.ToArray());
                var state = engine.Execute();
                var result = engine.ResultStack.Pop().GetString();

            }





            Console.WriteLine("Hello World!");
        }


        public static void TestSubStr()
        {
            //不支持中文字符
            using (var engine = new ExecutionEngine())
            using (var sb = new ScriptBuilder())
            {
                sb.EmitPush("！Hello");
                sb.EmitPush(1);
                sb.EmitPush(2);
                sb.Emit(OpCode.SUBSTR);


                engine.LoadScript(sb.ToArray());
                var state = engine.Execute();
                var result = engine.ResultStack.Pop().GetString();

            }
        }

        public static void SaveOpCodes()
        {
            var opCodes = new List<OpCodeInfo>();
            foreach (FieldInfo field in typeof(OpCode).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                OperandSizeAttribute attribute = field.GetCustomAttribute<OperandSizeAttribute>();
                var opcode = (OpCode)field.GetValue(null);
                byte index = (byte)opcode;
                var opcodeInfo = new OpCodeInfo() { Index = index, Name = opcode.ToString(), Operand = attribute };
                opCodes.Add(opcodeInfo);
                //if (attribute == null) continue;
            }

            var lines = new List<string>();
            lines.Add("number,name,prefix,size");
            foreach (var opCodeInfo in opCodes)
            {
                lines.Add($"{opCodeInfo.Index},{opCodeInfo.Name},{opCodeInfo.Operand?.SizePrefix},{opCodeInfo.Operand?.Size}");

                Console.WriteLine($"[{opCodeInfo.Index}]-{opCodeInfo.Name}:{opCodeInfo.Operand?.SizePrefix}-{opCodeInfo.Operand?.Size}");
            }


            File.WriteAllLines("opcodes.csv", lines);
            Console.WriteLine($"{opCodes.Count}");
        }
    }

}
