using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenHardwareMonitor.Hardware;

namespace CPUTEMPW
{
    public partial class Main : Form
    {
        BindingList<CpuTemp> _temps = new BindingList<CpuTemp>();
        private readonly Computer _computer;
        private bool _closing = false;
        public Main()
        {
            InitializeComponent();
            var updateVisitor = new UpdateVisitor();
            _computer = new Computer();
            _computer.Open();
            _computer.CPUEnabled = true;
            _computer.Accept(updateVisitor);
            cpuTempBindingSource.DataSource = _temps;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            
        }

        private void DoSomething(Computer com)
        {
            GetSystemInfo(com);
        }

        void GetSystemInfo(Computer computer)
        {
            var sensors = computer.Hardware.FirstOrDefault(h => h.HardwareType == HardwareType.CPU)?.Sensors;
            if (sensors?.Any() == true)
            {
                _temps.Clear();
                sensors.Where(s => s.SensorType == SensorType.Temperature).ToList().ForEach(s =>
                {
                    _temps.Add(new CpuTemp()
                    {
                        Name = s.Name,
                        Temp = s.Value.GetValueOrDefault()
                    });
                });
            }
        }

        private void runBtn_Click(object sender, EventArgs e)
        {
            DoSomething(_computer);
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}
