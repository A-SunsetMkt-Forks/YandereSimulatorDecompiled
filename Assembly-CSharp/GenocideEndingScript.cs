﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020002E4 RID: 740
public class GenocideEndingScript : MonoBehaviour
{
	// Token: 0x0600150A RID: 5386 RVA: 0x000D82D0 File Offset: 0x000D64D0
	private void Start()
	{
		Time.timeScale = 1f;
		if (GameGlobals.EightiesCutsceneID == 12)
		{
			Debug.Log("We're here for the end of 1980s Mode.");
			this.SpeechText = this.EightiesText;
			this.Subtitle.text = this.SpeechText[1];
			this.SpeechClip = this.EightiesSpeechClip;
			this.MyAudio.clip = this.SpeechClip[1];
			this.MyAudio.Play();
			this.SpeechPhase = 1;
			this.YellowifyLabel(this.Subtitle);
			this.Senpai["kidnapTorture_01"].speed = 0.1f;
			this.Senpai.Play();
			this.SenpaiRopes.SetActive(true);
			this.OsanaRopes.SetActive(false);
			this.Senpai.transform.parent.gameObject.SetActive(true);
			this.Osana.SetActive(false);
			this.EightiesEnding = true;
			this.FadeSpeed = 0.1f;
			return;
		}
		if (EventGlobals.OsanaConversation)
		{
			Debug.Log("We're here for a Betray cutscene.");
			this.Osana.GetComponent<StudentScript>().CharacterAnimation["f02_kidnapTorture_01"].speed = 0.8f;
			this.Osana.GetComponent<CosmeticScript>().SetFemaleUniform();
			this.SenpaiRopes.SetActive(false);
			this.OsanaRopes.SetActive(true);
			this.Senpai.transform.parent.gameObject.SetActive(false);
			this.Osana.SetActive(true);
			this.SpeechText[10] = "...huh? ...what is this? ...why am I tied to a...chair?! Why are you doing this?! This isn't funny! Lemme go! Lemme go right now!";
			this.Subtitle.text = this.SpeechText[10];
			this.MyAudio.clip = this.OsanaClip;
			this.MyAudio.Play();
			this.SpeechPhase = 10;
			this.TimeLimit = 9f;
			this.Delay = 10f;
			if (GameGlobals.Eighties)
			{
				this.RivalHair[0].SetActive(false);
				this.RivalHair[DateGlobals.Week].SetActive(true);
				this.YellowifyLabel(this.Subtitle);
				return;
			}
		}
		else
		{
			Debug.Log("We're here for the Genocide Ending.");
			this.Senpai["kidnapTorture_01"].speed = 0.9f;
			this.SenpaiRopes.SetActive(true);
			this.OsanaRopes.SetActive(false);
			this.Senpai.transform.parent.gameObject.SetActive(true);
			this.Osana.SetActive(false);
			GameGlobals.DarkEnding = true;
		}
	}

	// Token: 0x0600150B RID: 5387 RVA: 0x000D8554 File Offset: 0x000D6754
	private void Update()
	{
		if (!this.EightiesEnding)
		{
			if (this.SpeechPhase > 9)
			{
				base.transform.Translate(Vector3.forward * -0.1f * Time.deltaTime);
				if (this.MyAudio.isPlaying)
				{
					this.Senpai.Play();
					if (this.MyAudio.time < this.TimeLimit)
					{
						this.Alpha = Mathf.MoveTowards(this.Alpha, 0f, Time.deltaTime * 0.25f);
					}
					else
					{
						this.Alpha = Mathf.MoveTowards(this.Alpha, 1f, Time.deltaTime * 0.25f);
					}
				}
				this.Darkness.color = new Color(0f, 0f, 0f, this.Alpha);
				if (this.Darkness.color.a == 1f)
				{
					this.Subtitle.text = "";
				}
			}
			if (!this.MyAudio.isPlaying)
			{
				this.Timer += Time.deltaTime;
				if (this.Delay == 10f && this.Timer > 1f)
				{
					if (this.Timer < 3f)
					{
						this.RIVAL.SetActive(true);
						this.ELIMINATED.SetActive(true);
					}
					else if (this.Timer < 5f)
					{
						if (this.ELIMINATED.activeInHierarchy)
						{
							this.ELIMINATED.SetActive(false);
							AudioSource.PlayClipAtPoint(this.Slam, base.transform.position);
						}
					}
					else
					{
						this.SecondAlpha = Mathf.MoveTowards(this.SecondAlpha, 1f, Time.deltaTime * 0.25f);
						this.SecondDarkness.color = new Color(0f, 0f, 0f, this.SecondAlpha);
					}
				}
				if (this.Timer > this.Delay)
				{
					this.SpeechPhase++;
					this.Timer = 0f;
					if (this.SpeechPhase < this.SpeechClip.Length)
					{
						this.Subtitle.text = this.SpeechText[this.SpeechPhase];
						this.MyAudio.clip = this.SpeechClip[this.SpeechPhase];
						this.Delay = this.SpeechDelay[this.SpeechPhase];
						this.MyAudio.Play();
						return;
					}
					if (!EventGlobals.OsanaConversation)
					{
						SceneManager.LoadScene("CreditsScene");
						return;
					}
					DateGlobals.PassDays = 1;
					SceneManager.LoadScene("CalendarScene");
					EventGlobals.OsanaConversation = false;
					return;
				}
			}
		}
		else
		{
			base.transform.Translate(Vector3.forward * -0.01f * Time.deltaTime);
			if (!this.FadeOut)
			{
				this.Darkness.alpha = Mathf.MoveTowards(this.Darkness.alpha, 0f, Time.deltaTime * this.FadeSpeed);
			}
			else
			{
				this.SecondDarkness.alpha = Mathf.MoveTowards(this.SecondDarkness.alpha, 1f, Time.deltaTime * this.FadeSpeed);
				if (this.SecondDarkness.alpha == 1f)
				{
					GameGlobals.DarkEnding = false;
					SceneManager.LoadScene("CreditsScene");
				}
			}
			if (Input.GetButtonDown("A"))
			{
				this.MyAudio.Stop();
			}
			if (!this.MyAudio.isPlaying && this.SpeechPhase < this.SpeechClip.Length - 1)
			{
				this.SpeechPhase++;
				this.Subtitle.text = this.SpeechText[this.SpeechPhase];
				this.MyAudio.clip = this.SpeechClip[this.SpeechPhase];
				this.MyAudio.Play();
				if (this.SpeechPhase == this.SpeechClip.Length - 1)
				{
					this.FadeOut = true;
				}
			}
		}
	}

