﻿// Decompiled with JetBrains decompiler
// Type: DistractionScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 76B31E51-17DB-470B-BEBA-6CF1F4AD2F4E
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class DistractionScript : MonoBehaviour
{
  private int Frame;

  private void Update()
  {
    if (this.Frame > 5)
      Object.Destroy((Object) this.gameObject);
    ++this.Frame;
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.layer != 9)
      return;
    EvilPhotographerScript component = other.gameObject.GetComponent<EvilPhotographerScript>();
    if (!((Object) component != (Object) null))
      return;
    component.DistractionPoint = this.transform.position;
    component.DistractionTimer = 5f;
    component.Distracted = true;
  }
}
