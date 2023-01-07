using UnityEngine;

namespace Infrastructure.EcsSystems.ChainWeapon
{
    public struct ChainWeaponComponent
    {
        public Transform WeaponModel;
        public Transform WeaponController;
        public Transform[] ChainPoints;
        public LineRenderer ChainLine;
        public ChainDirection ChainDirection;
        public float SlidingRequired;
    }
}