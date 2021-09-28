using Newtonsoft.Json;
using SchoolManagementSystem.Models;
using SchoolManagementSystem.Utilities;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows;

namespace SchoolManagementSystem.ViewModels
{
    class CourseViewModel : ViewModelBase
    {
        private ObservableCollection<Course> _courseRecords;
        public ObservableCollection<Course> AllCourses
        {
            get
            {
                return _courseRecords;
            }
            set
            {
                _courseRecords = value;
                OnPropertyChanged("AllCourses");
            }
        }

        private int _CourseID;
        public int CourseID
        {
            get { return _CourseID; }
            set { _CourseID = value; }
        }

        private string _CourseName;
        public string CourseName
        {
            get { return _CourseName; }
            set
            {

                _CourseName = value;
                OnPropertyChanged("CourseName");

            }
        }

        private string _CourseCode;
        public string CourseCode
        {
            get { return _CourseCode; }
            set
            {
                _CourseCode = value;
                OnPropertyChanged("CourseCode");

            }
        }


        private string _Description;
        public string Description
        {
            get { return _Description; }
            set
            {

                _Description = value;
                OnPropertyChanged("Description");

            }
        }

        private DateTime _ExamDate;
        public DateTime ExamDate
        {
            get { return _ExamDate; }
            set
            {
                _ExamDate = value;
                OnPropertyChanged("ExamDate");

            }
        }

        private string _responseMessage = "";

        public string ResponseMessage
        {
            get { return _responseMessage; }
            set
            {
                _responseMessage = value;
                OnPropertyChanged("ResponseMessage");
            }
        }

        #region CRUD
        /// <summary>
        /// Check Course Code
        /// </summary>
        public bool CheckCourseCode(string courseCode)

        {
            var courseDetails1 = WebAPI.GetCall(API_URIs.courses + "/CheckCourseCode?CourseCode=" + courseCode + "");
         //  MessageBox.Show(API_URIs.courses + "/CheckCourseCode?CourseCode=" + courseCode + "");
            //Result.Content.ReadAsStringAsync().Result.ToString()
            if (courseDetails1.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                
                MessageBox.Show("FOUND");
                return true;
            }
            else 
            {
                MessageBox.Show("NOT FOUND");
                return false;
            }
        }


        /// <summary>
        /// Fetches Course details
        /// </summary>
        public void GetCourseDetails()

        {
            var courseDetails = WebAPI.GetCall(API_URIs.courses);
            if (courseDetails.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                AllCourses = courseDetails.Result.Content.ReadAsAsync<ObservableCollection<Course>>().Result;
            }
        }

        /// <summary>
        /// Adds new Course
        /// </summary>
        public void CreateNewCourse(string courseName, string courseCode, string description, DateTime examDate)
        {
            Course newCourse = new Course()
            {
                CourseName = courseName,
                CourseCode = courseCode,
                Description = description,
                ExamDate = examDate
            };

            var courseDetails = WebAPI.PostCall(API_URIs.courses, newCourse);
            if (courseDetails.Result.StatusCode == System.Net.HttpStatusCode.Created)
            {
                MessageBox.Show(newCourse.CourseID + "'s details has successfully been added!");
            }
            else
            {
                MessageBox.Show("Failed to add" + newCourse.CourseID + "'s details.");
            }
        }


        /// <summary>
        /// Updates Course's record
        /// </summary>
        /// <param name="courses"></param>
        public void UpdateCourseDetails(int CourseID, string courseName, string courseCode, string description, DateTime examDate)
        {
            Course updateCourse = new Course()
            {
                CourseID = CourseID,
                CourseName = courseName,
                CourseCode = courseCode,
                Description = description,
                ExamDate = examDate
            };


            var courseDetails = WebAPI.PutCall(API_URIs.courses + "?id=" + updateCourse.CourseID, updateCourse);
            if (courseDetails.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ResponseMessage = updateCourse.CourseID + "'s details has successfully been updated!";

            }
            else
            {
                ResponseMessage = "Failed to update" + updateCourse.CourseID + "'s details.";
            }
        }

        /// <summary>
        /// Deletes Course's record
        /// </summary>
        public void DeleteCourseDetails(int CourseID)
        {

            Course deleteCourse = new Course()
            {
                CourseID = CourseID,
                CourseCode = CourseCode

            };
            MessageBox.Show(deleteCourse.CourseID.ToString());

            var courseDetails = WebAPI.DeleteCall(API_URIs.courses + "?id=" + deleteCourse.CourseID);
            MessageBox.Show(courseDetails.ToString());
            if (courseDetails.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ResponseMessage = deleteCourse.CourseID + "'s details has successfully been deleted!";
                MessageBox.Show(ResponseMessage);
            }
            else
            {
                ResponseMessage = "Failed to delete" + deleteCourse.CourseID + "'s details.";
                MessageBox.Show(ResponseMessage);
            }
        }
        #endregion
    }
}