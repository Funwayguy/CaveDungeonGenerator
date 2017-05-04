using System;
using System.Collections.Generic;

public static class MethodExtensions
{
    public static IEnumerable<KeyValuePair<MapPos, T>> MapToKeyPairs<T>(IMap<T> map)
    {
        Dictionary<MapPos, T> dict = new Dictionary<MapPos, T>();

        for(int i = 0; i < map.Width(); i++)
        {
            for(int j = 0; j < map.Height(); j++)
            {
                MapPos p = new MapPos(i, j);
                dict.Add(p, map.GetSegment(p));
            }
        }

        return dict;
    }

    // DIRECTIONS
    public static int BitMask(this EnumDirection dir)
    {
        return (int)(Math.Pow(2D, (int)dir));
    }

    public static EnumDirection Opposite(this EnumDirection dir)
    {
        return (EnumDirection)(((int)dir + 2)%4);
    }

    public static int OffsetX(this EnumDirection dir)
    {
        switch(dir)
        {
            case EnumDirection.EAST:
                return 1;
            case EnumDirection.WEST:
                return -1;
        }

        return 0;
    }

    public static int OffsetY(this EnumDirection dir)
    {
        switch(dir)
        {
            case EnumDirection.SOUTH:
                return 1;
            case EnumDirection.NORTH:
                return -1;
        }

        return 0;
    }

    public static IEnumerable<EnumDirection> AllDirections()
    {
        return (IEnumerable<EnumDirection>)Enum.GetValues(typeof(EnumDirection));
    }

    // CELLS
    public static bool IsLocked(this EnumCellState state)
    {
        return state == EnumCellState.LOCK_OPEN || state == EnumCellState.LOCK_CLOSED;
    }

    public static bool SimpleState(this EnumCellState state)
    {
        return state == EnumCellState.OPEN || state == EnumCellState.LOCK_OPEN;
    }

    // LISTS
    public static void Shuffle<T>(this List<T> list, Random rand)
    {
        for(int i = list.Count - 1; i > 0; i--)
        {
            int n = rand.Next(i + 1);

            T a = list[n];
            list[n] = list[i];
            list[i] = a;
        }
    }

    public static void AddAll<T>(this List<T> list, IEnumerable<T> e, Predicate<T> p)
    {
        foreach(T v in e)
        {
            if(p.Invoke(v))
            {
                list.Add(v);
            }
        }
    }

    public static void AddAll<T>(this List<T> list, IEnumerable<T> e)
    {
        foreach(T v in e)
        {
            list.Add(v);
        }
    }
}