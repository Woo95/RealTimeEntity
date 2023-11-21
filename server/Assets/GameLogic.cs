using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BalloonData
{
	public int m_ID;
	public Vector2 m_Position;
	public BalloonData(int id, Vector2 position)
	{
		m_ID = id;
		m_Position = position;
	}
	public override string ToString()
	{
		return m_ID + "," + m_Position.x + "," + m_Position.y;
	}
}

public class GameLogic : MonoBehaviour
{
    public List<int> m_ConnectedPlayers = new List<int>();

	public List<BalloonData> m_SpawnedBalloons = new List<BalloonData>();
	int m_BalloonID = 0;
	float CONST_DURATION_NEXT_BALLOON_TIME = 3.0f;
	float m_DurationUntilNextBalloon;

	#region connectedPlayers Functions
	public void Add(int clientConnectionID)
    {
		m_ConnectedPlayers.Add(clientConnectionID);
	}
	public void Remove(int clientConnectionID)
	{
		m_ConnectedPlayers.Remove(clientConnectionID);
	}
    public int Count()
    {
        return m_ConnectedPlayers.Count;
    }
    #endregion

    void Start()
    {
        NetworkServerProcessing.SetGameLogic(this);
    }

	void Update()
    {
		//if (Input.GetKeyDown(KeyCode.A))
		//    NetworkServerProcessing.SendMessageToClient("2,Hello client's world, sincerely your network server", 0, TransportPipeline.ReliableAndInOrder);

		if (m_ConnectedPlayers.Count <= 0)
			return;

		m_DurationUntilNextBalloon -= Time.deltaTime;

		if (m_DurationUntilNextBalloon < 0)
		{
			m_DurationUntilNextBalloon = CONST_DURATION_NEXT_BALLOON_TIME;

			float screenPositionXPercent = Random.Range(0.0f, 1.0f);
			float screenPositionYPercent = Random.Range(0.0f, 1.0f);
			Vector2 screenPosition = new Vector2(screenPositionXPercent * (float)Screen.width, screenPositionYPercent * (float)Screen.height);

			BalloonData newBalloon = new BalloonData(m_BalloonID++, screenPosition);
			m_SpawnedBalloons.Add(newBalloon);

			NetworkServerProcessing.SpawnBalloonEvent(newBalloon);
		}
	}
}
