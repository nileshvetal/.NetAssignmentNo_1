using System;
using Xunit;
using logtocsvconverter;

namespace LogToCsvConverter.Test
{
    public class UnitTest
    {
        [Fact]
        public void ValidateInputCommandSuccess()   //--log-dir and --csv are compulsory
        {
            var command = "--log-dir ..\\..\\Logs --log-level Info --csv ..\\Logs --log-level Warn";
            var isValid = Program.ValidateCommand(command);

            command = "--log-dir ..\\..\\Logs --csv ..\\Logs";
            isValid = Program.ValidateCommand(command);

            var expected = true;
            Assert.Equal(expected, isValid);
        }

        [Fact]
        public void ValidateInputCommandFail()   //--log-dir and --csv both are compulsory
        {
            var command = "--log-level Info --csv ..\\Logs --log-level Warn";
            var isValid = Program.ValidateCommand(command);

            command = "--log-dir ..\\..\\Logs";  //Log level is optional
            isValid = Program.ValidateCommand(command);

            var expected = false;
            Assert.Equal(expected, isValid);
        }

        [Fact]
        public void GetInputArgumentFromCommandInput()
        {
            //Given
            var commands = "--log-dir ..\\..\\Logs --log-level Info --csv ..\\Logs --log-level Warn";
            var commandArray = commands.Split(" ");
            //When
            var inputArguments = new InputArguments();
            inputArguments.Get(commandArray);
            //Then
            var expectedArguments = new InputArguments()
            {
                LogDir = "..\\..\\Logs",
                LogLevels = new System.Collections.Generic.List<string>() { "Info", "Warn" },
                OutPath = "..\\Logs"
            };

            Assert.Equal(expectedArguments.LogDir, inputArguments.LogDir);
            Assert.Equal(expectedArguments.LogLevels, inputArguments.LogLevels);
            Assert.Equal(expectedArguments.OutPath, inputArguments.OutPath);
        }

        [Fact]
        public void LogToCSVSucces()
        {
            //Given
            var log = new Log()
            {
                Lines = new System.Collections.Generic.List<string>(){
                "03/22 08:51:06 TRACE  :....read_physical_netif: Home list entries returned = 7",
                "03/22 08:51:06 INFO :...read_physical_netif: index #1, interface TR1 has address 9.37.65.139, ifidx 1",
                "03/22 08:51:06 INFO :...read_physical_netif: index #2, interface LINK11 has address 9.67.100.1, ifidx 2",
                "03/22 08:51:06 INFO :...read_physical_netif: index #3, interface LINK12 has address 9.67.101.1, ifidx 3",
                "03/22 08:51:06 INFO :...read_physical_netif: index #4, interface CTCD0 has address 9.67.116.98, ifidx 4",
                "03/22 08:51:06 INFO :...read_physical_netif: index #5, interface CTCD2 has address 9.67.117.98, ifidx 5",
                "03/22 08:51:06 INFO :...read_physical_netif: index #6, interface LOOPBACK has address 127.0.0.1, ifidx 0",
                "03/22 08:51:06 DEBUG :....mailslot_create: creating mailslot for timer",
                "03/22 08:51:06 INFO :...mailbox_register: mailbox allocated for timer"
            }
            };
            //When
            var expectedCSVFormats = Program.LogToCSV(log);
            //Then

            Assert.Equal(expectedCSVFormats[0].Number, 1);
            Assert.Equal(expectedCSVFormats[0].Date, "22 Mar 2020");
            Assert.Equal(expectedCSVFormats[0].Time, "8:51 AM");
            Assert.Equal(expectedCSVFormats[0].Level, "TRACE");
            Assert.Equal(expectedCSVFormats[0].Text, "read_physical_netif: Home list entries returned = 7");
        }

    }
}
