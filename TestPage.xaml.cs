namespace Help
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestPage : ContentPage
    {
        public TestPage()
        {
            InitializeComponent();
        }

        private void OnOpenChildClicked(object sender, EventArgs e)
        {
            Shell.Current.Navigation.PushAsync(new ChildPage());
        }
    }
}