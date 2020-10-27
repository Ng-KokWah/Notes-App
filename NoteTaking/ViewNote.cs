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
    public partial class ViewNote : Form
    {
        public ViewNote()
        {
            InitializeComponent();
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private static int Id = 0;

        public static int getId()
        {
            return Id;
        }

        public void setId(int id)
        {
            Id = id;
        }
        private void ViewNote_Load(object sender, EventArgs e)
        {
            NoteModel nm = DBCalls.retrieveSingleNoteInfo(Id);
            tbTitle.Text = nm.Title;
            tbContent.Text = nm.Content;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DBCalls.UpdateNote(getId(), tbTitle.Text, tbContent.Text);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            tbTitle.ReadOnly = false;
            tbContent.ReadOnly = false;
            btnSave.Enabled = true;
        }
    }
}
