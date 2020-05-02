using System.Linq;
using Scripts.Enums;
using Scripts.Interfaces;
using Scripts.Models;
using Signals;
using UnityEngine;
using Zenject;

namespace Controllers.Behaviours
{
    public class BatterySocket : MonoBehaviour
    {
        public float Number;

        private GameObject _battery;

        [Inject] private IItemRepository<Item> _itemRepository;
        [Inject] private SignalBus _signalBus;


        private void Start()
        {
            _signalBus.Subscribe<AddItemsSignal>(OnAddItems);
            _battery = GetComponentInChildren<MonoBehaviour>().gameObject;
            
            _battery.SetActive(_itemRepository.GetAll().Count(x => x.Type == ItemType.Battery) < Number);
        }

        private void OnAddItems(AddItemsSignal signal)
        {
            if (signal.item.Type != ItemType.Battery)
                return;
            
            _battery.SetActive(_itemRepository.GetAll().Count(x => x.Type == ItemType.Battery) < Number);
        }
    }
}