	// Token: 0x0600150C RID: 5388 RVA: 0x000D893C File Offset: 0x000D6B3C
	private void LateUpdate()
	{
		this.Neck.transform.localEulerAngles = new Vector3(0f, this.Neck.transform.localEulerAngles.y, this.Neck.transform.localEulerAngles.z);
	}

	// Token: 0x0600150D RID: 5389 RVA: 0x000D8990 File Offset: 0x000D6B90
	public void YellowifyLabel(UILabel Label)
	{
		Label.trueTypeFont = this.Arial;
		Label.applyGradient = false;
		Label.color = new Color(1f, 1f, 0f, 1f);
		Label.fontStyle = FontStyle.Bold;
		Label.effectDistance = new Vector2(4f, 4f);
	}

	// Token: 0x040021B1 RID: 8625
	public AudioSource MyAudio;

	// Token: 0x040021B2 RID: 8626
	public UISprite SecondDarkness;

	// Token: 0x040021B3 RID: 8627
	public UISprite Darkness;

	// Token: 0x040021B4 RID: 8628
	public UILabel Subtitle;

	// Token: 0x040021B5 RID: 8629
	public Animation Senpai;

	// Token: 0x040021B6 RID: 8630
	public Transform Neck;

	// Token: 0x040021B7 RID: 8631
	public AudioClip[] EightiesSpeechClip;

	// Token: 0x040021B8 RID: 8632
	public AudioClip[] SpeechClip;

	// Token: 0x040021B9 RID: 8633
	public AudioClip OsanaClip;

	// Token: 0x040021BA RID: 8634
	public AudioClip Slam;

	// Token: 0x040021BB RID: 8635
	public string[] EightiesText;

	// Token: 0x040021BC RID: 8636
	public string[] SpeechText;

	// Token: 0x040021BD RID: 8637
	public float[] SpeechDelay;

	// Token: 0x040021BE RID: 8638
	public float[] SpeechTime;

	// Token: 0x040021BF RID: 8639
	public GameObject RIVAL;

	// Token: 0x040021C0 RID: 8640
	public GameObject ELIMINATED;

	// Token: 0x040021C1 RID: 8641
	public GameObject SenpaiRopes;

	// Token: 0x040021C2 RID: 8642
	public GameObject OsanaRopes;

	// Token: 0x040021C3 RID: 8643
	public GameObject Osana;

	// Token: 0x040021C4 RID: 8644
	public int SpeechPhase;

	// Token: 0x040021C5 RID: 8645
	public float SecondAlpha;

	// Token: 0x040021C6 RID: 8646
	public float FadeSpeed = 0.2f;

	// Token: 0x040021C7 RID: 8647
	public float TimeLimit;

	// Token: 0x040021C8 RID: 8648
	public float Alpha;

	// Token: 0x040021C9 RID: 8649
	public float Delay;

	// Token: 0x040021CA RID: 8650
	public float Timer;

	// Token: 0x040021CB RID: 8651
	public bool EightiesEnding;

	// Token: 0x040021CC RID: 8652
	public bool FadeOut;

	// Token: 0x040021CD RID: 8653
	public GameObject[] RivalHair;

	// Token: 0x040021CE RID: 8654
	public Font Arial;
}
