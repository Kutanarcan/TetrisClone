using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="BlockMovementSO",menuName ="ScriptableObjects/Block Movement")]
public class BlockMoveDataSO : ScriptableObject
{
    Dictionary<BlockMoveType, Vector3> blockMoveTypeDict = new Dictionary<BlockMoveType, Vector3>();

    public Dictionary<BlockMoveType, Vector3> BlockMoveTypeDict => blockMoveTypeDict;

    void OnEnable()
    {
        InitializeMoveTypeDictionary();
    }

    void InitializeMoveTypeDictionary()
    {
        blockMoveTypeDict.Add(BlockMoveType.Down, Vector3.down);
        blockMoveTypeDict.Add(BlockMoveType.Left, Vector3.left);
        blockMoveTypeDict.Add(BlockMoveType.Right, Vector3.right);
    }
}

public enum BlockMoveType
{
    Down,
    Left,
    Right
}