﻿// Decompiled with JetBrains decompiler
// Type: MiniMapEntity
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 12831466-57D6-4F5A-B867-CD140BE439C0
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class MiniMapEntity
{
  public bool showDetails;
  public UnityEngine.Sprite icon;
  public bool rotateWithObject = true;
  public Vector3 upAxis;
  public float rotation;
  public Vector2 size;
  public bool clampInBorder;
  public float clampDist;
  public List<GameObject> mapObjects;
}