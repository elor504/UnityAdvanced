using System;

[Serializable]
public class Weapon
{
	public string WeaponName;
	public int weaponBaseDamage;
	public int weaponLevel;
	public int currentDamage
	{
		get
		{
			return weaponLevel * weaponBaseDamage;
		}
	}


	public int weaponBuyBase;
	public int currentBuy
	{
		get
		{
			if (weaponLevel == 0)
				return weaponBuyBase;
			else
				return weaponBuyBase * (weaponLevel + 1);
		}
	}



	public Weapon(string name, int baseDamage, int Level, int buybase)
	{
		WeaponName = name;
		weaponBaseDamage = baseDamage;
		weaponLevel = Level;
		weaponBuyBase = buybase;
	}

}
