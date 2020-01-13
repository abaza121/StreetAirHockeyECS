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
    private TouchHandler touchHandler;
    public bool isCreated;

    protected override void OnCreate()
    {
        EntityManager.CreateEntity(typeof(TouchInputComponent));
        this.SetSingleton(new TouchInputComponent());
        isCreated = true;
        lastPosition = new float3(-Input.mousePosition.x, Input.mousePosition.z, -Input.mousePosition.y);
        touchHandler = new TouchHandler();
    }

    protected override void OnUpdate()
    {
        this.HandleTouchInput();
    }

    private void HandlePCInput()
    {
        var currentPosition = new float3(-Input.mousePosition.x, Input.mousePosition.z, -Input.mousePosition.y);
        var deltaPosition = lastPosition - currentPosition;
        var player1Input = deltaPosition;
        var player2Input = new float3(Input.GetKey(KeyCode.A) ? 1 : 0, 0, Input.GetKey(KeyCode.D) ? 1 : 0);
        lastPosition = currentPosition;
        UpdateSingletonValue(in player1Input, in player2Input);
    }

    private void HandleTouchInput()
    {
        var touchInputs = this.touchHandler.GetInput();
        UpdateSingletonValue(in touchInputs[0], in touchInputs[1]);
    }


    private void UpdateSingletonValue(in float3 player1Position,in float3 player2Position)
    {
        var input = this.GetSingleton<TouchInputComponent>();
        input.player1Input = player1Position;
        input.player2Input = player2Position;
        this.SetSingleton(input);
    }
}