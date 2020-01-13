using Unity.Mathematics;
using UnityEngine;

public class TouchHandler
{
    Touch? player1Touch;
    Touch? player2Touch;

    public float3[] GetInput()
    {
        var inputArray = new float3[2];
        this.SetPlayerTouches();
        if(player1Touch.HasValue && player1Touch.Value.phase == TouchPhase.Moved)
        {
            inputArray[0] = new float3(player1Touch.Value.deltaPosition.x, 0, player1Touch.Value.deltaPosition.y);
        }

        if (player2Touch.HasValue && player2Touch.Value.phase == TouchPhase.Moved)
        {
            inputArray[1] = new float3(player2Touch.Value.deltaPosition.x, 0, player2Touch.Value.deltaPosition.y);
        }

        return inputArray;
    }

    private void SetPlayerTouches()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                if (touch.position.x / Screen.width > 0.5f)
                {
                    player1Touch = touch;
                }
                else
                {
                    player2Touch = touch;
                }
            }
        }
    }
}
