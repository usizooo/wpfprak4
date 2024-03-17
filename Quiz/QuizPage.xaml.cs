using System.Windows.Controls;

namespace Quiz
{
    /// <summary>
    /// Логика взаимодействия для QuizPage.xaml
    /// </summary>
    public partial class QuizPage : Page
    {
        private Frame mainFrame;
        public QuizPage(Frame mainFrame)
        {
            InitializeComponent();
            QuizManager.Instance.AnswerSessionStarted?.Invoke(this, EventArgs.Empty);
            UpdateQuestionInfo();
            this.mainFrame = mainFrame;
        }

        public void UpdateQuestionInfo()
        {
            if (QuizManager.Instance.CurrentQuestion == null)
            {
                throw new ArgumentNullException(nameof(QuizManager.Instance.CurrentQuestion));
            }
            if (QuizManager.Instance.CurrentQuestion == QuizManager.Instance.Questions.Count)
            {
                mainFrame.Content = new QuizResultsPage();
                return;
            }

            var question = QuizManager.Instance.Questions[(int)QuizManager.Instance.CurrentQuestion];
            questionNameLabel.Content = question.Name;
            questionDescriptionLabel.Content = question.Description;
            answerStatusLabel.Content = string.Empty;
            firstAnswerButton.Content = question.FirstAnswer;
            secondAnswerButton.Content = question.SecondAnswer;
            thirdAnswerButton.Content = question.ThirdAnswer;
        }

        private void firstAnswerButton_Click(object sender, System.Windows.RoutedEventArgs e)
            => AnswerProcessing(RightAnswerNumber.First);

        private void secondAnswerButton_Click(object sender, System.Windows.RoutedEventArgs e)
            => AnswerProcessing(RightAnswerNumber.Second);

        private void thirdAnswerButton_Click(object sender, System.Windows.RoutedEventArgs e)
            => AnswerProcessing(RightAnswerNumber.Third);

        private async void AnswerProcessing(RightAnswerNumber answerNumber)
        {
            if (QuizManager.Instance.CurrentQuestion == null)
            {
                throw new ArgumentNullException(nameof(QuizManager.Instance.CurrentQuestion));
            }
            
            answerStatusLabel.Content = 
                QuizManager
                    .Instance
                    .Questions[(int)QuizManager.Instance.CurrentQuestion]
                    .RightAnswer == answerNumber
                ? "Правильный ответ"
                : "Неправильный ответ";

            AnswerButtonHitTestVisibleState(false);
            await Task.Delay(1000);
            AnswerButtonHitTestVisibleState(true);

            QuizManager.Instance.AnswerWasDone?.Invoke(this, new AnswerArgs(answerNumber));
            UpdateQuestionInfo();
        }

        private void AnswerButtonHitTestVisibleState(bool isHitTestVisible)
        {
            firstAnswerButton.IsHitTestVisible = isHitTestVisible;
            secondAnswerButton.IsHitTestVisible = isHitTestVisible;
            thirdAnswerButton.IsHitTestVisible = isHitTestVisible;
        }
    }
}