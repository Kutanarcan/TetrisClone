using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRotate : MonoBehaviour
{
    Block block;
    float degrees = 90f;

    void Awake()
    {
        block = GetComponent<Block>();
        InputController.OnRotateButtonPressed += RotateBlock;
    }

    public void RotateBlock()
    {
        transform.Rotate(0, 0, 90f, Space.Self);

        if (!IsValidRotation())
        {
            transform.Rotate(0, 0, -90f, Space.Self);
        }
    }

    private bool IsValidRotation()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            int x = Mathf.RoundToInt(transform.GetChild(i).transform.position.x);
            int y = Mathf.RoundToInt(transform.GetChild(i).transform.position.y);
            
            if (!GridManager.Instance.IsValidCoordinate(x, y))
            {
                return false;
            }
        }
        return true;
    }

    public void UnSucsribe()
    {
        InputController.OnRotateButtonPressed -= RotateBlock;
    }

    private void OnDestroy()
    {
        UnSucsribe();
    }
}
