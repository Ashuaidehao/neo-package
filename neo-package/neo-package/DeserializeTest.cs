using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Neo.IO.Json;
using Newtonsoft.Json;

namespace neo_package
{
    [SimpleJob(launchCount: 3, warmupCount: 10, targetCount: 10)]

    public class DeserializeTest
    {
        public const string part100 = "[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[[]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]]";

        public const string part20 = "[[[[[[[[[[[[[[[[[[[[]]]]]]]]]]]]]]]]]]]]";
        public const string part10 = "[[[[[[[[[[]]]]]]]]]]";
        private const string part5 = "[[[[[]]]]]";



        public DeserializeTest()
        {
            json = BuildJson(311295, part100);

            //json = JsonConvert.SerializeObject(new object[] { new int[] { 1, 2, 3 }, new int[] { 4, 5 } });
            Console.WriteLine(json.Length);
        }

        private static string loadJson()
        {
            return File.ReadAllText("test.json");
        }


        //private static string json = loadJson();
        public string json;
        private const int limit = 1000_000;
        private string BuildJson(int n, string part)
        {
            var sb = new StringBuilder();
            sb.Append("[");
            for (int i = 1; i < n; i++)
            {
                sb.Append(part);
                sb.Append(",");
                if (sb.Length > limit)
                {
                    break;
                }
            }

            sb.Append(part);
            sb.Append("]");
            return sb.ToString();
        }



        //[Benchmark]
        //public object Test1()
        //{
        //    return JObject.Parse(json);
        //}

        //[Benchmark]
        //public object Test1_1()
        //{
        //    return Neo.SmartContract.JsonSerializer.Deserialize(JObject.Parse(json));
        //}

        //[Benchmark]
        //public object Test2()
        //{
        //    return JsonConvert.DeserializeObject(json);
        //}

        //[Benchmark]
        //public object Test3()
        //{
        //    JObject.Parse(json)
        //    return JsonConvert.DeserializeObject(json);
        //}
    }
}
