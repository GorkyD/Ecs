using UnityEngine;

namespace CodeBase.Infrastructure.Components
{
    public struct EnemyComponent
    {
        public Animator Animator;
        public Rigidbody Rigidbody;
        public bool IsAttack;
    }
}