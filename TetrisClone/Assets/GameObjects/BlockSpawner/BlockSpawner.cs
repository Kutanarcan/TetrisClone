using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField]
    BlockDataSO blockDataSO;

    public static event System.Action OnGameFinished;

    private void Awake()
    {
        GridManager.OnBlockReachEndOfTheMove += CreateRandomBlock;
        UI_StartMenu.OnStartButtonPressed += CreateRandomBlock;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            CreateRandomBlock();
    }

    private void OnDestroy()
    {
        GridManager.OnBlockReachEndOfTheMove -= CreateRandomBlock;
        UI_StartMenu.OnStartButtonPressed -= CreateRandomBlock;
    }

    public void CreateRandomBlock()
    {
        int randomIndex = Random.Range(0, blockDataSO.blockDatas.Count);
        //int randomIndex = Random.Range(0, 2);

        int startPointIndex = GridGenerator.Instance.GetValidIndexWithCoordinate(blockDataSO.blockDatas[randomIndex].startPoint);

        if (GridGenerator.Instance.GetItemByIndex(startPointIndex).HasItem != null)
        {
            OnGameFinished?.Invoke();
            return;
        }

        Block block = Instantiate(blockDataSO.blockTemplate);
        block.name = blockDataSO.blockDatas[randomIndex].name;
        block.transform.position = blockDataSO.blockDatas[randomIndex].startPoint;
        block.SetColor(blockDataSO.blockDatas[randomIndex].color);
        block.InitializeCoordinates(blockDataSO.blockDatas[randomIndex].coordinates);
        block.PositionChildBlocks();
        block.RotationPoint = blockDataSO.blockDatas[randomIndex].rotationPoint;
    }
}
