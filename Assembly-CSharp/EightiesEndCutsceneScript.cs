﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200029F RID: 671
public class EightiesEndCutsceneScript : MonoBehaviour
{
	// Token: 0x06001417 RID: 5143 RVA: 0x000BF494 File Offset: 0x000BD694
	private void Start()
	{
		this.MainCamera.transform.localPosition = new Vector3(0f, 1.482f, -10f);
		this.MainCamera.clearFlags = CameraClearFlags.Color;
		this.MainCamera.backgroundColor = new Color(1f, 1f, 1f, 1f);
		this.MainCamera.farClipPlane = 150f;
		this.Clock.BloomFadeSpeed = 5f;
		this.Clock.StopTime = true;
		this.Clock.BloomWait = 1f;
		this.SkipPanel.alpha = 0f;
		this.Subtitle.text = "";
		for (int i = 1; i < 11; i++)
		{
			if (GameGlobals.GetRivalEliminations(i) == 1 || GameGlobals.GetRivalEliminations(i) == 2)
			{
				Debug.Log("Rival #" + i.ToString() + " was killed.");
				this.Deaths++;
			}
			else
			{
				Debug.Log("Apparently, Rival #" + i.ToString() + " does not appear to have bee killed.");
			}
			if (GameGlobals.GetRivalEliminations(i) == 11)
			{
				this.Disappearances++;
			}
		}
		if (this.Deaths == 10)
		{
			this.Text[6] = "...and your connection to at least one other person's death.";
			this.Text[12] = "...and every single one of those girls is dead now!";
			this.Clip[6] = this.DeadClip;
			this.Clip[12] = this.AllDeadClip;
		}
		else if (this.Disappearances == 10)
		{
			this.Text[6] = "...and your connection to at least one other person's disappearance.";
			this.Text[12] = "...and every single one of those girls has gone missing!";
			this.Clip[6] = this.MissingClip;
			this.Clip[12] = this.AllMissingClip;
		}
		else if (this.Deaths + this.Disappearances == 10)
		{
			this.Text[6] = "...and your connection to several other deaths and disappearances over the past 10 weeks.";
			this.Text[12] = "...and every single one of those girls is dead or missing!";
			this.Clip[6] = this.DeadOrMissingClip;
			this.Clip[12] = this.AllDeadOrMissingClip;
		}
		else if (this.Deaths > 0)
		{
			this.Text[6] = "...and your connection to at least one other person's death.";
			this.Text[12] = "Some of those girls are dead now! And the others? They're conveniently...out of the picture.";
			this.Clip[6] = this.DeadClip;
			this.Clip[12] = this.SomeDeadOrMissingClip;
		}
		else if (this.Disappearances > 0)
		{
			this.Text[6] = "...and your connection to at least one other person's disappearance.";
			this.Text[12] = "And some of those girls have gone missing. Tch...how convenient for you.";
			this.Clip[6] = this.MissingClip;
			this.Clip[12] = this.SomeMissingClip;
		}
		else if (this.Deaths + this.Disappearances == 0)
		{
			this.SkipLine6 = true;
			this.Text[12] = "...and from what I've heard, you've been doing everything in your power to keep girls away from him.";
			this.Clip[6] = this.Clip[0];
			this.Clip[12] = this.PacifistClip;
		}
		if (SchoolGlobals.SchoolAtmosphere < 0.5f)
		{
			this.Darkness.color = new Color(1f, 1f, 1f, 1f);
		}
	}

