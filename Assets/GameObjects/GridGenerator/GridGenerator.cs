using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public static GridGenerator Instance { get; private set; }

    [SerializeField]
    private int height;
    [SerializeField]
    private int width;
    [SerializeField]
    private int offset;
    [SerializeField]
    private GameObject gridItemPrefab;

    public GridItem[] GridItems => gridItems;

    public int Width => width;
    public int Height => height;

    private GridItem[] gridItems;

    private void Awake()
    {
        Instance = this;
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        Vector2 gridItemPos = Vector2.zero;
        gridItems = new GridItem[height * width];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                gridItemPos.x = x + offset;
                gridItemPos.y = y + offset;

                var tmpGo = Instantiate(gridItemPrefab, gridItemPos, Quaternion.identity);
                tmpGo.transform.SetParent(transform);
                tmpGo.name = $"{x + offset},{(y + offset)} - I : {x + y * width}";

                gridItems[x + y * width] = new GridItem { Index = x + y * width, HasItem = null, WorldCoordinates = gridItemPos };
            }
        }
    }

    public GridItem GetItemByIndex(int index)
    {
        return (index >= width * height || index < 0) ? null : gridItems[index];
    }

    public bool IsValidIndex(int index)
    {
        return (index >= width * height || index < 0) ? false : true;
    }

    public void FillTheGridWithCoordinate(int x, int y, GameObject block)
    {
        int validX = GetValidCoordinate(x);
        int validY = GetValidCoordinate(y);

         gridItems[validX + validY * width].HasItem = block;
    }

    public void SwitchItems(int firstIndex, int secondIndex)
    {
        var tmpItem = new GridItem();
        tmpItem.ChangeItemValues(gridItems[firstIndex]);
        gridItems[firstIndex].ChangeItemValues(gridItems[secondIndex]);
        gridItems[secondIndex].ChangeItemValues(tmpItem);
    }

    public int GetValidIndexWithCoordinate(Vector3 coordinate)
    {
        int validX = GetValidCoordinate((int)coordinate.x);
        int validY = GetValidCoordinate((int)coordinate.y);

        return validX + validY * width;
    }

    public int GetValidCoordinate(int i)
    {
        return i - offset;
    }
}