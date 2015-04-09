// Spreadbot (c) 2015 Crocodev
// Tests.NUnit
// NLog_Tests.cs
// Roman, 2015-04-09 2:44 PM

using System;
using System.IO;
using NLog;
using NLog.Targets;
using NUnit.Framework;

// Code: NLog Test

namespace Tests.NUnit.Units
{
    [TestFixture]
    public class NLog_Tests
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        [Test]
        public void NLog_Works()
        {
            Logger.Trace( "Ok" );
        }

        [Test]
        public void Log_File_Created()
        {
            Logger.Trace( "CreateLog" );

            var fileTarget = ( FileTarget ) LogManager.Configuration.FindTargetByName( "logfile" );
            Assert.NotNull( fileTarget, "File Target" );

            var logEventInfo = new LogEventInfo { TimeStamp = DateTime.Now };
            var fileName = fileTarget.FileName.Render( logEventInfo );
            
            Console.WriteLine("fileName: {0}", fileName);
            Assert.That( !String.IsNullOrEmpty( fileName ), "Target File Name is assigned" );
            Assert.That( File.Exists( fileName ), "Log file exists" );
        }
    }
}