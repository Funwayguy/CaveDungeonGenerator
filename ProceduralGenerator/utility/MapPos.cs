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

    public MapPos Offset(int x, int y)
    {
        return new MapPos(this.posX + x, this.posY + y);
    }

    public MapPos Offset(EnumDirection dir, int distance)
    {
        return new MapPos(this.posX + (dir.OffsetX() * distance), this.posY + (dir.OffsetY() * distance));
    }
}