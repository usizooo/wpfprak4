using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Quiz
{
    /// <summary>
    /// Логика взаимодействия для EditPage.xaml
    /// </summary>
    public partial class EditPage : Page
    {
        private readonly Frame mainFrame;
        public EditPage(Frame mainFrame)
        {
            InitializeComponent();
            this.mainFrame = mainFrame;
        }

        private void goToEditQuizButton_Click(object sender, RoutedEventArgs e)
        {
            if (passwordTextBox.Text != QuizManager.Instance.ForEditQuizPassword)
            {
                MessageBox.Show(
                    "Неверный пароль", 
                    string.Empty, 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Information);
                return;
            }

            // Получаем родительский элемент Frame
            FrameworkElement? parent = mainFrame.Parent as FrameworkElement;

            // Переходим к родительским элементам, пока не дойдем до окна или пока не достигнем корневого элемента
            while (parent != null && parent is not Window)
            {
                parent = parent.Parent as FrameworkElement;
            }

            // Проверяем, найдено ли окно
            if (parent is Window)
            {
                Window? window = parent as Window;
                if (window != null)
                {
                    // Вы можете использовать window здесь
                    if (mainFrame.CanGoBack)
                    {
                        mainFrame.GoBack();
                    }
                    WindowManager.Instance.OpenNewWindow(new QuizWindow(new QuizEditPage()), window);
                }
            }            
        }

        private void passwordTextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            passwordTextBox.Text = "";
            passwordTextBox.Foreground = new SolidColorBrush(Color.FromRgb(0, 72, 66));
        }

        private void passwordTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (passwordTextBox.Text.Length == 0)
                {
                    passwordTextBox.Text = "Введите пароль админа";
                    passwordTextBox.Foreground = Brushes.Teal;
                }
                Keyboard.ClearFocus();
                e.Handled = true; // Чтобы предотвратить дальнейшую обработку клавиши Enter
            }
        }
    }
}
