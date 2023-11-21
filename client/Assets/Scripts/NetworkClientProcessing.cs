using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class NetworkClientProcessing
{
	#region Send and Receive Data Functions
	static public void SendMessageToServer(string msg, TransportPipeline pipeline)
	{
		networkClient.SendMessageToServer(msg, pipeline);
	}

    static public void ReceivedMessageFromServer(string msg, TransportPipeline pipeline)
    {
        Debug.Log("Network msg received =  " + msg + ", from pipeline = " + pipeline);

        string[] csv = msg.Split(',');
        int signifier = int.Parse(csv[0]);

		switch (signifier)
		{
			case ServerToClientSignifiers.PTS_CONNECTED_PLAYER:
				{
					Debug.Log("PTS_CONNECTED_PLAYER");
					int connectedPlayers = int.Parse(csv[1]);

                    UI_Gameplay.instance.SetConnectedPlayers(connectedPlayers);
				}
				break;
			case ServerToClientSignifiers.PTS_BALLOON_LIST:
				{
					Debug.Log("PTS_BALLOON_LIST");
				}
				break;
			case ServerToClientSignifiers.PTS_BALLOON_SPAWN:
				{
					Debug.Log("PTS_BALLOON_SPAWN");
                    int balloonID = int.Parse(csv[1]); 

                    float x = float.Parse(csv[2]);
					float y = float.Parse(csv[3]);
					Vector2 spawnPos = new Vector2(x, y);

                    gameLogic.SpawnNewBalloon(balloonID, spawnPos);
				}
				break;
			case ServerToClientSignifiers.PTS_BALLOON_POP:
				{
					Debug.Log("PTS_BALLOON_POP");
				}
				break;
		}
	}

    #endregion

    #region Connection Related Functions and Events
    static public void ConnectionEvent()
    {
        Debug.Log("Network Connection Event!");
    }
    static public void DisconnectionEvent()
    {
        Debug.Log("Network Disconnection Event!");
    }
    static public bool IsConnectedToServer()
    {
        return networkClient.IsConnected();
    }
    static public void ConnectToServer()
    {
        networkClient.Connect();
    }
    static public void DisconnectFromServer()
    {
        networkClient.Disconnect();
    }

    #endregion

    #region Setup
    static NetworkClient networkClient;
    static GameLogic gameLogic;

    static public void SetNetworkedClient(NetworkClient NetworkClient)
    {
        networkClient = NetworkClient;
    }
    static public NetworkClient GetNetworkedClient()
    {
        return networkClient;
    }
    static public void SetGameLogic(GameLogic GameLogic)
    {
        gameLogic = GameLogic;
    }

    #endregion

}

#region Protocol Signifiers
static public class ClientToServerSignifiers
{
    public const int PTC_CONNECTED_PLAYER   = 1;
    public const int PTC_BALLOON_LIST       = 2;
    public const int PTC_BALLOON_SPAWN      = 3;
    public const int PTC_BALLOON_POP        = 4;
}

static public class ServerToClientSignifiers
{
	public const int PTS_CONNECTED_PLAYER   = 1;
	public const int PTS_BALLOON_LIST       = 2;
	public const int PTS_BALLOON_SPAWN      = 3;
	public const int PTS_BALLOON_POP        = 4;
}

#endregion

