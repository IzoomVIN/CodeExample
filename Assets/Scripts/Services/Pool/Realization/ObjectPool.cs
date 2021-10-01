using System.Collections.Generic;

namespace Services.Pool.Realization
{
    
public class ObjectPool<T>
{
    private readonly List<T> _elements;
    private readonly int _maxCount;
    private readonly ICreation<T> _creator;

    public int Count => _elements.Count;

    public ObjectPool(ICreation<T> creator, int startCount = 15, int maxCount = 30)
    {
        _elements = new List<T>();
        _maxCount = maxCount;
        _creator = creator;
        Initialize(startCount);
    }

    public T GetElement()
    {
        if (_elements.Count > 0)
        {
            var index = _elements.Count - 1;
            var element = _elements[index];
            _elements.RemoveAt(index);

            return element;
        }
        else
        {
            return _creator.Create();
        }
    }

    public void SetElement(T element)
    {
        if (_elements.Count < _maxCount) _elements.Add(element);
    }

    private void Initialize(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SetElement(_creator.Create());
        }
    }

    public interface ICreation<O>
    {
        O Create();
    }
}
}