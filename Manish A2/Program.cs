// Here i am defining get and set method
public class Position
{
    public int X { get; set; }
    public int Y { get; set; }

    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }
}

// Here i am defining player public class
public class Player
{
    public string Name { get; }
    public Position Position { get; set; }
    public int GemCount { get; set; }
    public int TurnCount { get; set; }

    public Player(string name, Position position)
    {
        Name = name;
        Position = position;
        GemCount = 0;
        TurnCount = 1; // Start counting from 1
    }

    public void Move(char direction)
    {
        TurnCount++;

        switch (direction)
        {
            case 'U':
                Position.Y--;
                break;
            case 'D':
                Position.Y++;
                break;
            case 'L':
                Position.X--;
                break;
            case 'R':
                Position.X++;
                break;
        }
    }
}

// Here i am defining cell public class
public class Cell
{
    public string Occupant { get; set; }

    public Cell(string occupant = "-")
    {
        Occupant = occupant;
    }
}

public class Board
{
    public Cell[,] Grid { get; }

    public Board()
    {
        Grid = new Cell[6, 6];
        InitializeBoard();
    }

    private void InitializeBoard()
    {
        // Initialize grid with empty cells
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                Grid[i, j] = new Cell();
            }
        }

        // Place players
        Grid[0, 0].Occupant = "P1";
        Grid[5, 5].Occupant = "P2";

        // Place obstacles
        PlaceObstacles();

        // Place gems
        PlaceGems();
    }

    private void PlaceObstacles()
    {
        Random rand = new Random();
        for (int i = 0; i < 6; i++)
        {
            int x = rand.Next(6);
            int y = rand.Next(6);
            while (Grid[y, x].Occupant != "-")
            {
                x = rand.Next(6);
                y = rand.Next(6);
            }
            Grid[y, x].Occupant = "O";
        }
    }

    private void PlaceGems()
    {
        Random rand = new Random();
        for (int i = 0; i < 6; i++)
        {
            int x = rand.Next(6);
            int y = rand.Next(6);
            while (Grid[y, x].Occupant != "-")
            {
                x = rand.Next(6);
                y = rand.Next(6);
            }
            Grid[y, x].Occupant = "G";
        }
    }

    public void Display()
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                Console.Write(Grid[i, j].Occupant + " ");
            }
            Console.WriteLine();
        }
    }

    public bool IsValidMove(Player player, char direction)
    {
        int x = player.Position.X;
        int y = player.Position.Y;

        switch (direction)
        {
            case 'U':
                y--;
                break;
            case 'D':
                y++;
                break;
            case 'L':
                x--;
                break;
            case 'R':
                x++;
                break;
        }

        bool validMove = x >= 0 && x < 6 && y >= 0 && y < 6 && Grid[y, x].Occupant != "O";
        if (!validMove)
        {
            player.TurnCount++; // Count invalid moves as a turn
        }
        return validMove;
    }

    public void CollectGem(Player player)
    {
        int x = player.Position.X;
        int y = player.Position.Y;

        if (Grid[y, x].Occupant == "G")
        {
            player.GemCount++;
            Grid[y, x].Occupant = "-";
        }
    }
}