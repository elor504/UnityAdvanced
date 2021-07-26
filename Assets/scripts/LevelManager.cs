using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;
    public static LevelManager getInstance => _instance;


    [Header("References")]
    public Image monsterImage;
    public RegionSO[] regions;


    [Header("Current Region Info")]
    public GameRegions currentRegion;
    public int currentRegionLevel;



    [Header("Current Mob Info")]
    MonsterSO currentMonsterInfo;
    public string currentMobName;
    public int currentHP;


	private void Awake()
	{
		if(_instance == null)
		{
            _instance = this;
		}
        else if(getInstance != this)
		{
            Destroy(this.gameObject);
		}
	}




	public void SetAsFirstRegion()
	{
        //only for new players

        currentRegion = GameRegions.GrassLand;

        SetMonsterByRegionAndIndex(currentRegion, 0);
    }




    void SetMonsterByRegionAndIndex(GameRegions region, int index)
	{
        int regionIndex = (int)region;

        currentMonsterInfo = regions[regionIndex].monsters[index];

        SetMonsterInfo();
    }


    void SetMonsterInfo()
	{
        currentMobName = currentMonsterInfo.monsterName;
        currentHP = currentMonsterInfo.monsterHP;
    }


    public void DealDamage(int damage)
	{
        currentHP -= damage;

        if (currentHP <= 0)
        {
            RewardPlayerForKilling();
        }
           
      
    }


    void RewardPlayerForKilling()
	{
        GameManager.getInstance.playerData.playerCurrency += currentMonsterInfo.monsterReward;
        SetMonsterInfo();
        UIManager.getInstance.ShowPlayerCurrency();
        UIManager.getInstance.UpdatePlayerDamageInUI();
    }



}
