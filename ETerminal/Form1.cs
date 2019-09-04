using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ETerminal
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void GetData()
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
            DialogResult dialogResult = MessageBox.Show("Start collecting user data? This is expensive operation!!", "Collecting user data", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                var controller = new TerminalController();
                controller.StartCollectUserData();
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }

            groupBox1.Enabled = true;
            groupBox2.Enabled = true;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
            DialogResult dialogResult = MessageBox.Show("Start collecting last logs?", "Collecting logs", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                var controller = new TerminalController();
                controller.StartCollectingLogs(clean: checkBox1.Checked);
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }

            groupBox1.Enabled = true;
            groupBox2.Enabled = true;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
            DialogResult dialogResult = MessageBox.Show("Start cleaning all devices logs?", "Cleaning logs", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                var controller = new TerminalController();
                controller.StartCleaninDevicesLogs();
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }

            groupBox1.Enabled = true;
            groupBox2.Enabled = true;

        }

        private void Button8_Click(object sender, EventArgs e)
        {

        }

        private void Button7_Click(object sender, EventArgs e)
        {

        }

        private void Button4_Click(object sender, EventArgs e)
        {

        }

        private void Button5_Click(object sender, EventArgs e)
        {

        }

        private void Button9_Click(object sender, EventArgs e)
        {

        }

        private void Button6_Click(object sender, EventArgs e)
        {
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
            DialogResult dialogResult = MessageBox.Show("Start collecting all logs?", "Collecting all logs", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                var controller = new TerminalController();
                controller.StartCollectingLogs(true);
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }

            groupBox1.Enabled = true;
            groupBox2.Enabled = true;
        }
    }
}
