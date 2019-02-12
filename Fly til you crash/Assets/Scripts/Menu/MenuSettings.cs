using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSettings : MonoBehaviour
{
    public void UpdatePlayerController(bool update)
    {
        PlayerController.yAxisIsOn = update;
    }
}
