using System.Collections.Generic;
using Infrastructure.EcsSystems.ChainWeapon;
using Leopotam.Ecs;
using UnityEngine;

public class ChainWeaponSystem : IEcsRunSystem
{
    private EcsFilter<ChainWeaponComponent> _filter;

    private Transform _weaponModel;
    private Transform _weaponController;
    private Transform[] _chainPoints;
    private LineRenderer _chainLine;
    private ChainDirection _chainDirection;
    
    private readonly LayerMask _wallLayers = 1 << 6;
    
    private const float CurveSizeMultiplier = 1;
    private const float CurveSmoothing = 0.1f;  // range from 0.02f to 0.2f
    private const float RayDistance = 1;        // range from 0f to 2f
    private const float WtsDistance = 0.8f;     // range from 0f to 1f

    private readonly List<Vector3> _newPoints = new();
    private readonly List<Vector3> _finalPoints = new();

    private float _currentDistance;
    private float _slidingRequired;
    private float _requiredDistance;

    private bool _negativeDirection;

    public void Run()
    {
        foreach (int i in _filter)
        {
            ref var chainWeaponComponent = ref _filter.Get1(i);
            _weaponModel = chainWeaponComponent.WeaponModel;
            _weaponController = chainWeaponComponent.WeaponController;
            _chainPoints = chainWeaponComponent.ChainPoints;
            _chainLine = chainWeaponComponent.ChainLine;
            _chainDirection = chainWeaponComponent.ChainDirection;
        
            CheckWalls();
            GetNewPoints();
        }
    }

    private void CheckWalls()
    {
        Vector3 startPoint = _chainPoints[0].position;
        Vector3 tipPoint = Bezier.LinearBezierCurve(startPoint, _weaponController.position, RayDistance);
        
        _requiredDistance = (tipPoint - startPoint).magnitude;

        if (Physics.Linecast(startPoint, tipPoint, out var hit, _wallLayers))
        {
            Vector3 distanceModifier = Bezier.LinearBezierCurve(startPoint, hit.point, WtsDistance);
            _currentDistance = (startPoint - distanceModifier).magnitude;
            if (_currentDistance < _requiredDistance) _slidingRequired = _currentDistance / _requiredDistance; else _slidingRequired = 1;
        }
        else
        {
            _slidingRequired = 1;
        }
    }

    private void GetNewPoints()
    {
        _newPoints.Clear();
       
        Vector3 firstPoint = _chainPoints[0].position;
        _newPoints.Add(firstPoint);

        Vector3 secondPoint = firstPoint + (GetDir(0) * _chainPoints[0].localScale.x) * CurveSizeMultiplier;
        secondPoint = GetDistance(firstPoint, secondPoint);
        _newPoints.Add(secondPoint);

        Vector3 finalPoint = GetDistance(_chainPoints[0].position, _chainPoints[1].position);


        Vector3 thirdPoint = finalPoint - (GetDir(1) * _chainPoints[1].localScale.x) * CurveSizeMultiplier;
        thirdPoint = GetDistance(finalPoint, thirdPoint);
        _newPoints.Add(thirdPoint);

        _newPoints.Add(finalPoint);

        SubdividePoints();
    }

    private Vector3 GetDir(int i)
    {
        Vector3 calculationDirection = _chainPoints[i].right;
        if (_chainDirection != ChainDirection.Default)
        {
            calculationDirection = _chainDirection == ChainDirection.Forward ? _chainPoints[i].forward :
                            _chainDirection == ChainDirection.Right ? _chainPoints[i].right :
                            _chainDirection == ChainDirection.Up ? _chainPoints[i].up : _chainPoints[i].right;
        }
        if (_negativeDirection) calculationDirection = -calculationDirection;
        return calculationDirection;
    }

    private Vector3 GetDistance(Vector3 previousPoint, Vector3 currentPoint)
    {
        Vector3 pos = Bezier.LinearBezierCurve(previousPoint, currentPoint, _slidingRequired);
        return pos;
    }

    private void SubdividePoints()
    {
        _finalPoints.Clear();
        for (int i = 0; i < _newPoints.Count - 1; i += 3)
        {
            for (float j = 0; j < 1; j += CurveSmoothing)
            {
                Vector3 points = Bezier.CubicBezierCurve(_newPoints[i], _newPoints[i + 1], _newPoints[i + 2], _newPoints[i + 3], j);
                _finalPoints.Add(points);
            }
        }
        _finalPoints[0] = _chainPoints[0].position;
        _finalPoints[^1] = _newPoints[^1];
        UpdateChainRenderer();
    }

    private void UpdateChainRenderer()
    {
        if (_finalPoints.Count <= 0) return;
        _weaponModel.position = _newPoints[^1];

        _chainLine.positionCount = _finalPoints.Count;
        for (int i = 0; i < _finalPoints.Count; i++)
        {
            _chainLine.SetPosition(i, _finalPoints[i]);
        }
    }
}