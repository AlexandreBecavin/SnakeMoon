using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public void showRestartButton()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

}
