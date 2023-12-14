using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawnController : MonoBehaviour
{
    [SerializeField] private enemiesspawnScriptable spawnType;
    [SerializeField] private GameObject enemySpawnArea;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < spawnType.enemies.Count; i++)
        {
            float posx = i * 0.2f;
            GameObject enemy = Instantiate(spawnType.enemies[i], Vector3.zero, Quaternion.identity);
            enemyManager manager = enemy.GetComponent<enemyManager>();
            enemy.transform.parent = enemySpawnArea.transform;
            enemy.transform.localPosition = new Vector3(posx, 0, 0);
        }
    }
    
}
