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
	public List<BalloonData> m_SpawnedBalloons = new List<BalloonData>();

	Sprite m_CircleTexture;

	void Start()
    {
        NetworkClientProcessing.SetGameLogic(this);

		m_SpawnedBalloons.Clear();

	}

	public void SpawnNewBalloon(int balloonID, Vector2 screenPosition)
	{
		if (m_CircleTexture == null)
			m_CircleTexture = Resources.Load<Sprite>("Circle");

		GameObject balloon = new GameObject("Balloon");

		balloon.AddComponent<SpriteRenderer>();
		balloon.GetComponent<SpriteRenderer>().sprite = m_CircleTexture;
		balloon.AddComponent<CircleClick>();
		balloon.AddComponent<CircleCollider2D>();

		Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, 0));
		pos.z = 0;
		balloon.transform.position = pos;

		BalloonData spawnedBalloon = new BalloonData(balloonID, screenPosition);
		m_SpawnedBalloons.Add(spawnedBalloon);
	}
}
