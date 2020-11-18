using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Block : MonoBehaviour
{
    Vector2[] coordinates;
    Color childColor;
    public Vector3 RotationPoint { get; set; }

    private void Awake()
    {
        GridManager.OnBlocksRePositioned += OnBlocksRePositioned;
    }

    private void OnBlocksRePositioned()
    {
        if (transform.childCount<=0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        GridManager.OnBlocksRePositioned -= OnBlocksRePositioned;
    }

    public void PositionChildBlocks()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var tmp = transform.GetChild(i);
            tmp.transform.localPosition = coordinates[i];
            tmp.gameObject.GetComponent<SpriteRenderer>().color = childColor;
        }
    }

    public void InitializeCoordinates(Vector2[] coordinates)
    {
        this.coordinates = coordinates;
    }

    public void SetColor(Color color)
    {
        childColor = color;
    }
}
