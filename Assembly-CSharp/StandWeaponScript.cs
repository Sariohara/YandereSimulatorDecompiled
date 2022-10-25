﻿// Decompiled with JetBrains decompiler
// Type: StandWeaponScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03C576EE-B2A0-4A87-90DA-D90BE80DF8AE
// Assembly location: C:\YandereSimulator\latest\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class StandWeaponScript : MonoBehaviour
{
  public PromptScript Prompt;
  public StandScript Stand;
  public float RotationSpeed;

  private void Update()
  {
    if (this.Prompt.enabled)
    {
      if ((double) this.Prompt.Circle[0].fillAmount != 0.0)
        return;
      this.MoveToStand();
    }
    else
    {
      this.transform.Rotate(Vector3.forward * (Time.deltaTime * this.RotationSpeed));
      this.transform.Rotate(Vector3.right * (Time.deltaTime * this.RotationSpeed));
      this.transform.Rotate(Vector3.up * (Time.deltaTime * this.RotationSpeed));
    }
  }

  private void MoveToStand()
  {
    this.Prompt.Hide();
    this.Prompt.enabled = false;
    this.Prompt.MyCollider.enabled = false;
    ++this.Stand.Weapons;
    this.transform.parent = this.Stand.Hands[this.Stand.Weapons];
    this.transform.localPosition = new Vector3(-0.277f, 0.0f, 0.0f);
  }
}
