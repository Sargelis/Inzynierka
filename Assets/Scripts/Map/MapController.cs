using System.Collections;
using System.Collections.Generic;
using System.Net.Cache;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] List<GameObject> terrainChunks;
    [SerializeField] GameObject player;
    [SerializeField] float checkerRadius;
    [SerializeField] LayerMask terrainMask;
    [HideInInspector] public GameObject currentChunk; //musi public
    Vector3 playerLastPosition;

    [Header("Optimization")]
    [SerializeField] List<GameObject> spawnedChunks;
    GameObject latestChunk;
    [SerializeField] float optimizerCooldownDur;
    [SerializeField] float maxOpDist; //wiêksze od obu width and lenght
    float opDist;
    float optimizerCooldown;

    void Start()
    {
        playerLastPosition = player.transform.position;
    }
    void Update()
    {
        ChunkChecker();
        ChunkOptimizer();
    }

    void ChunkChecker()
    {
        if (!currentChunk) return;

        Vector3 moveDir = player.transform.position - playerLastPosition;
        playerLastPosition = player.transform.position;

        string directionName = GetDirectionName(moveDir);

        CheckAndSpawnChunk(directionName);

        if (directionName.Contains("Up"))
        {
            CheckAndSpawnChunk("Up");
            CheckAndSpawnChunk("Left Up");
            CheckAndSpawnChunk("Right Up");
        }
        if (directionName.Contains("Down"))
        {
            CheckAndSpawnChunk("Down");
            CheckAndSpawnChunk("Left Down");
            CheckAndSpawnChunk("Right Down");
        }
        if (directionName.Contains("Right"))
        {
            CheckAndSpawnChunk("Right");
            CheckAndSpawnChunk("Right Down");
            CheckAndSpawnChunk("Right Up");
        }
        if (directionName.Contains("Left"))
        {
            CheckAndSpawnChunk("Left");
            CheckAndSpawnChunk("Left Up");
            CheckAndSpawnChunk("Left Down");
        }
    }
    void CheckAndSpawnChunk(string direction)
    {
        if (!Physics2D.OverlapCircle(currentChunk.transform.Find(direction).position, checkerRadius, terrainMask))
        {
            SpawnChunk(currentChunk.transform.Find(direction).position);
        }
    }
    void SpawnChunk(Vector3 spawnPosition)
    {
        int rand = Random.Range(0, terrainChunks.Count);
        latestChunk = Instantiate(terrainChunks[rand], spawnPosition, Quaternion.identity);
        spawnedChunks.Add(latestChunk);
    }
    void ChunkOptimizer()
    {
        optimizerCooldown -= Time.deltaTime;

        if (optimizerCooldown <= 0) optimizerCooldown = optimizerCooldownDur;
        else return;

        foreach (GameObject chunk in spawnedChunks)
        { 
            opDist = Vector3 .Distance(player.transform.position, chunk.transform.position);
            if(opDist > maxOpDist) chunk.SetActive(false);
            else chunk.SetActive(true);
        }
    }
    string GetDirectionName(Vector3 direction)
    {
        direction = direction.normalized;
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) 
        {
            //movment horizontal more than vertical
            if (direction.y > 0.5f)
            {
                return direction.x > 0 ? "Right Up" : "Left Up";
            }
            else if (direction.y < -0.5f)
            {
                return direction.x > 0 ? "Right Down" : "Left Down";
            }
            else
            {
                return direction.x > 0 ? "Right" : "Left";
            }
        }
        else 
        {
            //movment vertical more than horizontal
            if (direction.x > 0.5f)
            {
                return direction.y > 0 ? "Right Up" : "Right Down";
            }
            else if (direction.x < -0.5f)
            {
                return direction.y > 0 ? "Left Up" : "Left Down";
            }
            else
            {
                return direction.y > 0 ? "Up" : "Down";
            }
        }
    }
}