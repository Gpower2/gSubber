using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gSubber;
using gSubber.Formats.Ass;

namespace gSubberGUI
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Time t = new Time(1, 12, 35, 135);
            //textBox1.AppendText(String.Format("Time: {0}\r\n", t));
            //textBox1.AppendText(String.Format("Hours: {0}\r\n", t.Hours));
            //textBox1.AppendText(String.Format("Minutes: {0}\r\n", t.Minutes));
            //textBox1.AppendText(String.Format("Seconds: {0}\r\n", t.Seconds));
            //textBox1.AppendText(String.Format("Milliseconds: {0}\r\n", t.Milliseconds));
            //textBox1.AppendText(String.Format("Microseconds: {0}\r\n", t.Microseconds));
            //textBox1.AppendText(String.Format("Nanoseconds: {0}\r\n", t.Nanoseconds));
            //textBox1.AppendText(String.Format("Total Hours: {0}\r\n", t.TotalHours));
            //textBox1.AppendText(String.Format("Total Minutes: {0}\r\n", t.TotalMinutes));
            //textBox1.AppendText(String.Format("Total Seconds: {0}\r\n", t.TotalSeconds));
            //textBox1.AppendText(String.Format("Total Milliseconds: {0}\r\n", t.TotalMilliseconds));
            //textBox1.AppendText(String.Format("Total Microseconds: {0}\r\n", t.TotalMicroseconds));
            //textBox1.AppendText(String.Format("Total Nanoseconds: {0}\r\n", t.TotalNanoseconds));
            //textBox1.AppendText(String.Format("Time: {0}\r\n", t.ToString( Time.TimeFormat.WithSeconds)));
            //textBox1.AppendText(String.Format("Time: {0}\r\n", t.ToString(Time.TimeFormat.WithMilliseconds)));
            //textBox1.AppendText(String.Format("Time: {0}\r\n", t.ToString(Time.TimeFormat.WithMicroseconds)));
            //textBox1.AppendText(String.Format("Time: {0}\r\n", t.ToString(Time.TimeFormat.WithNanoseconds)));

            AssFileParser p = new AssFileParser();
            SubFileParserResults r = p.Load(@"H:\AnimeClipse\FairyTail\Karaoke\Karaoke06 [061-072]\Karaoke_op6.ass", Encoding.UTF8);
        }
    }
}
