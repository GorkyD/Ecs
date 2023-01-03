using System;
using UnityEngine;

[Serializable]
public struct Player
{
    public CharacterController characterController;
    public Transform playerTransform;
    public Animator animator;
    public float playerSpeed;
}