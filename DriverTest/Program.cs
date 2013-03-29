using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.CoreAudioApi;

namespace DriverTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var deviceEnum = new MMDeviceEnumerator();
            var deviceCol = deviceEnum.EnumerateAudioEndPoints(DataFlow.All, DeviceState.All);

            Debugger.Log(1, "output", String.Format("\n=======================================\n"));
            foreach (var device in deviceCol.Where(d => d.FriendlyName.Contains("PunchLight")))
            {
                Debugger.Log(1, "output", String.Format("{0}\n", device.DeviceFriendlyName));
                Debugger.Log(1, "output", String.Format("{0}\n", device.FriendlyName));
                Debugger.Log(1, "output", String.Format("\tDataFlow: {0}\n", device.DataFlow));
                Debugger.Log(1, "output", String.Format("\tState: {0}\n", device.State));
                Debugger.Log(1, "output", String.Format("\tID: {0}\n", device.ID));

                Debugger.Log(1, "output", String.Format("---------------------------------------\n"));
            }
            Debugger.Log(1, "output", String.Format("=======================================\n"));

            var dev = deviceCol.Where(d => d.FriendlyName.Contains("jack4")).First();
                Debugger.Log(1, "output", String.Format("{0}\n", dev.FriendlyName));
                Debugger.Log(1, "output", String.Format("\tDataFlow: {0}\n", dev.DataFlow));
                Debugger.Log(1, "output", String.Format("\tState: {0}\n", dev.State));
                Debugger.Log(1, "output", String.Format("\tID: {0}\n", dev.ID));
                //var buff = dev.AudioClient.AudioRenderClient.GetBuffer(1);
                //dev.AudioClient.AudioRenderClient.ReleaseBuffer(1, AudioClientBufferFlags.Silent);
        }
    }
}
