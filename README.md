# Reels Implementation

## Tests

I've looked for the observable behaviour, trying to not reveal the internal implementation. That's why there is no particular tests looking for the nodes that are created when inserting a word into the `Trie` data structure, and I applied the same approach for the `Game` class as well.

## Trie implementation

For fast indexing, search and delete I tried to make `Trie` as performant as possible, trying to make it's operations time and space complexity optimal.

## Game objects design

I tried to follow Object-Oriented Programming principles by leveraging encapsulation of behaviour, creating speific classes to represent important concepts for the subject. I've considered to make `Reel` class immutable.

## Error handling

I have taken the privilege of ignoring almost any error handling or safety code (argument null checks, etc) in the majority of cases, there's several places in the challenge that I could check for null arguments. That'd have increased the number of lines. Also the number of tests. If you consider this as a negative point I can add them. 
