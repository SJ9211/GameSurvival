using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 커스텀 메뉴를 생성하는 속성
[CreateAssetMenu(fileName = "Item", menuName = "Scriptble object/ItemData")]
public class ItemData : ScriptableObject
{
    // 기여 아이템 구분 type
    public enum ItemType { Melee, Range, Glove, Shoe, Heal} //근접,원거리,장갑,신발,물약
        
    [Header("# Main Info")]
    public ItemType itemType;
    public int itemId;
    public string itemName;
    public string itemDesc;
    public Sprite itemIcon;

    [Header("# Level Data")]
    public float baseDamage;
    public int baseCount;
    public float[] damages;
    public int[] counts;

    [Header("# Weapon")]
    public GameObject projectile;

}
