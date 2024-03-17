namespace Quiz
{
    public enum RightAnswerNumber
    {
        First,
        Second,
        Third
    }

    class Question
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string FirstAnswer { get; set; }
        public string SecondAnswer { get; set; }
        public string ThirdAnswer { get; set; }
        public RightAnswerNumber RightAnswer { get; set; }

        public static Question Empty 
            => new Question(
                string.Empty, 
                string.Empty,
                string.Empty, 
                string.Empty, 
                string.Empty,
                RightAnswerNumber.First);

        public Question(
            string name,
            string description,
            string firstAnswer,
            string secondAnswer,
            string thirdAnswer,
            RightAnswerNumber rightAnswer)
        {
            Name = name;
            Description = description;
            FirstAnswer = firstAnswer;
            SecondAnswer = secondAnswer;
            ThirdAnswer = thirdAnswer;
            RightAnswer = rightAnswer;
        }
    }
}
