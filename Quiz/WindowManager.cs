using System.Windows;

namespace Quiz
{
    public class WindowManager
    {
        private static WindowManager? instance;
        public static WindowManager Instance 
        {
            get
            {
                if (instance == null)
                {
                    instance = new WindowManager();
                }
                return instance;
            }
        }

        private WindowManager() { }

        public void OpenNewWindow(Window to, Window from)
        {
            to.Closing += delegate { from.Show(); };
            to.Show();
            from.Hide();
        }
    }
}
