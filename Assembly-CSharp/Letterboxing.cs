﻿// Decompiled with JetBrains decompiler
// Type: Letterboxing
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03C576EE-B2A0-4A87-90DA-D90BE80DF8AE
// Assembly location: C:\YandereSimulator\latest\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

[RequireComponent(typeof (Camera))]
public class Letterboxing : MonoBehaviour
{
  private const float KEEP_ASPECT = 1.77777779f;

  private void Start()
  {
    float num = (float) (1.0 - (double) ((float) Screen.width / (float) Screen.height) / 1.7777777910232544);
    this.GetComponent<Camera>().rect = new Rect(0.0f, num / 2f, 1f, 1f - num);
  }
}
