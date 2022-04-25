using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if(GameManager.instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(hud);
            Destroy(menu);
            return;
        }

        instance = this;
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // In-game Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    // References
    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;
    public RectTransform hitPointBar;
    public Animator deathMenuAnim;
    public GameObject hud;
    public GameObject menu;

    // keeping track of:
    public int gold;
    public int experience;


    // Floating text
    // Calling this function from everywhere so that the game object says it
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 mention, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, mention, duration);
    }

    // Upgrade Weapon
    public bool TryUpgradeWeapon()
    {
        // is the weapon maxed out?
        if (weaponPrices.Count <= weapon.weaponLevel)
        {
            return false;
        }

        if (gold >= weaponPrices[weapon.weaponLevel])
        {
            gold -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }

        return false;
    }


    // Hitpoint Bar
    public void OnHitPointChange()
    {
        float ratio = (float)player.hitPoint / (float)player.maxHitPoint;
        hitPointBar.localScale = new Vector3(1, ratio, 1);
    }


    // Experience System
    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;

        // Leveling
        while(experience >= add)
        {
            add += xpTable[r];
            r++;

            if (r == xpTable.Count) // checking if we're max level
            {
                return r;
            }
        }
        return r;
    }
    public int GetXpToLevel(int level)
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
        experience += xp;
        if(currLevel < GetCurrentLevel())
        {
            OnLevelUp();
        }
    }
    public void OnLevelUp()
    {
        Debug.Log("Level Up!");
        player.OnLevelUp();
        OnHitPointChange();
    }

    // On Scene Loaded
    public void OnSceneLoaded(Scene s, LoadSceneMode mode)
    {
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }

    // Death menu and respwan
    public void Respawn()
    {
        deathMenuAnim.SetTrigger("Hide");
        SceneManager.LoadScene("Main");
        player.Respawn();
    }

    // Save game
    public void SaveState()
    {
        string s = ""; //s stands for saving

        s += "0" + "|"; // PerferedSkin
        s += gold.ToString() + "|"; // Amount of gold we collect
        s += experience.ToString() + "|"; // Amount of experience we gain
        s += weapon.weaponLevel.ToString(); // Level of our weapon

        PlayerPrefs.SetString("SaveState", s);
    }
    // Load game
    public void LoadState(Scene s,LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= LoadState;

        if (!PlayerPrefs.HasKey("SaveState")) 
        { 
            return;
        }
        // spliting resource strings
        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        // Change player skin
        gold = int.Parse(data[1]);

        // Experience
        experience = int.Parse(data[2]);
        if (GetCurrentLevel() != 1)
        { 
            player.SetLevel(GetCurrentLevel()); 
        }

        // Change weapon level
        weapon.SetWeaponLevel(int.Parse(data[3]));


    }

}

