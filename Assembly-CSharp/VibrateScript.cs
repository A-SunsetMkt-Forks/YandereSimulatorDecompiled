﻿// Decompiled with JetBrains decompiler
// Type: VibrateScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D5F971C-3CB1-4F04-A688-57005AB18418
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class VibrateScript : MonoBehaviour
{
  public Vector3 Origin;

  private void Start() => this.Origin = this.transform.localPosition;

  private void Update() => this.transform.localPosition = new Vector3(this.Origin.x + Random.Range(-5f, 5f), this.Origin.y + Random.Range(-5f, 5f), this.transform.localPosition.z);
}
