using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyView : CombatanView
{
    [SerializeField] private TMP_Text attackText;
    public int AttackPower {  get;  set; }
    public void Setup(EnemyData enemyData)
    {
        AttackPower = enemyData.AttackPower;
        UpdateAttackText();
        SetupBase(enemyData.AttackPower,enemyData.Image);
    }

    private void UpdateAttackText()
    {
        attackText.text = "ATK:" + AttackPower;
    }

}
