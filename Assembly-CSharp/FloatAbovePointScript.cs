﻿// Decompiled with JetBrains decompiler
// Type: FloatAbovePointScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D5F971C-3CB1-4F04-A688-57005AB18418
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class FloatAbovePointScript : MonoBehaviour
{
  public Transform Target;

  private void Update() => this.transform.position = this.Target.position + new Vector3(0.0f, 0.15f, 0.0f);
}
