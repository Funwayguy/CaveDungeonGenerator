using System.Collections.Generic;
using System;

public class CellularGenerator
{
    private Random rand;

    private float fillWeight = 0.5F;
    private int passes = 8;

    private int minCells = 4;
    private int maxCells = 5;

    public CellularGenerator(Random rand)
    {
        this.rand = rand;
    }

    public CellularGenerator SetThresholds(int min, int max)
    {
        this.minCells = min;
        this.maxCells = max;
        return this;
    }

    public CellularGenerator SetFillWeight(float value)
    {
        this.fillWeight = value;
        return this;
    }

    public CellularGenerator SetPasses(int count)
    {
        this.passes = count;
        return this;
    }

    public CellMap Generate(MazeMap maze, SegmentMask masks)
    {
        CellMap map = new CellMap(maze.Width() * masks.SegmentWidth(), maze.Height() * masks.SegmentHeight());

        BakeMask(map, maze, masks);
        SeedMap(map);

        for(int n = 0; n < passes; n++)
        {
            DoPass(map);
        }

        return map;
    }

    private void DoPass(CellMap map)
    {
        for(int i = 0; i < map.Width(); i++)
        {
            for(int j = 0; j < map.Height(); j++)
            {
                MapPos p1 = new MapPos(i, j);

                if(!map.IsValid(p1) || map.GetSegment(p1).IsLocked())
                {
                    continue;
                }

                int neighbours = GetNeighbours(map, p1);

                if(neighbours < minCells)
                {
                    map.SetSegment(p1, EnumCellState.OPEN);
                } else if(neighbours > maxCells)
                {
                    map.SetSegment(p1, EnumCellState.CLOSED);
                }
            }
        }
    }

    private int GetNeighbours(CellMap map, MapPos pos)
    {
        int total = 0;

        for(int i = -1; i <= 1; i++)
        {
            for(int j = -1; j <= 1; j++)
            {
                MapPos p2 = pos.Offset(i, j);

                if(!map.IsValid(p2))
                {
                    continue;
                } else if(!map.GetSegment(p2).SimpleState())
                {
                    total++;
                }
            }
        }

        return total;
    }

    private void SeedMap(CellMap map)
    {
        for(int i = 0; i < map.Width(); i++)
        {
            for(int j = 0; j < map.Height(); j++)
            {
                MapPos pos = new MapPos(i, j);

                if(map.GetSegment(pos).IsLocked())
                {
                    continue;
                }

                map.SetSegment(pos, rand.NextDouble() < fillWeight ? EnumCellState.CLOSED : EnumCellState.OPEN);
            }
        }
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