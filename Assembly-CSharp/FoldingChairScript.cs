﻿// Decompiled with JetBrains decompiler
// Type: FoldingChairScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D5F971C-3CB1-4F04-A688-57005AB18418
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class FoldingChairScript : MonoBehaviour
{
  public GameObject[] Student;

  private void Start() => Object.Instantiate<GameObject>(this.Student[Random.Range(0, this.Student.Length)], this.transform.position - new Vector3(0.0f, 0.4f, 0.0f), this.transform.rotation);
}
