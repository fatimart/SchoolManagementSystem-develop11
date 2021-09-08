using SchoolManagementSystem.ViewModels;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Controls;

namespace SchoolManagementSystem.Views.StudentViews
{
    /// <summary>
    /// Interaction logic for StudentScheduleScreen.xaml
    /// </summary>
    public partial class StudentScheduleScreen : Page
    {
        TimeTableViewModel table = new TimeTableViewModel();

        public StudentScheduleScreen ()
        {
            InitializeComponent();
            FillDataGrid();
        }

        private void FillDataGrid ()

        {
            string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(strcon))
            {
                SqlCommand command = new SqlCommand(
                     "select TimeTable.CourseCode,TimeTable.CourseName,TimeTable.SectionNo,TimeTable.RoomNo, TimeTable.Time, Course.Examdate,TimeTable.TeacherName from TimeTable, Course, Section where Course.CourseID=TimeTable.CourseID AND Course.CourseID=Section.CourseID AND UserID='" + UserViewModel.userSession.UserID + "'", connection);
                connection.Open();

                SqlDataAdapter sda = new SqlDataAdapter(command);

                DataTable dt = new DataTable("TimeTable");

                sda.Fill(dt);

                tableDataGrid.ItemsSource = dt.DefaultView;
                tableDataGrid.Items.Refresh();

            }

        }
    }
}
