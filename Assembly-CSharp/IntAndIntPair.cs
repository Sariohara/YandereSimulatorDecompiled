﻿// Decompiled with JetBrains decompiler
// Type: IntAndIntPair
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F8D6662-C74B-4D30-A4EA-D74F7A9A95B9
// Assembly location: C:\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using System;

[Serializable]
public class IntAndIntPair : SerializablePair<int, int>
{
  public IntAndIntPair(int first, int second)
    : base(first, second)
  {
  }

  public IntAndIntPair()
    : base(0, 0)
  {
  }
}
