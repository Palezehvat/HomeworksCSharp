namespace TestsForGame;

using Game;
using System.Linq;

public class Tests
{
    [Test]
    public void TheIncorrectMapWithIncorrectSymbolShouldThrowException()
    {
        var forTests = new List<((int, int), char)>();
        Assert.Throws<InvalidMapException>(() => EventLoop.Run(Game.OnLeft, Game.OnRight, Game.OnUp, Game.OnDown, Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForGame", "IncorrectMapWithIncorrectSymbol.txt"), new PrintInList(), ref forTests));
    }

    [Test]
    public void TheIncorrectMapWithTooSmallSizeShouldThrowException()
    {
        var forTests = new List<((int, int), char)>();
        Assert.Throws<InvalidMapException>(() => EventLoop.Run(Game.OnLeft, Game.OnRight, Game.OnUp, Game.OnDown, Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForGame", "IncorrectMapWithToSmallSize.txt"), new PrintInList(), ref forTests));
    }

    [Test]
    public void TheCharacterMustWalkCorrectlyToTheLeft()
    {
        var printInList = new PrintInList();
        var forTests = new List<((int, int), char)>();
        var listChar = new List<char>();
        listChar.Add((char)37);
        var listCheck = new List<((int, int), char)>();
        listCheck.Add(((8, 3), '\0'));
        listCheck.Add(((-1, -1), '@'));
        listCheck.Add(((8, 3), '\0'));
        listCheck.Add(((-1, -1), ' '));
        listCheck.Add(((7, 3), '\0'));
        listCheck.Add(((-1, -1), '@'));
        listCheck.Add(((7, 3), '\0'));
        EventLoop.Run(Game.OnLeft, Game.OnRight, Game.OnUp, Game.OnDown,
                      Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForGame", "NormalMap.txt"),
                      printInList, ref forTests, listChar); 
        Assert.True(forTests.SequenceEqual(listCheck));
    }

    [Test]
    public void TheCharacterMustWalkCorrectlyToTheUp()
    {
        var printInList = new PrintInList();
        var forTests = new List<((int, int), char)>();
        var listChar = new List<char>();
        listChar.Add((char)38);
        var listCheck = new List<((int, int), char)>();
        listCheck.Add(((8, 3), '\0'));
        listCheck.Add(((-1, -1), '@'));
        listCheck.Add(((8, 3), '\0'));
        listCheck.Add(((-1, -1), ' '));
        listCheck.Add(((8, 2), '\0'));
        listCheck.Add(((-1, -1), '@'));
        listCheck.Add(((8, 2), '\0'));
        EventLoop.Run(Game.OnLeft, Game.OnRight, Game.OnUp, Game.OnDown,
                      Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForGame", "NormalMap.txt"),
                      printInList, ref forTests, listChar); 
        Assert.True(forTests.SequenceEqual(listCheck));
    }

    [Test]
    public void TheCharacterMustWalkCorrectlyToTheRight()
    {
        var printInList = new PrintInList();
        var forTests = new List<((int, int), char)>();
        var listChar = new List<char>();
        listChar.Add((char)39);
        var listCheck = new List<((int, int), char)>();
        listCheck.Add(((8, 3), '\0'));
        listCheck.Add(((-1, -1), '@'));
        listCheck.Add(((8, 3), '\0'));
        listCheck.Add(((-1, -1), ' '));
        listCheck.Add(((9, 3), '\0'));
        listCheck.Add(((-1, -1), '@'));
        listCheck.Add(((9, 3), '\0'));
        EventLoop.Run(Game.OnLeft, Game.OnRight, Game.OnUp, Game.OnDown,
                      Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForGame", "NormalMap.txt"),
                      printInList, ref forTests, listChar);
        Assert.True(forTests.SequenceEqual(listCheck));
    }

    [Test]
    public void TheCharacterMustWalkCorrectlyToTheDown()
    {
        var printInList = new PrintInList();
        var forTests = new List<((int, int), char)>();
        var listChar = new List<char>();
        listChar.Add((char)40);
        var listCheck = new List<((int, int), char)>();
        listCheck.Add(((8, 3), '\0'));
        listCheck.Add(((-1, -1), '@'));
        listCheck.Add(((8, 3), '\0'));
        listCheck.Add(((-1, -1), ' '));
        listCheck.Add(((8, 4), '\0'));
        listCheck.Add(((-1, -1), '@'));
        listCheck.Add(((8, 4), '\0'));
        EventLoop.Run(Game.OnLeft, Game.OnRight, Game.OnUp, Game.OnDown,
                      Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForGame", "NormalMap.txt"),
                      printInList, ref forTests, listChar); 
        Assert.True(forTests.SequenceEqual(listCheck));
    }

    [Test]
    public void TheCharacterMustNotWalkIfPressedIncorrectKey()
    {
        var printInList = new PrintInList();
        var forTests = new List<((int, int), char)>();
        var listChar = new List<char>();
        listChar.Add('b');
        var listCheck = new List<((int, int), char)>();
        listCheck.Add(((8, 3), '\0'));
        listCheck.Add(((-1, -1), '@'));
        listCheck.Add(((8, 3), '\0'));
        EventLoop.Run(Game.OnLeft, Game.OnRight, Game.OnUp, Game.OnDown,
                      Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForGame", "NormalMap.txt"),
                      printInList, ref forTests, listChar); 
        Assert.True(forTests.SequenceEqual(listCheck));
    }

    [Test]
    public void TheCharacterMustNotWalkIfItRestsAgainstTheWallWhenWalkingToTheLeft()
    {
        var printInList = new PrintInList();
        var forTests = new List<((int, int), char)>();
        var listChar = new List<char>();
        listChar.Add((char)37);
        var listCheck = new List<((int, int), char)>();
        listCheck.Add(((1, 1), '\0'));
        listCheck.Add(((-1, -1), '@'));
        listCheck.Add(((1, 1), '\0'));
        EventLoop.Run(Game.OnLeft, Game.OnRight, Game.OnUp, Game.OnDown,
                      Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForGame", "MapWithCloseWalls.txt"),
                      printInList, ref forTests, listChar);
        Assert.True(forTests.SequenceEqual(listCheck));
    }

    [Test]
    public void TheCharacterMustNotWalkIfItRestsAgainstTheWallWhenWalkingToTheUp()
    {
        var printInList = new PrintInList();
        var forTests = new List<((int, int), char)>();
        var listChar = new List<char>();
        listChar.Add((char)38);
        var listCheck = new List<((int, int), char)>();
        listCheck.Add(((1, 1), '\0'));
        listCheck.Add(((-1, -1), '@'));
        listCheck.Add(((1, 1), '\0'));
        EventLoop.Run(Game.OnLeft, Game.OnRight, Game.OnUp, Game.OnDown,
              Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForGame", "MapWithCloseWalls.txt"),
              printInList, ref forTests, listChar); 
        Assert.True(forTests.SequenceEqual(listCheck));
    }

    [Test]
    public void TheCharacterMustNotWalkIfItRestsAgainstTheWallWhenWalkingToTheRight()
    {
        var printInList = new PrintInList();
        var forTests = new List<((int, int), char)>();
        var listChar = new List<char>();
        listChar.Add((char)39);
        var listCheck = new List<((int, int), char)>();
        listCheck.Add(((1, 1), '\0'));
        listCheck.Add(((-1, -1), '@'));
        listCheck.Add(((1, 1), '\0'));
        EventLoop.Run(Game.OnLeft, Game.OnRight, Game.OnUp, Game.OnDown,
              Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForGame", "MapWithCloseWalls.txt"),
              printInList, ref forTests, listChar); 
        Assert.True(forTests.SequenceEqual(listCheck));
    }


    [Test]
    public void TheCharacterMustNotWalkIfItRestsAgainstTheWallWhenWalkingToTheDown()
    {
        var printInList = new PrintInList();
        var forTests = new List<((int, int), char)>();
        var listChar = new List<char>();
        listChar.Add((char)40);
        var listCheck = new List<((int, int), char)>();
        listCheck.Add(((1, 1), '\0'));
        listCheck.Add(((-1, -1), '@'));
        listCheck.Add(((1, 1), '\0'));
        EventLoop.Run(Game.OnLeft, Game.OnRight, Game.OnUp, Game.OnDown,
              Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForGame", "MapWithCloseWalls.txt"),
              printInList, ref forTests, listChar); 
        Assert.True(forTests.SequenceEqual(listCheck));
    }
}