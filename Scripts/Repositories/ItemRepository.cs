using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.Enums;
using Scripts.Interfaces;
using Scripts.Models;
using Signals;
using UnityEngine;
using Zenject;

namespace Scripts.Repositories
{
    public class ItemRepository : IItemRepository<Item>
    {
        private readonly List<Item> _items = new List<Item>()
        {
            new Item() {Type = ItemType.Battery},
            new Item() {Type = ItemType.Scrap}
        };

        [Inject] private SignalBus _signalBus;

        public ItemRepository()
        {
        }


        public void Load()
        {
            if (PlayerPrefs.HasKey("battery"))
                Add(new Item() {Type = ItemType.Battery, Count = PlayerPrefs.GetFloat("battery")});

            if (PlayerPrefs.HasKey("scrap"))
                Add(new Item() {Type = ItemType.Scrap, Count = PlayerPrefs.GetFloat("scrap")});
            
            Debug.Log("LoadRepo");
        }


        public Item Get(ItemType type)
        {
            return _items.FirstOrDefault(X => X.Type == type);
        }

        public void Add(Item entity)
        {
            var item = _items.FirstOrDefault(x => x.Type == entity.Type);

            item.Count += entity.Count;
            _signalBus.Fire(new AddItemsSignal() {item = item});
        }

        public void Remove(Item entity)
        {
            _items.Remove(entity);
        }

        public IEnumerable<Item> GetAll()
        {
            return _items;
        }

        public void Save()
        {
            PlayerPrefs.SetFloat("battery", Get(ItemType.Battery).Count);
            PlayerPrefs.SetFloat("scrap", Get(ItemType.Scrap).Count);
            Debug.Log("SaveRepo");
        }
    }
}