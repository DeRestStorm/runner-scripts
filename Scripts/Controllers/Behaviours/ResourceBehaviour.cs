using System;
using Scripts.Models;
using Scripts.Repositories;
using Scripts.Enums;
using Scripts.Interfaces;
using UnityEngine;
using Zenject;

namespace Scripts.Controllers.Behaviours
{
    public class ResourceBehaviour : MonoBehaviour
    {
        public ItemType Type;
        public float Count;
        [Inject] private IItemRepository<Item> _itemRepository;

        private void OnMouseDown()
        {
            _itemRepository.Add(new Item() {Type = Type, Count = Count});
            Destroy(gameObject);
        }
    }
}