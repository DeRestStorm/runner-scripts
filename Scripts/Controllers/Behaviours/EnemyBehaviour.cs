using BezierSolution;
using UnityEngine;

namespace Controllers.Behaviours
{
    public class EnemyBehaviour : MonoBehaviour
    {
        private void Start()
        {
            var walker= GetComponent<BezierWalkerWithSpeed>();
            walker.NormalizedT = 0;
            walker.enabled = true;
            
        }
    }
}