﻿// Decompiled with JetBrains decompiler
// Type: MaskingTapeScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 41FC567F-B14D-47B6-963A-CEFC38C7B329
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class MaskingTapeScript : MonoBehaviour
{
  public CarryableCardboardBoxScript Box;
  public PromptScript Prompt;

  private void Update()
  {
    if ((double) this.Prompt.Circle[0].fillAmount != 0.0)
      return;
    this.Prompt.Yandere.Inventory.MaskingTape = true;
    this.Box.Prompt.enabled = true;
    this.Box.enabled = true;
    this.gameObject.SetActive(false);
  }
}
