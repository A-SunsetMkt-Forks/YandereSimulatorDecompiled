﻿// Decompiled with JetBrains decompiler
// Type: ArcTrailScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 76B31E51-17DB-470B-BEBA-6CF1F4AD2F4E
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class ArcTrailScript : MonoBehaviour
{
  private static readonly Color TRAIL_TINT_COLOR = new Color(1f, 0.0f, 0.0f, 1f);
  public TrailRenderer Trail;

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.layer != 9)
      return;
    this.Trail.material.SetColor("_TintColor", ArcTrailScript.TRAIL_TINT_COLOR);
  }
}
