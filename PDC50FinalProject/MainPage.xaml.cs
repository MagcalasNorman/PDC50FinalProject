﻿namespace PDC50FinalProject
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void ClickedViewStudent(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//StudentList");
        }
    }

}
