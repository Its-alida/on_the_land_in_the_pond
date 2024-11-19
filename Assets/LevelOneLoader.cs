using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelOneLoader : MonoBehaviour
{
    public void ClickButton()
    {
        SceneManager.LoadScene("Level1");
    }
}
