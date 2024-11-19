using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelThreeLoader : MonoBehaviour
{
    public void ClickButton()
    {
        SceneManager.LoadScene("Level3");
    }
}
