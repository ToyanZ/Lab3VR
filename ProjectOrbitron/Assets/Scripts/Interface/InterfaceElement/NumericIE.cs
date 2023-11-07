using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumericIE : InterfaceElement
{
    public enum NumericType { SingleValue, Percentage, Ratio, Seconds }
    public enum NumericFormat { Raw, Integer, PointOne, PointTwo }
    public enum TitleFormat { Default, UseAssigned, GetFromInterfaceData}
    public enum RoundingMode { None, Ceiling, Floor}

    [Space(20)]
    public NumericType numericType = NumericType.Percentage;
    public string singleValueMetricUnit = "";
    public string pctFormat = " %";
    public string secondsFormat = " s";
    public string ratioFormat = "|";
    
    [Space(10)]
    public NumericFormat numericFormat = NumericFormat.Integer;
    public RoundingMode roundingMode = RoundingMode.Floor;

    [Space(5)]
    public TitleFormat titleFormat = TitleFormat.Default;
    public TMP_Text titleText;
    public string title = "";
    public bool replaceCurrentWithTitleOnZero = false;

    [Space(5)]
    public TMP_Text currentValueText;
    public TMP_Text maxValueText;


    protected float currentValue = 0;
    protected float maxValue = 0;
    public override void Refresh(InterfaceData interfaceData)
    {
        switch (titleFormat)
        {
            case TitleFormat.Default:

                break;
            case TitleFormat.UseAssigned:
                titleText.text = title;
                break;
            case TitleFormat.GetFromInterfaceData:
                titleText.text = interfaceData.GetDisplayValue();
                break;
        }


        string format = string.Empty;
        switch(numericFormat)
        {
            case NumericFormat.Integer:
                format = "0";
                break;
            case NumericFormat.PointOne:
                format = "0.0";
                break;
            case NumericFormat.PointTwo:
                format = "0.00";
                break;
            case NumericFormat.Raw: 
                format = string.Empty;
                break;
        }

        maxValue = interfaceData.GetMaxValue();
        currentValue = interfaceData.GetCurrentValue();
        
        float pctValue = 0.0f;
        float currentValueMod = 0;
        switch (roundingMode)
        {
            case RoundingMode.Floor:
                pctValue = Mathf.Floor(currentValue / maxValue * 100.0f);
                currentValueMod = Mathf.Floor(currentValue);
                break;
            case RoundingMode.Ceiling:
                pctValue = Mathf.Ceil(currentValue / maxValue * 100.0f);
                currentValueMod = Mathf.Ceil(currentValue);
                break;
            case RoundingMode.None:
                pctValue = currentValue / maxValue * 100.0f;
                currentValueMod = currentValue;
                break;
        }

        if (replaceCurrentWithTitleOnZero && currentValueMod == 0)
        {
            currentValueText.text = interfaceData.GetDisplayValue();
            maxValueText.text = "";
        }
        else
        {
            switch (numericType)
            {
                case NumericType.SingleValue:
                    currentValueText.text = currentValueMod.ToString(format) + singleValueMetricUnit;
                    maxValueText.text = "";
                    break;
                case NumericType.Percentage:
                    currentValueText.text = pctValue.ToString(format) + pctFormat;
                    maxValueText.text = "";
                    break;
                case NumericType.Seconds:
                    currentValueText.text = currentValueMod.ToString(format) + secondsFormat;
                    maxValueText.text = "";
                    break;
                case NumericType.Ratio:
                    currentValueText.text = currentValueMod.ToString(format) + ratioFormat;
                    maxValueText.text = maxValue.ToString(format);
                    break;
            }
        }
    }
}
