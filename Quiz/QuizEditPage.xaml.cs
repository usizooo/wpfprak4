using System.Windows;
using System.Windows.Controls;

namespace Quiz
{
    /// <summary>
    /// Логика взаимодействия для QuizEditPage.xaml
    /// </summary>
    public partial class QuizEditPage : Page
    {
        public QuizEditPage()
        {
            InitializeComponent();

            dataGrid.ItemsSource = QuizManager.Instance.Questions;
            dataGrid.IsReadOnly = false;
        }

        private void dataGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            QuizManager.Instance.Serialization();
        }

        private void addQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            QuizManager.Instance.Questions.Add(Question.Empty);
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = QuizManager.Instance.Questions;
        }

        private void removeQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Не выбран элемент на удаление",
                    string.Empty,
                    MessageBoxButton.OK, 
                    MessageBoxImage.Warning);
                return;
            }
            var indx = dataGrid.SelectedIndex;
            QuizManager.Instance.Questions.RemoveAt(indx);
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = QuizManager.Instance.Questions;
        }

        private void dataGrid_Sorting(object sender, DataGridSortingEventArgs e)
        {
            var newQuestionsOrder = new List<Question>();
           
            foreach (var question in dataGrid.Items)
            {
                if (question != null)
                {
                    newQuestionsOrder.Add((Question)question);
                }
                else
                {
                    throw new ArgumentNullException(nameof(question));
                }
            }

            QuizManager.Instance.Questions = newQuestionsOrder;
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = QuizManager.Instance.Questions;
        }
    }
}
