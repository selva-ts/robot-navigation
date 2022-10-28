using System;

namespace RobotNavigation.NaivgationApp;

public static class Program
{
    public static void Main()
    {
        Console.WriteLine("Grid size (5x5, 3x4, etc)");
        var grid = Console.ReadLine();

        Console.WriteLine("Command (FLFLFR, etc)");
        var command = Console.ReadLine();

        var navigation = new MarsNavigation(grid, command);
        navigation.NavigateRobot();
        Console.WriteLine(navigation.FinalPosition);

        Console.ReadKey();
    }
}