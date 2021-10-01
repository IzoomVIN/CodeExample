namespace Services.Pool.Control
{
    public interface IPoolable
    {
        void Clean();
        void Release();
    }
}