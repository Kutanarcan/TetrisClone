using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }

    public static event System.Action OnBlockReachEndOfTheMove;
    public static event System.Action OnBlocksRePositioned;

    private void Awake()
    {
        Instance = this;
    }

    public void OnBlockReachedEndOfTheMoveFunc()
    {
        OnBlockReachEndOfTheMove?.Invoke();
    }

    public void DeleteRow(int row)
    {
        int validRow = GridGenerator.Instance.GetValidCoordinate(row);

        for (int i = 0; i < GridGenerator.Instance.Width; i++)
        {
            var block = GridGenerator.Instance.GridItems[i + validRow * GridGenerator.Instance.Width];
            block.HasItem.transform.parent = null;
            Destroy(block.HasItem);
            block.HasItem = null;
        }
    }

    public void RePositionBlocks(int rowToStart)
    {
        int index = GridGenerator.Instance.GetValidCoordinate(rowToStart) * GridGenerator.Instance.Width;
        int emptyRowCounter = 0;

        for (int i = 0; i < GridGenerator.Instance.Width; i++)
        {
            if (GridGenerator.Instance.IsValidIndex(index) && IsAllRowEmpty(index))
            {
                emptyRowCounter++;
            }
            else
                continue;
            
            index += 10;
        }

        for (int i = 0; i < emptyRowCounter; i++)
        {
            for (int j = 10; j < GridGenerator.Instance.GridItems.Length; j++)
            {
                var itemToMove = GridGenerator.Instance.GetItemByIndex(j);
                var itemToCheck = GridGenerator.Instance.GetItemByIndex(j - GridGenerator.Instance.Width);

                if (itemToMove.HasItem != null && itemToCheck.HasItem == null)
                {
                    MoveBlockOneUnitDown(itemToMove.HasItem.transform);
                    GridGenerator.Instance.SwitchItems(j, j - GridGenerator.Instance.Width);
                }
            }
        }
        OnBlocksRePositioned?.Invoke();
    }

    private void MoveBlockOneUnitDown(Transform blockTransform)
    {
        blockTransform.position += Vector3.down;
    }

    public bool IsAllTheRowIsEmpty(int row)
    {
        int validRow = GridGenerator.Instance.GetValidCoordinate(row);

        for (int i = 0; i < GridGenerator.Instance.Width; i++)
        {
            if (GridGenerator.Instance.GridItems[i + validRow * GridGenerator.Instance.Width].HasItem == null)
            {
                return false;
            }
        }

        return true;
    }

    public bool IsAllRowEmpty(int row)
    {
        for (int i = 0; i < GridGenerator.Instance.Width; i++)
        {
            if (GridGenerator.Instance.GridItems[i + row].HasItem != null)
            {
                return false;
            }
        }

        return true;
    }

    public bool IsValidCoordinate(int x, int y)
    {
        int validX = GridGenerator.Instance.GetValidCoordinate(x);
        int validY = GridGenerator.Instance.GetValidCoordinate(y);

        if (validX < 0 || validY < 0 || validX >= GridGenerator.Instance.Width || validY > GridGenerator.Instance.Height)
            return false;

        if (GridGenerator.Instance.GridItems[validX + validY * GridGenerator.Instance.Width].HasItem != null)
            return false;

        return true;
    }

}
