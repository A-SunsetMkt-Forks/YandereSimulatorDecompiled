﻿// Decompiled with JetBrains decompiler
// Type: LookAtSCP
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 41FC567F-B14D-47B6-963A-CEFC38C7B329
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class LookAtSCP : MonoBehaviour
{
  public Transform SCP;

  private void Start()
  {
    if ((Object) this.SCP == (Object) null)
      this.SCP = GameObject.Find("SCPTarget").transform;
    this.transform.LookAt(this.SCP);
  }

  private void LateUpdate() => this.transform.LookAt(this.SCP);
}
