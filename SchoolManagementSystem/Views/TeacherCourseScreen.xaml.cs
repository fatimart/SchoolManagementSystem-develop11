﻿using System;
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
    /// Interaction logic for TeacherCourseScreen.xaml
    /// </summary>
    public partial class TeacherCourseScreen : Page
    {
        TeacherScreen Tscreen = new TeacherScreen();
        public TeacherCourseScreen()
        {
            InitializeComponent();
          
              MessageBox.Show(Tscreen.comboBoxValue());
        }
    }
}
