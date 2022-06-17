﻿// Decompiled with JetBrains decompiler
// Type: YancordGlobals
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75854DFC-6606-4168-9C8E-2538EB1902DD
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public static class YancordGlobals
{
  private const string Str_JoinedYancord = "JoinedYancord";
  private const string Str_CurrentConversation = "CurrentConversation";

  public static bool JoinedYancord
  {
    get => GlobalsHelper.GetBool("Profile_" + GameGlobals.Profile.ToString() + "_JoinedYancord");
    set => GlobalsHelper.SetBool("Profile_" + GameGlobals.Profile.ToString() + "_JoinedYancord", value);
  }

  public static int CurrentConversation
  {
    get => PlayerPrefs.GetInt("Profile_" + GameGlobals.Profile.ToString() + "_CurrentConversation");
    set => PlayerPrefs.SetInt("Profile_" + GameGlobals.Profile.ToString() + "_CurrentConversation", value);
  }

  public static void DeleteAll()
  {
    Globals.Delete("Profile_" + GameGlobals.Profile.ToString() + "_JoinedYancord");
    Globals.Delete("Profile_" + GameGlobals.Profile.ToString() + "_CurrentConversation");
  }
}
