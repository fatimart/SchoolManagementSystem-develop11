using SchoolManagementSystem.Models;
using SchoolManagementSystem.ViewModels;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.Windows;
using SchoolManagementSystem.Utilities;

namespace SchoolManagementSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SchoolMSEntities1 dataEntities = new SchoolMSEntities1();
        private readonly EditProfileViewModel _viewModel;

        private HttpClient client = new HttpClient();
        
        //client.DefaultRequestHeaders.Accept.Clear();  


        public MainWindow ()
        {
            client.BaseAddress = new Uri("");
            InitializeComponent();


            _viewModel = new EditProfileViewModel();
            DataContext = _viewModel;
        }

       
        private void Window_Loaded ( object sender, RoutedEventArgs e )
        {

        }

        private void StudentList_Click ( object sender, RoutedEventArgs e )
        {
            
        }

        
    }
}
