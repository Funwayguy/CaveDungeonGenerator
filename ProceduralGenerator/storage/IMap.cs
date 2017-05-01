public interface IMap<T>
{
    int Width();
    int Height();

    bool IsValid(MapPos pos);

    T GetSegment(MapPos pos);
    void SetSegment(MapPos pos, T value);

    void Reset();
}