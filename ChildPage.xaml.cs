namespace Help;

public partial class ChildPage : ContentPage
{
    public ChildPage()
    {
        InitializeComponent();
    }

    private void OnHelpIconClicked(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PushAsync(new HelpPage(nameof(ChildPage)));
    }

    private void OnHelpIndexButtonClicked(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PushAsync(new HelpPage());
    }
}