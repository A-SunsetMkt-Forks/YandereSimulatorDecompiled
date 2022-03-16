﻿using System;
using UnityEngine;

// Token: 0x02000469 RID: 1129
public class TapePlayerScript : MonoBehaviour
{
	// Token: 0x06001EA0 RID: 7840 RVA: 0x001AF334 File Offset: 0x001AD534
	private void Start()
	{
		this.Tape.SetActive(false);
	}

	// Token: 0x06001EA1 RID: 7841 RVA: 0x001AF344 File Offset: 0x001AD544
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Yandere.HeartCamera.enabled = false;
			this.Yandere.RPGCamera.enabled = false;
			this.TapePlayerMenu.TimeBar.gameObject.SetActive(true);
			this.TapePlayerMenu.List.gameObject.SetActive(true);
			this.TapePlayerCamera.enabled = true;
			this.TapePlayerMenu.UpdateLabels();
			this.TapePlayerMenu.Show = true;
			this.NoteWindow.SetActive(false);
			this.Yandere.CanMove = false;
			this.Yandere.HUD.alpha = 0f;
			Time.timeScale = 0.0001f;
			this.PromptBar.ClearButtons();
			this.PromptBar.Label[1].text = "EXIT";
			this.PromptBar.Label[4].text = "CHOOSE";
			this.PromptBar.Label[5].text = "CATEGORY";
			this.TapePlayerMenu.CheckSelection();
			this.PromptBar.Show = true;
			this.Prompt.Hide();
			this.Prompt.enabled = false;
		}
		if (this.Spin)
		{
			Transform transform = this.Rolls[0];
			transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + 0.016666668f * (360f * this.SpinSpeed), transform.localEulerAngles.z);
			Transform transform2 = this.Rolls[1];
			transform2.localEulerAngles = new Vector3(transform2.localEulerAngles.x, transform2.localEulerAngles.y + 0.016666668f * (360f * this.SpinSpeed), transform2.localEulerAngles.z);
		}
		if (this.FastForward)
		{
			this.FFButton.localEulerAngles = new Vector3(Mathf.MoveTowards(this.FFButton.localEulerAngles.x, 6.25f, 1.6666666f), this.FFButton.localEulerAngles.y, this.FFButton.localEulerAngles.z);
			this.SpinSpeed = 2f;
		}
		else
		{
			this.FFButton.localEulerAngles = new Vector3(Mathf.MoveTowards(this.FFButton.localEulerAngles.x, 0f, 1.6666666f), this.FFButton.localEulerAngles.y, this.FFButton.localEulerAngles.z);
			this.SpinSpeed = 1f;
		}
		if (this.Rewind)
		{
			this.RWButton.localEulerAngles = new Vector3(Mathf.MoveTowards(this.RWButton.localEulerAngles.x, 6.25f, 1.6666666f), this.RWButton.localEulerAngles.y, this.RWButton.localEulerAngles.z);
			this.SpinSpeed = -2f;
			return;
		}
		this.RWButton.localEulerAngles = new Vector3(Mathf.MoveTowards(this.RWButton.localEulerAngles.x, 0f, 1.6666666f), this.RWButton.localEulerAngles.y, this.RWButton.localEulerAngles.z);
	}

	// Token: 0x04003F5F RID: 16223
	public TapePlayerMenuScript TapePlayerMenu;

	// Token: 0x04003F60 RID: 16224
	public PromptBarScript PromptBar;

	// Token: 0x04003F61 RID: 16225
	public YandereScript Yandere;

	// Token: 0x04003F62 RID: 16226
	public PromptScript Prompt;

	// Token: 0x04003F63 RID: 16227
	public Transform RWButton;

	// Token: 0x04003F64 RID: 16228
	public Transform FFButton;

	// Token: 0x04003F65 RID: 16229
	public Camera TapePlayerCamera;

	// Token: 0x04003F66 RID: 16230
	public Transform[] Rolls;

	// Token: 0x04003F67 RID: 16231
	public GameObject NoteWindow;

	// Token: 0x04003F68 RID: 16232
	public GameObject Tape;

	// Token: 0x04003F69 RID: 16233
	public bool FastForward;

	// Token: 0x04003F6A RID: 16234
	public bool Rewind;

	// Token: 0x04003F6B RID: 16235
	public bool Spin;

	// Token: 0x04003F6C RID: 16236
	public float SpinSpeed;
}
