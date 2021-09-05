using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for uploadcourseTry2.xaml
    /// </summary>
    public partial class uploadcourseTry2 : Page
    {
        public uploadcourseTry2()
        {
            InitializeComponent();
        }

        private void sheettxt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void browsebtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "Select File";
            fdlg.FileName = txtfilename.Text;
            fdlg.Filter = "Excel Sheet (*.xls)|*.xls|All Files(*.*)|*.*|Excell|*.xlsx";
            fdlg.FilterIndex = 1;
            fdlg.RestoreDirectory = true;

            if (fdlg.ShowDialog() == true)
            {
                txtfilename.Text = fdlg.FileName;
            }


        }
    }
}
