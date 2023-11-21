using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
	Sprite m_CircleTexture;

	void Start()
    {
        NetworkClientProcessing.SetGameLogic(this);
    }


	public void SpawnNewBalloon(Vector2 screenPosition)
	{
		if (m_CircleTexture == null)
			m_CircleTexture = Resources.Load<Sprite>("Circle");

		GameObject balloon = new GameObject("Balloon");

		balloon.AddComponent<SpriteRenderer>();
		balloon.GetComponent<SpriteRenderer>().sprite = m_CircleTexture;
		//balloon.AddComponent<CircleClick>();
		balloon.AddComponent<CircleCollider2D>();

		Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, 0));
		pos.z = 0;
		balloon.transform.position = pos;
		//go.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, -Camera.main.transform.position.z));
	}
}
