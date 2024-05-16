using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Classroom_Manager_System
{
    public partial class HomePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //Method or function 
        //Return type is before method name. int returns number, string returns text, void returns  nothing , bool returns t/f
        protected void DDLClassName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            string sqlquery = "select * from [dbo].[CourseTable] where CourseName=@CourseName";
            sqlconn.Open();
            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
            sqlcomm.Parameters.AddWithValue("@CourseName", DDLClass.SelectedItem.Text);
            SqlDataReader sdr = sqlcomm.ExecuteReader();
            while (sdr.Read())
            {
                //int courseId = Convert.int32(sdr
            }
            sqlconn.Close();

            //Join statement



            // TeacherTextBox.Text = TeacherTextBox.Text + sdr2.ToString();
        }


        protected void BtAddStudent_Click(object sender, System.EventArgs e)
        {
            

            string mainconn = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);

            string sqlquery = "Insert into [dbo].[StudentTable] (StudentName) values (@StudentName)";
            
            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
            sqlconn.Open();
            sqlcomm.Parameters.AddWithValue("@StudentName", AddStudentTextBox.Text);
            sqlcomm.ExecuteNonQuery();
            
            int StudentId = GetStudentid(sqlconn);
            //var StudCourse = AddStudentTextBox.Text;
            int GetCourseId = GetCourseid(sqlconn);
            
            string sqlquery2 = "Insert into [dbo].[StudentCourseTable] (Studentid, Courseid) values (@StudentId, @GetCourseId)";
            SqlCommand sqlcomm2 = new SqlCommand(sqlquery2, sqlconn);

            sqlcomm2.Parameters.AddWithValue("@StudentId", StudentId);
            sqlcomm2.Parameters.AddWithValue("@GetCourseId", GetCourseId);

            sqlcomm2.ExecuteNonQuery();
            sqlconn.Close();
        }
        protected int GetCourseid(SqlConnection sqlconn)
        {
            //@ is the parameter (Select Cid from coursetable where the course name is "MATH" --> Parameter passed
            string sqlquery = "select Cid from [dbo].[CourseTable] where CourseName = @CourseName";

            int courseID;
            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
            //Parameter. On the right of the comma is the value being passed. This is the text of the html element id
            sqlcomm.Parameters.AddWithValue("@CourseName", DDLClass.SelectedItem.Text);
            //Reads sql command
            SqlDataReader sdr = sqlcomm.ExecuteReader();
            if (sdr.Read())
            {
                //Convert the Cid to an integer to use later.
                courseID = Convert.ToInt32(sdr[0]);

            }
            //Return the integer
            return Convert.ToInt32(sdr[0]);
            





        }
        //Pass in parameter / argument  Ex: sqlconn     
        protected int GetStudentid(SqlConnection sqlconn)
        {
            string sqlquery = "select Stid from [dbo].[StudentTable] where StudentName = @StudentName";

            int courseID;
            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);

            sqlcomm.Parameters.AddWithValue("@StudentName", AddStudentTextBox.Text);
            SqlDataReader sdr = sqlcomm.ExecuteReader();
            if (sdr.Read())
            {
                courseID = Convert.ToInt32(sdr[0]);
            }
            return Convert.ToInt32(sdr[0]);

        }
        protected void DropDownIndexChange_TextChanged(object sender, EventArgs e)
        {


            string mainconn = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            sqlconn.Open();


            TeacherTextBox1_TextChanged(sqlconn);

            StudentListBox_TextChanged(sqlconn);
            sqlconn.Close();


        }




        protected string TeacherTextBox1_TextChanged(SqlConnection sqlconn)
        {
            if (sqlconn is null)
            {
                throw new ArgumentNullException(nameof(sqlconn));
            }


            string mainconn = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;

            //courseId represents the integer fetched from this method/ function
            int courseId = GetCourseid(sqlconn);

            //the course id above is bing used on line 144 to be passed into @Courseid to get the teacher name from the sql query below
            string sqlquery2 = "SELECT TeacherName FROM [dbo].[TeacherTable] INNER JOIN [dbo].[TeacherCourseTable] ON [dbo].[TeacherTable].Tid = [dbo].[TeacherCourseTable].Teacherid INNER JOIN [dbo].[CourseTable] ON [dbo].[TeacherCourseTable].Courseid = [dbo].[CourseTable].Cid WHERE [dbo].[CourseTable].Cid = @Courseid;";

            //We want to run sqlquery2 using sqlconn... sqlconn is the parameter being passed in
            SqlCommand sqlcomm2 = new SqlCommand(sqlquery2, sqlconn);
            //course Id form line 138 is being passed in as the value for the parameter @Courseid
            sqlcomm2.Parameters.AddWithValue("@Courseid", courseId);

            //We set the  html element with the id of TeacherTextBox's vallue to the TeacherName returned from the sql query
            TeacherTextBox.Text = (string)sqlcomm2.ExecuteScalar();

            //we can set this function equal to a string somewhere else because the value is being returned here Ex: string teachername = TeacherTextBox1_TextChanged(sqlconn)
            return TeacherTextBox.Text;
        }








        protected string StudentListBox_TextChanged(SqlConnection sqlconn)
        {
            //Connection being passed in above as parameter
            string mainconn = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
            StudentListTextBox.Text = "";
            int courseId = GetCourseid(sqlconn);
            string sqlquery3 = "SELECT StudentName FROM [dbo].[StudentTable] INNER JOIN [dbo].[StudentCourseTable] ON [dbo].[StudentTable].Stid = [dbo].[StudentCourseTable].Studentid INNER JOIN [dbo].[CourseTable] ON [dbo].[StudentCourseTable].Courseid = [dbo].[CourseTable].Cid WHERE [dbo].[CourseTable].Cid = @Courseid;";
            //Hover over built in functions to see parameters needed to run...
            SqlCommand sqlcomm3 = new SqlCommand(sqlquery3, sqlconn);
            sqlcomm3.Parameters.AddWithValue("@Courseid", courseId);
            //sdr will be the student names from the executed query
            SqlDataReader sdr = sqlcomm3.ExecuteReader();
            while (sdr.Read())
            {
                for (int i = 0; i < sdr.FieldCount; i++)
                {
                    StudentListTextBox.Text += sdr[i].ToString();
                    // \n adds a new line
                    StudentListTextBox.Text = StudentListTextBox.Text + '\n';

                }
            }


            return mainconn;

        }

        protected void BtSortAsc_Click(object sender, EventArgs e)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
            StudentListTextBox.Text = "";
            SqlConnection sqlconn = new SqlConnection(mainconn);
            sqlconn.Open();
            int courseId = GetCourseid(sqlconn);
            string sqlquery = "SELECT StudentName FROM [dbo].[StudentTable] JOIN [dbo].[StudentCourseTable] ON [dbo].[StudentTable].Stid = [dbo].[StudentCourseTable].Studentid JOIN [dbo].[CourseTable] ON [dbo].[StudentCourseTable].Courseid = [dbo].[CourseTable].Cid WHERE [dbo].[CourseTable].Cid = @Courseid ORDER BY StudentName ASC";

            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
            sqlcomm.Parameters.AddWithValue("@Courseid", courseId);
            SqlDataReader sdr2 = sqlcomm.ExecuteReader();
            while (sdr2.Read())
            {
                for (int i = 0; i < sdr2.FieldCount; i++)
                {
                    StudentListTextBox.Text += sdr2[i].ToString();
                    StudentListTextBox.Text = StudentListTextBox.Text + '\n';

                }
            }

            // StudentListBox_TextChanged(sdr[i].ToString());
            sqlconn.Close();

            


        }

        protected void BtSortDesc_Click(object sender, EventArgs e)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
            StudentListTextBox.Text = "";
            SqlConnection sqlconn = new SqlConnection(mainconn);
            sqlconn.Open();
            int courseId = GetCourseid(sqlconn);
            string sqlquery = "SELECT StudentName FROM [dbo].[StudentTable] JOIN [dbo].[StudentCourseTable] ON [dbo].[StudentTable].Stid = [dbo].[StudentCourseTable].Studentid JOIN [dbo].[CourseTable] ON [dbo].[StudentCourseTable].Courseid = [dbo].[CourseTable].Cid WHERE [dbo].[CourseTable].Cid = @Courseid ORDER BY StudentName DESC";

            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
            sqlcomm.Parameters.AddWithValue("@Courseid", courseId);
            SqlDataReader sdr2 = sqlcomm.ExecuteReader();
            while (sdr2.Read())
            {
                for (int i = 0; i < sdr2.FieldCount; i++)
                {
                    StudentListTextBox.Text += sdr2[i].ToString();
                    StudentListTextBox.Text = StudentListTextBox.Text + '\n';

                }
            }

            // StudentListBox_TextChanged(sdr[i].ToString());
            sqlconn.Close();

        }


        protected void TeacherTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        protected void StudentListTextBox_TextChanged1(object sender, EventArgs e)
        {

        }

        
    }
}