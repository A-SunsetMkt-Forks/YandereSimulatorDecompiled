﻿// Decompiled with JetBrains decompiler
// Type: StandPunchScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 41FC567F-B14D-47B6-963A-CEFC38C7B329
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class StandPunchScript : MonoBehaviour
{
  public Collider MyCollider;

  private void OnTriggerEnter(Collider other)
  {
    StudentScript component = other.gameObject.GetComponent<StudentScript>();
    if (!((Object) component != (Object) null) || component.StudentID <= 1)
      return;
    component.JojoReact();
  }
}
