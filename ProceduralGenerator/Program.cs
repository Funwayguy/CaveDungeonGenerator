using System;

public class Program
{
    public static void Main(string[] args)
    {
        Console.SetWindowSize(160, 80);

        SegmentMask maskSet = new SegmentMask(10, 10);

        for(int n = 0; n < 16; n++)
        {
            Console.WriteLine("ID: " + n);
            CellMap map = maskSet.GetMask(n);

            for(int j = 0; j < 10; j++)
            {
                for(int i = 0; i < 10; i++)
                {
                    Console.Write(GetCharFromCell(map.GetSegment(i, j)) + " ");
                }

                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }

    public static string GetCharFromCell(EnumCellState state)
    {
        switch(state)
        {
            case EnumCellState.OPEN:
                return ".";
            case EnumCellState.CLOSED:
                return "X";
            case EnumCellState.LOCK_OPEN:
                return "*";
            case EnumCellState.LOCK_CLOSED:
                return "#";
        }

        return " ";
    }
}