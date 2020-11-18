using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMovement : MonoBehaviour
{
    [SerializeField]
    BlockMoveDataSO blockMoveDataSO;
    [SerializeField]
    AudioClip moveSound;
    [SerializeField]
    AudioClip moveFinishedSound;

    BlockRotate blockRotate;
    Block block;
    bool isRepositionNeeded;
    Coroutine movementCoroutine;
    WaitForSeconds movementWaitSeconds;

    private void Awake()
    {
        block = GetComponent<Block>();
        movementWaitSeconds = new WaitForSeconds(1f);
        movementCoroutine = StartCoroutine(Movement());
        InputController.OnMovementButtonPressed += MoveBlock;
        blockRotate = GetComponent<BlockRotate>();
    }

    IEnumerator Movement()
    {
        while (true)
        {
            yield return movementWaitSeconds;
            //MoveBlock(BlockMoveType.Down);
        }
    }

    private bool IsMovementValid(BlockMoveType type)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            int x = Mathf.RoundToInt(transform.GetChild(i).transform.position.x + blockMoveDataSO.BlockMoveTypeDict[type].x);
            int y = Mathf.RoundToInt(transform.GetChild(i).transform.position.y + blockMoveDataSO.BlockMoveTypeDict[type].y);

            if (!GridManager.Instance.IsValidCoordinate(x, y))
            {
                return false;
            }
        }

        return true;
    }
    List<int> deletionRows = new List<int>();

    private void FillTheGridItem()
    {
        deletionRows.Clear();

        for (int i = 0; i < transform.childCount; i++)
        {
            int x = Mathf.RoundToInt(transform.GetChild(i).transform.position.x);
            int y = Mathf.RoundToInt(transform.GetChild(i).transform.position.y);

            deletionRows.Add(y);
            GridGenerator.Instance.FillTheGridWithCoordinate(x, y, transform.GetChild(i).gameObject);
            //Debug.Log(transform.GetChild(i).transform.position);
        }
        deletionRows.Sort();
    }

    public void MoveBlock(BlockMoveType type)
    {
        if (type == BlockMoveType.Down && !IsMovementValid(type))
        {
            AudioController.AudioSource.PlayOneShot(moveFinishedSound);
            InputController.OnMovementButtonPressed -= MoveBlock;
            StopMovementCoroutine();
            blockRotate.UnSucsribe();
            FillTheGridItem();
            GridManager.Instance.OnBlockReachedEndOfTheMoveFunc();
            HandleDeletion();
            if (isRepositionNeeded)
                GridManager.Instance.RePositionBlocks(deletionRows[0]);
        }
        else if (IsMovementValid(type))
        {
            transform.position = transform.position + blockMoveDataSO.BlockMoveTypeDict[type];
            AudioController.AudioSource.PlayOneShot(moveSound);
        }
    }

    void HandleDeletion()
    {
        isRepositionNeeded = false;
        for (int i = 0; i < deletionRows.Count; i++)
        {
            if (GridManager.Instance.IsAllTheRowIsEmpty(deletionRows[i]))
            {
                GridManager.Instance.DeleteRow(deletionRows[i]);
                isRepositionNeeded = true;
            }
        }
    }

    void StopMovementCoroutine()
    {
        if (movementCoroutine != null)
        {
            StopCoroutine(movementCoroutine);
            movementCoroutine = null;
        }
    }

    private void OnDestroy()
    {
        InputController.OnMovementButtonPressed -= MoveBlock;
    }
}
