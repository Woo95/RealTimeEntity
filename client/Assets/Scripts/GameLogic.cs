using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BalloonData
{
	public int m_ID;
	public Vector2 m_Position;
	public CircleClick m_CircleClick;
	public BalloonData(int id, Vector2 position, CircleClick circleClick)
	{
		m_ID = id;
		m_Position = position;
		m_CircleClick = circleClick;
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
		CircleClick circleClick = balloon.AddComponent<CircleClick>();
		balloon.AddComponent<CircleCollider2D>();

		Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, 0));
		pos.z = 0;
		balloon.transform.position = pos;

		BalloonData spawnedBalloon = new BalloonData(balloonID, screenPosition, circleClick);
		m_SpawnedBalloons.Add(spawnedBalloon);

		circleClick.AddData(spawnedBalloon);
	}

	public void PopBalloon(BalloonData popBalloonData)
	{
		UI_Gameplay.instance.AddPop();

		Destroy(popBalloonData.m_CircleClick.gameObject);
	}
}
