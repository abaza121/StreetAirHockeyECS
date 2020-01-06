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
        var input = this.GetSingleton<TouchInputComponent>();
        input.touchPositionDelta = deltaPosition;
        input.keyboardPositionDelta = new float3(Input.GetKey(KeyCode.A) ? 1 : 0, 0, Input.GetKey(KeyCode.D) ? 1 : 0);
        lastPosition = currentPosition;
        this.SetSingleton(input);
    }
}