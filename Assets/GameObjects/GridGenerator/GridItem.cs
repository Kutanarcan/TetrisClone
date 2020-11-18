
using UnityEngine;

[System.Serializable]
public class GridItem
{
    public int Index { get; set; }
    public GameObject HasItem { get; set; } = null; // TODO: When you're make the Block Objects turn this a Block
    public Vector2 WorldCoordinates { get; set; }

    public void ChangeItemValues(GridItem item)
    {
        Index = item.Index;
        HasItem = item.HasItem;
        WorldCoordinates = item.WorldCoordinates;
    }
}
