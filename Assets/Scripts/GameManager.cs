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
            Destroy(player.gameObject);
            Destroy(floatingText.gameObject);
            Destroy(hud);
            Destroy(menu);
            return;
        }

        instance = this;
        SceneManager.sceneLoaded += LoadState;
        //DontDestroyOnLoad(gameObject);
        //DontDestroyOnLoad(hitpointBar.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //resources
    public List<Sprite> playerSprite;
    public List<Sprite> weaponSprite;
    public List<int> weaponPrice;
    public List<int> xpTable;

    //References
    public PlayerController player;

    public Weapon weapon;
    public FloatingTextManager floatingText;
    public RectTransform hitpointBar;
    public GameObject hud;
    public GameObject menu;
    public Animator deadMenuAnim;

    //logic
    public int pesos;
    public int exp;

    //FloatingText
    public void Showtext(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingText.Show(msg, fontSize, color, position, motion, duration);
    }

    //upgrade weapon
    public bool TryUpgradeWeapon()
    {
        // is the weapon max level
        if (weaponPrice.Count <= weapon.weaponLevel)
            return false;

        if (pesos >= weaponPrice[weapon.weaponLevel])
        {
            pesos -= weaponPrice[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }

        return false;
    }

    //hitpon tBar
    public void OnHitpointChange()
    {
        float ratio = (float)player.hitPonit / (float)player.maxHitPonit;
        hitpointBar.localScale = new Vector3(ratio, 1, 1);
    }

    //private void Update()
    //{
    //     Debug.Log(GetCurrentLevel());
    //}

    //exp system
    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;
        while (exp >= add)
        {
            add += xpTable[r];
            r++;

            if (r == xpTable.Count)//max level
                return r;
        }
        return r;
    }


    public int GetXpLevel(int level)
    {
        int r = 0;
        int xp = 0;

        while (r < level)
        {
            xp += xpTable[r];
            r++;
        }
        return xp;
    }

    public void GrantXp(int xp)
    {
        int currLevel = GetCurrentLevel();
        exp += xp;
        if (currLevel < GetCurrentLevel())
            OnLevelUp();
    }


    public void OnLevelUp()
    {
        Debug.Log("Level Up !!!");
        player.OnLevelUp();
        OnHitpointChange();
    }

    public void OnSceneLoaded(Scene s,LoadSceneMode mode)
    {
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }

    //dead menu and respwan
    public void Respawn()
    {
        deadMenuAnim.SetTrigger("Hide");
        UnityEngine.SceneManagement.SceneManager.LoadScene("game");
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
        s += weapon.weaponLevel.ToString();

        PlayerPrefs.SetString("SaveState", s);
    }

    public void LoadState(Scene s,LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= LoadState;
        if (!PlayerPrefs.HasKey("SaveState")) return;

        string[] data = PlayerPrefs.GetString("SaveState").Split("/");

        //change playerSkin
        pesos = int.Parse(data[1]);

        //exp
        exp = int.Parse(data[2]);
        if(GetCurrentLevel()!=1)
            player.SetLevel(GetCurrentLevel());

        //change weaponLevel
        weapon.SetWeaponLevel(int.Parse(data[3]));
        

        Debug.Log("LoadSate");

        player.transform.position = GameObject.Find("SpawnPoint").transform.position;

    }
}
 