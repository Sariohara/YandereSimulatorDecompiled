﻿// Decompiled with JetBrains decompiler
// Type: CreditsLabelScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F8D6662-C74B-4D30-A4EA-D74F7A9A95B9
// Assembly location: C:\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class CreditsLabelScript : MonoBehaviour
{
  public float RotationSpeed;
  public float MovementSpeed;
  public float Rotation;

  private void Start()
  {
    this.Rotation = -90f;
    this.transform.localEulerAngles = new Vector3(this.transform.localEulerAngles.x, this.Rotation, this.transform.localEulerAngles.z);
  }

  private void Update()
  {
    this.Rotation += Time.deltaTime * this.RotationSpeed;
    this.transform.localEulerAngles = new Vector3(this.transform.localEulerAngles.x, this.Rotation, this.transform.localEulerAngles.z);
    this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y + Time.deltaTime * this.MovementSpeed, this.transform.localPosition.z);
    if ((double) this.Rotation <= 90.0)
      return;
    Object.Destroy((Object) this.gameObject);
  }
}
