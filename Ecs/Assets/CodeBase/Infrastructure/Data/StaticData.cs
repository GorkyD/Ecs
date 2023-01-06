using UnityEngine;

[CreateAssetMenu]
public class StaticData : ScriptableObject
{
    [Header("Joystick")] [Space]
    public GameObject joystickPrefab;

    [Space] [Header("Player")] [Space]
    public GameObject playerPrefab;
    public float playerSpeed;
}