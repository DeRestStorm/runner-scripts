using System;
using System.Collections.Generic;
using UnityEngine;

namespace Samples.UI_sample.Scripts.Controllers.Behaviours
{
    public class BreakingBehaviour : MonoBehaviour
    {
        private Renderer _renderer;
        [Range(1f, 5f)] public int Durability = 1;
        private float _currentDurability;
        private Color _baseColor;

        public List<GameObject> Shapes;

        private void Start()
        {
            _currentDurability = Durability;
            // _renderer = GetComponent<Renderer>();
            // _baseColor = _renderer.material.color;
            Shapes.ForEach(x => x.SetActive(false));
            Shapes[0].SetActive(true);
        }

        private void OnMouseDown()
        {
            _currentDurability -= 1;
            // var durabilityDifference = _currentDurability / Durability;
            // _renderer.material.color = new Color(_baseColor.r, _baseColor.g * durabilityDifference,
            //     _baseColor.b * durabilityDifference);

            if (_currentDurability <= 0)
            {
                Destroy(gameObject);
            }

            Shapes.ForEach(x => x.SetActive(false));
            try
            {
                Shapes[(int) (Durability - _currentDurability)].SetActive(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}