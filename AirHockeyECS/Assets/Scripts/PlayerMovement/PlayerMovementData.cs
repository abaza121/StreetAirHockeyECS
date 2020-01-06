using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct PlayerMovementData : IComponentData
{
    public float sensitivity;
    public int id;
}
