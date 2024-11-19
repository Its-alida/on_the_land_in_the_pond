using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSecondLevel : MonoBehaviour
{
    public void ClickButton()
    {
        SceneManager.LoadScene("Level2");
    }
}
