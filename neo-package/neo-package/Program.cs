using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using BenchmarkDotNet.Running;
using Neo;
using Neo.SmartContract;
using Neo.SmartContract.Native;
using Neo.VM;
using Neo.Wallets;

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
            //var script = "c2104d11c0114d11c0560100199d60114d114d12c05312c0584a24f3455145".HexToBytes();
            //var engine = new ExecutionEngine();
            //engine.LoadScript(script);
            //engine.Execute();
            //var test1 = new DeserializeTest();

            ////var o1 = test1.Test1();
            //var o2 = test1.Test1_1();

            //var o3 = test1.Test2();
            //var test = new EngineBenchmarkTest();
            //test.Test4();


            //var test = new SqrtTest();
            //test.TestSqrtBit();
            //test.TestSqrtDivision();
            BenchmarkRunner.Run<DeserializeTest>();
            //BenchmarkRunner.Run<BufferCopyTest>();


            Console.WriteLine("End!");
            Console.ReadLine();

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



        public static void TestRandom()
        {
            byte[] privateKey = new byte[32];
            byte[] privateKey2 = new byte[32];
            byte[] privateKey3 = new byte[32];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(privateKey);
                rng.GetBytes(privateKey2);
                rng.GetBytes(privateKey3);
            }

            Console.WriteLine($"{privateKey.ToHexString()}");
            Console.WriteLine($"{privateKey2.ToHexString()}");
            Console.WriteLine($"{privateKey3.ToHexString()}");


            var p = new MyStruct() {Name = "S"};
            var p2 = new MyStruct() {Name = "S"};

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
        }
    }

}
