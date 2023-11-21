using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public List<int> connectedPlayers = new List<int>();

	#region connectedPlayers Functions
	public void Add(int clientConnectionID)
    {
		connectedPlayers.Add(clientConnectionID);
	}
	public void Remove(int clientConnectionID)
	{
		connectedPlayers.Remove(clientConnectionID);
	}
    public int Count()
    {
        return connectedPlayers.Count;
    }
    #endregion

    void Start()
    {
        NetworkServerProcessing.SetGameLogic(this);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            NetworkServerProcessing.SendMessageToClient("2,Hello client's world, sincerely your network server", 0, TransportPipeline.ReliableAndInOrder);
    }

}
