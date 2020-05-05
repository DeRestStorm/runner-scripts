using Scripts.Enums;

namespace Scripts.Models
{
    public class Item
    {
        public ItemType Type;
        public float Count;

        public Item(){}
        public Item(ItemType type, int count)
        {
            Type = type;
            Count = count;
        }
    }
}