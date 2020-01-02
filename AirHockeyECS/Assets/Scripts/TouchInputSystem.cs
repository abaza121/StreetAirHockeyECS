using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using static Unity.Mathematics.math;

public class TouchInputSystem : ComponentSystem
{
    public float3 lastPosition;
    public bool isCreated;

    protected override void OnUpdate()
    {
        if (isCreated == false)
        {
            EntityManager.CreateEntity(typeof(TouchInputComponent));
            this.SetSingleton(new TouchInputComponent());
            isCreated = true;
            lastPosition = new float3(-Input.mousePosition.x, Input.mousePosition.z, -Input.mousePosition.y);
        }

        var currentPosition = new float3(-Input.mousePosition.x, Input.mousePosition.z, -Input.mousePosition.y);
        var deltaPosition = lastPosition - currentPosition;
        Debug.Log(deltaPosition);
        var input = this.GetSingleton<TouchInputComponent>();
        input.touchPositionDelta = deltaPosition;
        lastPosition = currentPosition;
        this.SetSingleton(input);
    }
}