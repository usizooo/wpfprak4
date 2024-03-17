using System.Windows.Controls;

namespace Quiz
{
    /// <summary>
    /// Логика взаимодействия для QuizResultsPage.xaml
    /// </summary>
    public partial class QuizResultsPage : Page
    {
        public QuizResultsPage()
        {
            InitializeComponent();
            quizRezultsLabel.Content = "Правильных ответов " + QuizManager.Instance.CountRightAnswer
                + " из " + QuizManager.Instance.Questions.Count;
            QuizManager.Instance.AnswerSessionFinished?.Invoke(this, EventArgs.Empty);
        }
    }
}
