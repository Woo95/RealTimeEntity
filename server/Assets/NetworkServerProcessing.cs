using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class NetworkServerProcessing
{

    #region Send and Receive Data Functions
    static public void ReceivedMessageFromClient(string msg, int clientConnectionID, TransportPipeline pipeline)
    {
        Debug.Log("Network msg received =  " + msg + ", from connection id = " + clientConnectionID + ", from pipeline = " + pipeline);

        string[] csv = msg.Split(',');
        int signifier = int.Parse(csv[0]);

        switch (signifier)
        {
            /*
            case ClientToServerSignifiers.PTC_CONNECTED_PLAYER:
                {

                }
                break;
            */
            /*
			case ClientToServerSignifiers.PTC_BALLOON_LIST:
                {

                }
				break;
            */
			/*
			case ClientToServerSignifiers.PTC_BALLOON_SPAWN:
                {

                }
				break;
            */
			case ClientToServerSignifiers.PTC_BALLOON_POP:
                {

                }
				break;
		}

        //gameLogic.DoSomething();
    }

    static public void SendMessageToClient(string msg, int clientConnectionID, TransportPipeline pipeline)
    {
        networkServer.SendMessageToClient(msg, clientConnectionID, pipeline);
    }

    #endregion

    #region Connection Events
    static public void ConnectionEvent(int clientConnectionID)
    {
        Debug.Log("Client connection, ID == " + clientConnectionID);
        gameLogic.Add(clientConnectionID);

        string msg = ServerToClientSignifiers.PTS_CONNECTED_PLAYER + "," + gameLogic.Count();
        foreach (int connectedPlayerID in gameLogic.m_ConnectedPlayers)
        {
			SendMessageToClient(msg, connectedPlayerID, TransportPipeline.ReliableAndInOrder);
		}
        SendMessageToClient(SendSpawnedBalloonListEvent(), clientConnectionID, TransportPipeline.ReliableAndInOrder);
	}
    static public void DisconnectionEvent(int clientConnectionID)
    {
        Debug.Log("Client disconnection, ID == " + clientConnectionID);
		gameLogic.Remove(clientConnectionID);

		string msg = ServerToClientSignifiers.PTS_CONNECTED_PLAYER + "," + gameLogic.Count();
		foreach (int connectedPlayerID in gameLogic.m_ConnectedPlayers)
		{
			SendMessageToClient(msg, connectedPlayerID, TransportPipeline.ReliableAndInOrder);
		}
	}

    #endregion

    #region Balloon Events
    static public void SpawnBalloonEvent(BalloonData balloonData)
    {
        string msg = ServerToClientSignifiers.PTS_BALLOON_SPAWN + "," + balloonData.ToString();
		foreach (int connectedPlayerID in gameLogic.m_ConnectedPlayers)
		{
			SendMessageToClient(msg, connectedPlayerID, TransportPipeline.ReliableAndInOrder);
		}
	}
    static public string SendSpawnedBalloonListEvent()
    {
		string msg = ServerToClientSignifiers.PTS_BALLOON_LIST.ToString();

        foreach (var spawnedBalloon in gameLogic.m_SpawnedBalloons)
        {
			msg += "," + spawnedBalloon.ToString();
		}
        return msg;     // PTS_BALLOON_LIST,ID,pos.x,pos.y,
	}
    #endregion

    #region Setup
    static NetworkServer networkServer;
    static GameLogic gameLogic;

    static public void SetNetworkServer(NetworkServer NetworkServer)
    {
        networkServer = NetworkServer;
    }
    static public NetworkServer GetNetworkServer()
    {
        return networkServer;
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

