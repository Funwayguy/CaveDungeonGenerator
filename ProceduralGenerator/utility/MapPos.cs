public struct MapPos
{
    private readonly int posX;
    private readonly int posY;

    public MapPos(int x, int y)
    {
        this.posX = x;
        this.posY = y;
    }

    public int PosX()
    {
        return posX;
    }

    public int PosY()
    {
        return posY;
    }

    public MapPos Offset(EnumDirection dir)
    {
        return Offset(dir, 1);
    }

    public MapPos Offset(EnumDirection dir, int distance)
    {
        return Offset(dir.OffsetX() * distance, dir.OffsetY() * distance);
    }

    public MapPos Offset(int x, int y)
    {
        return new MapPos(this.posX + x, this.posY + y);
    }
}