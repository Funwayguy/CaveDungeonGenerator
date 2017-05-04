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
            MapPos curPos = pending[0];
            int curSeg = map.GetSegment(curPos);
            List<EnumDirection> dirs = GetEmptySides(map, curPos, lastDir);

            if(lastDir.HasValue)
            {
                curSeg |= lastDir.Value.Opposite().BitMask();
                map.SetSegment(curPos, curSeg);
            }

            if(dirs.Count <= 0)
            {
                lastDir = null;
                pending.RemoveAt(0);
                pending.Shuffle(rand);
            } else
            {
                EnumDirection nxtDir = dirs[rand.Next(dirs.Count)];
                curSeg |= nxtDir.BitMask();
                map.SetSegment(curPos, curSeg);
                lastDir = nxtDir;

                if(dirs.Count == 1)
                {
                    pending.RemoveAt(0);
                }

                pending.Insert(0, curPos.Offset(nxtDir));
            }
        }

        return map;
    }

    public List<EnumDirection> GetEmptySides(MazeMap map, MapPos pos, EnumDirection? lastDir)
    {
        List<EnumDirection> list = new List<EnumDirection>();
        int curMask = map.GetSegment(pos);

        Predicate<EnumDirection> dirPre = delegate(EnumDirection d)
        {
            MapPos off = pos.Offset(d);

            if((curMask & d.BitMask()) != 0)
            {
                return false;
            } else if(map.IsValid(off) && (map.GetSegment(off) <= 0 || rand.NextDouble() < lpChance))
            {
                return true;
            }

            return false;
        };

        list.AddAll(MethodExtensions.AllDirections(), dirPre);

        if(lastDir.HasValue && rand.NextDouble() < strBias && list.Contains(lastDir.Value))
        {
            for(int n = list.Count - 1; n >= 0; n--)
            {
                list[n] = lastDir.Value;
            }
        }

        return list;
    }
}