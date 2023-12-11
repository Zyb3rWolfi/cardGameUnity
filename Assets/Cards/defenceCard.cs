using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDefenceCard", menuName = "Object/Create New Card/defense", order = 1)]
public class defenceCard : cardScriptable
{
    public string type = "Defence";
    public int defenceAmount;
}
