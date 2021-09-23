using Newtonsoft.Json;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;
using System.Windows.Controls;

namespace SchoolManagementSystem.Views
{
    /// <summary>
    /// Interaction logic for YearScreen.xaml
    /// </summary>
    public partial class YearScreen : Page
    {
        YearViewModel year = new YearViewModel();
        public HttpClient apiClient;

        public YearScreen ()
        {
            InitializeComponent();
            InitielizeClient();
            
            //GetYears();
            DataContext = year.AllYears;
            FillDataGrid();
        }

        private void InitielizeClient ()
        {
            string api = ConfigurationManager.AppSettings["api"];

            apiClient = new HttpClient();
            apiClient.BaseAddress = new Uri(api);
            apiClient.DefaultRequestHeaders.Accept.Clear();
            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //GetYears();
          //  GetYears1();
        }

        public void Load()
        {
            FillDataGrid();
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {//add

            if (yearNotEmpty())
            {
                year.CreateNewYear(yearNumTextBox.Text);
                Load();
            }
            else
            {
                MessageBox.Show("Please add a year number!");

            }

        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {//update

            if (yearIDtEmpty() && yearNotEmpty())
            {
                year.UpdateYearDetails(Convert.ToInt32(yearIDTextBox.Text),
                                        yearNumTextBox.Text
                                       );
                    Load();

            }

            else
            {
                MessageBox.Show("Please Enter the Year ID and YearNum");
            }

        }

        private void Button_Click4(object sender, RoutedEventArgs e)
        {//delete
            if (yearIDtEmpty())
            {
               

                    year.DeleteYeareDetails(Convert.ToInt32(yearIDTextBox.Text));
                    Load();
            }

            else
            {
                MessageBox.Show("Please Enter Year ID");
            }

        }

        private void Button_Clear(object sender, RoutedEventArgs e)
        {
            yearIDTextBox.Text = yearNumTextBox.Text = "";
        }

        public bool yearNotEmpty()
        {
            if(yearNumTextBox.Text != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool yearIDtEmpty ()
        {
            if (yearIDTextBox.Text != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //To select the row and pop it in the textboxes
        private void FillDataGrid ()

        {
            string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(strcon))
            {
                SqlCommand command = new SqlCommand(
                   "select * from Year", connection);
                connection.Open();

                SqlDataAdapter sda = new SqlDataAdapter(command);

                DataTable dt = new DataTable("Users");

                sda.Fill(dt);

                yearDataGrid.ItemsSource = dt.DefaultView;
                yearDataGrid.Items.Refresh();

            }

        }

        public async void GetYears ()
        {
            var response = await apiClient.GetStringAsync("Year");
            var years = JsonConvert.DeserializeObject<List<Year>>(response);

            yearDataGrid.DataContext = years;
        }

        public async void GetYears1 ()
        {
            year.GetYearDetails();
        }

            private void yearDataGrid_SelectionChanged_1 ( object sender, SelectionChangedEventArgs e )
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            if (row_selected != null)
            {
                yearIDTextBox.Text = row_selected["YearID"].ToString();
                yearNumTextBox.Text = row_selected["YearNum"].ToString();

            }
        }
    }
}

  

