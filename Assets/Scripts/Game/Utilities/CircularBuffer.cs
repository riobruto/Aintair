 public class CircularBuffer<T>
{
    private T[] _buffer { get; }
    private int _index { get; set; }

    public CircularBuffer(T[] buffer)
    {
        _buffer = buffer;
    }
    
    public T GetNext()
    {
        T t = _buffer[_index];
        _index = (_index + 1) % _buffer.Length;
        return t;
    }
}
