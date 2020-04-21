using System.Globalization;
using Scripts.Enums;
using Scripts.Interfaces;
using Scripts.Models;
using Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.Views
{
    public class ItemCountView : MonoBehaviour
    {
        public Text Label;
        [Inject] private SignalBus _signalBus;
        public ItemType Type;
        [Inject] private IItemRepository<Item> _itemRepository;

        private void Start ()
        {
            _signalBus.Subscribe<AddItemsSignal>(OnAddItems);
            Label.text = _itemRepository.Get(Type)?.Count.ToString(CultureInfo.InvariantCulture);
        }

        private void OnAddItems(AddItemsSignal signal)
        {
            Debug.Log(signal.item.Type);
            if(signal.item.Type != Type)
                return;

            Label.text = signal.item.Count.ToString(CultureInfo.InvariantCulture);
        }
        
    }
}