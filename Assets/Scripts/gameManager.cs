using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class gameManager : MonoBehaviour
{
    public static event Action<int> executeAttack;
    public static event Action<int> enemyTurn;
    private int cardSelectedId;
    private static int enemyCounter = 1;
    [SerializeField] private int enemyAmount;
    static Dictionary<enemyManager, int> enemyIdMap = new Dictionary<enemyManager, int>();
    [SerializeField] private int queueNum = 1;
    [SerializeField] private int amount;

    public static int RegisterEnemy(enemyManager enemy)
    {
        int enemyId = enemyCounter;
        enemyIdMap.Add(enemy, enemyId);
        enemyCounter++;
        return enemyId;
    }

    private int _energy = 3;
    private int _playerHealth = 50;
    [SerializeField] private TextMeshProUGUI healthUi;
    [SerializeField] private TextMeshProUGUI energyUi;

    private void Start()
    {
        amount = enemyIdMap.Count;
    }

    private void OnEnable()
    {
        cardManager.onCardSelected += CardSelected;
        enemyManager.onEnemySelected += startAttack;
        enemyManager.attackPlayer += PlayerTakeDmg;
    }

    private void OnDisable()
    {
        cardManager.onCardSelected -= CardSelected;
        enemyManager.onEnemySelected -= startAttack;
        enemyManager.attackPlayer -= PlayerTakeDmg;
    }

    private void CardSelected(int id)
    {
        cardSelectedId = id;
        print("Current card selected: " + id);
        amount = enemyIdMap.Count;
    }

    private void startAttack(string enemy)
    {
        if (_energy == 0)
        {
            return;
        }
        
        if (cardSelectedId != -1)
        {
            _energy--;
            energyUi.text = $"{_energy}/3";
            executeAttack?.Invoke(10);
        }
    }

    public void StartEnemyAttack()
    {
        enemyTurn?.Invoke(queueNum);
        queueNum++;
    }

    public void EndRound()
    {
        _energy = 3;
        queueNum = 0;
        energyUi.text = $"{_energy}/3";
    }

    private void PlayerTakeDmg(int dmg)
    {
        if (queueNum == enemyIdMap.Count)
        {
            EndRound();
            return;
        }
        if (queueNum <= enemyIdMap.Count)
        {
            StartCoroutine(waitForNext(dmg));
            
        }
        
    }

    private IEnumerator waitForNext(int dmg)
    {
        print("run this" + queueNum);
        yield return new WaitForSeconds(1.0f);
        print("running");
        queueNum++;
        enemyTurn?.Invoke(queueNum);
        _playerHealth -= dmg;
        healthUi.text = $"{_playerHealth}/50";
        print("run this" + queueNum);
    } 


}
