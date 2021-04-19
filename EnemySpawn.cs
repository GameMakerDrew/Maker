using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawn : MonoBehaviour
{

    public GameObject enemyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 1f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {
        Vector3 randomPos = new Vector3(Random.Range(-100f,0),0f, Random.Range(-100f, 0f));
        Debug.Log(randomPos);
        Instantiate(enemyPrefab, randomPos, enemyPrefab.transform.rotation);
    }
}
