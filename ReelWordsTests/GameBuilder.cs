using ReelWords;
using System;

namespace ReelWordsTests;

public class GameBuilder
{
    private string[] validWords = Array.Empty<string>();
    private Reel[] reels = Array.Empty<Reel>();

    public Game Build()
    {
        var trie = new Trie();
        foreach (var word in validWords)
        {
            trie.Insert(word);
        }

        return new Game(trie, new Reels(reels));
    }

    public GameBuilder WithValidWords(params string[] words)
    {
        validWords = words;
        return this;
    }

    public GameBuilder WithReels(params Reel[] reels)
    {
        this.reels = reels;
        return this;
    }
}
