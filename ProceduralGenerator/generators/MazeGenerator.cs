using System.Collections.Generic;
using System;

public class MazeGenerator
{
    private Random rand;
    private float strBias = 0F;
    private float lpChance = 0F;

    public MazeGenerator(Random rand)
    {
        this.rand = rand;
    }

    public MazeGenerator SetStraightBias(float bias)
    {
        this.strBias = bias;
        return this;
    }
    
    public MazeGenerator SetLoopChance(float chance)
    {
        this.lpChance = chance;
        return this;
    }

    public MazeMap Generate(int sizeX, int sizeY)
    {
        MazeMap map = new MazeMap(sizeX, sizeY);

        List<MapPos> pending = new List<MapPos>();
        pending.Add(new MapPos(map.Width()/2, map.Height()/2));

        EnumDirection? lastDir = null;

        while(pending.Count > 0)
        {
            int idx = rand.Next(pending.Count);
            MapPos curPos = pending[idx];
            int curSeg = map.GetSegment(curPos);
            List<EnumDirection> dirs = GetEmptySides(map, curPos, lastDir.Value);

            if(lastDir.HasValue)
            {
                curSeg |= lastDir.Value.Opposite();
                map.SetSegment(curPos, curSeg);
            }

            if(dirs.Count <= 0)
            {
                lastDir = null;
                pending.RemoveAt(idx);
                continue;
            } else
            {
                EnumDirection nxtDir = dirs[rand.Next(dirs.Count)];
                curSeg |= nxtDir.BitMask();
                map.SetSegment(curPos, curSeg);
            }
        }

        return map;
    }

    public List<EnumDirection> GetEmptySides(MazeMap map, MapPos pos, EnumDirection lastDir)
    {
        List<EnumDirection> list = new List<EnumDirection>();

        foreach(EnumDirection d in MethodExtensions.AllDirections())
        {
            if(map.GetSegment(pos.Offset(d)) <= 0)
            {
                list.Add(d);
            }
        }

        return list;
    }
}