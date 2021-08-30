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
using System.Windows.Shapes;

namespace SchoolManagementSystem.Views
{
    /// <summary>
    /// Interaction logic for CourseRegistrationScreen.xaml
    /// </summary>
    public partial class CourseRegistrationScreen : Page
    {
        CourseViewModel _viewmodel = new CourseViewModel();
        public CourseRegistrationScreen ()
        {
            InitializeComponent();
            DataContext = _viewmodel;

        }

        private void Register_Click ( object sender, RoutedEventArgs e )
        {

        }
    }
}
