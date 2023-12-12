using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Object/Create New Card", order = 1)]
public class cardScriptable : ScriptableObject
{
    public string cardName;
    public string description;
    public int damage;
    public int id;
    public int energyAmount;
    public string type = "";
    public int defenceAmount;
}
