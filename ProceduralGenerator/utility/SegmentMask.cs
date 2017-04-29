using System.Collections.Generic;
using System;

public class SegmentMask
{
    private readonly CellMap[] masks;
    private readonly int sizeX;
    private readonly int sizeY;

    private readonly int[] xMidpoints = new int[2];
    private readonly int[] yMidpoints = new int[2];

    public SegmentMask(int sizeX, int sizeY)
    {
        this.sizeX = sizeX;
        this.sizeY = sizeY;

        this.masks = new CellMap[16];

        xMidpoints[0] = sizeX / 2 - 1;
        xMidpoints[1] = sizeX % 2 == 0 ? (sizeX / 2) : xMidpoints[0];

        yMidpoints[0] = sizeY / 2 - 1;
        yMidpoints[1] = sizeY % 2 == 0 ? (sizeY / 2) : yMidpoints[0];

        Setup();
    }

    public CellMap GetMask(int id)
    {
        if(id < 0 || id >= 16)
        {
            return null;
        }

        return masks[id];
    }

    private void Setup()
    {
        for(int n = 0; n < 16; n++)
        {
            CellMap map = new CellMap(sizeX, sizeY);
            masks[n] = map;

            // NORTH
            if((n & 1) == 0)
            {
                // BLOCKED
                FillCells(map, EnumCellState.LOCK_CLOSED, 0, 0, sizeX - 1, 0);
            } else
            {
                // OPENED
                FillCells(map, EnumCellState.LOCK_OPEN, xMidpoints[0], 0, xMidpoints[1], yMidpoints[1]);
            }

            // EAST
            if((n & 2) == 0)
            {
                // BLOCKED
                FillCells(map, EnumCellState.LOCK_CLOSED, sizeX - 1, 0, sizeX - 1, sizeY - 1);
            } else
            {
                // OPENED
                FillCells(map, EnumCellState.LOCK_OPEN, xMidpoints[0], yMidpoints[0], sizeX - 1, yMidpoints[1]);
            }

            // SOUTH
            if((n & 4) == 0)
            {
                // BLOCKED
                FillCells(map, EnumCellState.LOCK_CLOSED, 0, sizeY - 1, sizeX - 1, sizeY - 1);
            } else
            {
                // OPENED
                FillCells(map, EnumCellState.LOCK_OPEN, xMidpoints[0], yMidpoints[0], xMidpoints[1], sizeY - 1);
            }

            // WEST
            if((n & 8) == 0)
            {
                // BLOCKED
                FillCells(map, EnumCellState.LOCK_CLOSED, 0, 0, 0, sizeY - 1);
            } else
            {
                // OPENED
                FillCells(map, EnumCellState.LOCK_OPEN, 0, yMidpoints[0], xMidpoints[1], yMidpoints[1]);
            }
        }
    }

    private void FillCells(CellMap map, EnumCellState state, int x1, int y1, int x2, int y2)
    {
        for(int i = x1; i <= x2; i++)
        {
            for(int j = y1; j <= y2; j++)
            {
                map.SetSegment(i, j, state);
            }
        }
    }
}