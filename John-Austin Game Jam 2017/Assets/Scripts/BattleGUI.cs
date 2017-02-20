using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleGUI : MonoBehaviour {

    private List<string> m_RationalQuotes = new List<string>();
    private List<string> m_RageQuotes = new List<string>();
    private List<string> m_TrollQuotes = new List<string>();
    private List<string> m_EnemyQuotes = new List<string>();

    public Texture2D playerBarFull;
    public Texture2D enemyBarFull;
    public Texture2D barEmpty;

    public GameObject m_TextObject;

    public Battle_Target m_Player;
    public Battle_Target m_Enemy;

    // Use this for initialization
    void Start () {

        System.IO.StreamReader[] m_Quotes = new System.IO.StreamReader[4] { new System.IO.StreamReader("Assets\\Battle Quotes\\Rational Quotes.txt"),
                                                                        new System.IO.StreamReader("Assets\\Battle Quotes\\Nazi Quotes.txt"),
                                                                        new System.IO.StreamReader("Assets\\Battle Quotes\\Teasing Quotes.txt"),
                                                                        new System.IO.StreamReader("Assets\\Battle Quotes\\Alt-Facts.txt") };


        string Line;


        while ((Line = m_Quotes[0].ReadLine()) != null)
        {
            m_RationalQuotes.Add(Line);
        }

        while ((Line = m_Quotes[1].ReadLine()) != null)
        {
            m_RageQuotes.Add(Line);

        }

        while ((Line = m_Quotes[2].ReadLine()) != null)
        {
            m_TrollQuotes.Add(Line);
        }

        while ((Line = m_Quotes[3].ReadLine()) != null)
        {
            m_EnemyQuotes.Add(Line);
        }

        m_Quotes[0].Close();
        m_Quotes[1].Close();
        m_Quotes[2].Close();
        m_Quotes[3].Close();

        pushEnemyQuote(true);
    }

    public void startFlameWar()
    {
        pushEnemyQuote();
    }


    private void OnGUI()
    {
        enemyHealth();
        playerHealth();
    }


    public void pushPlayerQuote(int type, int respondingTo = 0)
    {

        switch (type)
        {
            case 0:
                if (respondingTo == 0 || respondingTo >= m_RationalQuotes.Count)
                {
                    respondingTo = Random.Range(1, (m_RationalQuotes.Count));
                }
                writeMessage(m_RationalQuotes[respondingTo], Color.green);
                break;

            case 1:
                if (respondingTo == 0 || respondingTo >= m_RageQuotes.Count)
                {
                    respondingTo = Random.Range(1, (m_RageQuotes.Count));
                }
                writeMessage(m_RageQuotes[respondingTo], Color.green);
                break;

            case 2:
                if (respondingTo == 0 || respondingTo >= m_TrollQuotes.Count)
                {
                    respondingTo = Random.Range(1, (m_TrollQuotes.Count));
                }
                writeMessage(m_TrollQuotes[respondingTo], Color.green);
                break;
            default:
                writeMessage("Error", Color.black);
                break;
       }
    }


    public int pushEnemyQuote(bool firstRun = false)
    {
        if (!firstRun)
        {
            int random = Random.Range(1, (m_EnemyQuotes.Count));

            writeMessage(m_EnemyQuotes[random], Color.red);

            return random;
        }
        else
        {
            writeMessage(m_EnemyQuotes[2], Color.red);

            return 2;
        }
    }


    private void writeMessage(string message, Color messageColor)
    {
        m_TextObject.GetComponent<UnityEngine.UI.Text>().color = messageColor;
        m_TextObject.GetComponent<UnityEngine.UI.Text>().text = message;
    }


    private void enemyHealth()
    {
        // draw the background:
        GUI.BeginGroup(new Rect(Screen.width - (Screen.width / 8) - 130, 100, 130, 40));
        GUI.Box(new Rect(0, 0, 130, 40), barEmpty);

        // draw the filled-in part:
        GUI.BeginGroup(new Rect(0, 0, 130 * (m_Enemy.GetHealth() / m_Enemy.GetMaxHealth()), 40));
        GUI.Box(new Rect(0, 0, 130, 40), enemyBarFull);
        GUI.EndGroup();

        GUI.EndGroup();
    }


    private void playerHealth()
    {
        // draw the background:
        GUI.BeginGroup(new Rect(Screen.width / 8, 100, 130, 40));
        GUI.Box(new Rect(0, 0, 130, 40), barEmpty);
        
        // draw the filled-in part:
        GUI.BeginGroup(new Rect(0, 0, 130 * (m_Player.GetHealth() / m_Player.GetMaxHealth()), 40));
        GUI.Box(new Rect(0, 0, 130, 40), playerBarFull);
        GUI.EndGroup();

        GUI.EndGroup();
    }




    // Update is called once per frame
    void Update () {
		


	}
}
