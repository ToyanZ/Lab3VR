using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarSegmentedIE : NumericIE
{
    public enum FillingDirection { Default, Reverse }
    public enum FillMode { Instantaneous, Smoothed, Delayed}
    public enum ColorMode { Color, Gradient}
    public enum StartMode { Full, Empty}

    [Space(30)]
    public StartMode startMode = StartMode.Full;
    public List<BarSection> sections;

    [Space(10)]
    public FillingDirection fillingDirection = FillingDirection.Default;
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
    private float prevFillAmount = 0;
    private float scale = 0;
    private bool smoothing = false;

    private void Start()
    {
        if (startMode == StartMode.Full) { prevFillAmount = 1; newFillAmount = 1; }
        else {  prevFillAmount = 0; newFillAmount = 0; }
    }

    public override void Refresh(InterfaceData interfaceData)
    {
        base.Refresh(interfaceData);

        scale = maxValue / sections.Count * 1.0f;
        prevFillAmount = newFillAmount;
        newFillAmount = fillingDirection == FillingDirection.Default ? currentValue / maxValue : 1.0f - (currentValue / maxValue);
        
        float sectionValue = currentValue;
        for (int i = 0; i< sections.Count; i++)
        {
            if (sectionValue > scale)
            {
                sections[i].fillAmount = 1;
            }
            else if (sectionValue > 0)
            {
                sections[i].fillAmount = sectionValue / scale;
            }
            else
            {
                sections[i].fillAmount = 0;
            }
            sectionValue -= scale;
        }

        
        switch (fillMode)
        {
            case FillMode.Instantaneous:
                Instantaneous();
                break;
            case FillMode.Smoothed:
                Smoothed();
                break;
            case FillMode.Delayed:
                Delayed();
                break;
        }
    }

    private void Instantaneous()
    {
        if (numericFormat == NumericFormat.Integer)
        {
            if(newFillAmount < prevFillAmount)
            {
                for (int i = sections.Count - 1; i > -1 ; i--)
                {
                    float fillAmount = sections[i].fillAmount > 0.0f ? 1.0f : 0.0f;
                    sections[i].filler.fillAmount = fillAmount;
                    SetColor(sections[i]);
                }
            }
            else
            {
                for (int i = 0; i < sections.Count; i++)
                {
                    float fillAmount = sections[i].fillAmount < 1.0f ? 0.0f : 1.0f;
                    sections[i].filler.fillAmount = fillAmount;
                    SetColor(sections[i]);
                }
            }
        }
        else
        {
            if (newFillAmount < prevFillAmount)
            {
                for (int i = sections.Count - 1; i > -1; i--)
                {
                    sections[i].filler.fillAmount = sections[i].fillAmount;
                    SetColor(sections[i]);
                }
            }
            else
            {
                for (int i = 0; i < sections.Count; i++)
                {
                    sections[i].filler.fillAmount = sections[i].fillAmount;
                    SetColor(sections[i]);
                }
            }
        }
        //SetColor(1, 1);
    }
    private void Instantaneous2()
    {
        if (numericFormat == NumericFormat.Integer)
        {
            if (newFillAmount < prevFillAmount)
            {
                for (int i = sections.Count - 1; i > -1; i--)
                {
                    float fillAmount = sections[i].fillAmount > 0.0f ? 1.0f : 0.0f;
                    sections[i].secondaryFiller.fillAmount = fillAmount;
                }
            }
            else
            {
                for (int i = 0; i < sections.Count; i++)
                {
                    float fillAmount = sections[i].fillAmount < 1.0f ? 0.0f : 1.0f;
                    sections[i].secondaryFiller.fillAmount = fillAmount;
                }
            }
        }
        else
        {
            if (newFillAmount < prevFillAmount)
            {
                for (int i = sections.Count - 1; i > -1; i--)
                {
                    sections[i].secondaryFiller.fillAmount = sections[i].fillAmount;
                }
            }
            else
            {
                for (int i = 0; i < sections.Count; i++)
                {
                    sections[i].secondaryFiller.fillAmount = sections[i].fillAmount;
                }
            }
        }
        //SetColor(1, 1);
    }
    private void Smoothed()
    {
        if (!smoothing) StartCoroutine(SmoothingFill());
    }
    private void Smoothed2()
    {
        if (!smoothing) StartCoroutine(SmoothingFill2());
    }

    IEnumerator SmoothingFill()
    {
        smoothing = true;

        float timePerSegment = 0;
        float count = 0;
        for (int i = 0; i < sections.Count; i++)
        {
            if (sections[i].fillAmount != sections[i].filler.fillAmount) count++;
        }
        timePerSegment = count == 0 ? progressiveFillingTime : progressiveFillingTime / count;

        float time = 0;
        if (numericFormat == NumericFormat.Integer)
        {
            if (newFillAmount < prevFillAmount)
            {
                for (int i = sections.Count - 1; i > -1; i--)
                {
                    if (sections[i].fillAmount > 0) continue;
                    if (sections[i].fillAmount == sections[i].filler.fillAmount) continue;
                    
                    time = 0;
                    while (time < timePerSegment)
                    {
                        time += Time.deltaTime;
                        sections[i].filler.fillAmount = (Mathf.Lerp(1, 0, smoothingCurve.Evaluate(time / timePerSegment)));
                        SetColor(sections[i]);
                        yield return new WaitForSeconds(Time.deltaTime);
                    }
                    sections[i].filler.fillAmount = (0);
                    SetColor(sections[i]);
                }
            }
            else
            {
                for (int i = 0; i < sections.Count; i++)
                {
                    if (sections[i].fillAmount < 1) continue;
                    if (sections[i].fillAmount == sections[i].filler.fillAmount) continue;

                    time = timePerSegment;
                    while (time > 0)
                    {
                        sections[i].filler.fillAmount = (Mathf.Lerp(1, 0, smoothingCurve.Evaluate(time / timePerSegment)));
                        SetColor(sections[i]);
                        time -= Time.deltaTime;
                        yield return new WaitForSeconds(Time.deltaTime);
                    }
                    sections[i].filler.fillAmount = (1);
                    SetColor(sections[i]);
                }
            }
        }
        else
        {
            if (newFillAmount < prevFillAmount)
            {
                for (int i = sections.Count - 1; i > -1; i--)
                {
                    if (sections[i].fillAmount == 1) continue;
                    if (sections[i].fillAmount == sections[i].filler.fillAmount) continue;

                    time = 0;
                    float start = sections[i].filler.fillAmount;
                    float end = sections[i].fillAmount;
                    while (time < timePerSegment)
                    {
                        time += Time.deltaTime;
                        sections[i].filler.fillAmount = (Mathf.Lerp(start, end, smoothingCurve.Evaluate(time / timePerSegment)));
                        SetColor(sections[i]);
                        yield return new WaitForSeconds(Time.deltaTime);
                    }
                }
            }
            else
            {
                for (int i = 0; i < sections.Count; i++)
                {
                    if (sections[i].fillAmount == 0) continue;
                    if (sections[i].fillAmount == sections[i].filler.fillAmount) continue;

                    time = timePerSegment;
                    float end = sections[i].fillAmount;
                    float start = sections[i].filler.fillAmount;
                    while (time > 0)
                    {
                        sections[i].filler.fillAmount = (Mathf.Lerp(end, start, smoothingCurve.Evaluate(time / timePerSegment)));
                        SetColor(sections[i]);
                        time -= Time.deltaTime;
                        yield return new WaitForSeconds(Time.deltaTime);
                    }
                }
            }
        }

        smoothing = false;
    }
    IEnumerator SmoothingFill2()
    {
        smoothing = true;

        float timePerSegment = 0;
        float count = 0;
        for (int i = 0; i < sections.Count; i++)
        {
            if (sections[i].fillAmount != sections[i].secondaryFiller.fillAmount) count++;
        }
        timePerSegment = count == 0 ? progressiveFillingTime : progressiveFillingTime / count;

        float time = 0;
        if (numericFormat == NumericFormat.Integer)
        {
            if (newFillAmount < prevFillAmount)
            {
                for (int i = sections.Count - 1; i > -1; i--)
                {
                    if (sections[i].fillAmount > 0) continue;
                    if (sections[i].fillAmount == sections[i].secondaryFiller.fillAmount) continue;

                    time = 0;
                    while (time < timePerSegment)
                    {
                        time += Time.deltaTime;
                        sections[i].secondaryFiller.fillAmount = (Mathf.Lerp(1, 0, smoothingCurve.Evaluate(time / timePerSegment)));
                        SetColor(sections[i]);
                        yield return new WaitForSeconds(Time.deltaTime);
                    }
                    sections[i].secondaryFiller.fillAmount = (0);
                    SetColor(sections[i]);
                }
            }
            else
            {
                for (int i = 0; i < sections.Count; i++)
                {
                    if (sections[i].fillAmount < 1) continue;
                    if (sections[i].fillAmount == sections[i].secondaryFiller.fillAmount) continue;

                    time = timePerSegment;
                    while (time > 0)
                    {
                        sections[i].secondaryFiller.fillAmount = (Mathf.Lerp(1, 0, smoothingCurve.Evaluate(time / timePerSegment)));
                        SetColor(sections[i]);
                        time -= Time.deltaTime;
                        yield return new WaitForSeconds(Time.deltaTime);
                    }
                    sections[i].secondaryFiller.fillAmount = (1);
                    SetColor(sections[i]);
                }
            }
        }
        else
        {
            if (newFillAmount < prevFillAmount)
            {
                for (int i = sections.Count - 1; i > -1; i--)
                {
                    if (sections[i].fillAmount == 1) continue;
                    if (sections[i].fillAmount == sections[i].secondaryFiller.fillAmount) continue;
                    time = 0;
                    float start = sections[i].secondaryFiller.fillAmount;
                    float end = sections[i].fillAmount;
                    while (time < timePerSegment)
                    {
                        time += Time.deltaTime;
                        sections[i].secondaryFiller.fillAmount = (Mathf.Lerp(start, end, smoothingCurve.Evaluate(time / timePerSegment)));
                        SetColor(sections[i]);
                        yield return new WaitForSeconds(Time.deltaTime);
                    }
                }
            }
            else
            {
                for (int i = 0; i < sections.Count; i++)
                {
                    if (sections[i].fillAmount == 0) continue;
                    if (sections[i].fillAmount == sections[i].secondaryFiller.fillAmount) continue;

                    time = timePerSegment;
                    float end = sections[i].fillAmount;
                    float start = sections[i].secondaryFiller.fillAmount;
                    while (time > 0)
                    {
                        sections[i].secondaryFiller.fillAmount = (Mathf.Lerp(end, start, smoothingCurve.Evaluate(time / timePerSegment)));
                        SetColor(sections[i]);
                        time -= Time.deltaTime;
                        yield return new WaitForSeconds(Time.deltaTime);
                    }
                }
            }
        }

        smoothing = false;
    }
    private void Delayed()
    {
        Instantaneous();
        Smoothed2();
    }


    public void SetColor(BarSection section)
    {

        switch (colorMode)
        {
            case ColorMode.Color:
                section.filler.color = fillerColor;
                section.secondaryFiller.color = secondaryFillerColor;
                break;
            case ColorMode.Gradient:
                section.filler.color = gradient.Evaluate(section.filler.fillAmount);
                section.secondaryFiller.color = gradient.Evaluate(section.secondaryFiller.fillAmount);
                break;
        }
    }

}