namespace ReelWords
{
    public class WordSubmissionResult
    {
        public bool IsSuccessful { get; }
        public int Score { get; }
        public string Message { get; }

        private WordSubmissionResult(int score, bool isSuccessful, string message)
        {
            Score = score;
            IsSuccessful = isSuccessful;
            Message = message;
        }

        public static WordSubmissionResult Success(string word, int score)
        {
            return new WordSubmissionResult(score, true, $"The word '{word}' is valid, {score} points scored");
        }

        public static WordSubmissionResult Fail(string message)
        {
            return new WordSubmissionResult(0, false, message);
        }
    }
}
