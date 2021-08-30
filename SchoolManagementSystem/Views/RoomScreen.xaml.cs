using SchoolManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SchoolManagementSystem.Views
{
    /// <summary>
    /// Interaction logic for RoomScreen.xaml
    /// </summary>
    public partial class RoomScreen : Page
    {
        RoomViewModel room = new RoomViewModel();
        public RoomScreen()
        {
            InitializeComponent();
        }

        private void roomDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SchoolManagementSystem.SchoolMSDataSet schoolMSDataSet = ((SchoolManagementSystem.SchoolMSDataSet)(this.FindResource("schoolMSDataSet")));
            // Load data into the table Room. You can modify this code as needed.
            SchoolManagementSystem.SchoolMSDataSetTableAdapters.RoomTableAdapter schoolMSDataSetCourseTableAdapter = new SchoolManagementSystem.SchoolMSDataSetTableAdapters.RoomTableAdapter();
            schoolMSDataSetCourseTableAdapter.Fill(schoolMSDataSet.Room);
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            //add room 
            room.AddRoom(roomNumTextBox.Text.Trim());
            Load();
        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {//update 
            if (room.CheckRoomID(Convert.ToInt32(roomIDTextBox.Text)))
            {

                room.UpdateRoom(Convert.ToInt32(roomIDTextBox.Text), roomNumTextBox.Text.Trim());
                Load();

            }

            else
            {
                MessageBox.Show("ID not existed");
            }
         
        }

        private void Button_Click4(object sender, RoutedEventArgs e)
        {//delete
            if (room.CheckRoomID(Convert.ToInt32(roomIDTextBox.Text)))
            {
                room.DeleteRoom(Convert.ToInt32(roomIDTextBox.Text));
            }
            else
            {
                MessageBox.Show("ID not existed");
            }

        }

        private void Button_Clear(object sender, RoutedEventArgs e)
        {
            roomIDTextBox.Text = roomNumTextBox.Text = "";
        }
        private void Load()
        {
            SchoolManagementSystem.SchoolMSDataSet schoolMSDataSet = ((SchoolManagementSystem.SchoolMSDataSet)(this.FindResource("schoolMSDataSet")));
            // Load data into the table Room. You can modify this code as needed.
            SchoolManagementSystem.SchoolMSDataSetTableAdapters.RoomTableAdapter schoolMSDataSetCourseTableAdapter = new SchoolManagementSystem.SchoolMSDataSetTableAdapters.RoomTableAdapter();
            schoolMSDataSetCourseTableAdapter.Fill(schoolMSDataSet.Room);

        } 
    }
}
