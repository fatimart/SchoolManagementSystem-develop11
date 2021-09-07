using SchoolManagementSystem.ViewModels;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace SchoolManagementSystem.Views.AdminViews
{
    /// <summary>
    /// Interaction logic for StudentsListScreen.xaml
    /// </summary>
    public partial class StudentsListScreen : Page
    {
        StudentListViewModel _userViewModel = new StudentListViewModel();

        public StudentsListScreen ()
        {
            InitializeComponent();
            DataContext = _userViewModel;

            FillDataGrid();
        }

        public void Load ()
        {

            usersDataGrid.Items.Refresh();

        }

        private void Window_Loaded ( object sender, RoutedEventArgs e )
        {
            usersDataGrid.Items.Refresh();
        }

        private void FillDataGrid ()

        {
             string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

             using (SqlConnection connection = new SqlConnection(strcon))
             {
                SqlCommand command = new SqlCommand(
                   "select * from Users where Type='" + "student" + "'", connection);
                connection.Open();

                SqlDataAdapter sda = new SqlDataAdapter(command);

                DataTable dt = new DataTable("Users");

                sda.Fill(dt);
                usersDataGrid.ItemsSource = dt.DefaultView;

                //usersDataGrid.ItemsSource = _userViewModel.AllUsers;

                usersDataGrid.Items.Refresh();

             }

        }



        private void AddNewStudent_Click ( object sender, RoutedEventArgs e )
        {

            _userViewModel.InsertUser(
                                      //Convert.ToInt32(userIDTextBox.Text.Trim()),
                                      userNameTextBox.Text.Trim(),
                                      nameTextBox.Text.Trim(),
                                      emailTextBox.Text.Trim(),
                                      Convert.ToDecimal(cPRTextBox.Text),
                                      addressTextBox.Text.Trim(),
                                      Convert.ToDateTime(dOBDatePicker.Text),
                                      passwordTextBox.Text.Trim(),
                                      contactNoTextBox.Text.Trim()
                                  );

            FillDataGrid();
        }

        private void UpdateStudent_Click ( object sender, RoutedEventArgs e )
        {
            _userViewModel.UpdateUser(
                                      Convert.ToInt32(userIDTextBox.Text.Trim()),
                                      userNameTextBox.Text.Trim(),
                                      nameTextBox.Text.Trim(),
                                      emailTextBox.Text.Trim(),
                                      Convert.ToDecimal(cPRTextBox.Text),
                                      addressTextBox.Text.Trim(),
                                      Convert.ToDateTime(dOBDatePicker.Text),
                                      passwordTextBox.Text.Trim(),
                                      contactNoTextBox.Text.Trim()
                                  );
            FillDataGrid();
        }


        private void DeleteStudent_Click ( object sender, RoutedEventArgs e )
        {
            _userViewModel.DeleteUser(Convert.ToInt32(userIDTextBox.Text.Trim()));
            FillDataGrid();
        }


        private void Button_Click_1 ( object sender, RoutedEventArgs e )
        {
            getUserByID();
        }

        private void usersDataGrid_SelectionChanged ( object sender, SelectionChangedEventArgs e )
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            if (row_selected != null)
            {

                userIDTextBox.Text = row_selected["UserID"].ToString();
                userNameTextBox.Text = row_selected["UserName"].ToString();
                nameTextBox.Text = row_selected["Name"].ToString();
                emailTextBox.Text = row_selected["Email"].ToString();
                cPRTextBox.Text = row_selected["CPR"].ToString();
                addressTextBox.Text = row_selected["Address"].ToString();
                dOBDatePicker.Text = row_selected["DOB"].ToString();
                passwordTextBox.Text = row_selected["Password"].ToString();
                contactNoTextBox.Text = row_selected["ContactNo"].ToString();
            }
        }

        private void Reset_Click ( object sender, RoutedEventArgs e )
        {
            Clear();
            _userViewModel.ResetData();
        }

        public void Clear ()
        {
            userIDTextBox.Text = "";
            userNameTextBox.Text = "";
            nameTextBox.Text = "";
            emailTextBox.Text = "";
            cPRTextBox.Text = "";
            addressTextBox.Text = "";
            dOBDatePicker.Text = "";
            passwordTextBox.Text = "";
            contactNoTextBox.Text = "";
        }

        public void getUserByID ()
        {
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from Users WHERE UserID='" + userIDTextBox.Text.Trim() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable(); //store data in dt

                da.Fill(dt);

                if (dt.Rows.Count >= 1) //check if id exist -- got one record
                {

                    userIDTextBox.Text = dt.Rows[0]["UserID"].ToString();
                    userNameTextBox.Text = dt.Rows[0]["UserName"].ToString();
                    nameTextBox.Text = dt.Rows[0]["Name"].ToString();
                    emailTextBox.Text = dt.Rows[0]["Email"].ToString();
                    cPRTextBox.Text = dt.Rows[0]["CPR"].ToString();
                    addressTextBox.Text = dt.Rows[0]["Address"].ToString();
                    dOBDatePicker.Text = Convert.ToDateTime(dt.Rows[0]["DOB"]).ToString();
                    passwordTextBox.Text = dt.Rows[0]["Password"].ToString();
                    contactNoTextBox.Text = dt.Rows[0]["ContactNo"].ToString();

                }
                else
                {
                    MessageBox.Show("Invalid user ID");


                }
            }

            catch (Exception ex)
            { 
                MessageBox.Show(ex.Message);

            }
        }

    }
}