using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public static event Action<int, int> executeAttack;
    public static event Action<int, int> destroyCard;
    public static event Action<int> enemyTurn;
    private cardScriptable cardSelected;
    private int cardID;
    private static int enemyCounter = 0;
    private int _shield = 0;
    [SerializeField] private int enemyAmount;
    [SerializeField] private discardPileScriptable discardPile;
    [SerializeField] private discardPileScriptable currentPile;
    [SerializeField] private GameObject cardTemplate;
    [SerializeField] private GameObject placeholder;
    [SerializeField] private float spacing;
    [SerializeField] private TextMeshProUGUI _shieldsUi;
        static Dictionary<enemyManager, int> enemyIdMap = new Dictionary<enemyManager, int>();
    public int queueNum = 1;

    public static int RegisterEnemy(enemyManager enemy)
    {
        int enemyId = enemyCounter;
        enemyIdMap.Add(enemy, enemyId);
        enemyCounter++;
        return enemyId;
    }

    
    public static int RestartDict(enemyManager enemy)
    {
        enemyCounter = 0;
        enemyIdMap.Clear();
        int enemyId = enemyCounter;
        enemyIdMap.Add(enemy, enemyId);
        enemyCounter++;
        return enemyId;
    }

    private int _energy = 3;
    private int _playerHealth = 50;
    [SerializeField] private TextMeshProUGUI healthUi;
    [SerializeField] private TextMeshProUGUI energyUi;
    

    private void OnEnable()
    {
        cardManager.onCardSelected += CardSelected;
        enemyManager.onEnemySelected += StartAttack;
        enemyManager.attackPlayer += PlayerTakeDmg;
    }

    private void OnDisable()
    {
        cardManager.onCardSelected -= CardSelected;
        enemyManager.onEnemySelected -= StartAttack;
        enemyManager.attackPlayer -= PlayerTakeDmg;
    }

    private void Start()
    {
        discardPile.discardedCards.Clear();
        PopulateGrid();
    }

    private void CardSelected(cardScriptable selectedCard, int id)
    {
        cardID = id;
        cardSelected = selectedCard;
        switch (selectedCard.type)
        {
            case "defence":
                _shield += selectedCard.defenceAmount;
                _shieldsUi.text = $"{_shield}";
                DefenceCardClicked();
                break;
        }
        print("Current card selected: " + selectedCard.id + "  " + id);
    }
    private void PopulateGrid()
    {
        for (int i = 0; i < currentPile.discardedCards.Count; i++)
        {
            float posx = i * spacing;
            GameObject item = Instantiate(cardTemplate, Vector3.zero, Quaternion.identity);
            cardManager card = item.GetComponent<cardManager>();
            card.cardType = currentPile.discardedCards[i];
            item.transform.parent = placeholder.transform;
            item.transform.localPosition = new Vector3(posx, 0, 0);
        } 
    }

    private void DefenceCardClicked()
    {
        if (cardSelected.energyAmount <= _energy)
        {
            _energy -= cardSelected.energyAmount;
            energyUi.text = $"{_energy}/3";
            discardPile.discardedCards.Add(cardSelected);
            destroyCard?.Invoke(cardSelected.id, cardID);
        }
    }
    private void StartAttack(int enemy)
    {
        if (_energy == 0)
        {
            return;
        }

        switch (cardSelected.type)
        {
            case "attack":
                if (cardSelected.id != -1)
                {
                    if (cardSelected.energyAmount <= _energy)
                    {
                        _energy -= cardSelected.energyAmount;
                        energyUi.text = $"{_energy}/3";
                        executeAttack?.Invoke(enemy, cardSelected.damage); 
                        discardPile.discardedCards.Add(cardSelected);
                        destroyCard?.Invoke(cardSelected.id, cardID);
                        
                    }
                }

                break;
            
        }
    }
    
    // Runs when end round button is clicked
    public void HandleEndRoundClick()
    {
        if (queueNum == 0)
        {
            enemyTurn?.Invoke(queueNum);
            queueNum++;
        }
        else
        {
            queueNum = 0;
            enemyTurn?.Invoke(queueNum);
            queueNum++;
        }
    }
    public void EndRound()
    {
        print("ending round");
        _energy = 3;
        queueNum = 0;
        _shield = 0;
        _shieldsUi.text = $"{_shield}";
        energyUi.text = $"{_energy}/3";
        
        currentPile.discardedCards.Clear();
        
        for (int i = 0; i < discardPile.discardedCards.Count; i++)
        {
            print(i);
            cardScriptable card = discardPile.discardedCards[i];
            currentPile.discardedCards.Add(card);
            
        }
        
        discardPile.discardedCards.Clear();
        PopulateGrid();
    }

    private void PlayerTakeDmg(int dmg)
    {
        _shield -= dmg;
        _playerHealth += _shield;
        healthUi.text = $"{_playerHealth}/50";
        queueNum++;
        enemyTurn?.Invoke(queueNum);
        
        if (queueNum == enemyIdMap.Count)
        {
            EndRound();
        }

    }


}
