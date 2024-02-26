using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kreta.Desktop.Repos;
using MenuProject.ViewModels.Base;
using StudentProject.Models;
using System.Collections.ObjectModel;

namespace MenuProject.ViewModels
{
    public partial class StudentViewModel : BaseViewModel
    {
        private readonly EducationLevelsRepo _educationLevelsRepo = new();
        private readonly StudentRepo _studentRepo = new();

        [ObservableProperty]
        private int _numberOfStudent = 0;

        [ObservableProperty]
        private ObservableCollection<string> _educationLevels = new();

        [ObservableProperty]
        private ObservableCollection<Student> _students = new();

        [ObservableProperty]
        private Student _selectedStudent;

        [ObservableProperty]
        private string _statusBarText = string.Empty;
        
        public StudentViewModel()
        {
            _selectedStudent = new Student();
            Update();
            NumberOfStudent = _studentRepo.GetNumberOfStudents();
            StatusBarText = $"Diák adatok betöltve.";
        }

        [RelayCommand]
        public void DoSave(Student student)
        {
            if (student.HasId)
            {
                _studentRepo.Update(student);
                StatusBarText = "A diák adata frissítve lett!";
            }
            else
            {
                _studentRepo.Insert(student);
                StatusBarText = "Új diák adata mentésre került!";
            }
            Update();
        }

        [RelayCommand]
        void DoNewStudent()
        {
            SelectedStudent = new Student();
            StatusBarText = "Adja meg az új diák adatait!";
        }

        [RelayCommand]
        public void DoRemove(Student studentToDelete)
        {
            _studentRepo.Delete(studentToDelete);
            Update();
            StatusBarText = $"{studentToDelete.HungarianName} törölve lett.";
        }

        private void Update()
        {
            EducationLevels = new ObservableCollection<string>(_educationLevelsRepo.FindAll());
            Students = new ObservableCollection<Student>(_studentRepo.FindAll());
            NumberOfStudent = _studentRepo.GetNumberOfStudents() ;
        }
    }
}
