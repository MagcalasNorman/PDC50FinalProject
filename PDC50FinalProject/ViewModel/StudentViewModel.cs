using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PDC50FinalProject.Model;
using PDC50FinalProject.Services;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace PDC50FinalProject.ViewModel
{
    public class StudentViewModel : BindableObject
    {
        private readonly StudentService _studentService;
        public ObservableCollection<Student> Students { get; set; }
        private Student _selectedStudent;
        public Student SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                _selectedStudent = value;
                OnPropertyChanged();
                UpdateEntryField();
            }
        }

        private string _nameInput;
        public string NameInput
        {
            get => _nameInput;
            set
            {
                _nameInput = value;
                OnPropertyChanged();
            }
        }

        private string _genderInput;
        public string GenderInput
        {
            get => _genderInput;
            set
            {
                _genderInput = value;
                OnPropertyChanged();
            }
        }

        private string _contactNoInput;
        public string ContactNoInput
        {
            get => _contactNoInput;
            set
            {
                _contactNoInput = value;
                OnPropertyChanged();
            }
        }
        private string _emailInput;
        public string EmailInput
        {
            get => _emailInput;
            set
            {
                _emailInput = value;
                OnPropertyChanged();
            }
        }

        private string _courseInput;
        public string CourseInput
        {
            get => _courseInput;
            set
            {
                _courseInput = value;
                OnPropertyChanged();
            }
        }

        private string _gradeInput;
        public string GradeInput
        {
            get => _gradeInput;
            set
            {
                _gradeInput = value;
                OnPropertyChanged();
            }
        }

        private string _attendanceInput;
        public string AttendanceInput
        {
            get => _attendanceInput;
            set
            {
                _attendanceInput = value;
                OnPropertyChanged();
            }
        }
        private void ClearInput()
        {
            NameInput = string.Empty;
            GenderInput = string.Empty;
            ContactNoInput = string.Empty;
            EmailInput = string.Empty;
            AttendanceInput = string.Empty;
            CourseInput = string.Empty;
            GradeInput = string.Empty;

        }
        private void UpdateEntryField()
        {
            if (SelectedStudent != null)
            {
                NameInput = SelectedStudent.Name;
                GenderInput = SelectedStudent.Gender;
                ContactNoInput = SelectedStudent.ContactNo;
                EmailInput = SelectedStudent.Email;
                CourseInput = SelectedStudent.Course;
                GradeInput = SelectedStudent.GradeLevel;
                AttendanceInput = SelectedStudent.AttendanceRecords;
            }
            else
            {
                ClearInput();
            }
        }
        public StudentViewModel()
        {
            _studentService = new StudentService();
            Students = new ObservableCollection<Student>();
            LoadStudentCommand = new Command(async () => await LoadStudents());
            AddStudentCommand = new Command(async () => await AddStudents());
            UpdateStudentCommand = new Command(async () => await UpdateStudents());
            DeleteStudentCommand = new Command(async () => await DeleteStudents());
        }


        public ICommand LoadStudentCommand { get; }
        public ICommand AddStudentCommand { get; }
        public ICommand UpdateStudentCommand { get; }
        public ICommand DeleteStudentCommand { get; }
        private async Task LoadStudents()
        {
            Students.Clear();

            var students = await _studentService.GetStudentAsync();
            foreach (var student in students)
            {
                Students.Add(student);
                ClearInput();
            }
        }

        private async Task AddStudents()
        {
            // Check if all required inputs are provided
            if (!string.IsNullOrWhiteSpace(NameInput) &&
                !string.IsNullOrWhiteSpace(GenderInput) &&
                !string.IsNullOrWhiteSpace(ContactNoInput) &&
                !string.IsNullOrWhiteSpace(EmailInput) &&
                !string.IsNullOrWhiteSpace(CourseInput) &&
                !string.IsNullOrWhiteSpace(GradeInput) &&
                !string.IsNullOrWhiteSpace(AttendanceInput))
            {
                // Create a new student object
                var newStudent = new Student
                {
                    Name = NameInput,
                    Gender = GenderInput,
                    ContactNo = ContactNoInput,
                    Email = EmailInput,
                    Course = CourseInput,
                    GradeLevel = GradeInput,
                    AttendanceRecords = AttendanceInput
                };

                var result = await _studentService.AddStudentsAsync(newStudent);
                if (result.Equals("Success", StringComparison.OrdinalIgnoreCase))
                {
                    await LoadStudents();
                    ClearInput();
                }

            }
        }

        private async Task UpdateStudents()
        {
            if (SelectedStudent != null)
            {
                SelectedStudent.Name = NameInput;
                SelectedStudent.Gender = GenderInput;
                SelectedStudent.ContactNo = ContactNoInput;
                SelectedStudent.Email = EmailInput;
                SelectedStudent.Course = CourseInput;
                SelectedStudent.GradeLevel = GradeInput;
                SelectedStudent.AttendanceRecords = AttendanceInput;

                var result = await _studentService.UpdateStudentsAsync(SelectedStudent);
                await LoadStudents();
                ClearInput();
            }
        }

        private async Task DeleteStudents()
        {
            if (SelectedStudent != null)
            {
                var result = await _studentService.DeleteUsersAsync(SelectedStudent.ID);
                await LoadStudents();
                ClearInput();
            }
        }
    }
}
