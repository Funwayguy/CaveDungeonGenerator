public interface IMap<T>
{
    int Width();
    int Height();

    T GetSegment(MapPos pos);
    void SetSegment(MapPos pos, T value);

    void Reset();
}