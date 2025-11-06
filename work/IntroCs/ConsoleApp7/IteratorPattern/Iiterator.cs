namespace IteratorPattern
{
    public interface Iiterator<T>
    {

        void Next();
        bool HasNext();
        T Current();

    }
}
