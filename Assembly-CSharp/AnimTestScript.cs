﻿// Decompiled with JetBrains decompiler
// Type: AnimTestScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F8D6662-C74B-4D30-A4EA-D74F7A9A95B9
// Assembly location: C:\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class AnimTestScript : MonoBehaviour
{
  public Animation CharacterA;
  public Animation CharacterB;
  public int ID;

  private void Start() => Time.timeScale = 1f;

  private void Update()
  {
    if (Input.GetKeyDown("space"))
    {
      ++this.ID;
      if (this.ID > 4)
        this.ID = 1;
    }
    if (this.ID == 1)
    {
      this.CharacterB.transform.eulerAngles = new Vector3(0.0f, -90f, 0.0f);
      this.CharacterA.Play("f02_weightHighSanityA_00");
      this.CharacterB.Play("f02_weightHighSanityB_00");
    }
    else if (this.ID == 2)
    {
      this.CharacterA.Play("f02_weightMedSanityA_00");
      this.CharacterB.Play("f02_weightMedSanityB_00");
    }
    else if (this.ID == 3)
    {
      this.CharacterA.Play("f02_weightLowSanityA_00");
      this.CharacterB.Play("f02_weightLowSanityB_00");
    }
    else
    {
      if (this.ID != 4)
        return;
      this.CharacterB.transform.eulerAngles = new Vector3(0.0f, 90f, 0.0f);
      this.CharacterA.Play("f02_weightStealthA_00");
      this.CharacterB.Play("f02_weightStealthB_00");
    }
  }
}
