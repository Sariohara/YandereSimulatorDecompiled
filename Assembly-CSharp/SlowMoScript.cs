﻿// Decompiled with JetBrains decompiler
// Type: SlowMoScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03C576EE-B2A0-4A87-90DA-D90BE80DF8AE
// Assembly location: C:\YandereSimulator\latest\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class SlowMoScript : MonoBehaviour
{
  public bool Spinning;
  public float Speed;

  private void Update()
  {
    if (Input.GetKeyDown("s"))
      this.Spinning = !this.Spinning;
    if (Input.GetKeyDown("a"))
      Time.timeScale = 0.1f;
    if (Input.GetKeyDown("-"))
      --Time.timeScale;
    if (Input.GetKeyDown("="))
      ++Time.timeScale;
    if (Input.GetKeyDown("z"))
      this.Speed += Time.deltaTime;
    if ((double) this.Speed > 0.0)
      this.transform.position += new Vector3(Time.deltaTime * 0.1f, 0.0f, Time.deltaTime * 0.1f);
    if (!this.Spinning)
      return;
    this.transform.parent.transform.localEulerAngles += new Vector3(0.0f, Time.deltaTime * 36f, 0.0f);
  }
}
