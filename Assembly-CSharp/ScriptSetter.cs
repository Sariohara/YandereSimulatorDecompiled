﻿// Decompiled with JetBrains decompiler
// Type: ScriptSetter
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A8EFE0B-B8E4-42A1-A228-F35734F77857
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using System.Reflection;
using UnityEngine;

public class ScriptSetter : MonoBehaviour
{
  public StudentScript OldStudent;
  public StudentScript NewStudent;

  private void Start()
  {
    foreach (Component component in this.GetComponents(typeof (Component)))
    {
      Debug.Log((object) ("name " + component.name + " type " + ((object) component).GetType()?.ToString() + " basetype " + ((object) component).GetType().BaseType?.ToString()));
      foreach (FieldInfo field in ((object) component).GetType().GetFields())
      {
        object obj = (object) component;
        Debug.Log((object) (field.Name + " value is: " + field.GetValue(obj)?.ToString()));
      }
    }
  }
}
