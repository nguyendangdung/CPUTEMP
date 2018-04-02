using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenHardwareMonitor.Hardware;

namespace CPUTEMPW
{
    public class UpdateVisitor : IVisitor
    {
        public void VisitComputer(IComputer computer)
        {
            computer.Traverse(this);
        }
        public void VisitHardware(IHardware hardware)
        {
            hardware.Update();
            foreach (IHardware subHardware in hardware.SubHardware) subHardware.Accept(this);
        }
        public void VisitSensor(ISensor sensor) { }
        public void VisitParameter(IParameter parameter) { }
    }
    public partial class Main : Form
    {
        private readonly UpdateVisitor _updateVisitor;
        private readonly Computer _computer;
        public Main()
        {
            InitializeComponent();
            _updateVisitor = new UpdateVisitor();
            _computer = new Computer();
            _computer.Open();
            _computer.CPUEnabled = true;
            _computer.Accept(_updateVisitor);
        }

        private async void Main_Load(object sender, EventArgs e)
        {
            
        }

        private async Task DoSomething()
        {

        }
    }
}
