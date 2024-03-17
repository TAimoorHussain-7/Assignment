namespace ProjectCore.Variables
{
    public interface IDBVariable
    {
        void Update(object value);
        object GetValue();
    }
}