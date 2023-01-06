using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Player
{
    public List<Transform> hands;
    public CharacterController characterController;
    public Transform playerTransform;
    public Animator animator;
    public float playerSpeed;
}