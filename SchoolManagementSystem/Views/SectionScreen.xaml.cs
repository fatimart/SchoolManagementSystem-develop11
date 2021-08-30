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
    /// Interaction logic for SectionScreen.xaml
    /// </summary>
    public partial class SectionScreen : Page
    {
        public SectionScreen()
        {
            InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SchoolManagementSystem.SchoolMSDataSet schoolMSDataSet = ((SchoolManagementSystem.SchoolMSDataSet)(this.FindResource("schoolMSDataSet")));
            // Load data into the table Section. You can modify this code as needed.
            SchoolManagementSystem.SchoolMSDataSetTableAdapters.SectionTableAdapter schoolMSDataSetCourseTableAdapter = new SchoolManagementSystem.SchoolMSDataSetTableAdapters.SectionTableAdapter();
            schoolMSDataSetCourseTableAdapter.Fill(schoolMSDataSet.Section);
        }

        public void Load()
        {
            SchoolManagementSystem.SchoolMSDataSet schoolMSDataSet = ((SchoolManagementSystem.SchoolMSDataSet)(this.FindResource("schoolMSDataSet")));
            // Load data into the table Section. You can modify this code as needed.
            SchoolManagementSystem.SchoolMSDataSetTableAdapters.SectionTableAdapter schoolMSDataSetCourseTableAdapter = new SchoolManagementSystem.SchoolMSDataSetTableAdapters.SectionTableAdapter();
            schoolMSDataSetCourseTableAdapter.Fill(schoolMSDataSet.Section);

        }
        private void Button_Click1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click4(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Clear(object sender, RoutedEventArgs e)
        {

        }
    }
}
