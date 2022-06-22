﻿// Decompiled with JetBrains decompiler
// Type: SponsorScript
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 41FC567F-B14D-47B6-963A-CEFC38C7B329
// Assembly location: C:\YandereSimulator\YandereSimulator\YandereSimulator_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.SceneManagement;

public class SponsorScript : MonoBehaviour
{
  public GameObject[] Set;
  public UISprite Darkness;
  public float Timer;
  public int ID;

  private void Start()
  {
    Time.timeScale = 1f;
    this.Set[1].SetActive(true);
    this.Set[2].SetActive(false);
    this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, 1f);
  }

  private void Update()
  {
    this.Timer += Time.deltaTime;
    if ((double) this.Timer < 3.20000004768372)
    {
      this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 0.0f, Time.deltaTime));
    }
    else
    {
      this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 1f, Time.deltaTime));
      if ((double) this.Darkness.color.a != 1.0)
        return;
      SceneManager.LoadScene("NewTitleScene");
    }
  }
}
