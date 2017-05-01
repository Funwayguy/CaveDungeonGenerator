public class MazeMap : IMap<int>
{
    private readonly int sizeX;
    private readonly int sizeY;

    private int[] map;

    public MazeMap(int sizeX, int sizeY)
    {
        this.sizeX = sizeX;
        this.sizeY = sizeY;

        map = new int[sizeX * sizeY];
    }

    public int Width()
    {
        return sizeX;
    }

    public int Height()
    {
        return sizeY;
    }

    public bool IsValid(MapPos pos)
    {
        int x = pos.PosX();
        int y = pos.PosY();

        return x >= 0 && x < sizeX && y >= 0 && y < sizeY;
    }

    public int GetSegment(MapPos pos)
    {
        int x = pos.PosX();
        int y = pos.PosY();
        
        if(x < 0 || x >= sizeX || y < 0 || y >= sizeY)
        {
            return -1;
        }

        return map[(y * sizeX) + x];
    }

    public void SetSegment(MapPos pos, int value)
    {
        int x = pos.PosX();
        int y = pos.PosY();
        
        if(x < 0 || x >= sizeX || y < 0 || y >= sizeY)
        {
            return;
        }

        map[(y * sizeX) + x] = value;
    }

    public void Reset()
    {
        map = new int[sizeX * sizeY];
    }
}