﻿// Decompiled with JetBrains decompiler
// Type: Stance
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75854DFC-6606-4168-9C8E-2538EB1902DD
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

[Serializable]
public class Stance
{
  [SerializeField]
  private StanceType current;
  [SerializeField]
  private StanceType previous;

  public Stance(StanceType initialStance)
  {
    this.current = initialStance;
    this.previous = initialStance;
  }

  public StanceType Current
  {
    get => this.current;
    set
    {
      this.previous = this.current;
      this.current = value;
    }
  }

  public StanceType Previous => this.previous;
}
