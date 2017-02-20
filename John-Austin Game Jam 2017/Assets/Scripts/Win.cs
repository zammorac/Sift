using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour {

    public GameManager m_Manager;
    public GameObject m_Player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if((m_Player.transform.position - transform.position).magnitude < 1.3)
        {
            m_Manager.WinGame();
        }
	}
}
