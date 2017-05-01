using System;

public class Program
{
    public static void Main(string[] args)
    {
        Console.SetWindowSize(160, 80);

        Random rand = new Random();
        SegmentMask maskSet = new SegmentMask(10, 10);
        MazeMap mazeMap = new MazeGenerator(rand).Generate(8,8);
        CellMap cellMap = new CellularGenerator(rand).Generate(mazeMap, maskSet);

        for(int j = 0; j < cellMap.Height(); j++)
        {
            for(int i = 0; i < cellMap.Width(); i++)
            {
                Console.Write(GetCharFromCell(cellMap.GetSegment(new MapPos(i, j))) + " ");
            }
        }
    }

    public static string GetCharFromCell(EnumCellState state)
    {
        switch(state)
        {
            case EnumCellState.OPEN:
                return " ";
            case EnumCellState.CLOSED:
                return "*";
            case EnumCellState.LOCK_OPEN:
                return ".";
            case EnumCellState.LOCK_CLOSED:
                return "#";
        }

        return " ";
    }
}