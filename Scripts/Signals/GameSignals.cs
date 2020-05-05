using Scripts.Models;

namespace Signals
{
    public class AddItemsSignal
    {
        public Item item;
    }
    
    public class GamePausedSignal
    {
    }
    public class GameUnPausedSignal
    {
    }
    public class TheftTimerSignal
    {
        public float Time;
    }

    public class DeathSignal
    {
    }
}