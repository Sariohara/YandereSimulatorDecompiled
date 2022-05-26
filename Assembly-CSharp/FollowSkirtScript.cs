﻿// Decompiled with JetBrains decompiler
// Type: FollowSkirtScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F8D6662-C74B-4D30-A4EA-D74F7A9A95B9
// Assembly location: C:\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class FollowSkirtScript : MonoBehaviour
{
  public Transform[] TargetSkirtFront;
  public Transform[] TargetSkirtBack;
  public Transform[] TargetSkirtRight;
  public Transform[] TargetSkirtLeft;
  public Transform[] SkirtFront;
  public Transform[] SkirtBack;
  public Transform[] SkirtRight;
  public Transform[] SkirtLeft;
  public Transform TargetSkirtHips;
  public Transform SkirtHips;

  private void LateUpdate()
  {
    this.SkirtHips.position = this.TargetSkirtHips.position;
    for (int index = 0; index < 3; ++index)
    {
      this.SkirtFront[index].position = this.TargetSkirtFront[index].position;
      this.SkirtFront[index].rotation = this.TargetSkirtFront[index].rotation;
      this.SkirtBack[index].position = this.TargetSkirtBack[index].position;
      this.SkirtBack[index].rotation = this.TargetSkirtBack[index].rotation;
      this.SkirtRight[index].position = this.TargetSkirtRight[index].position;
      this.SkirtRight[index].rotation = this.TargetSkirtRight[index].rotation;
      this.SkirtLeft[index].position = this.TargetSkirtLeft[index].position;
      this.SkirtLeft[index].rotation = this.TargetSkirtLeft[index].rotation;
    }
  }
}
