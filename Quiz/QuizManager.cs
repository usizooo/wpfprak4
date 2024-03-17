using Newtonsoft.Json;
using System.IO;

namespace Quiz
{
    class AnswerArgs : EventArgs
    {
        public readonly RightAnswerNumber AnswerNumber;
        public AnswerArgs(RightAnswerNumber answerNumber) => AnswerNumber = answerNumber;
    }

    class QuizManager
    {
        public List<Question> Questions { get; set; }
        public int? CurrentQuestion { get; private set; }
        public int? CountRightAnswer { get; private set; }

        public string ForEditQuizPassword => "admin";

        public EventHandler AnswerSessionStarted;
        public EventHandler AnswerSessionFinished;
        public EventHandler<AnswerArgs> AnswerWasDone;

        private string quizDataJsonPath = "quizData.json";

        private static QuizManager? instance;
        public static QuizManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new QuizManager();
                }
                return instance;
            }
        }

        private QuizManager() 
        {
            CurrentQuestion = null;
            CountRightAnswer = null;
            //Questions = new List<Question>()
            //{
            //    new Question(
            //        "2 + 2 = ?",
            //        "Посчитай сколько будет??",
            //        "3", "4", "5",
            //        RightAnswerNumber.Second),
            //    new Question(
            //        "Вопрос про Голландию",
            //        "Назови столицу Нидерландов",
            //        "Амстердам", "Лондон", "Рига",
            //        RightAnswerNumber.First),
            //    new Question(
            //        "А что на небе?",
            //        "Сколько созвездий на небосводе",
            //        "57", "94", "88",
            //        RightAnswerNumber.Third),
            //};
            Questions = Deserialization();

            AnswerWasDone += QuizManager_AnswerWasDone;
            AnswerSessionStarted += QuizManager_AnswerSessionStarted;
            AnswerSessionFinished += QuizManager_AnswerSessionFinished;
        }

        private void QuizManager_AnswerSessionFinished(object? sender, EventArgs e)
        {
            CurrentQuestion = null;
            CountRightAnswer = null;
        }

        private void QuizManager_AnswerSessionStarted(object? sender, EventArgs e)
        {
            CurrentQuestion = 0;
            CountRightAnswer = 0;
        }

        private void QuizManager_AnswerWasDone(object? sender, AnswerArgs e)
        {
            if (CurrentQuestion == null)
            {
                throw new ArgumentNullException(nameof(CurrentQuestion));
            }

            CountRightAnswer = e.AnswerNumber == Questions[(int)CurrentQuestion].RightAnswer
                ? CountRightAnswer + 1
                : CountRightAnswer;

            CurrentQuestion++;
        }

        public void Serialization()
        {
            using (StreamWriter streamWriter = new StreamWriter(quizDataJsonPath))
            {
                streamWriter.Write(JsonConvert.SerializeObject(Questions));
            }
        }

        private List<Question> Deserialization()
        {
            using (StreamReader streamReader = new StreamReader(quizDataJsonPath))
            {
                string jsonData = streamReader.ReadToEnd();
                if (jsonData == string.Empty)
                {
                    throw new NullReferenceException("Файл пуст.");
                }
                return JsonConvert.DeserializeObject<List<Question>>(jsonData)
                    ?? throw new NullReferenceException("Данные файла повреждены повреждены.");
            }
        }
    }
}
