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
        private Text _label;
        [Inject] private SignalBus _signalBus;
        public ItemType Type;
        [Inject] private IItemRepository<Item> _itemRepository;

        private void Start()
        {
            _label = GetComponentInChildren<Text>();
            _signalBus.Subscribe<AddItemsSignal>(OnAddItems);
            _label.text = _itemRepository.Get(Type)?.Count.ToString(CultureInfo.InvariantCulture);
        }

        private void OnAddItems(AddItemsSignal signal)
        {
            Debug.Log(signal.item.Type);
            if (signal.item.Type != Type)
                return;

            _label.text = signal.item.Count.ToString(CultureInfo.InvariantCulture);
        }
    }
}