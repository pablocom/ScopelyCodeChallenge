namespace ReelWords;

public class Game
{
    public int TotalScore { get; private set; }

    private readonly Trie _validWords;
    private readonly Reels _reels;

    public Game(Trie validWords, Reels reels)
    {
        _validWords = validWords;
        _reels = reels;
    }

    public WordSubmissionResult SubmitWord(string word)
    {
        if (!_reels.AvailableLettersCanForm(word))
            return WordSubmissionResult.Fail("Input cannot contain more letters than letters available in reels");

        if (!_validWords.Search(word))
            return WordSubmissionResult.Fail($"Input word '{word}' is not valid");

        _reels.MoveReelsUsedToForm(word);

        var scoredPoints = word.Sum(LetterScores.GetScore);
        TotalScore += scoredPoints;
        return WordSubmissionResult.Success(word, scoredPoints);
    }

    public IEnumerable<char> GetAvailableLetters() => _reels.AvailableLetters;
}
