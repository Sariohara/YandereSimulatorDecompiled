﻿// Decompiled with JetBrains decompiler
// Type: HomeWindowScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 03C576EE-B2A0-4A87-90DA-D90BE80DF8AE
// Assembly location: C:\YandereSimulator\latest\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class HomeWindowScript : MonoBehaviour
{
  public UISprite Sprite;
  public bool Show;

  private void Start() => this.Sprite.color = new Color(this.Sprite.color.r, this.Sprite.color.g, this.Sprite.color.b, 0.0f);

  private void Update() => this.Sprite.color = new Color(this.Sprite.color.r, this.Sprite.color.g, this.Sprite.color.b, Mathf.Lerp(this.Sprite.color.a, this.Show ? 1f : 0.0f, Time.deltaTime * 10f));
}
