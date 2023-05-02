using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
    }

    //resources
    public List<Sprite> playerSprite;
    public List<Sprite> weaponSprite;
    public List<int> weaponPrice;
    public List<int> xpTable;

    //References
    public PlayerController player;

    //public weapon...
    public FloatingTextManager floatingText;

    //logic
    public int pesos;
    public int exp;

    //FloatingText
    public void Showtext(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingText.Show(msg, fontSize, color, position, motion, duration);
    }

    //save Satte

    /*\
     *int preferedSkin
     *int pesos
     *int exp
     *int weaponLevel
     */
    public void SaveState()
    {
        string s = " ";

        s += "0" + "/";
        s += pesos.ToString() + "/";
        s += exp.ToString() + "/";
        s += "0";

        PlayerPrefs.SetString("SaveState", s);
    }

    public void LoadState(Scene s,LoadSceneMode mode)
    {
        if (PlayerPrefs.HasKey("SaveState")) return;

        string[] data = PlayerPrefs.GetString("SaveState").Split("/");

        //change playerSkin
        pesos = int.Parse(data[1]);
        exp = int.Parse(data[2]);

        //change weaponLevel

        Debug.Log("LoadSate");
    }
}
