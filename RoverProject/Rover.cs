using System;

namespace RoverProject
{
  /// <summary>
  /// Use a heading enum for direction instead of using magic numbers. 
  /// </summary>
  enum Heading
  {
    North = 0,
    East = 1,
    South = 2,
    West = 3
  }
  class Rover
  {
    public int XPos { get; set; }
    public int YPos { get; set; }
    public int XBoundary { get; set; }
    public int YBoundary { get; set; }
    public Heading Direction { get; set; }
    public static int numOfHeadings = Enum.GetNames(typeof(Heading)).Length;

    public Rover(int xPos, int yPos, int xSize, int ySize, string direction)
    {
      this.XPos = xPos;
      this.YPos = yPos;
      XBoundary = xSize;
      YBoundary = ySize;
      this.Direction = CalculateHeading(direction);
    }

    /// <summary>
    /// Used to convert the string value of direction to the correct enum heading.
    ///
    /// Assumptions:
    /// Only allow the user to enter N, E, S, W for the direction.
    /// Throw error prompting user on what the correct heading values are. 
    /// </summary>
    public Heading CalculateHeading(string direction)
    {
      direction = direction.ToUpper();
      switch (direction)
      {
        case "N":
          return Heading.North;
        case "E":
          return Heading.East;
        case "S":
          return Heading.South;
        case "W":
          return Heading.West;
        default:
          throw new ArgumentException("Heading must be N, E, S or W");
      }
    }

    /// <summary>
    /// Turn the rover left or right depending on the turnDirection. Right adds, left subtracts
    /// 
    /// Assumptions:
    /// Handle negative direction. If turning makes the direction negative then it has gone from North to West.
    /// Handle values greater than number of directions. Moding direction after adding or subtracting a value will
    /// keep the direction value within the values of the heading enum. 
    /// </summary>
    public void Turn(char turnDirection)
    {
      int direction = (int)this.Direction;
      direction += turnDirection == 'R' ? 1 : -1;
      direction = direction < 0 ? (int)Heading.West : direction % numOfHeadings;
      this.Direction = (Heading)direction;
    }

    /// <summary>
    /// Move the rover and upadte its position depending on the direction.
    ///
    /// Assumptions:
    /// Handle movement that will move the rover outside the boundaries.
    /// Throw an exeception with the current heading of the rover. 
    /// </summary>
    public void Move()
    {
      try
      {
        switch (Direction)
        {
          case Heading.North:
            if (YPos + 1 > YBoundary) throw new ArgumentOutOfRangeException("N");
            YPos++;
            break;
          case Heading.East:
            if (XPos + 1 > XBoundary) throw new ArgumentOutOfRangeException("E");
            XPos++;
            break;
          case Heading.South:
            if (YPos - 1 < 0) throw new ArgumentOutOfRangeException("S");
            YPos--;
            break;
          case Heading.West:
            if (XPos - 1 < 0) throw new ArgumentOutOfRangeException("W");
            XPos--;
            break;
        }
      }
      catch (ArgumentOutOfRangeException e)
      {
        Console.WriteLine("\nTrying to head {0}", e.ParamName);
        Console.WriteLine("\nWARNING!!! Heading this direction, while at the rover's current position, will cause the rover to fall off the cliff and explode. Costing your company Millions of Dollars!!!");
        Console.WriteLine("\nFor your job security, command was not executed.");
      }
    }

    /// <summary>
    /// Go through the command stream entered by the user and execute the orders.
    ///
    /// Assumption:
    /// Ignore all other values that are not L, R, M.
    /// </summary>
    public void ExecuteOrder(string orders)
    {
      foreach (var order in orders)
      {
        if (order == 'R' || order == 'L')
        {
          Turn(order);
        }
        else if (order == 'M')
        {
          Move();
        }
      }

    }

  }
}
