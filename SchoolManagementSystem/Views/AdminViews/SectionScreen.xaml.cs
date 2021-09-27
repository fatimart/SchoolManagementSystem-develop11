using SchoolManagementSystem.Utilities;
using SchoolManagementSystem.ViewModels;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;
using System.Windows.Controls;

namespace SchoolManagementSystem.Views.AdminViews
{
    /// <summary>
    /// Interaction logic for SectionScreen.xaml
    /// </summary>
    public partial class SectionScreen : Page
    {

        SectionViewModel sectionViewModel = new SectionViewModel();
        InitielizeHttpClient initielizeHttpClient = new InitielizeHttpClient();
        public static  int courseID;
        public static int roomID;
        string CourseComboBox;
        public SectionScreen ()
        {
            InitializeComponent();
            initielizeHttpClient.InitielizeClient();

            fillCourseBox();
            fillRoomBox();
            FillDataGrid();
            CourseCodeComboBox(course_code_combobox.Text.ToString());

        }
        public string CourseCodeComboBox(string c)
        {
            CourseComboBox = c;
            return CourseComboBox;
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataGrid();

        }

        //Add
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            if (fillNotEmpty())
            {
                try
                {
                    sectionViewModel.CreateNewSection(Convert.ToInt32(sectionnumtxtbox.Text),
                                                courseID,
                                                roomID,
                                                timetxtbox.Text.Trim().ToString()
                                                  );

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
                finally
                {
                    //Clear();
                    FillDataGrid();

                }
            }
            else
            {
                MessageBox.Show("Please fill all the fields");

            }
        }

        //Update
        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            if (fillNotEmpty())
            {

                try
                {
                    //if (sectionViewModel.checkSectionExists(Convert.ToInt32(sectionnumtxtbox.Text), courseID))
                    //{
                        sectionViewModel.UpdateSectionDetails(Convert.ToInt32(sectionIDtxtbox_Copy.Text),
                                        Convert.ToInt32(sectionnumtxtbox.Text),
                                                    courseID,
                                                    roomID,
                                                    timetxtbox.Text.Trim().ToString()
                                                      );
                    //}
                    //else
                    //{
                    //    MessageBox.Show("course with section number " + Convert.ToInt32(sectionnumtxtbox.Text) + " does not exists!");

                    //}
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
                finally
                {
                    //Clear();
                    FillDataGrid();
                }
            }
            else
            {
                MessageBox.Show("Please fill all the fields");

            }
            
        }

        //Delete
        private void Button_Click4(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Confirm delete of this record?", "Section", MessageBoxButton.YesNo)
                == MessageBoxResult.Yes)
            {
                if (fillNotEmpty())
                {
                    
                        sectionViewModel.DeleteSectionDetails(Convert.ToInt32(sectionIDtxtbox_Copy.Text));
                   
                        //Clear();
                        FillDataGrid();
                   
                }
                else
                {
                    MessageBox.Show("Please fill all the fields of course code and section num");

                }
            }
        }

        //Clear textbox e
        private void Button_Clear(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        public void fillCourseBox ()
        {
            try
            {
                course_code_combobox.Items.Clear();
                sectionViewModel.GetCourseDetails();
                
               
                course_code_combobox.ItemsSource = sectionViewModel.AllCourse;
                course_code_combobox.SelectedIndex = -1;
                course_code_combobox.DisplayMemberPath = "CourseCode";
                course_code_combobox.SelectedValuePath = "CourseCode";

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        public void fillRoomBox ()
        {
            try
            {
                roomNo_combobox.Items.Clear();
                sectionViewModel.GetRoomBox();

                roomNo_combobox.ItemsSource = sectionViewModel.RoomBox;
                roomNo_combobox.SelectedIndex = -1;
                roomNo_combobox.DisplayMemberPath = "RoomNum";
                roomNo_combobox.SelectedValuePath = "RoomNum";
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        public void getroomIDD(string c)
        {

        }
        
        public void getcourseIDD ()
        {

            sectionViewModel.getcourseIDD();
        }

        private void FillDataGrid ()

        {
                sectionViewModel.GetSectionetails();
                sectionDataGrid.ItemsSource =sectionViewModel.AllSections;

                //usersDataGrid.ItemsSource = _userViewModel.AllSections;

                sectionDataGrid.Items.Refresh();

         }

        

        private void course_code_combobox_DropDownClosed ( object sender, EventArgs e )
        {
            if (course_code_combobox.Text != "")
            {
                getcourseIDD();
            }
        }

        private void roomNo_combobox_DropDownClosed ( object sender, EventArgs e )
        {
            if (roomNo_combobox.Text != "")
            {
              //  getroomIDD();
            }
        }

        private bool fillNotEmpty()
        {
            if(course_code_combobox.Text.Length>0 && roomNo_combobox.Text.Length > 0 && sectionnumtxtbox.Text.Length > 0 && timetxtbox.Text.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Clear()
        {
            course_code_combobox.Text = "";
            roomNo_combobox.Text = "";
            sectionnumtxtbox.Text = "";
            timetxtbox.Text = "";
        }

        private void sectionDataGrid_SelectionChanged ( object sender, SelectionChangedEventArgs e )
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            if (row_selected != null)
            {
               sectionIDtxtbox_Copy.Text = row_selected["SectionID"].ToString();
                course_code_combobox.Text = row_selected["CourseCode"].ToString();
                roomNo_combobox.Text = row_selected["RoomNum"].ToString();
                sectionnumtxtbox.Text = row_selected["SectionNum"].ToString();
                timetxtbox.Text = row_selected["Time"].ToString();

            }
            getcourseIDD();
          //  getroomIDD();
        }
    }
}
