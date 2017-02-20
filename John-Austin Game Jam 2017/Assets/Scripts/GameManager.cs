using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public Maze_Manager m_MazeManager;
    public MusicManager m_Music;

    public GameObject m_Player;
    public GameObject m_PlayerBattle;

    public GameObject mazeObjects;
    public GameObject battleObjects;
    public GameObject winGameObjects;
    public GameObject badEndObjects;
    public GameObject gameOverObjects;
    public GameObject knowledge;
    public GameObject badKnowledge;

    public int m_Facts_Used = 0;
    public int m_Trolls_Used = 0;
    public int m_Rage_Used = 0;

    private bool m_IsChaseHappening = false;

    // Use this for initialization
    void Awake () {

        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
	}

    // Update is called once per frame
    void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        m_IsChaseHappening = false;

        for (int i = 0; i < m_MazeManager.m_Enemies.Length; i++)
        {
            if (m_MazeManager.m_Enemies[i].GetComponent<EnemyAI>().GetState() != EnemyAI.enemyState.idle)
            {
                m_IsChaseHappening = true;
                break;
            }
        }

        if (m_IsChaseHappening)
        {
            m_Music.PlayChase();
            m_Music.StopExplore();
        }
        else if (mazeObjects.activeInHierarchy)
        {
            m_Music.StopChase();
            m_Music.PlayExplore();
        }
    }

    

    public void startBattle(float enemyHealth)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        mazeObjects.SetActive(false);
        battleObjects.SetActive(true);
        gameObject.GetComponent<Battle_Manager>().m_Enemy.SetHealth(enemyHealth);
        gameObject.GetComponent<Battle_Manager>().m_Player.SetHealth(20);
        gameObject.GetComponent<Battle_Manager>().m_GUI.startFlameWar();
    }

    public void endBattle(bool playerLost = false)
    {
        if (playerLost)
        {
            battleObjects.SetActive(false);
            gameOverObjects.SetActive(true);
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            mazeObjects.SetActive(true);
            battleObjects.SetActive(false);
        }

        if (m_Trolls_Used > m_Rage_Used && m_Trolls_Used > m_Facts_Used)
        {
            m_Player.GetComponent<Renderer>().material.color = Color.yellow;
            m_PlayerBattle.GetComponent<Renderer>().material.color = Color.yellow;
            knowledge.SetActive(false);
            badKnowledge.SetActive(false);
        }

        if (m_Rage_Used > m_Trolls_Used && m_Rage_Used > m_Facts_Used)
        {
            m_Player.GetComponent<Renderer>().material.color = Color.red;
            m_PlayerBattle.GetComponent<Renderer>().material.color = Color.red;
            knowledge.SetActive(false);
            badKnowledge.SetActive(true);
        }

        if (m_Facts_Used > m_Rage_Used && m_Facts_Used > m_Trolls_Used)
        {
            m_Player.GetComponent<Renderer>().material.color = Color.blue;
            m_PlayerBattle.GetComponent<Renderer>().material.color = Color.blue;
            knowledge.SetActive(true);
            badKnowledge.SetActive(false);
        }

        m_Music.StopChase();
        m_Music.PlayExplore();
    }

    public void WinGame()
    {
        mazeObjects.SetActive(false);
        winGameObjects.SetActive(true);
    }
    public void Bad_End()
    {
        mazeObjects.SetActive(false);
        badEndObjects.SetActive(true);
    }

}
