using System.Windows;
using System.Windows.Controls;

namespace Quiz
{
    /// <summary>
    /// Логика взаимодействия для QuizWindow.xaml
    /// </summary>
    public partial class QuizWindow : Window
    {
        public QuizWindow()
        {
            InitializeComponent();

            quizEditorButton.IsEnabled = false;

            pageFrame.Content = QuizManager.Instance.Questions.Count > 0
                ? new QuizPage(pageFrame)
                : new EmptyPage();
        }

        public QuizWindow(Page page) : this()
        {
            quizEditorButton.IsEnabled = true;
            pageFrame.Content = page;
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void quizEditorButton_Click(object sender, RoutedEventArgs e)
        {
            pageFrame.Content = new QuizEditPage();
        }

        private void quizButton_Click(object sender, RoutedEventArgs e)
        {
            pageFrame.Content = QuizManager.Instance.Questions.Count > 0
                ? new QuizPage(pageFrame) 
                : new EmptyPage();
        }
    }
}
