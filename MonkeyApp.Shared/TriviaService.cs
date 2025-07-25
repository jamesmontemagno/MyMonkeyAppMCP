namespace MonkeyApp.Shared
{
    public class TriviaQuestion
    {
        public string Question { get; set; } = string.Empty;
        public List<string> Options { get; set; } = new();
        public int CorrectAnswerIndex { get; set; }
        public string Explanation { get; set; } = string.Empty;
    }

    public static class TriviaService
    {
        private static readonly Random _random = new();
        
        public static List<TriviaQuestion> GetTriviaQuestions()
        {
            return new List<TriviaQuestion>
            {
                new TriviaQuestion
                {
                    Question = "Which monkey is known for bathing in hot springs?",
                    Options = new List<string> { "Baboon", "Japanese Macaque", "Capuchin", "Howler Monkey" },
                    CorrectAnswerIndex = 1,
                    Explanation = "Japanese macaques are famous for bathing in hot springs, especially during winter."
                },
                new TriviaQuestion
                {
                    Question = "How far can a Howler Monkey's call be heard?",
                    Options = new List<string> { "1 mile", "2 miles", "3 miles", "5 miles" },
                    CorrectAnswerIndex = 2,
                    Explanation = "Howler monkeys have one of the loudest calls in the animal kingdom, audible up to 3 miles away."
                },
                new TriviaQuestion
                {
                    Question = "Which monkey species is endemic to Borneo?",
                    Options = new List<string> { "Proboscis Monkey", "Mandrill", "Blue Monkey", "Golden Lion Tamarin" },
                    CorrectAnswerIndex = 0,
                    Explanation = "The Proboscis monkey is endemic to the island of Borneo and known for its distinctive large nose."
                },
                new TriviaQuestion
                {
                    Question = "What is a group of monkeys called?",
                    Options = new List<string> { "Pack", "Herd", "Troop", "Flock" },
                    CorrectAnswerIndex = 2,
                    Explanation = "A group of monkeys is called a 'troop', though they can also be called a 'barrel'."
                },
                new TriviaQuestion
                {
                    Question = "Which monkey species doesn't have thumbs?",
                    Options = new List<string> { "Capuchin", "Spider Monkey", "Baboon", "Macaque" },
                    CorrectAnswerIndex = 1,
                    Explanation = "Spider monkeys lack thumbs, which makes them more agile when swinging through trees."
                },
                new TriviaQuestion
                {
                    Question = "Where are Golden Lion Tamarins primarily found?",
                    Options = new List<string> { "Africa", "Brazil", "Japan", "Borneo" },
                    CorrectAnswerIndex = 1,
                    Explanation = "Golden Lion Tamarins are found in Brazil and are known for their beautiful golden mane."
                }
            };
        }

        public static TriviaQuestion GetRandomQuestion()
        {
            var questions = GetTriviaQuestions();
            return questions[_random.Next(questions.Count)];
        }
    }
}