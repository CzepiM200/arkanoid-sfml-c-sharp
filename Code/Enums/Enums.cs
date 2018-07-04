namespace ArkanoidCS
{
    enum GameStatus
    {
        MENU = 0,
        SHOP = 1,
        OPTIONS = 2,
        GAME = 3,
        EXIT = 4,
        GAMESUMATION = 5,
        STATS = 6,
        CHOSERACKET = 7
    };

    enum GameSolution
    {
        LOSE = 0,
        WIN = 1
    };

    enum Rackets
    {
        BASIC = 0,
        FAST = 1,
        STRONG = 2,
        LONG = 3,
        LUCKY = 4,
        MASTER = 5
    };

    enum Blocks
    {
        GREEN = 0,
        BLUE = 1,
        RED = 2,
        YELLOW = 3
    }
}