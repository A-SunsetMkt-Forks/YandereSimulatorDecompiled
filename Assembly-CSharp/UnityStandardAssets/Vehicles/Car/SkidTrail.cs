﻿// Decompiled with JetBrains decompiler
// Type: UnityStandardAssets.Vehicles.Car.SkidTrail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75854DFC-6606-4168-9C8E-2538EB1902DD
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Car
{
  public class SkidTrail : MonoBehaviour
  {
    [SerializeField]
    private float m_PersistTime;

    private IEnumerator Start()
    {
      SkidTrail skidTrail = this;
      while (true)
      {
        do
        {
          yield return (object) null;
        }
        while (!((Object) skidTrail.transform.parent.parent == (Object) null));
        Object.Destroy((Object) skidTrail.gameObject, skidTrail.m_PersistTime);
      }
    }
  }
}
