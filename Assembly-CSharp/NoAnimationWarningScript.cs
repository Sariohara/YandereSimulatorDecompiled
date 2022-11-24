﻿// Decompiled with JetBrains decompiler
// Type: NoAnimationWarningScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F38A0724-AA2E-44D4-AF10-35004D386EF8
// Assembly location: D:\YandereSimulator\latest\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.SceneManagement;

public class NoAnimationWarningScript : MonoBehaviour
{
  public UISprite Darkness;
  public bool FadeOut;
  public float Alpha;

  private void Start() => this.Darkness.color = new Color(0.0f, 0.0f, 0.0f, 1f);

  private void Update()
  {
    if (!this.FadeOut)
    {
      this.Alpha = Mathf.MoveTowards(this.Alpha, 0.0f, Time.deltaTime);
      this.Darkness.color = new Color(0.0f, 0.0f, 0.0f, this.Alpha);
      if ((double) this.Alpha != 0.0 || !Input.GetButtonDown("A"))
        return;
      this.FadeOut = true;
    }
    else
    {
      this.Alpha = Mathf.MoveTowards(this.Alpha, 1f, Time.deltaTime);
      this.Darkness.color = new Color(0.0f, 0.0f, 0.0f, this.Alpha);
      if ((double) this.Alpha != 1.0)
        return;
      SceneManager.LoadScene("BusStopScene");
    }
  }
}
