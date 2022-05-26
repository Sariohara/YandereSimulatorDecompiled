﻿// Decompiled with JetBrains decompiler
// Type: CheckmarkScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F8D6662-C74B-4D30-A4EA-D74F7A9A95B9
// Assembly location: C:\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class CheckmarkScript : MonoBehaviour
{
  public GameObject[] Checkmarks;
  public int ButtonPresses;
  public int ID;

  private void Start()
  {
    for (; this.ID < this.Checkmarks.Length; ++this.ID)
      this.Checkmarks[this.ID].SetActive(false);
    this.ID = 0;
  }

  private void Update()
  {
    if (!Input.GetKeyDown("space") || this.ButtonPresses >= 26)
      return;
    ++this.ButtonPresses;
    this.ID = Random.Range(0, this.Checkmarks.Length - 4);
    while (this.Checkmarks[this.ID].active)
      this.ID = Random.Range(0, this.Checkmarks.Length - 4);
    this.Checkmarks[this.ID].SetActive(true);
  }
}
