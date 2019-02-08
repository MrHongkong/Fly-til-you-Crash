using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public GameObject[] buttonList;

    private void Start()
    {
        foreach(GameObject button in buttonList)
        {
            if (button.name == "New game") button.SetActive(true);
            else button.SetActive(false);
        }
        
    }

    public void SwapButton(float swap)
    {
        int length = buttonList.Length;
        for (int i = 0; i < length; i++)
        {
        if (buttonList[i].activeInHierarchy && swap < 0)
        {
            if (i == 0)
            {
                buttonList[buttonList.Length - 1].SetActive(true);
                buttonList[i].SetActive(false);
                break;
            }
            else
            {
                buttonList[(i - 1) % (buttonList.Length)].SetActive(true);
                buttonList[i].SetActive(false);
                break;
            }
            }

            if (buttonList[i].activeInHierarchy && swap > 0)
            {
                buttonList[(i + 1) % buttonList.Length].SetActive(true);
                buttonList[i].SetActive(false);
                break;
            }
        }
    }    
}
//Philips kod
/*int next = ((index + ((((index / length) * length) < 0 ? -((index / length) * length) : ((index / length) * length)) + length)) % length < 0) ?
                                  -((index + ((((index / length) * length) < 0 ? -((index / length) * length) : ((index / length) * length)) + length)) % length) :
                                   ((index + ((((index / length) * length) < 0 ? -((index / length) * length) : ((index / length) * length)) + length)) % length);

*/
