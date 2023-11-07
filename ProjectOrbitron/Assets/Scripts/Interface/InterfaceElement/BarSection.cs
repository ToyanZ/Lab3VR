using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarSection : MonoBehaviour
{
    public float fillAmount = 1;
    public Image filler;
    public Image secondaryFiller;

    public void SetBoth(float value)
    {
        filler.fillAmount = value;
        secondaryFiller.fillAmount = value;
    }
}
