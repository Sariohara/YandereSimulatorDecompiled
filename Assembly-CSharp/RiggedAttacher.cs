﻿// Decompiled with JetBrains decompiler
// Type: RiggedAttacher
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03C576EE-B2A0-4A87-90DA-D90BE80DF8AE
// Assembly location: C:\YandereSimulator\latest\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class RiggedAttacher : MonoBehaviour
{
  public Transform BasePelvisRoot;
  public Transform AttachmentPelvisRoot;

  private void Start() => this.Attaching(this.BasePelvisRoot, this.AttachmentPelvisRoot);

  private void Attaching(Transform Base, Transform Attachment)
  {
    Attachment.transform.SetParent(Base);
    Base.localEulerAngles = Vector3.zero;
    Base.localPosition = Vector3.zero;
    Transform[] componentsInChildren = Base.GetComponentsInChildren<Transform>();
    foreach (Transform componentsInChild in Attachment.GetComponentsInChildren<Transform>())
    {
      foreach (Transform p in componentsInChildren)
      {
        if (componentsInChild.name == p.name)
        {
          componentsInChild.SetParent(p);
          componentsInChild.localEulerAngles = Vector3.zero;
          componentsInChild.localPosition = Vector3.zero;
        }
      }
    }
  }
}
