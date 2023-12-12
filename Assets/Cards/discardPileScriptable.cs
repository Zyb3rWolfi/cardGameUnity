using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewDiscard", menuName = "Object/Create New Discard Pile", order = 1)]
public class discardPileScriptable : ScriptableObject
{
    public List<cardScriptable> discardedCards = new List<cardScriptable>();
}
