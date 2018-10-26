using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComOverseer
{
    public partial class Form1 : ApplicationContext
    {
        private UsbMonitor usb;
        private string[] serialPortNamesList;
        private string[] displaySerialPortNamesList;
        public Form1()
        {
            InitializeComponent();
            usb = new UsbMonitor();
            serialPortNamesList = SerialPort.GetPortNames();
            displaySerialPortNamesList = SerialPort.GetPortNames();
            usb.OnInsert_handler += new EventArrivedEventHandler(OnInsert);
            notifyIcon1.Icon = SystemIcons.Information;
            notifyIcon1.Click += NotifyIcon1_Click;

        }

        private void NotifyIcon1_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            if (me.Button == MouseButtons.Left)
            {
                notifyIcon1.ShowBalloonTip(3000, "Available serial ports:", String.Join(Environment.NewLine, displaySerialPortNamesList), ToolTipIcon.Info);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void OnInsert(Object s, EventArrivedEventArgs e)
        {
            string[] updatedSerialPortNamseList = SerialPort.GetPortNames();
            if(updatedSerialPortNamseList.Length != serialPortNamesList.Length)
            {
                var updated = updatedSerialPortNamseList.Intersect(serialPortNamesList);
                displaySerialPortNamesList = updated.Concat(updatedSerialPortNamseList.Except(serialPortNamesList).Select(x => x = string.Concat(x, " NEW!"))).ToArray<string>();
                serialPortNamesList = updated.Concat(updatedSerialPortNamseList.Except(serialPortNamesList)).ToArray<string>();
                Array.Sort(displaySerialPortNamesList);
                notifyIcon1.ShowBalloonTip(3000, "Available serial ports:", String.Join(Environment.NewLine, displaySerialPortNamesList), ToolTipIcon.Info);
            }
        }


        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ExitThread();
        }
    }
}
