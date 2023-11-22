using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Gameplay : MonoBehaviour
{
	#region singletone
	public static UI_Gameplay instance;
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(instance);
		}
	}
	#endregion

	public Text connectedPlayer;
	public Text pop;
	int popCount = 0;

	private void Start()
	{
		popCount = 0;
		pop.text = popCount.ToString();
	}

	public void SetConnectedPlayers(int count)
	{
		connectedPlayer.text = count.ToString();
	}

	public void AddPop()
	{
		popCount++;
		pop.text = popCount.ToString();
	}
}