using System;
using System.Collections.Generic;
using Commands;
using UnityEngine;

namespace Controllers.Behaviours
{
    public class CharacterEffects : MonoBehaviour
    {
        private List<IEffect> _pull = new List<IEffect>();

        public void Calculate(PlayerMovement pm)
        {
        }

        public void Add(IEffect effect)
        {
            _pull.Add(effect);
        }

        private void Update()
        {
            var removeList = new List<IEffect>();
            foreach (var effect in _pull)
            {
                if (effect.BeginTime.Equals(default(DateTime)))
                {
                    effect.BeginTime = DateTime.Now;

                    effect.Begin(gameObject);
                }
                else
                {
                    var effectSeconds = (DateTime.Now - effect.BeginTime).TotalSeconds;
                    if (effectSeconds >= effect.Time)
                    {
                        removeList.Add(effect);
                        effect.End(gameObject);
                    }
                }
            }

            foreach (var effect in removeList)
            {
                _pull.Remove(effect);
            }
        }
    }


    public interface IEffect
    {
        float Time { get; }
        DateTime BeginTime { get; set; }
        void Begin(GameObject go);
        void End(GameObject go);
    }
}