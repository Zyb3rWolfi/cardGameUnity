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
    [SerializeField] private enemyScriptableClass enemyType;
    public static event Action<int> onEnemySelected;
    public static event Action<int> attackPlayer;
    public static event Action restartDictionary;

    private void Start()
    {
        _enemyId = gameManager.RegisterEnemy(this);
    }

    private void OnEnable()
    {
        gameManager.executeAttack += EnemyAttacked;
        gameManager.enemyTurn += EnemyTurn;
        restartDictionary += RestartIdDictionary;
        _healthText.text = $"{enemyType.health}/{enemyType.health}";
        enemyHealth = enemyType.health;
        enemyDmg = enemyType.dmg;
    }
    private void OnDisable()
    {
        gameManager.executeAttack -= EnemyAttacked;
        gameManager.enemyTurn -= EnemyTurn;
        restartDictionary -= RestartIdDictionary;
        

    }
    private void OnMouseDown()
    {
        onEnemySelected?.Invoke(_enemyId);
    }

    private void RestartIdDictionary()
    {
        _enemyId = gameManager.RestartDict(this);
    }
    private void EnemyAttacked(int id, int dmg)
    {
        if (id == _enemyId)
        {
            enemyHealth -= dmg;
            _healthText.text = $"{enemyHealth}/{enemyType.health}";

            if (enemyHealth <= 0)
            {
                Destroy(this.gameObject);
                restartDictionary?.Invoke();
            }
        }
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
