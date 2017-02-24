using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn_Timer : MonoBehaviour {

    public float m_Time;
    private float m_Countdown;

    bool m_Active;

    // Use this for initialization
    void Start () {
		
	}

    public void StartCountdown(float seconds = 0f)
    {
        if (seconds == 0f)
        {
            m_Countdown = m_Time;
        }
        else
        {
            m_Countdown = seconds;
        }
        m_Active = true;
    }  

    // Update is called once per frame
    void Update ()
    {
		if(m_Active && gameObject.activeInHierarchy)
        {
            m_Countdown -= Time.deltaTime;

            if (m_Countdown <= 0f)
            {
                gameObject.GetComponent<EnemyAI>().Respawn();
                m_Active = false;
            }
        }
	}

}
