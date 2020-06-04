using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparencyBlockData : IBlockComponentData
{
    public float transparency;
    public TransparencyBlockData(float transparency)
    {
        this.transparency = Mathf.Clamp(transparency, 0f, 1f);
    }
}
