﻿// Decompiled with JetBrains decompiler
// Type: LoadingScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 76B31E51-17DB-470B-BEBA-6CF1F4AD2F4E
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScript : MonoBehaviour
{
  private void Start() => SceneManager.LoadScene("SchoolScene");
}
