namespace Help;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
	}
    private void OnHelpClicked(object sender, System.EventArgs e)
    {
        string pageName = Current.CurrentPage.GetType().Name;
        Current.FlyoutIsPresented = false;
        Current.Navigation.PushAsync(new HelpPage(pageName));
    }
}
