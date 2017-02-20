using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn_Timer : MonoBehaviour {

    private float m_Countdown;

    private bool m_Active;

    // Use this for initialization
    void Start () {
		
	}

    public void Countdown(float seconds = 3)
    {
        m_Countdown = seconds;
        m_Active = true;
    }

	// Update is called once per frame
	void Update ()
    {
        if (m_Active)
        {
            m_Countdown -= Time.deltaTime;

            if (m_Countdown < 0f)
            {
                m_Active = false;
                gameObject.GetComponent<Battle_Manager>().NextTurn();
            }
        }
    }
}
