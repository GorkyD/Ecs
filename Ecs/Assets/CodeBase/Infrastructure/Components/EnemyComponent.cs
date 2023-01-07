using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure.Components
{
    public struct EnemyComponent
    {
        public GameObject EnemyPrefab;
        public Stack<GameObject> EnemyPool;
    }
}