// Spreadbot (c) 2015 Crocodev
// Spreadbot.Tests.Core
// Yaml_Serializer_Tests.cs
// romak_000, 2015-03-26 14:44

using System;
using System.IO;
using NUnit.Framework;
using Spreadbot.Tests.Core.Common;
using YamlDotNet.Serialization;

namespace Spreadbot.Tests.Core.Channels.Ebay.Mip
{
    [TestFixture]
    public class Yaml_Serializer_Tests : SpreadbotBaseTest
    {
        // --------------------------------------------------------[]
        [SetUp]
        public void Init() {}

        // --------------------------------------------------------[]
        public class SimpleObject
        {
            public const string CkeckPhraze = "123 456";
            public string Name { get; set;  }
            public string CheckData { get; set; }
            public DateTime Time { get; set; }

            public SimpleObject( )
            {
                Time = DateTime.Now;
                Name = "I am Simple Object";
                CheckData = CkeckPhraze;
            }
        }

        // --------------------------------------------------------[]
        [Test]
        public static void SimpleObject_Serialization()
        {
            var obj = new SimpleObject();
            var serializer = new Serializer();
            var sw = new StringWriter();

            serializer.Serialize( sw, obj );
            var str = sw.ToString();
            Console.WriteLine( str );
            
            Assert.That( str.Contains( SimpleObject.CkeckPhraze ), "Contains check phrase" );
        }
    }
}