using System;

public class Program
{
    public static void Main(string[] args)
    {
        Console.SetWindowSize(160, 80);

        Random rand = new Random();
        SegmentMask maskSet = new SegmentMask(10, 10);

        MazeGenerator mazeGen = new MazeGenerator(rand);
        mazeGen.SetLoopChance(0.1F);
        MazeMap mazeMap = mazeGen.Generate(8,8);

        CellularGenerator cellGen = new CellularGenerator(rand);
        cellGen.SetPasses(12);
        cellGen.SetFillWeight(0.3F);
        cellGen.SetThresholds(4,4);
        CellMap cellMap = cellGen.Generate(mazeMap, maskSet);

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
                return "#";
            case EnumCellState.LOCK_OPEN:
                return " ";
            case EnumCellState.LOCK_CLOSED:
                return "#";
        }

        return " ";
    }
}