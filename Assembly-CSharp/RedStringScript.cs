﻿// Decompiled with JetBrains decompiler
// Type: RedStringScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A8EFE0B-B8E4-42A1-A228-F35734F77857
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class RedStringScript : MonoBehaviour
{
  public Transform Target;

  private void LateUpdate()
  {
    this.transform.LookAt(this.Target.position);
    this.transform.localScale = new Vector3(1f, 1f, Vector3.Distance(this.transform.position, this.Target.position));
  }
}
