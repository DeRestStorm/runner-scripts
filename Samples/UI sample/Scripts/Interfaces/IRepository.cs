using System.Collections.Generic;
using Scripts.Enums;

namespace Scripts.Interfaces
{
    public interface IRepository<T>
    {
        T Get(T model);
        void Add(T model);
        void Remove(T model);
        IEnumerable<T> GetAll();
    } 
    public interface IItemRepository<T>
    {
        T Get(ItemType model);
        void Add(T model);
        void Remove(T model);
        IEnumerable<T> GetAll();
    }
}