﻿// Decompiled with JetBrains decompiler
// Type: YandereSimulator.Yancord.PlayerPrefsHelper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D5F971C-3CB1-4F04-A688-57005AB18418
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace YandereSimulator.Yancord
{
  public static class PlayerPrefsHelper
  {
    public static void SetBool(string name, bool flag) => PlayerPrefs.SetInt(name, flag ? 1 : 0);

    public static bool GetBool(string name) => PlayerPrefs.GetInt(name) == 1;
  }
}
