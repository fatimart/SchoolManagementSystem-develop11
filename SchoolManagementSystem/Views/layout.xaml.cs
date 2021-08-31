using SchoolManagementSystem.Models;
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
    /// Interaction logic for layout.xaml
    /// </summary>
    public partial class layout : Window
    {
       // CourseListScreen CList = new CourseListScreen();
        RegisterUsers RScreen = new RegisterUsers();

        public layout()
        {
            InitializeComponent();
           // pages.Content = RScreen;

        }

        public bool button3Clicked=false;

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            // Set tooltip visibility

            if (Tg_Btn.IsChecked == true)
            {
                tt_home.Visibility = Visibility.Collapsed;
                tt_contacts.Visibility = Visibility.Collapsed;
                tt_messages.Visibility = Visibility.Collapsed;
                tt_maps.Visibility = Visibility.Collapsed;
                tt_settings.Visibility = Visibility.Collapsed;
                tt_signout.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt_home.Visibility = Visibility.Visible;
                tt_contacts.Visibility = Visibility.Visible;
                tt_messages.Visibility = Visibility.Visible;
                tt_maps.Visibility = Visibility.Visible;
                tt_settings.Visibility = Visibility.Visible;
                tt_signout.Visibility = Visibility.Visible;
            }
        }

        private void Tg_Btn_Unchecked(object sender, RoutedEventArgs e)
        {
          
        }

        private void Tg_Btn_Checked(object sender, RoutedEventArgs e)
        {
       
        }

        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tg_Btn.IsChecked = false;
        }

        //Logout Button
        private void Button_Click ( object sender, RoutedEventArgs e )
        {

            User user = new User();
            UserViewModel.userSession = user;


            MessageBox.Show(UserViewModel.userSession.UserID.ToString());
            LoginScreen dashboard = new LoginScreen();
            dashboard.Show();

            this.Close();
        }

        

        private void Home_Click ( object sender, RoutedEventArgs e )
        {

            string type = UserViewModel.userSession.Type.ToString();

            if (type == "Admin" || type == "Admin")
            {
                pages.Source = new Uri("RegisterUsers.xaml", UriKind.Relative);
            }
            else if (type == "Student" || type == "student")
            {
                pages.Source = new Uri("StudentView.xaml", UriKind.Relative);

            }
            else if (type == "Teacher" || type == "teacher")
            {
                //add teacher view
                pages.Source = new Uri("ProfileScreen.xaml", UriKind.Relative);

            }
        }

        //User tabs 
        private void Button_Click3 ( object sender, RoutedEventArgs e )
        {

            pages.Source = new Uri("ProfileScreen.xaml", UriKind.Relative);
            
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

 
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {

            SchoolManagementSystem.SchoolMSDataSet schoolMSDataSet = ((SchoolManagementSystem.SchoolMSDataSet)(this.FindResource("schoolMSDataSet")));
            // Load data into the table Course. You can modify this code as needed.
            SchoolManagementSystem.SchoolMSDataSetTableAdapters.CourseTableAdapter schoolMSDataSetCourseTableAdapter = new SchoolManagementSystem.SchoolMSDataSetTableAdapters.CourseTableAdapter();
            schoolMSDataSetCourseTableAdapter.Fill(schoolMSDataSet.Course);
            System.Windows.Data.CollectionViewSource courseViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("courseViewSource")));
            courseViewSource.View.MoveCurrentToFirst();
        }

        private void GridSplitter_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            button3Clicked = true;
            DataContext = new CourseListScreen();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            pages.Source = new Uri("RoomScreen.xaml", UriKind.Relative);
        }

        private void pages_Navigated ( object sender, NavigationEventArgs e )
        {

        }

        protected override void OnMouseLeftButtonDown ( MouseButtonEventArgs e )
        {
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            this.DragMove();
        }

    }
}
