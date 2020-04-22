using System;
using Controllers.Behaviours;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class EffectButton : MonoBehaviour
    {
        private Button _button;
        public event Action<CharacterEffects> OnClick = delegate { };
        public CharacterEffects Target;

        private void Start()
        {
            _button = GetComponent<Button>();
            var effect = GetComponent<IEffect>();

            _button.onClick.AddListener(() => OnClick(Target));
            OnClick += (x) =>
            {
                if (effect.BeginTime == default(DateTime))
                    x.Add(effect);
            };
        }
    }
}