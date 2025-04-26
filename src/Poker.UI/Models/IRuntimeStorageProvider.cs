using System.Threading.Tasks;

namespace Poker.UI.Models;

public interface IRuntimeStorageProvider<T>
{
    Task SaveObject(T obj, string key);
    Task<T?> LoadObject(string key);
}