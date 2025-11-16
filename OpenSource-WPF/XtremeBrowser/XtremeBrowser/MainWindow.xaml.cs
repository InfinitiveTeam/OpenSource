using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace XtremeBrowser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string LastUrl { get; set; }
        
        public MainWindow()
        {
            InitializeComponent();
            InitializeWebViewAsync(); // 异步初始化
        }

        private async void InitializeWebViewAsync()
        {
            try
            {
                // 初始化 WebView2 核心环境
                await webView.EnsureCoreWebView2Async(null);

                // 加载 Bing 首页
                webView.CoreWebView2.Navigate("https://www.limestart.cn/");
                LastUrl = "http://www.limestart.cn/";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"初始化失败: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        // 地址栏回车导航
        private void UrlTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && !string.IsNullOrEmpty(urlTextBox.Text))
                if (urlTextBox.Text.Contains("https://") || urlTextBox.Text.Contains("http://"))
                {
                    webView.CoreWebView2.Navigate(urlTextBox.Text);
                    LastUrl = urlTextBox.Text;
                }
                else if (Uri.CheckHostName(urlTextBox.Text) != UriHostNameType.Unknown)
                {
                    webView.CoreWebView2.Navigate("https://" + urlTextBox.Text);
                    LastUrl = "https://" + urlTextBox.Text;
                }
                else if (urlTextBox.Text == "/home")
                {
                    webView.CoreWebView2.Navigate("https://www.limestart.cn/");
                    LastUrl = "https://www.limestart.cn/";
                }
                else if (urlTextBox.Text == "/back")
                {
                    if(LastUrl != "") MessageBox.Show("上一页没有任何网址", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    webView.CoreWebView2.Navigate(LastUrl);
                }
                else
                {
                    MessageBox.Show("请输入正确的网址", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    urlTextBox.Clear();
                    return;
                }
        }
    }
}