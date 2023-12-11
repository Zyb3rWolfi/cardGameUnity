using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Object/Create New Enemy", order = 1)]
public class enemyScriptableClass : ScriptableObject
{
    public string name;
    public int dmg;
    public int health;

}
