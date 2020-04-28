using System;
using System.Collections.Generic;
using BezierSolution;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Controllers
{
    public class CameraRails : MonoBehaviour
    {
        [Inject] private BezierWalkerWithTime _walker;
        public bool Lock;
        public Stack<BezierSpline> Path = new Stack<BezierSpline>();
        private UnityAction _onPathCompleted;

        private void Start()
        {
            _walker.enabled = false;
        }

        private void Update()
        {
            if (Lock) return;
            if (Path.Count == 0)
                return;

            var spline = Path.Peek();
            if (spline == null) return;

            // if (Input.GetKey(KeyCode.S))
            // {
            //     _walker.NormalizedT = 0;
            //     _walker.spline = spline;
            //     _walker.lookAt = LookAtMode.Forward;
            //     _walker.travelMode = TravelMode.Once;
            //     _walker.enabled = true;
            //     Lock = true;
            //
            //     _onPathCompleted = delegate
            //     {
            //         _walker.enabled = false;
            //         Lock = false;
            //         Path.Pop();
            //         _walker.onPathCompleted.RemoveListener(_onPathCompleted);
            //     };
            //
            //     _walker.onPathCompleted.AddListener(_onPathCompleted);
            // }
        }
    }
}