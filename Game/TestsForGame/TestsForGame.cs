namespace TestsForGame;

using Game;
using System.Linq;

public class Tests
{
    Game game;

    [SetUp]
    public void Setup()
    {
        game = new Game();
    }

    [TestCaseSource(nameof(GameForTest))]
    public void TheIncorrectMapWithIncorrectSymbolShouldThrowException(Game game)
    {
        EventLoop eventLoop = new EventLoop();
        var forTests = new List<((int, int), char)>();
        Assert.Throws<InvalidMapException>(() => eventLoop.Run(game.OnLeft, game.OnRight, game.OnUp, game.OnDown, Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForGame", "IncorrectMapWithIncorrectSymbol.txt"), new PrintInList(), ref forTests));
    }

    [TestCaseSource(nameof(GameForTest))]
    public void TheIncorrectMapWithTooSmallSizeShouldThrowException(Game game)
    {
        EventLoop eventLoop = new EventLoop();
        var forTests = new List<((int, int), char)>();
        Assert.Throws<InvalidMapException>(() => eventLoop.Run(game.OnLeft, game.OnRight, game.OnUp, game.OnDown, Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForGame", "IncorrectMapWithToSmallSize.txt"), new PrintInList(), ref forTests));
    }

    [TestCaseSource(nameof(GameForTest))]
    public void TheCharacterMustWalkCorrectlyToTheLeft(Game game)
    {
        EventLoop eventLoop = new EventLoop();
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
        eventLoop.Run(game.OnLeft, game.OnRight, game.OnUp, game.OnDown,
                      Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForGame", "NormalMap.txt"),
                      printInList, ref forTests, listChar); 
        Assert.True(forTests.SequenceEqual(listCheck));
    }

    [TestCaseSource(nameof(GameForTest))]
    public void TheCharacterMustWalkCorrectlyToTheUp(Game game)
    {
        EventLoop eventLoop = new EventLoop();
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
        eventLoop.Run(game.OnLeft, game.OnRight, game.OnUp, game.OnDown,
                      Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForGame", "NormalMap.txt"),
                      printInList, ref forTests, listChar); 
        Assert.True(forTests.SequenceEqual(listCheck));
    }

    [TestCaseSource(nameof(GameForTest))]
    public void TheCharacterMustWalkCorrectlyToTheRight(Game game)
    {
        EventLoop eventLoop = new EventLoop();
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
        eventLoop.Run(game.OnLeft, game.OnRight, game.OnUp, game.OnDown,
                      Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForGame", "NormalMap.txt"),
                      printInList, ref forTests, listChar);
        Assert.True(forTests.SequenceEqual(listCheck));
    }

    [TestCaseSource(nameof(GameForTest))]
    public void TheCharacterMustWalkCorrectlyToTheDown(Game game)
    {
        EventLoop eventLoop = new EventLoop();
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
        eventLoop.Run(game.OnLeft, game.OnRight, game.OnUp, game.OnDown,
                      Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForGame", "NormalMap.txt"),
                      printInList, ref forTests, listChar); 
        Assert.True(forTests.SequenceEqual(listCheck));
    }

    [TestCaseSource(nameof(GameForTest))]
    public void TheCharacterMustNotWalkIfPressedIncorrectKey(Game game)
    {
        EventLoop eventLoop = new EventLoop();
        var printInList = new PrintInList();
        var forTests = new List<((int, int), char)>();
        var listChar = new List<char>();
        listChar.Add('b');
        var listCheck = new List<((int, int), char)>();
        listCheck.Add(((8, 3), '\0'));
        listCheck.Add(((-1, -1), '@'));
        listCheck.Add(((8, 3), '\0'));
        eventLoop.Run(game.OnLeft, game.OnRight, game.OnUp, game.OnDown,
                      Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForGame", "NormalMap.txt"),
                      printInList, ref forTests, listChar); 
        Assert.True(forTests.SequenceEqual(listCheck));
    }

    [TestCaseSource(nameof(GameForTest))]
    public void TheCharacterMustNotWalkIfItRestsAgainstTheWallWhenWalkingToTheLeft(Game game)
    {
        EventLoop eventLoop = new EventLoop();
        var printInList = new PrintInList();
        var forTests = new List<((int, int), char)>();
        var listChar = new List<char>();
        listChar.Add((char)37);
        var listCheck = new List<((int, int), char)>();
        listCheck.Add(((1, 1), '\0'));
        listCheck.Add(((-1, -1), '@'));
        listCheck.Add(((1, 1), '\0'));
        eventLoop.Run(game.OnLeft, game.OnRight, game.OnUp, game.OnDown,
                      Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForGame", "MapWithCloseWalls.txt"),
                      printInList, ref forTests, listChar);
        Assert.True(forTests.SequenceEqual(listCheck));
    }

    [TestCaseSource(nameof(GameForTest))]
    public void TheCharacterMustNotWalkIfItRestsAgainstTheWallWhenWalkingToTheUp(Game game)
    {
        EventLoop eventLoop = new EventLoop();
        var printInList = new PrintInList();
        var forTests = new List<((int, int), char)>();
        var listChar = new List<char>();
        listChar.Add((char)38);
        var listCheck = new List<((int, int), char)>();
        listCheck.Add(((1, 1), '\0'));
        listCheck.Add(((-1, -1), '@'));
        listCheck.Add(((1, 1), '\0'));
        eventLoop.Run(game.OnLeft, game.OnRight, game.OnUp, game.OnDown,
              Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForGame", "MapWithCloseWalls.txt"),
              printInList, ref forTests, listChar); 
        Assert.True(forTests.SequenceEqual(listCheck));
    }

    [TestCaseSource(nameof(GameForTest))]
    public void TheCharacterMustNotWalkIfItRestsAgainstTheWallWhenWalkingToTheRight(Game game)
    {
        EventLoop eventLoop = new EventLoop();
        var printInList = new PrintInList();
        var forTests = new List<((int, int), char)>();
        var listChar = new List<char>();
        listChar.Add((char)39);
        var listCheck = new List<((int, int), char)>();
        listCheck.Add(((1, 1), '\0'));
        listCheck.Add(((-1, -1), '@'));
        listCheck.Add(((1, 1), '\0'));
        eventLoop.Run(game.OnLeft, game.OnRight, game.OnUp, game.OnDown,
              Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForGame", "MapWithCloseWalls.txt"),
              printInList, ref forTests, listChar); 
        Assert.True(forTests.SequenceEqual(listCheck));
    }


    [TestCaseSource(nameof(GameForTest))]
    public void TheCharacterMustNotWalkIfItRestsAgainstTheWallWhenWalkingToTheDown(Game game)
    {
        EventLoop eventLoop = new EventLoop();
        var printInList = new PrintInList();
        var forTests = new List<((int, int), char)>();
        var listChar = new List<char>();
        listChar.Add((char)40);
        var listCheck = new List<((int, int), char)>();
        listCheck.Add(((1, 1), '\0'));
        listCheck.Add(((-1, -1), '@'));
        listCheck.Add(((1, 1), '\0'));
        eventLoop.Run(game.OnLeft, game.OnRight, game.OnUp, game.OnDown,
              Path.Combine(TestContext.CurrentContext.TestDirectory, "TestsForGame", "MapWithCloseWalls.txt"),
              printInList, ref forTests, listChar); 
        Assert.True(forTests.SequenceEqual(listCheck));
    }

    private static IEnumerable<TestCaseData> GameForTest
    => new TestCaseData[]
    {
        new TestCaseData(new Game())
    };
}