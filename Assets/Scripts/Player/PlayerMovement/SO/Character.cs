using UnityEngine;

[CreateAssetMenu(fileName = "Character",menuName = "Character Menu")]
public class Character : ScriptableObject
{
    public float StandHeight;
    public float CrouchHeight;

    public float CurrentSpeed;
    public float WalkSpeed;
    public float SprintSpeed;
}