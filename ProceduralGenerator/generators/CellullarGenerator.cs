using System.Collections.Generic;
using System;

public class CellularGenerator
{
    private Random rand; 
    private int minCells = 4;
    private int maxCells = 5;

    public CellularGenerator(Random rand)
    {
        this.rand = rand;
    }

    public void SetThresholds(int min, int max)
    {
        this.minCells = min;
        this.maxCells = max;
    }

    public CellMap Generate(MazeMap maze, SegmentMask masks)
    {
        CellMap map = new CellMap(maze.Width() * masks.SegmentWidth(), maze.Height() * masks.SegmentHeight());

        BakeMask(map, maze, masks);

        return map;
    }

    private void BakeMask(CellMap map, MazeMap maze, SegmentMask masks)
    {
        for(int i1 = 0; i1 < maze.Width(); i1++)
        {
            for(int j1 = 0; j1 < maze.Height(); j1++)
            {
                int segID = maze.GetSegment(new MapPos(i1, j1));

                for(int i2 = 0; i2 < masks.SegmentWidth(); i2++)
                {
                    for(int j2 = 0; j2 < masks.SegmentHeight(); j2++)
                    {
                        MapPos pos = new MapPos(i1 * masks.SegmentWidth() + i2, j1 * masks.SegmentHeight() + j2);
                        map.SetSegment(pos, masks.GetMask(segID).GetSegment(new MapPos(i2, j2)));
                    }
                }
            }
        }
    }
}