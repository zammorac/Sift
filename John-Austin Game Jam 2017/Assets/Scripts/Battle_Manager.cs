using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle_Manager : MonoBehaviour {

    public GameManager m_Manager;
    public Battle_Target m_Player;
    public Battle_Target m_Enemy;

    private bool m_IsPlayerTurn;

    public GameObject m_Button1;
    public GameObject m_Button2;
    public GameObject m_Button3;
    public BattleGUI m_GUI;

    private int m_LastEnemyQuote = 2;

    // Use this for initialization
    void Start ()
    {
        m_IsPlayerTurn = true;
    }
	
    void End_Battle()
    {
        m_Enemy.reload();
        m_Player.reload();
    }

    public Battle_Target GetPlayer()
    {
        return m_Player;      
    }


    public Battle_Target GetEnemy()
    {
        return m_Enemy;
    }


    public void Player_Attack(string attackType)
    {
        if (m_IsPlayerTurn)
        {
            switch (attackType)
            {
                case "Calm":
                    m_Player.Attack(m_Enemy, Random.Range(3f, 6f));
                    gameObject.GetComponent<Turn_Timer>().Countdown(3f);
                    m_Player.Attack1.GetComponent<AttackTimer>().play();
                    m_Manager.m_Facts_Used++;
                    m_GUI.pushPlayerQuote(0,m_LastEnemyQuote);
                    break;

                case "Troll":
                    m_Player.Attack(m_Enemy, Random.Range(2f, 4f));
                    m_Player.Attack(m_Player, -3f);
                    gameObject.GetComponent<Turn_Timer>().Countdown(3f);
                    m_Player.Attack3.GetComponent<AttackTimer>().play();
                    m_Manager.m_Trolls_Used++;
                    m_GUI.pushPlayerQuote(2, m_LastEnemyQuote);
                    break;

                case "Angry":
                    m_Player.Attack(m_Enemy, 7f);
                    m_Player.Attack(m_Player, 3f);
                    gameObject.GetComponent<Turn_Timer>().Countdown(4f);
                    m_Player.Attack2.GetComponent<AttackTimer>().play();
                    m_Manager.m_Rage_Used++;
                    m_GUI.pushPlayerQuote(1, m_LastEnemyQuote);
                    break;

                default:
                    Debug.Log("Incorrect Attack Type");
                    break;
            }

            HideButtons();
        }
    }

    private void Enemy_Attack()
    {
        if (!m_IsPlayerTurn)
        {
            int attackType = (int)Random.Range(1f, 3.09f);

            switch (attackType)
            {
                case 1:
                    m_Enemy.Attack(m_Player, Random.Range(3f, 6f));
                    gameObject.GetComponent<Turn_Timer>().Countdown(3f);
                    m_Enemy.Attack1.GetComponent<AttackTimer>().play();
                    break;

                case 2:
                    m_Enemy.Attack(m_Player, 7f);
                    m_Enemy.Attack(m_Enemy, 3f);
                    gameObject.GetComponent<Turn_Timer>().Countdown(4f);
                    m_Enemy.Attack2.GetComponent<AttackTimer>().play();
                    break;

                case 3:
                    m_Enemy.Attack(m_Player, Random.Range(2f, 4f));
                    m_Enemy.Attack(m_Enemy, -3f);
                    gameObject.GetComponent<Turn_Timer>().Countdown(3f);
                    m_Enemy.Attack3.GetComponent<AttackTimer>().play();
                    break;

                default:
                    Debug.Log("Incorrect Attack Type");
                    break;
            }

            m_LastEnemyQuote = m_GUI.pushEnemyQuote();

        }

    }

    public void NextTurn()
    {
        m_IsPlayerTurn = !m_IsPlayerTurn;

        if (!m_IsPlayerTurn)
        {      
            Enemy_Attack();
            HideButtons(false);
        }
        else
        {
            HideButtons(true);
        }
    }


    private void HideButtons(bool visible = false)
    {
        m_Button1.SetActive(visible);
        m_Button2.SetActive(visible);
        m_Button3.SetActive(visible);
    }

    // Update is called once per frame
    void Update ()
    {
		if(m_Player.is_dead || m_Enemy.is_dead)
        {
            m_IsPlayerTurn = false;
            m_Manager.endBattle(m_Player.is_dead);
            End_Battle();
        }
	}
}
