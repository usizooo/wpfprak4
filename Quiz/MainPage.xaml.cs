using System.Windows;
using System.Windows.Controls;
namespace Quiz
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private readonly Frame mainFrame;
        public MainPage(Frame mainFrame)
        {
            InitializeComponent();
            this.mainFrame = mainFrame;
        }

        private void goToEditQuizButton_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new EditPage(mainFrame);
        }
    }
}
