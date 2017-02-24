using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    public enum enemyState
    {
        idle,
        chasing,
        searching
    }

    public GameManager myGameManager;

    public GameObject SpawnNode;
    public GameObject Player;
    public GameObject Target;

    public float health;

    public float speed;
    public float maxSpeed;

    private RaycastHit HitInfo = new RaycastHit();
    private Vector3 Direction = new Vector3(0, 0, 0);

    private enemyState currentState;
    private enemyState previousState;


	// Use this for initialization
	void Start () {

        if (SpawnNode != null)
        {
            transform.position = SpawnNode.transform.position;
        }

        currentState = enemyState.idle;
        previousState = enemyState.idle;
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        Direction = Target.transform.position - this.transform.position;

        if (Direction.magnitude > 1.2f)
        {
            Direction.Normalize();
        }
        else
        {
            currentState = enemyState.idle;
            if (Target == Player)
            {
                transform.position = myGameManager.transform.position;
                gameObject.GetComponent<Respawn_Timer>().StartCountdown();
                myGameManager.startBattle(health);
            }
        }

        findPlayer();

        if (currentState != enemyState.idle)
        {
            moveToTarget();
        }


        if(currentState != previousState)
        {
            previousState = currentState;
        }

	}

    public enemyState GetState()
    {
        return currentState;
    }

    public void Respawn()
    {
        if (SpawnNode != null)
        {
            transform.position = SpawnNode.transform.position;
        }
        else
        {
            transform.position = Direction;
        }
    }


    void moveToTarget()
    { 
        transform.position += Direction * speed * Time.deltaTime;           
    }


    void findPlayer()
    {
        Physics.Raycast(transform.position, (Player.transform.position - transform.position), out HitInfo);

        if (HitInfo.collider == Player.GetComponent<Collider>())
        {

            if (currentState != enemyState.chasing)
            {
                Target = Player;
                currentState = enemyState.chasing;

            }
        }
        else
        {

            if (currentState != enemyState.idle && currentState != enemyState.searching)
            {
                Target = getNearestNode();
                currentState = enemyState.searching;
            }
        }

    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "PathNode")
        {
            currentState = enemyState.idle;
        }
    }



    GameObject getNearestNode()
    {
        Vector3 distance = new Vector3(0, 0, 0);
        Vector3 nextDistance = new Vector3(0, 0, 0);

        GameObject node;
        node = myGameManager.m_MazeManager.m_NodeList[0];

        distance = Player.transform.position - myGameManager.m_MazeManager.m_NodeList[0].transform.position;

        for (int i = 1; i < myGameManager.m_MazeManager.m_NodeList.Length; i++)
        {
            nextDistance = Player.transform.position - myGameManager.m_MazeManager.m_NodeList[i].transform.position;

            if (nextDistance.magnitude < distance.magnitude)
            {
                distance = nextDistance;
                node = myGameManager.m_MazeManager.m_NodeList[i];
            }
        }

        return node;
    }

}
