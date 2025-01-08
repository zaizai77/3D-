using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Useable,Weapon,Armor,
}

[CreateAssetMenu(fileName = "New Item",menuName = "Inventory/Item Data")]
public class ItemData_SO : ScriptableObject
{
    public ItemType itemType;
    public string itemName;
    public Sprite itemIcon;
    public int itemAmount;

    [TextArea]
    public string description = "";
    public bool stackable; // 是否可以堆叠（武器不可以，奶酪可以）

    [Header("Weapon")]
    public GameObject weaponPrefab;
}
