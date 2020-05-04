using System;
using Runtime.Scripts.Commands;
using UnityEngine;
using Zenject;

namespace Components
{
    public class NextLvl : MonoBehaviour
    {
        [Inject] private StartRunnerSceneCommand _startRunnerSceneCommand;
        private void OnMouseDown()
        {
            _startRunnerSceneCommand.Exequte();
        }
    }
}