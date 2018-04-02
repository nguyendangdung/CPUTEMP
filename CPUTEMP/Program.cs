using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OpenHardwareMonitor.Hardware;

namespace CPUTEMP
{
    partial class Program
    {
        static void Main(string[] args)
        {
            var updateVisitor = new UpdateVisitor();
            var computer = new Computer();
            computer.Open();
            computer.CPUEnabled = true;
            computer.Accept(updateVisitor);
            var i = 0;
            while (i < 100)
            {
                i++;
                GetSystemInfo(computer);
            }
            computer.Close();
            Console.WriteLine("Done");
            Console.ReadLine();
        }
        static void GetSystemInfo(Computer computer)
        {
            
            foreach (var t in computer.Hardware)
            {
                if (t.HardwareType != HardwareType.CPU) continue;
                foreach (var t1 in t.Sensors)
                {
                    if (t1.SensorType == SensorType.Temperature)
                        Console.WriteLine(t1.Name + ":" + t1.Value.ToString() + "\r");
                }
            }
        }
    }
}
