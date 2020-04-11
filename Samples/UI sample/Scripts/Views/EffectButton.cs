using System;
using Controllers.Behaviours;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class EffectButton : MonoBehaviour
    {
        public Button Button;
        public event Action<CharacterEffects> OnClick = delegate { };
        public CharacterEffects Target;

        [SerializeField]
        public Component Effect;


        private void Start()
        {
            Button.onClick.AddListener(() => OnClick(Target));
            OnClick += (x) =>
            {
                var effect = Effect.GetComponent<IEffect>();
                if (effect.BeginTime == default(DateTime))
                    x.Add(effect);
            };
        }
    }
}