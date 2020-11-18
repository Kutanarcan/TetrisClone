using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Block Datas",menuName ="ScriptableObjects/BlockDatas")]
public class BlockDataSO : ScriptableObject
{
    public Block blockTemplate;
    public List<BlockData> blockDatas = new List<BlockData>();
}

[System.Serializable]
public class BlockData
{
    public string name;
    public Color color;
    public Vector2[] coordinates = new Vector2[4];
    public Vector3 rotationPoint;
    public Vector3 startPoint;
}