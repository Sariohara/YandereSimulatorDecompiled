﻿// Decompiled with JetBrains decompiler
// Type: CirnoIceAttackScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B122114D-AAD1-4BC3-90AB-645D18AE6C10
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class CirnoIceAttackScript : MonoBehaviour
{
  public GameObject IceExplosion;

  private void Start()
  {
    Physics.IgnoreLayerCollision(18, 13, true);
    Physics.IgnoreLayerCollision(18, 18, true);
  }

  private void OnCollisionEnter(Collision collision)
  {
    Object.Instantiate<GameObject>(this.IceExplosion, this.transform.position, Quaternion.identity);
    if (collision.gameObject.layer == 9)
    {
      StudentScript component = collision.gameObject.GetComponent<StudentScript>();
      if ((Object) component != (Object) null && component.StudentID != 1)
      {
        component.SpawnAlarmDisc();
        component.BecomeRagdoll();
      }
    }
    Object.Destroy((Object) this.gameObject);
  }
}
