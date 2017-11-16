using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager Instance = null;

    int m_score = 0;
    static int m_hiscore = 0;
    int m_ammo = 30;

    Player m_player;

    Text txt_ammo;
    Text HighScore;
    Text Score;
    Text txt_health;

    public AudioClip m_Reload;

    // Use this for initialization
    void Start() {

        Instance = this;
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        foreach (RectTransform t in this.transform.GetComponentsInChildren<RectTransform>())
        {
            if (t.name.CompareTo("txt_ammo") == 0)
            {
                txt_ammo = t.GetComponent<Text>();
            }
            else if (t.name.CompareTo("txt_health") == 0)
            {
                txt_health = t.GetComponent<Text>();
            }
            else if (t.name.CompareTo("Score") == 0)
            {
                Score = t.GetComponent<Text>();
            }
            else if (t.name.CompareTo("HighScore") == 0)
            {
                HighScore = t.GetComponent<Text>();
                HighScore.text = "Record" + m_hiscore;
            }
        }
    }

    public void SetScore(int score)
    {
        m_score += score;

        if (m_score > m_hiscore)
        {
            m_hiscore = m_score;
        }
        Score.text = "Score<color=yellow>" + m_score + "</color>";
        HighScore.text = "Record" + m_hiscore;
    }

    public void SetAmmo(int ammo)
    {
        m_ammo -= ammo;
        txt_ammo.text = m_ammo.ToString() + "/30";

        if (m_ammo <= 0)
        {
            Reload();
        }
    }

    public void SetLife(int life)
    {
        txt_health.text = life.ToString();
    }

    private void OnGUI()
    {
        if (m_player.m_life <= 0)
        {
            GUI.skin.label.alignment = TextAnchor.MiddleCenter;
            GUI.skin.label.fontSize = 40;
            GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "Game Over");
            GUI.skin.label.fontSize = 30;

            if (GUI.Button(new Rect(Screen.width * 0.5f - 150, Screen.height * 0.75f, 300, 40), "Try Again"))
            {
                Application.LoadLevel(Application.loadedLevelName);
            }
        }
    }

    public void Reload()
    {
        this.GetComponent<AudioSource>().PlayOneShot(m_Reload);
        m_ammo = 30;
        txt_ammo.text = "30/30";
    }

    // Update is called once per frame
    void Update () {
        if(Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }
	}
}
