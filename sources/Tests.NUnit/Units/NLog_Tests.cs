// Spreadbot (c) 2015 Crocodev
// Tests.NUnit
// NLog_Tests.cs
// Roman, 2015-04-09 2:44 PM

using System;
using System.IO;
using Crocodev.Common.Extensions;
using NLog;
using NLog.Targets;
using NUnit.Framework;
using Spreadbot.Sdk.Common.Exceptions;

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
        public void Log_File_Exists()
        {
            Logger.Trace( "CreateLog" );

            var fileName = LogFileName( );

            Console.WriteLine("fileName: {0}", fileName);
            Assert.That( !String.IsNullOrEmpty( fileName ), "Target File Name is assigned" );
            Assert.That( File.Exists( fileName ), "Log file exists" );
        }

        [Test]
        public void Exception_Logged_With_Detailes()
        {
            var token = "Some error #{0}".SafeFormat( Guid.NewGuid() );
            Logger.ErrorException( "Exception",  new SpreadbotException (token) );
            
            Assert_That_Log_Contains_Text(token);
        }

        // ===================================================================================== []
        // Utils
        private void Assert_That_Log_Contains_Text( string text )
        {
            var content = File.ReadAllText( LogFileName() );
            Assert.That(content.Contains( text ));
        }
        private static string LogFileName()
        {
            var fileTarget = ( FileTarget ) LogManager.Configuration.FindTargetByName( "logfile" );
            Assert.NotNull( fileTarget, "File Target" );

            var logEventInfo = new LogEventInfo { TimeStamp = DateTime.Now };
            var fileName = fileTarget.FileName.Render( logEventInfo );
            return fileName;
        }
    }
}