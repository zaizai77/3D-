using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Date",menuName ="Character Status/Data")]

public class CharacterDate_SO : ScriptableObject
{
    [Header("Status Info")]
    public int maxHealth;
    public int currentHealth;
    public int baseDefence;
    public int currentDefence;

    [Header("kill")]
    public int killPoint;

    [Header("Level")]
    public int currentLevel;
    public int maxLevel;
    public int baseExp;
    public int currentExp;
    public float levelBuff;

    public float levelMultiplier
    {
        get { return 1 * (currentLevel - 1) * levelBuff; }
    }

    public void UpdateExp(int point)
    {
        currentExp += point;

        while(currentExp >= baseExp)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        //提升本领的数据方法
        currentLevel = Mathf.Clamp(currentLevel + 1, 0, maxLevel);
        baseExp += 10;
        GameManager.Instance.playerStatus.attackData.minDamage += 2;
        GameManager.Instance.playerStatus.attackData.maxDamage += 2;
        maxHealth = (int)(maxHealth * (1 + levelMultiplier));
        currentHealth = maxHealth;

        Debug.Log("Level Up!" + currentLevel + "Max Health:" + maxHealth);
    }
}
