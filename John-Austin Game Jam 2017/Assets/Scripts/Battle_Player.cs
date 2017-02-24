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


    }

    public override void Die()
    {
        is_dead = true;
    }

}
