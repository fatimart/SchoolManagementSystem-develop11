using SchoolManagementSystem.ViewModels;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using MessageBox = System.Windows.MessageBox;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace SchoolManagementSystem.Views.TeacherViews
{
    /// <summary>
    /// Interaction logic for FileUpload1.xaml
    /// </summary>
    public partial class FileUpload1 : Window
    {
        TeacherScreen TS = new TeacherScreen();
        public string CID;
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        private SqlConnection GetConnection ()
        {
            return new SqlConnection(strcon);

        }

        public FileUpload1 ()
        {
            InitializeComponent();
        }

        public FileUpload1 ( string CID )
        {
            InitializeComponent();
            string type = UserViewModel.userSession.Type.ToString();

            if (type == "Student" || type == "student")
            {
                BrowseBtn.Visibility = Visibility.Collapsed;
                SaveBtn.Visibility = Visibility.Collapsed;
            }
            ComboBoxID(CID);
            LoadData();
        }

        private void Button_Click ( object sender, RoutedEventArgs e )
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.ShowDialog();
            txtFilePath.Text = dlg.FileName;

        }

        private void Button_Click_1 ( object sender, RoutedEventArgs e )
        {
            SaveFile(txtFilePath.Text);
            MessageBox.Show("Saved");
            LoadData();
        }

        public void SaveFile ( string filePath )
        {
            using (Stream stream = File.OpenRead(filePath))
            {
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);

                var fi = new FileInfo(filePath);
                string extn = fi.Extension;
                string name = fi.Name;
                string query = "INSERT INTO Documents(Data,Extension,FileName,CourseID) Values(@data,@ext,@name,@courseID) ";

                using (SqlConnection cn = GetConnection())
                {
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.Parameters.Add("@data", SqlDbType.VarBinary).Value = buffer;
                    cmd.Parameters.Add("@ext", SqlDbType.Char).Value = extn;
                    cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = name;
                    cmd.Parameters.Add("@courseID", SqlDbType.Int).Value = CID;
                    cn.Open();
                    cmd.ExecuteNonQuery();

                }
            }

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
                string query = "Select ID,FileName,Data,Extension from Documents where CourseID='" + CID + "'";
                SqlDataAdapter adp = new SqlDataAdapter(query, cn);
                DataTable dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    dgvDocuments.ItemsSource = dt.DefaultView;
                }
            }
        }

        private void Button_Click_2 ( object sender, RoutedEventArgs e )
        {
            object item = dgvDocuments.SelectedItem;
            if (item != null)
            {
                string ID = (dgvDocuments.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
                OpenFile(Convert.ToInt32(ID));
            }
        }


        public void OpenFile ( int id )
        {
            using (SqlConnection cn = GetConnection())
            {

                string query = "Select ID,FileName,Data,Extension from Documents where CourseID='" + CID + "'And ID='" + id + "'";
                SqlCommand cmd = new SqlCommand(query, cn);
                cn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    var name = reader["FileName"].ToString();
                    var data = (byte[])reader["data"];
                    var extn = reader["Extension"].ToString();
                    var newFileName = name.Replace(extn, DateTime.Now.ToString("ddMMyyyyhhmmss")) + extn;
                    File.WriteAllBytes(newFileName, data);
                    Process.Start(newFileName);
                }
            }
        }

    }
}