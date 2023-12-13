using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "enemiesSpawnType", menuName = "Spawns/Create New Spawn", order = 0)]
public class enemiesspawnScriptable : ScriptableObject {
    public List<enemyScriptableClass> enemies = new List<enemyScriptableClass>();
    public List<GameObject> enemyPrefab = new List<GameObject>();


}
