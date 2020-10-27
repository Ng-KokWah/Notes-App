using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoteTaking
{
    public partial class AddNote : Form
    {
        public AddNote()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int numberOfNotes = DBCalls.countNoOfNotes();
            Console.Write(numberOfNotes);
            if(String.IsNullOrEmpty(Convert.ToString(numberOfNotes)) || String.IsNullOrWhiteSpace(Convert.ToString(numberOfNotes)))
            {
                numberOfNotes = 0;
            }
            DBCalls.createNote(numberOfNotes, tbTitle.Text, tbContent.Text);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
                m.Result = (IntPtr)(HT_CAPTION);
        }

        private const int WM_NCHITTEST = 0x84;
        private const int HT_CLIENT = 0x1;
        private const int HT_CAPTION = 0x2;

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
