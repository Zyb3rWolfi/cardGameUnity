using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public static event Action<int, int> executeAttack;
    public static event Action<int> destroyCard;
    public static event Action<int> enemyTurn;
    private cardScriptable cardSelected;
    private static int enemyCounter = 0;
    [SerializeField] private int enemyAmount;
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

    private void CardSelected(cardScriptable selectedCard)
    {
        cardSelected = selectedCard;
        print("Current card selected: " + selectedCard.id);
    }

    private void StartAttack(int enemy)
    {
        if (_energy == 0)
        {
            return;
        }
        
        if (cardSelected.id != -1)
        {
            destroyCard?.Invoke(cardSelected.id);
            if (cardSelected.energyAmount <= _energy)
            {
                _energy -= cardSelected.energyAmount;
                energyUi.text = $"{_energy}/3";
                executeAttack?.Invoke(enemy, cardSelected.damage);
                
            }
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
        energyUi.text = $"{_energy}/3";
    }

    private void PlayerTakeDmg(int dmg)
    {
        _playerHealth -= dmg;
        healthUi.text = $"{_playerHealth}/50";
        queueNum++;
        enemyTurn?.Invoke(queueNum);
        
        if (queueNum == enemyIdMap.Count)
        {
            EndRound();
        }

    }


}
