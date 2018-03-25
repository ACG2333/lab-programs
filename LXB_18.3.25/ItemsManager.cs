using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class ItemsManager : MonoBehaviour {

    private Items backPack;
    private Items receiveItems;

	void Start () {
        backPack = new Items();
        backPack.itemList = new List<Item>();//重要!!
	}
	
	void Update () {

        /*创建装备物品*/
        if (Input.GetKeyDown(KeyCode.E))
        {
            Item item = new Equipment(1001, "头盔", 5);
            backPack.itemList.Add(item);
            print("创建装备成功");
        }

        /*创建食品物品*/
        if (Input.GetKeyDown(KeyCode.F))
        {
            Item item = new Food(2001, "薯条", 10);
            backPack.itemList.Add(item);
            print("创建食品成功");
        }

        /*写入json*/
        if (Input.GetKeyDown(KeyCode.W))
        {
            File.WriteAllText(Application.dataPath + @"\\ItemsJson.json", JsonUtility.ToJson(backPack));
            print("写入json成功");
        }

        /*读取json*/
        if (Input.GetKeyDown(KeyCode.R))
        {
            receiveItems = JsonUtility.FromJson<Items>(File.ReadAllText(Application.dataPath + @"\\ItemsJson.json"));
            print("读取json成功");
        }

    }
}

/*------------------------------------------------类------------------------------------------------*/

/// <summary>
/// 物品列表类
/// </summary>
[Serializable]
public class Items
{
    /// <summary>
    /// 物品列表
    /// </summary>
    public List<Item> itemList;
}

/// <summary>
/// 单个物品信息类
/// </summary>
[Serializable]
public class Item
{
    /// <summary>
    /// 物品ID
    /// </summary>
    public int id;
    /// <summary>
    /// 物品名称
    /// </summary>
    public string name;
    /// <summary>
    /// 物品描述
    /// </summary>
    public string description;
    /// <summary>
    /// 是否是消耗品
    /// </summary>
    public bool consumable;
    /// <summary>
    /// 是否可叠加
    /// </summary>
    public bool countable;
}

/// <summary>
/// 装备类
/// </summary>
public class Equipment : Item
{
    /// <summary>
    /// 防御力
    /// </summary>
    public float defencePower;

    /// <summary>
    /// 构造函数
    /// 默认装备 可消耗、不可叠加
    /// </summary>
    /// <param name="id">物品的ID</param>
    /// <param name="name">物品的名称</param>
    /// <param name="defencePower">装备的防御力</param>
    public Equipment(int id,string name,float defencePower)
    {
        base.id = id;
        base.name = name;
        this.defencePower = defencePower;
        /*默认装备为消耗品*/
        consumable = true;
        /*默认装备不可叠加*/
        countable = false;
    }
}

/// <summary>
/// 食品类
/// </summary>
public class Food : Item
{
    /// <summary>
    /// 增加的血量
    /// </summary>
    public float incraseHp;

    /// <summary>
    /// 构造函数
    /// 默认食品 可消耗、可叠加
    /// </summary>
    /// <param name="id">物品的ID</param>
    /// <param name="name">物品的名称</param>
    /// <param name="incraseHp">食物增加的血量</param>
    public Food(int id, string name,float incraseHp)
    {
        base.id = id;
        base.name = name;
        this.incraseHp = incraseHp;
        /*默认食品为消耗品*/
        consumable = true;
        /*默认食品可叠加*/
        countable = true;
    }
}