using LiteDB;

namespace Kernel
{
    public interface IDataProviderFactory
    {
        LiteDatabase Create();
    }
}