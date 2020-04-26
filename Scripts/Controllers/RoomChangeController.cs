using BezierSolution;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Controllers
{
    public class RoomChangeController : MonoBehaviour
    {
        public BezierSpline ReturnTrip;
        [Inject] private BezierWalkerWithTime _walkerWithTime;
        [Inject] private CameraRails _rails;
        private BezierSpline _spline;
        private UnityAction _onPathCompleted;

        private void Start()
        {
            _spline = GetComponentInChildren<BezierSpline>();
        }

        private void OnMouseDown()
        {
            _walkerWithTime.spline = _spline;
            _walkerWithTime.travelMode = TravelMode.Once;
            _walkerWithTime.NormalizedT = 0;
            _walkerWithTime.enabled = true;
            _rails.Lock = true;
            _onPathCompleted = delegate
            {
                _walkerWithTime.enabled = false;
                _rails.Path.Push(ReturnTrip);
                _walkerWithTime.onPathCompleted.RemoveListener(_onPathCompleted);
                _rails.Lock = false;
            };
            _walkerWithTime.onPathCompleted.AddListener(_onPathCompleted);
        }
    }
}