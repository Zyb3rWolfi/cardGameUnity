using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "enemiesSpawnType", menuName = "Spawns/Create New Spawn", order = 0)]
public class enemiesspawnScriptable : ScriptableObject {
    public List<GameObject> enemies = new List<GameObject>();
}
