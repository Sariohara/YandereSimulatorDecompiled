﻿// Decompiled with JetBrains decompiler
// Type: DoorOpenerScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A8EFE0B-B8E4-42A1-A228-F35734F77857
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class DoorOpenerScript : MonoBehaviour
{
  public StudentScript Student;
  public DoorScript Door;

  private void Start()
  {
    this.gameObject.layer = 1;
    BoxCollider component = this.GetComponent<BoxCollider>();
    if ((double) component.size.y <= 0.5)
      return;
    this.transform.localPosition = new Vector3(this.transform.localPosition.x, 0.25f, this.transform.localPosition.z);
    component.size = new Vector3(component.size.x, 0.5f, component.size.y);
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.layer != 9)
      return;
    this.Student = other.gameObject.GetComponent<StudentScript>();
    if (!((Object) this.Student != (Object) null) || this.Student.Dying || this.Door.Open || this.Door.Locked)
      return;
    this.Door.Student = this.Student;
    this.Door.OpenDoor();
  }

  private void OnTriggerStay(Collider other)
  {
    if (this.Door.Open || other.gameObject.layer != 9)
      return;
    this.Student = other.gameObject.GetComponent<StudentScript>();
    if (!((Object) this.Student != (Object) null) || this.Student.Dying || this.Door.Open || this.Door.Locked)
      return;
    this.Door.Student = this.Student;
    this.Door.OpenDoor();
  }
}
