using System;
using System.Linq;

namespace RobotNavigation.NaivgationApp;

public class MarsNavigation
{
    public string FinalPosition
    {
        get
        {
            return _finalPosition;
        }
    }

    private string _finalPosition;

    private readonly char[] _commands;
    private readonly int _gridX;
    private readonly int _gridY;
    private readonly char[] validCommands = new[] { 'F', 'R', 'L' };

    // Initial position - always 1, 1
    private int robotGridX = 1;
    private int robotGridY = 1;

    // Initial direction - always North
    private Direction robotDirection = Direction.North;

    private enum Direction
    {
        East,
        West,
        North,
        South,
    }

    private enum CommandDirection
    {
        Left,
        Right,
        Forward,
    }

    public MarsNavigation(string gridPositions, string command)
    {
        var gridPos = gridPositions.Split('x');
        if (!gridPositions.Contains('x') || gridPos.Length < 2)
        {
            throw new ArgumentException("Incorrect input");
        }

        _gridX = int.Parse(gridPos[0]);
        _gridY = int.Parse(gridPos[1]);

        var commandArray = command.ToCharArray();
        var validSet = commandArray.Distinct().ToArray();
        var result = validSet.Except(validCommands);

        if (result.Any())
        {
            throw new ArgumentException("Invalid command");
        }

        _commands = commandArray;
    }

    public void NavigateRobot()
    {
        foreach (var command in _commands)
        {
            CommandDirection commandDirection = GetCommandDirection(command);

            switch (commandDirection)
            {
                case CommandDirection.Forward:
                        UpdateRobotPosition();
                    break;

                case CommandDirection.Left:
                    robotDirection = CalculateLeftFacingDirection(robotDirection);
                    break;

                case CommandDirection.Right:
                    robotDirection = CalculateRightFacingDirection(robotDirection);
                    break;
            }
        }

        _finalPosition = $"{robotGridX},{robotGridY},{robotDirection}";
    }


    private void UpdateRobotPosition()
    {
        switch (robotDirection)
        {
            case Direction.East:
                if (robotGridX < _gridX)
                {
                    robotGridX += 1;
                }
                break;

            case Direction.West:
                if (robotGridX > 1)
                {
                    robotGridX -= 1;
                }
                break;

            case Direction.North:
                if (robotGridY < _gridY)
                {
                    robotGridY += 1;
                }
                break;

            case Direction.South:
                if (robotGridY > 1)
                {
                    robotGridY -= 1;
                }
                break;
        }
    }

    private static CommandDirection GetCommandDirection(char command)
    {
        if (command == 'L') return CommandDirection.Left;
        else if (command == 'R') return CommandDirection.Right;
        else return CommandDirection.Forward;
    }

    private static Direction CalculateLeftFacingDirection(Direction inputDirection)
    {
        return inputDirection switch
        {
            Direction.North => Direction.West,
            Direction.East => Direction.North,
            Direction.West => Direction.South,
            Direction.South => Direction.East,
        };
    }

    private static Direction CalculateRightFacingDirection(Direction inputDirection)
    {
        return inputDirection switch
        {
            Direction.North => Direction.East,
            Direction.East => Direction.South,
            Direction.West => Direction.North,
            Direction.South => Direction.West,
        };
    }
}
