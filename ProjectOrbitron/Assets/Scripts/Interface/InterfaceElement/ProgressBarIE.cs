using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarIE : NumericIE
{
    public enum FillingDirection { Default, Reverse }
    public enum FillMode { Instantaneous, Smoothed, Delayed}
    public enum ColorMode { Color, Gradient}

    [Space(30)]
    public Image filler;
    public Image secondaryFiller;
    
    
    [Space(10)]
    public FillingDirection fillingDirection = FillingDirection.Default; //Cambiar a numeric
    public FillMode fillMode = FillMode.Instantaneous;
    
    [Space(10)]
    public ColorMode colorMode = ColorMode.Color;
    public Color fillerColor = Color.white;
    public Color secondaryFillerColor = Color.white;
    public Gradient gradient;

    [Space(10)]
    public float progressiveFillingTime = 0.5f;
    public AnimationCurve smoothingCurve;

    private float newFillAmount = 0;
    private bool smoothing = false;
    public override void Refresh(InterfaceData interfaceData)
    {
        base.Refresh(interfaceData);
        newFillAmount = fillingDirection == FillingDirection.Default ? currentValue / maxValue : 1.0f - (currentValue / maxValue);


        switch (fillMode)
        {
            case FillMode.Instantaneous:
                Instantaneous();
                break;
            case FillMode.Smoothed:
                Smoothed(filler, 1);
                break;
            case FillMode.Delayed:
                Delayed();
                break;
        }
    }

    private void Instantaneous()
    {
        filler.fillAmount = newFillAmount;
        SetColor(1, filler.fillAmount);
    }
    private void Smoothed(Image filler, int index)
    {
        if (!smoothing) StartCoroutine(SmoothingFill(filler, index));
    }
    IEnumerator SmoothingFill(Image filler, int index)
    {
        smoothing = true;
        float time = 0;
        float start = filler.fillAmount;
        while (time < progressiveFillingTime) 
        {
            time += Time.deltaTime;
            filler.fillAmount = Mathf.Lerp(start, newFillAmount, smoothingCurve.Evaluate(time / progressiveFillingTime));
            SetColor(index, filler.fillAmount);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        smoothing = false;
    }
    private void Delayed()
    {
        Instantaneous();
        if (secondaryFiller == null)
        {
            Debug.LogError("No secondary filler assigned on " + name);
            return;
        }
        Smoothed(secondaryFiller, 2);
    }

    public void SetColor(int index, float value)
    {
        
        switch (colorMode)
        {
            case ColorMode.Color:
                if (index == 1) filler.color = fillerColor;
                else secondaryFiller.color = secondaryFillerColor;
                break;
            case ColorMode.Gradient:
                if (index == 1) filler.color = gradient.Evaluate(value);
                else secondaryFiller.color = gradient.Evaluate(value);
                break;
        }
    }
}
