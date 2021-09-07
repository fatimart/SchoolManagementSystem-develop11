using SchoolManagementSystem.ViewModels;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace SchoolManagementSystem.Views.AdminViews
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
            DataContext = room;
        }

        private void roomDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            if (row_selected != null)
            {
                roomNumTextBox.Text = row_selected["RoomNum"].ToString();
                roomIDTextBox.Text = row_selected["RoomID"].ToString();
            }
        }

       

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            //add room 
            if (roomNotEmpty())
            {
                room.AddRoom(roomNumTextBox.Text.Trim());
                Load();
            }
            else
            {
                MessageBox.Show("Please enter the room number!");
            }
        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {//update
            if (roomIDtEmpty())
            {
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
            else
            {
                MessageBox.Show("Please enter room Id you to update");
            }

        }

        private void Button_Click4(object sender, RoutedEventArgs e)
        {//delete
            if (roomIDtEmpty())
            {
                if (room.CheckRoomID(Convert.ToInt32(roomIDTextBox.Text)))
                {
                    room.DeleteRoom(Convert.ToInt32(roomIDTextBox.Text));
                }
                else
                {
                    MessageBox.Show("ID not existed");
                }
            }
            else
            {
                MessageBox.Show("Please enter the room Id ");
            }

        }

        private void Button_Clear(object sender, RoutedEventArgs e)
        {
            roomIDTextBox.Text = roomNumTextBox.Text = "";
        }

        private void Load()
        {
           
        }

        public bool roomNotEmpty ()
        {
            if (roomNumTextBox.Text != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool roomIDtEmpty ()
        {
            if (roomIDTextBox.Text != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
