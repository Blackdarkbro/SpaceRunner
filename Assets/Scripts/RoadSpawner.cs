using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    [SerializeField] private GameObject roadBlockPrefab;
    [SerializeField] private GameObject startBlock;

    [SerializeField] private Transform shipTrans;

    private readonly Queue<GameObject> currentBlocks = new Queue<GameObject>();
    private readonly int safeZone = 40;

    private float startBlockZPos,
        currentBlockZPos;

    private readonly float blocksCount = 20;

    private float blockLenght = 15;

    private readonly Vector3 startPlayerPos = Vector3.zero;

    private void Start()
    {
        startBlockZPos = startBlock.transform.position.z;
        blockLenght = startBlock.GetComponent<BoxCollider>().bounds.size.z;

        StartGame();
    }

    private void Update()
    {
        CheckForSpawn();
    }

    private IEnumerable<GameObject> GetBloks()
    {
        return currentBlocks;
    }

    public void StartGame()
    {
        currentBlockZPos = startBlockZPos;

        // restart ship position
        shipTrans.gameObject.SetActive(true);
        shipTrans.position = startPlayerPos;
        shipTrans.rotation = Quaternion.identity;

        foreach (var go in currentBlocks) Destroy(go);
        currentBlocks.Clear();

        for (var i = 0; i < blocksCount; i++) SpawnBlock();
    }

    private void CheckForSpawn()
    {
        if (shipTrans.position.z - safeZone > currentBlockZPos - blocksCount * blockLenght)
        {
            SpawnBlock();
            DestroyBlock();
        }
    }

    private void SpawnBlock()
    {
        currentBlockZPos += blockLenght;

        var block = Instantiate(roadBlockPrefab, new Vector3(0, -2, currentBlockZPos), Quaternion.identity);

        currentBlocks.Enqueue(block);
    }

    private void DestroyBlock()
    {
        Destroy(currentBlocks.Dequeue());
    }
}