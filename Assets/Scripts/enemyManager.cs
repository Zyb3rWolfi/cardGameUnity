using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class enemyManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private int enemyHealth;
    [SerializeField] private int enemyDmg;

    [SerializeField] private int _enemyId;
    public static event Action<string> onEnemySelected;
    public static event Action<int> attackPlayer;

    private void Start()
    {
        _enemyId = gameManager.RegisterEnemy(this);
    }

    private void OnEnable()
    {
        gameManager.executeAttack += EnemyAttacked;
        gameManager.enemyTurn += EnemyTurn;
    }
    private void OnDisable()
    {
        gameManager.executeAttack -= EnemyAttacked;
        gameManager.enemyTurn -= EnemyTurn;

    }
    private void OnMouseDown()
    {
        onEnemySelected?.Invoke("normal");
    }

    private void EnemyAttacked(int dmg)
    {
        enemyHealth -= dmg;
        _healthText.text = $"{enemyHealth}/10";
    }

    private void EnemyTurn(int id)
    {
        if (id == _enemyId)
        {
            attackPlayer?.Invoke(enemyDmg);
            print($"{_enemyId}: Attacked");
        }
    }
}