	// Token: 0x06001418 RID: 5144 RVA: 0x000BF7A8 File Offset: 0x000BD9A8
	private void Update()
	{
		if (this.WarmUp)
		{
			this.Timer += Time.deltaTime;
			if (this.Timer > 1f)
			{
				this.MyAudio.Play();
				this.Jukebox.Play();
				this.WarmUp = false;
				this.Timer = 0f;
				return;
			}
		}
		else
		{
			this.Jukebox.volume = Mathf.MoveTowards(this.Jukebox.volume, 0.1f, Time.deltaTime);
			if (!this.MyAudio.isPlaying || (this.Debugging && Input.GetButtonDown("A") && this.Phase < 16))
			{
				this.Timer = 1.1f;
				if (this.Timer > 1f)
				{
					this.Timer = 0f;
					this.Phase++;
					if (this.Phase == 6 && this.SkipLine6)
					{
						this.Phase++;
					}
					if (this.Phase < this.Text.Length)
					{
						this.Subtitle.text = this.Text[this.Phase];
						this.MyAudio.clip = this.Clip[this.Phase];
						this.MyAudio.Play();
						if (this.Phase == 2 || this.Phase == 3)
						{
							if (this.Phase == 3)
							{
								this.MainCamera.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
							}
							this.MainCamera.transform.localPosition = new Vector3(0f, 1.482f, 0f);
							this.Cops.SetActive(true);
							this.Speed = 0f;
						}
						else if (this.Phase == 16)
						{
							this.FadeOut = true;
							this.Darkness.color = new Color(0f, 0f, 0f, 0f);
						}
					}
					else if (this.Darkness.alpha == 1f)
					{
						SceneManager.LoadScene("CourtroomScene");
					}
				}
			}
			if (this.Phase < 2)
			{
				this.Speed += Time.deltaTime * 0.05f;
				this.MainCamera.transform.localPosition = Vector3.Lerp(this.MainCamera.transform.localPosition, new Vector3(0f, 1.482f, 0f), Time.deltaTime * this.Speed);
				this.Rotation = Mathf.Lerp(this.Rotation, -15f, Time.deltaTime * this.Speed);
				this.MainCamera.transform.localEulerAngles = new Vector3(this.Rotation, 0f, 0f);
			}
			else if (this.Phase == 2)
			{
				this.Speed += Time.deltaTime * 3f;
				this.Rotation = Mathf.Lerp(this.Rotation, 0f, Time.deltaTime * this.Speed);
				this.MainCamera.transform.localEulerAngles = new Vector3(this.Rotation, 0f, 0f);
			}
			else if (this.Phase > 2 && this.Phase < this.Text.Length)
			{
				this.Speed += Time.deltaTime;
				this.Rotation = Mathf.Lerp(this.Rotation, -180f, Time.deltaTime * this.Speed);
				this.MainCamera.transform.localEulerAngles = new Vector3(0f, this.Rotation, 0f);
			}
			int phase = this.Phase;
			if (this.FadeOut)
			{
				this.Darkness.alpha = Mathf.MoveTowards(this.Darkness.alpha, 1f, Time.deltaTime * 0.33333f);
				this.SkipPanel.alpha = Mathf.MoveTowards(this.SkipPanel.alpha, 0f, Time.deltaTime * 0.33333f);
				this.Jukebox.volume = Mathf.MoveTowards(this.Jukebox.volume, 0f, Time.deltaTime * 2f);
				return;
			}
			this.Darkness.alpha = Mathf.MoveTowards(this.Darkness.alpha, 0f, Time.deltaTime * 0.33333f);
			if (!this.WarmUp)
			{
				this.SkipTimer += Time.deltaTime;
				if (this.SkipTimer > 2f)
				{
					this.SkipPanel.alpha = Mathf.MoveTowards(this.SkipPanel.alpha, 1f, Time.deltaTime * 0.33333f);
				}
			}
			if (this.SkipPanel.alpha == 1f)
			{
				if (Input.GetButton("X"))
				{
					this.SkipCircle.fillAmount -= Time.deltaTime;
					if (this.SkipCircle.fillAmount == 0f)
					{
						this.MyAudio.Stop();
						this.FadeOut = true;
						this.Phase = this.Text.Length;
						this.Darkness.color = new Color(0f, 0f, 0f, 0f);
						return;
					}
				}
				else
				{
					this.SkipCircle.fillAmount = 1f;
				}
			}
		}
	}

	// Token: 0x04001E1E RID: 7710
	public UISprite SkipCircle;

	// Token: 0x04001E1F RID: 7711
	public UIPanel SkipPanel;

	// Token: 0x04001E20 RID: 7712
	public AudioSource Jukebox;

	// Token: 0x04001E21 RID: 7713
	public AudioSource MyAudio;

	// Token: 0x04001E22 RID: 7714
	public ClockScript Clock;

	// Token: 0x04001E23 RID: 7715
	public UISprite Darkness;

	// Token: 0x04001E24 RID: 7716
	public Camera MainCamera;

	// Token: 0x04001E25 RID: 7717
	public UILabel Subtitle;

	// Token: 0x04001E26 RID: 7718
	public GameObject Cops;

	// Token: 0x04001E27 RID: 7719
	public AudioClip[] Clip;

	// Token: 0x04001E28 RID: 7720
	public string[] Text;

	// Token: 0x04001E29 RID: 7721
	public float SkipTimer;

	// Token: 0x04001E2A RID: 7722
	public float Rotation;

	// Token: 0x04001E2B RID: 7723
	public float Speed;

	// Token: 0x04001E2C RID: 7724
	public float Timer;

	// Token: 0x04001E2D RID: 7725
	public int Phase;

	// Token: 0x04001E2E RID: 7726
	public int Disappearances;

	// Token: 0x04001E2F RID: 7727
	public int Deaths;

	// Token: 0x04001E30 RID: 7728
	public bool Debugging;

	// Token: 0x04001E31 RID: 7729
	public bool SkipLine6;

	// Token: 0x04001E32 RID: 7730
	public bool FadeOut;

	// Token: 0x04001E33 RID: 7731
	public bool WarmUp;

	// Token: 0x04001E34 RID: 7732
	public AudioClip DeadClip;

	// Token: 0x04001E35 RID: 7733
	public AudioClip AllDeadClip;

	// Token: 0x04001E36 RID: 7734
	public AudioClip MissingClip;

	// Token: 0x04001E37 RID: 7735
	public AudioClip AllMissingClip;

	// Token: 0x04001E38 RID: 7736
	public AudioClip SomeMissingClip;

	// Token: 0x04001E39 RID: 7737
	public AudioClip DeadOrMissingClip;

	// Token: 0x04001E3A RID: 7738
	public AudioClip AllDeadOrMissingClip;

	// Token: 0x04001E3B RID: 7739
	public AudioClip SomeDeadOrMissingClip;

	// Token: 0x04001E3C RID: 7740
	public AudioClip PacifistClip;
}
