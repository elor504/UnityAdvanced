using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoginManager : MonoBehaviour
{

	public GameObject gamePanel;


	[Header("Login")]
	public GameObject loginPanel;
	public TMP_InputField emailInput;
	public TMP_InputField passwordInput;

	string emailToLogin;
	string passwordToLogin;



	[Header("Register")]
	public GameObject registerPanel;
	public TMP_InputField registerUserNameInput;
	public TMP_InputField registerEmailInput;
	public TMP_InputField registerPasswordInput;

	string userNameToRegister;
	string emailToRegister;
	string passwordToRegister;


	string playfabID;

	private void Awake()
	{
		if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
		{
			PlayFabSettings.TitleId = "AF62F";
		}
	}


	#region Login_Related

	public void OnLoginButtonClicked()
	{

	}

	public void OnRegisterPanelButtonClicked()
	{
		registerPanel.SetActive(true);
		loginPanel.SetActive(false);
	}

	public void OnChangeEmailInput()
	{
		emailToLogin = emailInput.text;
	}

	public void OnChangePasswordInput()
	{
		passwordToLogin = passwordInput.text;
	}









	#endregion

	#region Register_Related

	public void ReturnToLogin()
	{
		registerPanel.SetActive(false);
		loginPanel.SetActive(true);
	}



	public void OnChangeRegisterUserNameInput()
	{
		userNameToRegister = registerUserNameInput.text;
	}



	public void OnChangeRegisterEmailInput()
	{
		emailToRegister = registerEmailInput.text;
	}

	public void OnChangeRegisterPasswordInput()
	{
		passwordToRegister = registerPasswordInput.text;
	}






	#endregion


	public void OnSuccesfullyLogin()
	{
		gamePanel.SetActive(true);
		loginPanel.SetActive(false);
		registerPanel.SetActive(false);
		GameManager.getInstance.InitGame();
	}



	#region playFab_Related


	// login
	public void OnClickLogin()
	{
		var request = new LoginWithEmailAddressRequest { Email = emailToLogin, Password = passwordToLogin };
		PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
	}

	private void OnLoginFailure(PlayFabError obj)
	{
		Debug.LogError(obj);
	}

	private void OnLoginSuccess(LoginResult obj)
	{
		playfabID = obj.PlayFabId;



		var request = new GetUserDataRequest { PlayFabId = playfabID };

		PlayFabClientAPI.GetUserData(request, OnSuccessGettingPlayerData, OnLoginFailure);
		//get player info

	
	}

	void OnSuccessGettingPlayerData(GetUserDataResult result)
	{
		if (result.Data != null && result.Data.ContainsKey("Player Data"))
		{
			GameManager.getInstance.playerData = JsonUtility.FromJson<PlayerData>(result.Data["Player Data"].Value);
			Debug.Log("Getting Data");
			OnSuccesfullyLogin();
		}
		else
		{

		}
	}


	// register
	public void OnClickRegister()
	{
		if(registerPasswordInput.text != "" && registerEmailInput.text != "" && registerUserNameInput.text != "")
		{
			var request = new RegisterPlayFabUserRequest { Username = userNameToRegister, Email = emailToRegister, Password = passwordToRegister };
			PlayFabClientAPI.RegisterPlayFabUser(request, SetNewPlayInfo, OnLoginFailure);

		}
		else
		{
			Debug.Log("something is empty");
		}


		
	}

	void SetNewPlayInfo(RegisterPlayFabUserResult result)
	{
		PlayerData newPlayerData = new PlayerData(userNameToRegister);

		GameManager.getInstance.playerData = newPlayerData;

		Dictionary<string, string> newPlayerDataDic = new Dictionary<string, string>()
		{
			{"Player Data", JsonUtility.ToJson(newPlayerData)}
		};

		Debug.Log("Setting new Data");
		var request = new UpdateUserDataRequest { Data = newPlayerDataDic };
		PlayFabClientAPI.UpdateUserData(request, OnSuccessfullyUpdate, OnLoginFailure);


		
		var loginRequest = new LoginWithEmailAddressRequest { Email = emailToRegister, Password = passwordToRegister };
		PlayFabClientAPI.LoginWithEmailAddress(loginRequest, OnLoginSuccess, OnLoginFailure);


	}

	public void SavePlayerData()
	{

		Dictionary<string, string> newPlayerDataDic = new Dictionary<string, string>()
		{
			{"Player Data", JsonUtility.ToJson(GameManager.getInstance.playerData)}
		};


		var request = new UpdateUserDataRequest { Data = newPlayerDataDic };
		PlayFabClientAPI.UpdateUserData(request, OnSuccessfullyUpdate, OnLoginFailure);

	}

	void OnSuccessfullyUpdate(UpdateUserDataResult result)
	{
		Debug.Log("Successfully Saved");
	}





	#endregion

}
