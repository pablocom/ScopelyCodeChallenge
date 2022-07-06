using ReelWords;
using System.Linq;
using Xunit;

namespace ReelWordsTests
{
    public class GameTests
    { 
        [Fact]
        public void ReflectsAvailableLetterListForInput()
        {
            var expectedLetterFromReel1 = 'a';
            var expectedLetterFromReel2 = 'e';
            var game = new GameBuilder()
                .WithReels(
                    new Reel(new[] { expectedLetterFromReel1, 'b', 'c' }, initialPosition: 1),
                    new Reel(new[] { 'd', expectedLetterFromReel2, 'f' }, initialPosition: 2))
                .Build();

            var lettersAvailable = game.GetAvailableLetters().ToArray();

            Assert.Equal(2, lettersAvailable.Length);
            Assert.Equal(expectedLetterFromReel1, lettersAvailable[0]);
            Assert.Equal(expectedLetterFromReel2, lettersAvailable[1]);
        }

        [Fact]
        public void ScoresWhenUserInputsAValidWord()
        {
            var letterThatScores3Point = 'b';
            var letterThatScores4Point = 'y';
            var expectedScore = 7;
            var game = new GameBuilder()
                .WithValidWords("by")
                .WithReels(
                    new Reel(new[] { letterThatScores3Point }, 1),
                    new Reel(new[] { letterThatScores4Point }, 1))
                .Build();

            var result = game.SubmitWord("by");

            Assert.True(result.IsSuccessful);
            Assert.Equal(expectedScore, result.Score);
            Assert.Equal(expectedScore, game.TotalScore);
        }

        [Fact]
        public void ScoresWhenUserInputsAValidWordUsingTwoTimesTheSameLetterFromDifferentReels()
        {
            var letterThatScores3Points = 'b';
            var expectedScore = 6;
            var game = new GameBuilder()
                .WithValidWords("bb")
                .WithReels(
                    new Reel(new[] { letterThatScores3Points }, 1),
                    new Reel(new[] { letterThatScores3Points }, 1))
                .Build();

            var result = game.SubmitWord("bb");

            Assert.True(result.IsSuccessful);
            Assert.Equal(expectedScore, result.Score);
        }

        [Fact]
        public void SumsScoreOfMultipleSubmissions()
        {
            var letterThatScores3Points = 'b';
            var letterThatScores1Point = 'a';

            var game = new GameBuilder()
                .WithValidWords("b", "a")
                .WithReels(
                    new Reel(new[] { letterThatScores3Points }, 1), 
                    new Reel(new[] { letterThatScores1Point }, 1))
                .Build();

            game.SubmitWord("b");
            game.SubmitWord("a");

            Assert.Equal(4, game.TotalScore);
        }

        [Fact]
        public void MovesUsedReelsToTheNextLetter()
        {
            var game = new GameBuilder()
                .WithValidWords("az")
                .WithReels(
                    new Reel(new[] { 'a', 'b', 'c' }, initialPosition: 1),
                    new Reel(new[] { 'd', 'e', 'f' }, initialPosition: 2),
                    new Reel(new[] { 'x', 'y', 'z' }, initialPosition: 3))
                .Build();

            var result = game.SubmitWord("az");

            var availableLetters = game.GetAvailableLetters().ToArray();
            Assert.True(result.IsSuccessful);
            Assert.Equal(3, availableLetters.Length);
            Assert.Equal('b', availableLetters[0]);
            Assert.Equal('e', availableLetters[1]);
            Assert.Equal('x', availableLetters[2]);
        }

        [Fact]
        public void DoesNotMoveUsedReelsToNextLetterIfSubmissionFailed()
        {
            var game = new GameBuilder()
                .WithValidWords("nonmachingword")
                .WithReels(
                    new Reel(new[] { 'a', 'b', 'c' }, initialPosition: 1),
                    new Reel(new[] { 'd', 'e', 'f' }, initialPosition: 2),
                    new Reel(new[] { 'x', 'y', 'z' }, initialPosition: 3))
                .Build();

            var result = game.SubmitWord("ae");

            var availableLetters = game.GetAvailableLetters().ToArray();
            Assert.False(result.IsSuccessful);
            Assert.Equal(3, availableLetters.Length);
            Assert.Equal('a', availableLetters[0]);
            Assert.Equal('e', availableLetters[1]);
            Assert.Equal('z', availableLetters[2]);
        }

        [Fact]
        public void FailsWhenInputWordHasLettersNotPresentInReels()
        {
            var game = new GameBuilder()
                .WithValidWords("by")
                .WithReels(new Reel[0])
                .Build();

            var result = game.SubmitWord("by");

            Assert.False(result.IsSuccessful);
            Assert.Equal("Input cannot contain more letters than letters available in reels", result.Message);
        }

        [Fact]
        public void FailsWhenWordUsesMoreTimesThanAvailableTheSameCharacter()
        {
            var game = new GameBuilder()
                .WithValidWords("bbb")
                .WithReels(
                    new Reel(new[] { 'b' }, 1),
                    new Reel(new[] { 'b' }, 1))
                .Build();

            var result = game.SubmitWord("bbb");

            Assert.False(result.IsSuccessful);
            Assert.Equal("Input cannot contain more letters than letters available in reels", result.Message);
        }

        [Fact]
        public void FailsWhenWordHasMoreLettersThanAvailableInReels()
        {
            var game = new GameBuilder()
                .WithValidWords("bye")
                .WithReels(
                    new Reel(new[] { 'b' }, 1),
                    new Reel(new[] { 'y' }, 1))
                .Build();

            var result = game.SubmitWord("bye");

            Assert.False(result.IsSuccessful);
            Assert.Equal("Input cannot contain more letters than letters available in reels", result.Message);
        }

        [Fact]
        public void FailsWhenInputWordIsNotConsideredValid()
        {
            var game = new GameBuilder()
                .WithValidWords("hello")
                .WithReels(
                    new Reel(new[] { 'b' }, 1),
                    new Reel(new[] { 'y' }, 1),
                    new Reel(new[] { 'e' }, 1))
                .Build();

            var result = game.SubmitWord("bye");

            Assert.False(result.IsSuccessful);
            Assert.Equal("Input word 'bye' is not valid", result.Message);
        }
    }
}
