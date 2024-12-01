using PDC50FinalProject.ViewModel;

namespace PDC50FinalProject.View;

public partial class StudentList : ContentPage
{
	public StudentList()
	{
		InitializeComponent();
		BindingContext = new StudentViewModel();
	}
}