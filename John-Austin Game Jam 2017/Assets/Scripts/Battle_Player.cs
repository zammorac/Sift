using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle_Player : Battle_Target
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
        if (Input.GetKey("[3]"))
        {
            Attack(m_Manager.GetEnemy(), 20f);
        }


    }

    public override void Die()
    {
        is_dead = true;
    }

}
