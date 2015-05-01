// Spreadbot (c) 2015 Krokodev
// Spreadbot.Tests.Nunit
// YamlSerializer_Tests.cs

using System;
using NUnit.Framework;
using Spreadbot.Core.Abstracts.Channel.Operations.Responses;
using Spreadbot.Core.Channels.Ebay.Operations.Results;
using Spreadbot.Core.Channels.Ebay.Operations.Tasks;
using Spreadbot.Core.Stores.Demoshop.Operations.Tasks;
using Spreadbot.Nunit.Base;
using Spreadbot.Sdk.Common.Crocodev.Common;
using YamlDotNet.Serialization;

namespace Spreadbot.Nunit.Tests
{
    [TestFixture]
    public class YamlSerializer_Tests : Spreadbot_Tests
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
            var response = new ChannelResponse< EbaySubmissionResult > {
                Result = new EbaySubmissionResult()
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
            var submissionTask1 = new EbaySubmissionTask();
            var submissionTask2 = new EbaySubmissionTask();
            storeTask.AddSubTasks( submissionTask1, submissionTask2 );
            submissionTask2.AbstractResponse = new ChannelResponse< EbaySubmissionResult >( 
                new Exception( "Test Exception" ) );

            var str = storeTask.ToYamlString( SerializationOptions.EmitDefaults );

            Console.WriteLine( str );

            Assert.That( str.Contains( "SubTasks:" ), "Contains SubTasks" );
            Assert.That( str.Contains( "Test Exception" ), "Contains Test Exception" );
        }
    }
}