using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InterfaceElement : MonoBehaviour
{
    public RectTransform rectTransform;
    public virtual void Refresh(InterfaceData interfaceData) { }
}
