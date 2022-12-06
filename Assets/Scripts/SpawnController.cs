using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnController : MonoBehaviour
{
    [SerializeField] protected GameObject prefabToSpawn;
    [SerializeField] protected int amount = 1;

    [SerializeField] protected float randomAspect = .2f;
    

    protected virtual void Spawn(){
        for (int i = 0; i < amount; i++){
            GameObject spawnedObj = (GameObject) Instantiate(prefabToSpawn, transform.position, Quaternion.identity);

            spawnedObj.name = prefabToSpawn.name + " - " + i;

            EnemyAI enemyAI =  spawnedObj.GetComponent<EnemyAI>();

            enemyAI.followThreshold = Random.Range((1-randomAspect)*enemyAI.followThreshold, (1+randomAspect)*enemyAI.followThreshold);
            enemyAI.targetDistanceToPlayer = Random.Range((1-randomAspect) * enemyAI.targetDistanceToPlayer, (1+randomAspect) * enemyAI.targetDistanceToPlayer);
        }
    }
}
