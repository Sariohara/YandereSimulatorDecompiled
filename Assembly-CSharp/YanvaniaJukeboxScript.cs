﻿// Decompiled with JetBrains decompiler
// Type: YanvaniaJukeboxScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F8D6662-C74B-4D30-A4EA-D74F7A9A95B9
// Assembly location: C:\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class YanvaniaJukeboxScript : MonoBehaviour
{
  public AudioClip BossIntro;
  public AudioClip BossMain;
  public AudioClip ApproachIntro;
  public AudioClip ApproachMain;
  public bool Boss;

  private void Update()
  {
    AudioSource component = this.GetComponent<AudioSource>();
    if ((double) component.time + (double) Time.deltaTime <= (double) component.clip.length)
      return;
    component.clip = this.Boss ? this.BossMain : this.ApproachMain;
    component.loop = true;
    component.Play();
  }

  public void BossBattle()
  {
    AudioSource component = this.GetComponent<AudioSource>();
    component.clip = this.BossIntro;
    component.loop = false;
    component.volume = 0.25f;
    component.Play();
    this.Boss = true;
  }
}
