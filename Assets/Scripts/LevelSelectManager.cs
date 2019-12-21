using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour {

    public void Back() {
        // Will go back to main menu
    }

    public void LoadLevel(int num) {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
        SceneManager.LoadScene(num);
    }

}