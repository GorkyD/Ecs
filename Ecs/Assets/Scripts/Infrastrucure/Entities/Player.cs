using System;
using UnityEngine;

[Serializable]
public struct Player
{
    public Transform playerTransform;
    public Rigidbody playerRigidbody;
    public float playerSpeed;
}