using System.Windows;

namespace Quiz
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            pageFrame.Content = new MainPage(pageFrame);
        }

        private void goToQuizButton_Click(object sender, RoutedEventArgs e)
        {
            WindowManager.Instance.OpenNewWindow(new QuizWindow(), this);
        }
    }
}