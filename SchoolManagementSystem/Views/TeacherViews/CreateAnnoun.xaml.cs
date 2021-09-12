using SchoolManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

namespace SchoolManagementSystem.Views.TeacherViews
{
    /// <summary>
    /// Interaction logic for CreateAnnoun.xaml
    /// </summary>
    public partial class CreateAnnoun : Window
    {
        AnnoucnmentViewModel viewmodel = new AnnoucnmentViewModel();

        public string CID;
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        private SqlConnection GetConnection ()
        {
            return new SqlConnection(strcon);
        }

        public CreateAnnoun ()
        {
            InitializeComponent();
        }

        public CreateAnnoun ( string CID )
        {
            InitializeComponent();
            string type = UserViewModel.userSession.Type.ToString();

            
            ComboBoxID(CID);
            LoadData();
        }
        public string ComboBoxID ( string cid )
        {

            CID = cid;
            return CID;
        }
        private void LoadData ()
        {

            using (SqlConnection cn = GetConnection())
            {
                string query = "Select AnnounID,Announcement,TimeAnnounced from Announcement where CourseID='" + CID + "'";
                SqlDataAdapter adp = new SqlDataAdapter(query, cn);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    datagrid.ItemsSource = dt.DefaultView;
                }
            }
        }

        private void vase_Click ( object sender, RoutedEventArgs e )
        {
            //Save Button

            try
            {
                DateTime now = DateTime.Now;

                viewmodel.AddAnnoun(Convert.ToInt32(CID),
                                    announ_txtbox.Document.Blocks.ToString(),
                                    now);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        //Delete Button
        private void Button_Click ( object sender, RoutedEventArgs e )
        {
            if (notEmpty())
            {
                try
                {
                    viewmodel.DeleteAnnoun(Convert.ToInt32(CID));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
        }

        private void clear_Click ( object sender, RoutedEventArgs e )
        {
            Clear();
        }

        public void Clear()
        {
            aid_txt.Text = "";
            announ_txtbox.Document.Blocks.Clear();

        }
        public bool notEmpty()
        {
            if(aid_txt.Text != "")
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
