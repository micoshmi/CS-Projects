using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private GameObject[] characters;

    private int _charIndex;
    public int charIndex
    {
        get
        {
            return _charIndex;
        }
        set
        {
            _charIndex = value;
        }
    }
    private void Awake()
    {
        /*singleton pattern
                 only 1 copy of game object will be present, dupicates are being destroyed*/
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //events and delegations:
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene,LoadSceneMode mode)
    {
        if(scene.name == "Gameplay")
        {
            Instantiate(characters[charIndex]);
        }
    }
}
