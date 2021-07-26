using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
public class UIManager : MonoBehaviour
{
	private static UIManager _instance;
	public static UIManager getInstance => _instance;


	public GameManager gameManager => GameManager.getInstance;
	public GameObject itemSlotPF;
	[Header("Player Related")]
	public TextMeshProUGUI playerCurrency;
	public TextMeshProUGUI playerName;
	public TextMeshProUGUI playerDamage;


	[Header("Shop Related")]
	public Transform WeaponPanel;
	public Sprite[] weaponSprites;


	private void Awake()
	{
		if (_instance == null)
		{
			_instance = this;
		}
	}


	#region raycast
	//Returns 'true' if we touched or hovering on Unity UI element.
	public bool IsPointerOverUIElement()
	{
		return IsPointerOverUIElement(GetEventSystemRaycastResults());
	}


	//Returns 'true' if we touched or hovering on Unity UI element.
	private bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
	{
		for (int index = 0; index < eventSystemRaysastResults.Count; index++)
		{
			RaycastResult curRaysastResult = eventSystemRaysastResults[index];
			if (curRaysastResult.gameObject.layer == 8)
				return true;

		}
		return false;
	}
	static List<RaycastResult> GetEventSystemRaycastResults()
	{
		PointerEventData eventData = new PointerEventData(EventSystem.current);
		eventData.position = Input.mousePosition;
		List<RaycastResult> raysastResults = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventData, raysastResults);
		List<RaycastResult> test = new List<RaycastResult>();
		for (int i = 0; i < raysastResults.Count; i++)
		{
			if (raysastResults[i].gameObject.layer == 8)
			{
				test.Add(raysastResults[i]);
			}
		}
		return test;
	}
	#endregion



	#region player_ui_related

	public void SetPlayerNameInUI()
	{
		playerName.text = gameManager.playerData.playerName;
	}

	public void UpdatePlayerDamageInUI()
	{
		playerDamage.text ="Damage: " +  gameManager.playerData.playerDamage.ToString();
	}

	public void ShowPlayerCurrency()
	{
		playerCurrency.text = "Money: " + gameManager.playerData.playerCurrency.ToString();
	}




	#endregion



	public void WeaponItemSlotInit()
	{
		for (int i = 0; i < GameManager.getInstance.playerData.playerWeapons.Length; i++)
		{
			ItemSlot newSlot = Instantiate(itemSlotPF, WeaponPanel).GetComponent<ItemSlot>();

			string name = gameManager.playerData.playerWeapons[i].WeaponName;
			string level = gameManager.playerData.playerWeapons[i].weaponLevel.ToString();
			string damage = gameManager.playerData.playerWeapons[i].currentDamage.ToString();
			string currentCost = gameManager.playerData.playerWeapons[i].currentBuy.ToString();

			newSlot.InitSlot(weaponSprites[i], name, level, damage, i, currentCost);
		}
	}




}

