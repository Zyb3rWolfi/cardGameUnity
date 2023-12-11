using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class cardManager : MonoBehaviour
{
    public static event Action<cardScriptable> onCardSelected;
    public cardScriptable cardType;

    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI energy;

    // Start is called before the first frame update
    void Start()
    {
        title.text = cardType.name;
        description.text = cardType.description;
        energy.text = $"{cardType.energyAmount}";
    }

    private void OnEnable()
    {
        gameManager.destroyCard += DestroyCard;
    }

    private void OnDisable()
    {
        gameManager.destroyCard -= DestroyCard;
    }

    private void DestroyCard(int id)
    {
        if (id == cardType.id)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnMouseDown()
    {
        print("Clicked");
        onCardSelected?.Invoke(cardType);
    }
}
