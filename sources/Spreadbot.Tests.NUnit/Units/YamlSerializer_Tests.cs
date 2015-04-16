// Spreadbot (c) 2015 Crocodev
// Tests.NUnit
// YamlSerializer_Tests.cs

using System;
using NUnit.Framework;
using Spreadbot.Core.Abstracts.Channel.Operations.Responses;
using Spreadbot.Core.Channels.Ebay.Operations.Results;
using Spreadbot.Core.Channels.Ebay.Operations.Tasks;
using Spreadbot.Core.Stores.Demoshop.Operations.Tasks;
using Spreadbot.Sdk.Common.Crocodev.Common;
using Tests.NUnit.Code;
using YamlDotNet.Serialization;

namespace Tests.NUnit.Units
{
    [TestFixture]
    public class YamlSerializer_Tests : SpreadbotTestBase
    {
        // --------------------------------------------------------[]
        [SetUp]
        public void Init() {}

        // --------------------------------------------------------[]
        public class BaseObject
        {
            public bool FalseValue { get; set; }
            public int ZeroValue { get; set; }

            protected BaseObject()
            {
                FalseValue = false;
                ZeroValue = 0;
            }
        }

        public class Object : BaseObject
        {
            public const string CkeckPhraze = "123 456";
            public string Name { get; set; }
            public string CheckData { get; set; }
            public DateTime Time { get; set; }

            public Object()
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
            var obj = new Object();
            var str = obj.ToYamlString( SerializationOptions.EmitDefaults );

            Console.WriteLine( str );

            Assert.That( str.Contains( Object.CkeckPhraze ), "Contains check phrase" );
            Assert.That( str.Contains( "FalseValue" ), "Contains 'FalseValue'" );
            Assert.That( str.Contains( "ZeroValue" ), "Contains 'ZeroValue'" );
        }

        // --------------------------------------------------------[]
        [Test]
        public static void ChannelResponse_Success_Serialization()
        {
            var response = new ChannelResponse< EbayPublishResult > {
                StatusCode = ChannelResponseStatusCode.PublishSuccess,
                Result = new EbayPublishResult()
            };

            var str = response.ToYamlString();
            Console.WriteLine( str );

            Assert.That( str.Contains( "IsSuccess" ), "Contains IsSuccess" );
        }

        // --------------------------------------------------------[]
        [Test]
        public static void DemoshopStoreTask_Serialization()
        {
            var storeTask = new DemoshopStoreTask();
            var publishTask1 = new EbayPublishTask();
            var publishTask2 = new EbayPublishTask();
            storeTask.AddSubTasks( publishTask1, publishTask2 );
            publishTask2.AbstractResponse = new ChannelResponse< EbayPublishResult >(
                new Exception( "Test Exception" ) ) {
                    StatusCode = ChannelResponseStatusCode.PublishFailure,
                };

            var str = storeTask.ToYamlString( SerializationOptions.EmitDefaults );

            Console.WriteLine( str );

            Assert.That( str.Contains( "SubTasks:" ), "Contains SubTasks" );
            Assert.That( str.Contains( "Test Exception" ), "Contains Test Exception" );
        }
    }
}