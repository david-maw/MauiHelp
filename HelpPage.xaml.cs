using System.Windows.Input;

namespace Help
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HelpPage : ContentPage
    {
        // variables 
        private string urlPrefix = null, pageName;
        private List<string> PageNames = null;
        public HelpPage(string pageNameParameter = "index")
        {
            pageName = pageNameParameter; // Persist constructor parameter
            Title = pageName + " Help"; // Set page title
            BackCommand = new Command( (s) =>
            {
                if (webView.CanGoBack)
                    webView.GoBack();
                else
                    Shell.Current.Navigation.PopAsync();
            });
            InitializeComponent();
            _ = ShowWebPage(pageName);
            webView.Navigating += WebView_Navigating;
            webView.Navigated += WebView_Navigated;
        }

        ~HelpPage() 
        {
            webView.Navigating -= WebView_Navigating;
            webView.Navigated -= WebView_Navigated;
        }

        // Utility functions to make code showing web pages more concise
        private void ShowHtmlMessage(string msg)
        {
            ShowHtml($"<html><body>{msg}</body></html>");
        }
        private void ShowHtml(string htmlString) 
        {
            HtmlWebViewSource s = new HtmlWebViewSource() { Html = htmlString };
            webView.Source = s;
        }
        private async Task ShowWebPage(string pageName)
        {
            pageName = pageName.ToLower();
            ShowingIndex = pageName.Equals("index");
            // Read the source file
            using Stream fileStream = await FileSystem.Current.OpenAppPackageFileAsync(pageName + ".html");
            using StreamReader reader = new StreamReader(fileStream);
            string content = await reader.ReadToEndAsync();

            ShowHtml(content);
            //ShowHtml($"""<meta http-equiv="Refresh" content="2; url='{pageName}.html'"/><p>Should redirect to {pageName}.html in 2 seconds</p>""");
        }

        protected override void OnAppearing()
        {
            Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
            base.OnDisappearing();
        }

        public ICommand BackCommand { get; }
        public bool ShowingIndex { get => showingIndex; set => showingIndex = value; }

        private bool showingIndex = false;
        private async void OnHelpIconClicked(object sender, System.EventArgs e)
        {
            if (!ShowingIndex)
                await ShowWebPage("index");
        }

        private void WebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
        }

        private void WebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            if (e.Result == WebNavigationResult.Success)
            {
                ShowingIndex = e.Url.EndsWith("index.html");
            }
        }
    }
}