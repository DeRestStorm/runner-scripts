using System;
using Scripts.Enums;
using Scripts.Interfaces;
using Scripts.Models;
using UnityEngine;
using Zenject;

namespace Components
{
    public class ChangeResourceTrigger : MonoBehaviour
    {
        [Inject] private PlayerMovement _player;
        [Inject] private IItemRepository<Item> _itemRepository;
        public ItemType Type;
        public float Count;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject != _player.gameObject)
                return;

            _itemRepository.Add(new Item() {Type = Type, Count = Count});
        }
    }
}