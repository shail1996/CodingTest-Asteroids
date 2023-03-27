using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject lifeImage1;
    [SerializeField] GameObject lifeImage2;
    [SerializeField] GameObject lifeImage3;
    [SerializeField] TextMeshProUGUI score;
    public void UpdateLifeCount()
    {
        if (GlobalVariables.life == 1)
        {
            lifeImage2.SetActive(false);
            lifeImage3.SetActive(false);
        }
        if (GlobalVariables.life == 2)
        {
            lifeImage3.SetActive(false);
        }
    }

    public void UpdateScore()
    {
        score.text = GlobalVariables.score.ToString();
    }

}
