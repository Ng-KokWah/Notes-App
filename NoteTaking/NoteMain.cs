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
    public partial class NoteMain : Form
    {
        /// <summary>
        /// NOTE: When using this on a new computer, you would need to change the connection string
        /// stored in the app.config by:
        /// 1. double click on the NotesDatabase.mdf file
        /// 2. under the properties find Connection String
        /// 3. Paste this connection string in the App.config file
        /// 4. remove the double quotes " after the AttachDbFilename to prevent error from showing
        /// </summary>
        public NoteMain()
        {
            InitializeComponent();
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void minimise_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
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

        private static Boolean opened = false;
        private void btnExpand_Click(object sender, EventArgs e)
        {
            if (opened == false)
            {
                btnExpand.Text = "-";
                btnNote.Visible = true;
                opened = true;
            }
            else
            {
                btnExpand.Text = "+";
                btnNote.Visible = false;
                opened = false;
            }
        }

        private void btnNote_Click(object sender, EventArgs e)
        {
            AddNote next = new AddNote();
            next.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();

                dt.Columns.Add("Id", typeof(int));
                dt.Columns.Add("Title", typeof(String));
                dt.Columns.Add("Content", typeof(String));

                List<NoteModel> nmList = DBCalls.retrieveAllNoteInfo();
                foreach (NoteModel nm in nmList)
                {
                    dt.Rows.Add(nm.Id, nm.Title, nm.Content);
                }

                dataGridView1.DataSource = dt;
            }catch(Exception err)
            {
                Console.WriteLine(err);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();

                dt.Columns.Add("Id", typeof(int));
                dt.Columns.Add("Title", typeof(String));
                dt.Columns.Add("Content", typeof(String));

                List<NoteModel> nmList = DBCalls.retrieveAllNoteInfo();
                foreach (NoteModel nm in nmList)
                {
                    dt.Rows.Add(nm.Id, nm.Title, nm.Content);
                }

                dataGridView1.DataSource = dt;
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int id = 0;
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    id = Convert.ToInt16(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                }

                ViewNote next = new ViewNote();
                next.setId(id);
                next.Show();

            }
            catch (ArgumentOutOfRangeException error)
            {
                Console.WriteLine(error.Message);
            }
        }
    }
}
