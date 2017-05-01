public class CellMap : IMap<EnumCellState>
{
    private int sizeX;
    private int sizeY;

    private EnumCellState[] map;

    public CellMap(int sizeX, int sizeY)
    {
        this.sizeX = sizeX;
        this.sizeY = sizeY;

        this.map = new EnumCellState[sizeX * sizeY];
    }

    public int Width()
    {
        return this.sizeX;
    }

    public int Height()
    {
        return this.sizeY;
    }

    public bool IsValid(MapPos pos)
    {
        int x = pos.PosX();
        int y = pos.PosY();

        return x >= 0 && x < sizeX && y >= 0 && y < sizeY;
    }

    public EnumCellState GetSegment(MapPos pos)
    {
        int x = pos.PosX();
        int y = pos.PosY();

        if(x < 0 || x >= sizeX || y < 0 || y >= sizeY)
        {
            return EnumCellState.LOCK_CLOSED;
        }

        return map[(y * sizeX) + x];
    }

    public void SetSegment(MapPos pos, EnumCellState value)
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
        map = new EnumCellState[sizeX * sizeY];
    }
}