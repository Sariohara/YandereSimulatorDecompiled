﻿// Decompiled with JetBrains decompiler
// Type: AudioClipArrayWrapper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A8EFE0B-B8E4-42A1-A228-F35734F77857
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

[Serializable]
public class AudioClipArrayWrapper : ArrayWrapper<AudioClip>
{
  public AudioClipArrayWrapper(int size)
    : base(size)
  {
  }

  public AudioClipArrayWrapper(AudioClip[] elements)
    : base(elements)
  {
  }
}
