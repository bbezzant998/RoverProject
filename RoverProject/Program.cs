using System;

namespace RoverProject
{

  class Program
  {
    public const int SIZE_ARGS = 2;
    public const int POSITION_ARGS = 3;

    public static Rover theRover;
    public static int xSize, ySize;

    /// <summary>
    /// Prompt user for the values of the grid size and the position and heading of the rover.
    ///
    /// Assumptions:
    /// User can enter incorrect data.
    /// Handle missing arguments.
    /// Handle letters being entered instead of numbers.
    /// Handle negative numbers. Negative numbers will be converted to positive numbers.
    /// User can only enter N, E, S, W for heading. Lowercase will be converted to uppercase.
    /// Handle user entering in a position that is outside the grid size.  
    /// </summary>
    static void Main(string[] args)
    {
      string[] xySize;
      string[] xyPosition;
      bool invalidData;

      do
      {
        invalidData = false;
        Console.WriteLine("Please enter x and y for grid size: ");
        xySize = Console.ReadLine().Split(' ');
        Console.WriteLine("Please enter the x y position and heading of the rover:");
        xyPosition = Console.ReadLine().Split(' ');
        try
        {
          ParseData(xySize, xyPosition);
        }
        catch (Exception e)
        {
          Console.WriteLine(e.Message);
          invalidData = true;
        }
      } while (invalidData);

      EnterCommands();
    }

    /// <summary>
    /// Prompt user to enter in a command stream to move the rover around.
    /// Allow user to enter in values after execution. They can quit by typing q. 
    ///
    /// Assumptions:
    /// User will enter in other values other than L, R, M. Ingore all other data values.
    /// Lowercase values will be converted to uppercase.
    /// Handle empty command stream. Set command stream to an empty string if no values are entered. 
    /// </summary>
    public static void EnterCommands()
    {
      string commandStream;
      do
      {
        Console.WriteLine("Enter in a stream of commands for the Rover to execute (q to quit):");
        commandStream = Console.ReadLine();
        commandStream = commandStream.Length > 0 ? commandStream.ToUpper() : "";
        theRover.ExecuteOrder(commandStream);
        Console.WriteLine("{0} {1} {2}", theRover.XPos, theRover.YPos, theRover.Direction.ToString()[0]);
      } while (commandStream.Length > 0 ? char.ToLower(commandStream[0]) != 'q' : true);

    }

    /// <summary>
    ///Parse string data to integer values.
    ///Build the rover. Setting its position and heading
    /// 
    /// Assumptions:
    /// Handle negative values.
    /// Make sure positions are not greater than grid boundaries.
    /// 
    /// </summary>
    public static void ParseData(string[] xySize, string[] xyPosition)
    {
      if (xySize.Length == SIZE_ARGS && xyPosition.Length == POSITION_ARGS)
      {
        int xPos, yPos;
        try
        {
          xPos = Math.Abs(int.Parse(xyPosition[0]));
          yPos = Math.Abs(int.Parse(xyPosition[1]));
          xSize = Math.Abs(int.Parse(xySize[0]));
          ySize = Math.Abs(int.Parse(xySize[1]));

          if (xPos > xSize || yPos > ySize)
          {
            throw new ArgumentOutOfRangeException();
          }

          theRover = new Rover(xPos, yPos, xSize, ySize, xyPosition[2]);
        }
        catch (Exception e)
        {
          throw new FormatException(e.Message);
        }
      }
      else
      {
        throw new Exception("Missing data arguments for the setup. Check to make sure all data is entered correctly.");
      }
    }
  }
}
