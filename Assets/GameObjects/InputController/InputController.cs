using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static event System.Action<BlockMoveType> OnMovementButtonPressed;
    public static event System.Action OnRotateButtonPressed;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            OnMovementButtonPressed?.Invoke((BlockMoveType.Left));
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            OnMovementButtonPressed?.Invoke((BlockMoveType.Right));
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            OnMovementButtonPressed?.Invoke((BlockMoveType.Down));
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            OnRotateButtonPressed?.Invoke();
        }
    }
}
