﻿// Decompiled with JetBrains decompiler
// Type: UnityStandardAssets.Vehicles.Car.Suspension
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A8EFE0B-B8E4-42A1-A228-F35734F77857
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace UnityStandardAssets.Vehicles.Car
{
  public class Suspension : MonoBehaviour
  {
    public GameObject wheel;
    private Vector3 m_TargetOriginalPosition;
    private Vector3 m_Origin;

    private void Start()
    {
      this.m_TargetOriginalPosition = this.wheel.transform.localPosition;
      this.m_Origin = this.transform.localPosition;
    }

    private void Update() => this.transform.localPosition = this.m_Origin + (this.wheel.transform.localPosition - this.m_TargetOriginalPosition);
  }
}
