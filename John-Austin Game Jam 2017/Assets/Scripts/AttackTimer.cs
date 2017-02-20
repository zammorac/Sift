using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTimer : MonoBehaviour {

    public float m_Time;
    private float m_Countdown;
    public bool m_StartActive;
    private bool m_Active;

	// Use this for initialization
	void Start () {

        m_Countdown = m_Time;

        if (m_StartActive)
        {
            m_Active = true;
            GetComponent<ParticleSystem>().Play();
        }
        else
        {
            GetComponent<ParticleSystem>().Stop();
        }
	}
	
    public void play()
    {
        m_Countdown = m_Time;
        m_Active = true;
        GetComponent<ParticleSystem>().Play();
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
                GetComponent<ParticleSystem>().Stop();
            }
        }

	}

    bool is_active()
    {
        return m_Active;
    }
}
