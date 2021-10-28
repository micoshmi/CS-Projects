using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuController : MonoBehaviour
{
    public void playGame()
    {
        //getting the name of the game object
        int selectedCharacter = int.Parse(EventSystem.current.currentSelectedGameObject.name);

        //selection that we made, depedns on which character we pressed, will be stored here:
        GameManager.instance.charIndex = selectedCharacter;

        //using CharIndex to know which character we spawn
        SceneManager.LoadScene("Gameplay");
    }
}
