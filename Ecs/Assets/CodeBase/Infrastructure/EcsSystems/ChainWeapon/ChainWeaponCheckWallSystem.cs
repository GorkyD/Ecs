using Leopotam.Ecs;
using UnityEngine;

namespace Infrastructure.EcsSystems.ChainWeapon
{
    public class ChainWeaponCheckWallSystem : IEcsRunSystem
    {
        private EcsFilter<ChainWeaponComponent> _filter;
        
        private readonly LayerMask _wallLayers = 1 << 6;
        
        private Transform[] _chainPoints;
        private Transform _weaponController;
        
        private const float RayDistance = 1;        // range from 0f to 2f
        private const float WtsDistance = 0.8f;     // range from 0f to 1f
        
        private float _currentDistance;
        private float _requiredDistance;

        public void Run()
        {
            foreach (int i in _filter)
            {
                ref var chainWeaponComponent = ref _filter.Get1(i);
                _weaponController = chainWeaponComponent.WeaponController;
                Vector3 startPoint = _chainPoints[0].position;
                Vector3 tipPoint = Bezier.LinearBezierCurve(startPoint, _weaponController.position, RayDistance);
        
                _requiredDistance = (tipPoint - startPoint).magnitude;

                if (Physics.Linecast(startPoint, tipPoint, out var hit, _wallLayers))
                {
                    Vector3 distanceModifier = Bezier.LinearBezierCurve(startPoint, hit.point, WtsDistance);
                    _currentDistance = (startPoint - distanceModifier).magnitude;
                    if (_currentDistance < _requiredDistance) _filter.Get1(i).SlidingRequired = _currentDistance / _requiredDistance; else _filter.Get1(i).SlidingRequired = 1;
                }
                else
                {
                    _filter.Get1(i).SlidingRequired = 1;
                }
            }
        }
    }
}