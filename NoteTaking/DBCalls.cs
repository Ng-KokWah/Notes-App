using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteTaking
{
    class DBCalls
    {
        public static void createNote(int Id, String Title, String Content)
        {
            string cs = ConfigurationManager.ConnectionStrings["NotesDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(cs);

            String sqlinsert = "INSERT INTO NotesTable(Id, Title, Content) VALUES (" + Id + ", \'" + @Title + "\', \'" + @Content + "\')";
            SqlCommand cmd = new SqlCommand(sqlinsert, conn);

            Console.WriteLine(sqlinsert);
            ///System.Diagnostics.Debug.WriteLine("update history " + sqlinsert);
            using (conn)
            {
                conn.Open();
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
                cmd.Parameters.Add("@Title", SqlDbType.VarChar, 50).Value = CleaningInput.cleanUpInput(Title);
                cmd.Parameters.Add("@Content", SqlDbType.VarChar).Value = CleaningInput.cleanUpInput(Content);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public static int countNoOfNotes()
        {
            string cs3 = ConfigurationManager.ConnectionStrings["NotesDB"].ConnectionString.ToString();
            SqlConnection conn3 = new SqlConnection(cs3);

            SqlCommand cmd3 = new SqlCommand("SELECT COUNT(*) FROM NotesTable", conn3);
            Int32 count;
            using (conn3)
            {
                conn3.Open();
                count = (Int32)cmd3.ExecuteScalar();
                conn3.Close();
            }

            return count;
        }

        public static List<NoteModel> retrieveAllNoteInfo()
        {
            List<NoteModel> nmList = new List<NoteModel>();

            string cs3 = ConfigurationManager.ConnectionStrings["NotesDB"].ConnectionString.ToString();
            SqlConnection conn3 = new SqlConnection(cs3);

            String SqlCommand3 = "SELECT * FROM NotesTable";
            SqlCommand cmd3 = new SqlCommand(SqlCommand3, conn3);

            using (conn3)
            {
                conn3.Open();
                SqlDataReader rdr3 = cmd3.ExecuteReader();
                while (rdr3.Read())
                {
                    NoteModel nm = new NoteModel();
                    nm.Id = Convert.ToInt32(rdr3["Id"]);
                    nm.Title = CleaningInput.RemoveWhiteSpaces(rdr3["Title"].ToString());
                    nm.Content = CleaningInput.RemoveWhiteSpaces(rdr3["Content"].ToString());
                    nmList.Add(nm);
                }
                conn3.Close();
            }

            return nmList;
        }

        public static NoteModel retrieveSingleNoteInfo(int id)
        {
            NoteModel nm = new NoteModel();

            string cs3 = ConfigurationManager.ConnectionStrings["NotesDB"].ConnectionString.ToString();
            SqlConnection conn3 = new SqlConnection(cs3);

            String SqlCommand3 = "SELECT * FROM NotesTable WHERE Id = " + id;
            SqlCommand cmd3 = new SqlCommand(SqlCommand3, conn3);

            using (conn3)
            {
                conn3.Open();
                SqlDataReader rdr3 = cmd3.ExecuteReader();
                while (rdr3.Read())
                {
                    nm.Id = Convert.ToInt32(rdr3["Id"]);
                    nm.Title = CleaningInput.RemoveWhiteSpaces(rdr3["Title"].ToString());
                    nm.Content = CleaningInput.RemoveWhiteSpaces(rdr3["Content"].ToString());
                }
                conn3.Close();
            }

            return nm;
        }

        public static void UpdateNote(int Id, String Title, String Content)
        {
            string cs = ConfigurationManager.ConnectionStrings["NotesDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(cs);

            String sqlinsert = "UPDATE NotesTable SET Title = \'" + @Title + "\', Content = \'" + @Content + "\' WHERE Id = " + Id;
            SqlCommand cmd = new SqlCommand(sqlinsert, conn);

            Console.WriteLine(sqlinsert);
            ///System.Diagnostics.Debug.WriteLine("update history " + sqlinsert);
            using (conn)
            {
                conn.Open();
                cmd.Parameters.Add("@Title", SqlDbType.VarChar, 50).Value = Title;
                cmd.Parameters.Add("@Content", SqlDbType.VarChar).Value = Content;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
