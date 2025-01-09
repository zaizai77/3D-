using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Character_Status : MonoBehaviour
{
    public event Action<int, int> UpdateHealthBarOnAttack;
    public CharacterDate_SO characterData;
    public CharacterDate_SO templateData;
    public AttackData_SO attackData;

    [Header("Weapon")]
    public Transform weaponSlot;

    [HideInInspector]
    public bool isCritical;

    private void Awake()
    {
        if(templateData != null)
        {
            characterData = Instantiate(templateData);
        }
    }



    #region read from Data_SO

    public int MaxHealth
    {
        get { if (characterData != null) return characterData.maxHealth; else return 0; }
        set { characterData.maxHealth = value; }

    }
    public int CurrentHealth
    {
        get { if (characterData != null) return characterData.currentHealth; else return 0; }
        set { characterData.currentHealth = value; }

    }
    public int BaseDefence
    {
        get { if (characterData != null) return characterData.baseDefence; else return 0; }
        set { characterData.baseDefence = value; }

    }
    public int CurrentDefence
    {
        get { if (characterData != null) return characterData.currentDefence; else return 0; }
        set { characterData.currentDefence = value; }

    }

    #endregion

    #region Character Combot

    public void TakeDamage(Character_Status attacker,Character_Status defener)
    {
        if(defener.CurrentHealth != 0)
        {
            if (defener.CurrentHealth != 0)
            {
                int coreDamage = Mathf.Max(attacker.CurrentDamage() - defener.CurrentDefence, 0);
                CurrentHealth = Mathf.Max(CurrentHealth - coreDamage, 0);
                if (attacker.isCritical)
                {
                    defener.GetComponent<Animator>().SetTrigger("Hit");
                }
                UpdateHealthBarOnAttack?.Invoke(CurrentHealth, MaxHealth);
            }
            if (CurrentHealth <= 0)
            {
                attacker.characterData.UpdateExp(characterData.killPoint);
            }
        }
    }

    public void TakeDamage(int damage,Character_Status defener)
    {
        int currentDamage = Mathf.Max(damage - defener.CurrentDefence, 0);
        CurrentHealth = Mathf.Max(CurrentHealth - currentDamage, 0);
        UpdateHealthBarOnAttack?.Invoke(CurrentHealth, MaxHealth);
    }

    private int CurrentDamage()
    {
        float coreDamage = UnityEngine.Random.Range(attackData.minDamage, attackData.maxDamage);
        if(isCritical)
        {
            coreDamage *= attackData.criticalMultiplier;
            Debug.Log("暴击！" + coreDamage);
        }
        return (int)coreDamage;
    }

    #endregion

    #region Equip Weapon

    public void EquipWeapon(ItemData_SO weapon)
    {
        if(weapon.weaponPrefab != null)
        {
            //会保持物体原有的 position 和 rotation
            Instantiate(weapon.weaponPrefab, weaponSlot);

            //切换属性 将人物的攻击属性更改
            attackData.ApplyWeaponData(weapon.weaponData);
        }
    }

    #endregion
}
