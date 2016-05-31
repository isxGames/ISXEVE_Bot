using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace ISXEVE_Bot_Framework
{
    public partial class Form1 : Form
    {
        Main _Main;

        public Form1()
        {
            InitializeComponent();
            /* New instance of Main, which indirectly provides the majority of our capabilities */
            _Main = new Main(this);
            _Main.Start();
        }

        /* Do the actual UI logging */
        public void NewLogMessage(string message)
        {
            message = "(" + DateTime.Now.ToLongTimeString() + ") " + message;
            listBox_logging.Items.Add(message);
            listBox_logging.SelectedIndex = listBox_logging.Items.Count - 1;
        }

        /* Start the framework */
        private void button_start_Click(object sender, EventArgs e)
        {
            button_start.Enabled = false;
            button_stop.Enabled = true;
        }

        private void button_stop_Click(object sender, EventArgs e)
        {
            button_start.Enabled = true;
            button_stop.Enabled = false;
        }

        public void SetOreHoldStatusLabel(string used, string capacity, string percentage)
        {
            OreHoldStatusLabel.Text = "OreHold: " + used + "/" + capacity + " (" + percentage + ")";
        }

        public void SetDeliveryStationsDropdown(List<String> Stations)
        {
            DeliveryStationsDropDown.DataSource = Stations;
            DeliveryStationsDropDown.Refresh();
        }

        public String GetDeliveryStation()
        {
            return DeliveryStationsDropDown.SelectedText.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
