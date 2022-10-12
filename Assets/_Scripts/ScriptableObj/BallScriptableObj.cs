using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ball", menuName = "Ball/Data")]
public class BallScriptableObj : ScriptableObject
{
    public float ballSpeed;
    [Range(1, 1.5f)] public float ballAcceleration;
    public Material material;
}
