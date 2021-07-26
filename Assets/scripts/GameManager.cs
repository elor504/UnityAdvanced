using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private static GameManager _instance;
	public static GameManager getInstance => _instance;

	public UIManager uIManager;
	public LevelManager levelMananger;

	public PlayerData playerData;


	private void Awake()
	{
		if (_instance == null)
		{
			_instance = this;
		}
		else if (_instance != this)
		{
			Destroy(this.gameObject);
		}
		//CreateNewPlayerData("SilverPoop");

	



	}



	private void Update()
	{
		if (Input.GetMouseButtonDown(0) && uIManager.IsPointerOverUIElement())
		{
				Debug.Log("Deal Damage");
			LevelManager.getInstance.DealDamage(playerData.playerDamage);
		}
	}


	public void InitGame()
	{
		levelMananger.SetAsFirstRegion();
		uIManager.WeaponItemSlotInit();
		UIManager.getInstance.ShowPlayerCurrency();
		UIManager.getInstance.SetPlayerNameInUI();
		UIManager.getInstance.UpdatePlayerDamageInUI();
	}



	#region player data Related
	public void CreateNewPlayerData(string _playerName)
	{
		playerData = new PlayerData(_playerName);
	}

	#endregion
	#region Gameplay_Related










	public void GetMoneyFromClicking(int amount)
	{
		playerData.playerCurrency += amount;
	}




	#endregion




}

[Serializable]
public class PlayerData
{
	public string playerName;
	public int playerCurrency;

	public int playerLevel;
	public GameRegions[] UnlockedRegions;

	public PlayerData(string _name)
	{
		playerName = _name;
		playerCurrency = 0;
		playerLevel = 1;

		//new players unlocks the grassland first for them to start
		UnlockedRegions = new GameRegions[1];
		UnlockedRegions[0] = GameRegions.GrassLand;

		playerWeapons = new Weapon[4]
		{
			new Weapon("Wooden Sword",1,0,5),
			new Weapon("Steel Sword",5,0,10),
			new Weapon("Dark Sword",10,0,20),
			new Weapon("Hero's Sword",20,0,40)
		};
	}






	public int playerBaseDamage = 1;

	public Weapon[] playerWeapons;

	int weaponTotalDamage
	{
		get
		{
			int totalDamage = 0;
			for (int i = 0; i < playerWeapons.Length; i++)
			{
				totalDamage += playerWeapons[i].currentDamage;
			}

			return totalDamage;
		}
	}
	public int playerDamage
	{
		get
		{
			return playerBaseDamage + weaponTotalDamage;
		}
		
	}



}




public enum GameRegions
{
	GrassLand,
	Mushroom_Forest,
	Desert
}