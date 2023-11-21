using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleClick : MonoBehaviour
{
	BalloonData m_BalloonData;

	public void AddData(BalloonData balloonData)
	{
		m_BalloonData = balloonData;
	}

	void OnMouseDown()
    {
		string msg = ClientToServerSignifiers.PTC_BALLOON_POP + "," + m_BalloonData.m_ID;
		NetworkClientProcessing.SendMessageToServer(msg, TransportPipeline.ReliableAndInOrder);
    }
}