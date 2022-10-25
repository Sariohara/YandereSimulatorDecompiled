﻿// Decompiled with JetBrains decompiler
// Type: StopAnimationScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03C576EE-B2A0-4A87-90DA-D90BE80DF8AE
// Assembly location: C:\YandereSimulator\latest\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class StopAnimationScript : MonoBehaviour
{
  public StudentManagerScript StudentManager;
  public Transform Yandere;
  private Animation Anim;

  private void Start()
  {
    this.StudentManager = GameObject.Find("StudentManager").GetComponent<StudentManagerScript>();
    this.Anim = this.GetComponent<Animation>();
  }

  private void Update()
  {
    if (this.StudentManager.DisableFarAnims)
    {
      if ((double) Vector3.Distance(this.Yandere.position, this.transform.position) > 15.0)
      {
        if (!this.Anim.enabled)
          return;
        this.Anim.enabled = false;
      }
      else
      {
        if (this.Anim.enabled)
          return;
        this.Anim.enabled = true;
      }
    }
    else
    {
      if (this.Anim.enabled)
        return;
      this.Anim.enabled = true;
    }
  }
}
