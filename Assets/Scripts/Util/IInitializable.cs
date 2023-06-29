namespace Util
{
    public interface IInitializable<in T>
    {
        public void Init(T data);
    }
}
