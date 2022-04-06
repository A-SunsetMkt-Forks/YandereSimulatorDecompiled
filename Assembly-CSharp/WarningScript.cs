﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020004BF RID: 1215
public class WarningScript : MonoBehaviour
{
	// Token: 0x06001FC9 RID: 8137 RVA: 0x001C05A8 File Offset: 0x001BE7A8
	private void Start()
	{
		this.WarningLabel.gameObject.SetActive(false);
		this.Label.text = string.Empty;
		this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, 1f);
	}

	// Token: 0x06001FCA RID: 8138 RVA: 0x001C061C File Offset: 0x001BE81C
	private void Update()
	{
		AudioSource component = base.GetComponent<AudioSource>();
		if (!this.FadeOut)
		{
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 0f, Time.deltaTime));
			if (this.Darkness.color.a == 0f)
			{
				if (this.Timer == 0f)
				{
					this.WarningLabel.gameObject.SetActive(true);
					component.Play();
				}
				this.Timer += Time.deltaTime;
				if (this.ID < this.Triggers.Length && this.Timer > this.Triggers[this.ID])
				{
					this.Label.text = this.Text[this.ID];
					this.ID++;
				}
			}
		}
		else
		{
			component.volume = Mathf.MoveTowards(component.volume, 0f, Time.deltaTime);
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 1f, Time.deltaTime));
			if (this.Darkness.color.a == 1f)
			{
				SceneManager.LoadScene("SponsorScene");
			}
		}
		if (Input.anyKey)
		{
			this.FadeOut = true;
		}
	}

	// Token: 0x0400428C RID: 17036
	public float[] Triggers;

	// Token: 0x0400428D RID: 17037
	public string[] Text;

	// Token: 0x0400428E RID: 17038
	public UILabel WarningLabel;

	// Token: 0x0400428F RID: 17039
	public UISprite Darkness;

	// Token: 0x04004290 RID: 17040
	public UILabel Label;

	// Token: 0x04004291 RID: 17041
	public bool FadeOut;

	// Token: 0x04004292 RID: 17042
	public float Timer;

	// Token: 0x04004293 RID: 17043
	public int ID;
}
