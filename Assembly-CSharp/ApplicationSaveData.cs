﻿// Decompiled with JetBrains decompiler
// Type: ApplicationSaveData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B122114D-AAD1-4BC3-90AB-645D18AE6C10
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using System;

[Serializable]
public class ApplicationSaveData
{
  public float versionNumber;

  public static ApplicationSaveData ReadFromGlobals() => new ApplicationSaveData()
  {
    versionNumber = ApplicationGlobals.VersionNumber
  };

  public static void WriteToGlobals(ApplicationSaveData data) => ApplicationGlobals.VersionNumber = data.versionNumber;
}
