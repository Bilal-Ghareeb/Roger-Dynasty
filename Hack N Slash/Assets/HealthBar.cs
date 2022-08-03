using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Image healthsprite;
   
    public void UpdateHealthBar(float maxhp , float currhp)
    {
        healthsprite.fillAmount = currhp / maxhp;
   
    }

}
