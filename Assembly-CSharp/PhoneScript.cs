﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200039F RID: 927
public class PhoneScript : MonoBehaviour
{
	// Token: 0x06001A91 RID: 6801 RVA: 0x0011CDF4 File Offset: 0x0011AFF4
	private void Start()
	{
		Time.timeScale = 1f;
		this.Buttons.localPosition = new Vector3(this.Buttons.localPosition.x, -135f, this.Buttons.localPosition.z);
		this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, 1f);
		if (DateGlobals.Week > 1 && DateGlobals.Weekday == DayOfWeek.Sunday)
		{
			this.Darkness.color = new Color(0f, 0f, 0f, 0f);
		}
		if (EventGlobals.KidnapConversation)
		{
			this.VoiceClips = this.KidnapClip;
			this.Speaker = this.KidnapSpeaker;
			this.Text = this.KidnapText;
			this.Height = this.KidnapHeight;
			EventGlobals.BefriendConversation = true;
			EventGlobals.KidnapConversation = false;
		}
		else if (EventGlobals.BefriendConversation)
		{
			this.VoiceClips = this.BefriendClip;
			this.Speaker = this.BefriendSpeaker;
			this.Text = this.BefriendText;
			this.Height = this.BefriendHeight;
			EventGlobals.LivingRoom = true;
			EventGlobals.BefriendConversation = false;
		}
		else if (EventGlobals.OsanaConversation)
		{
			Debug.Log("The game believes that, right now, we're supposed to be seeing Osana's text message conversation.");
			this.VoiceClips = this.OsanaMessages.OsanaClips;
			this.Speaker = this.OsanaMessages.OsanaSpeakers;
			this.Text = this.OsanaMessages.OsanaTexts;
			this.Height = this.OsanaMessages.OsanaHeights;
			EventGlobals.OsanaConversation = true;
			EventGlobals.LivingRoom = true;
		}
		else
		{
			this.MeetingInfoChan = true;
		}
		if (GameGlobals.LoveSick)
		{
			Camera.main.backgroundColor = Color.black;
			this.LoveSickColorSwap();
		}
		if (this.PostElimination && ((GameGlobals.RivalEliminationID != 3 && GameGlobals.NonlethalElimination) || GameGlobals.RivalEliminationID == 9))
		{
			this.VoiceClips[1] = this.NonlethalClip[1];
			this.VoiceClips[2] = this.NonlethalClip[2];
			this.VoiceClips[3] = this.NonlethalClip[3];
			this.Text[1] = this.NonlethalText[1];
			this.Text[2] = this.NonlethalText[2];
			this.Text[3] = this.NonlethalText[3];
			this.Height[1] = this.NonlethalHeight[1];
			this.Height[2] = this.NonlethalHeight[2];
			this.Height[3] = this.NonlethalHeight[3];
		}
	}

	// Token: 0x06001A92 RID: 6802 RVA: 0x0011D084 File Offset: 0x0011B284
	private void Update()
	{
		if (!this.FadeOut)
		{
			if (this.Timer > 0f && this.Buttons.gameObject.activeInHierarchy)
			{
				this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 0f, Time.deltaTime));
				if (this.Darkness.color.a == 0f)
				{
					if (!this.Jukebox.isPlaying)
					{
						this.Jukebox.Play();
					}
					if (this.NewMessage == null)
					{
						this.SpawnMessage();
					}
				}
			}
			if (this.ShowPauseMenu)
			{
				this.PauseMenuTimer += Time.deltaTime;
				if (this.PauseMenuPhase == 0)
				{
					this.PauseMenu.localPosition = Vector3.Lerp(this.PauseMenu.localPosition, new Vector3(0f, -15f, 0f), Time.deltaTime * 10f);
					if (this.PauseMenuTimer > 1f)
					{
						base.GetComponent<AudioSource>().clip = this.AppInstall;
						base.GetComponent<AudioSource>().Play();
						this.PauseMenuPhase++;
					}
				}
				else if (this.PauseMenuPhase == 1)
				{
					this.InfoIcon.localScale = Vector3.Lerp(this.InfoIcon.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
					if (this.PauseMenuTimer > 2f)
					{
						base.GetComponent<AudioSource>().clip = this.SubtleWhoosh;
						base.GetComponent<AudioSource>().Play();
						this.PauseMenuPhase++;
					}
				}
				else if (this.PauseMenuPhase == 2)
				{
					this.PauseMenu.localPosition = Vector3.Lerp(this.PauseMenu.localPosition, new Vector3(-485f, -15f, 0f), Time.deltaTime * 10f);
					if (this.PauseMenuTimer > 3f)
					{
						base.GetComponent<AudioSource>().volume = 1f;
						this.ShowPauseMenu = false;
						this.ManuallyAdvance = true;
					}
				}
			}
			else
			{
				if (this.NewMessage != null)
				{
					this.Buttons.localPosition = new Vector3(this.Buttons.localPosition.x, Mathf.Lerp(this.Buttons.localPosition.y, 0f, Time.deltaTime * 10f), this.Buttons.localPosition.z);
					this.AutoTimer += Time.deltaTime;
					if (this.OsanaCover != null)
					{
						if (this.MeetingInfoChan && this.ID > 5 && this.ID < 11)
						{
							this.OsanaCover.alpha = Mathf.MoveTowards(this.OsanaCover.alpha, 0f, Time.deltaTime);
						}
						else
						{
							this.OsanaCover.alpha = Mathf.MoveTowards(this.OsanaCover.alpha, 1f, Time.deltaTime);
						}
					}
					if ((this.Auto && this.AutoTimer > this.VoiceClips[this.ID].length + 1f) || Input.GetButtonDown("A") || this.ManuallyAdvance)
					{
						this.ManuallyAdvance = false;
						if (this.MeetingInfoChan && this.ID == 16 && this.PauseMenuPhase == 0 && !this.PostElimination)
						{
							base.GetComponent<AudioSource>().clip = this.SubtleWhoosh;
							base.GetComponent<AudioSource>().volume = 0.5f;
							base.GetComponent<AudioSource>().Play();
							this.ShowPauseMenu = true;
						}
						else
						{
							this.AutoTimer = 0f;
							if (this.ID < this.Text.Length - 1)
							{
								this.ID++;
								this.SpawnMessage();
							}
							else
							{
								this.Darkness.color = new Color(0f, 0f, 0f, 0f);
								this.FadeOut = true;
								if (!this.Buttons.gameObject.activeInHierarchy)
								{
									this.Darkness.color = new Color(0f, 0f, 0f, 1f);
								}
							}
						}
					}
				}
				if (Input.GetButtonDown("X") && this.Buttons.localPosition.y > -1f)
				{
					this.FadeOut = true;
				}
			}
		}
		else
		{
			this.Buttons.localPosition = new Vector3(this.Buttons.localPosition.x, Mathf.Lerp(this.Buttons.localPosition.y, -135f, Time.deltaTime * 10f), this.Buttons.localPosition.z);
			base.GetComponent<AudioSource>().volume = 1f - this.Darkness.color.a;
			this.Jukebox.volume = 0.25f - this.Darkness.color.a * 0.25f;
			if (this.Darkness.color.a >= 1f)
			{
				if (DateGlobals.Week == 2)
				{
					GameGlobals.TransitionToPostCredits = true;
					SceneManager.LoadScene("CreditsScene");
				}
				else if (DateGlobals.Weekday == DayOfWeek.Sunday)
				{
					if (DateGlobals.PassDays < 1)
					{
						DateGlobals.PassDays = 1;
					}
					SceneManager.LoadScene("CalendarScene");
				}
				else if (!EventGlobals.BefriendConversation && !EventGlobals.LivingRoom)
				{
					if (DateGlobals.PassDays < 1)
					{
						DateGlobals.PassDays = 1;
					}
					SceneManager.LoadScene("CalendarScene");
				}
				else if (EventGlobals.LivingRoom)
				{
					SceneManager.LoadScene("LivingRoomScene");
				}
				else
				{
					SceneManager.LoadScene(SceneManager.GetActiveScene().name);
				}
			}
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, this.Darkness.color.a + Time.deltaTime);
		}
		this.Timer += Time.deltaTime;
	}

	// Token: 0x06001A93 RID: 6803 RVA: 0x0011D6FC File Offset: 0x0011B8FC
	private void SpawnMessage()
	{
		if (this.NewMessage != null)
		{
			this.NewMessage.transform.parent = this.OldMessages;
			this.OldMessages.localPosition = new Vector3(this.OldMessages.localPosition.x, this.OldMessages.localPosition.y + (72f + (float)this.Height[this.ID] * 32f), this.OldMessages.localPosition.z);
		}
		AudioSource component = base.GetComponent<AudioSource>();
		component.clip = this.VoiceClips[this.ID];
		component.Play();
		if (this.Speaker[this.ID] == 1)
		{
			this.NewMessage = UnityEngine.Object.Instantiate<GameObject>(this.LeftMessage[this.Height[this.ID]]);
			this.NewMessage.transform.parent = this.Panel;
			this.NewMessage.transform.localPosition = new Vector3(-225f, -375f, 0f);
			this.NewMessage.transform.localScale = Vector3.zero;
		}
		else
		{
			this.NewMessage = UnityEngine.Object.Instantiate<GameObject>(this.RightMessage[this.Height[this.ID]]);
			this.NewMessage.transform.parent = this.Panel;
			this.NewMessage.transform.localPosition = new Vector3(225f, -375f, 0f);
			this.NewMessage.transform.localScale = Vector3.zero;
			if (this.Speaker == this.KidnapSpeaker && this.Height[this.ID] == 8)
			{
				this.NewMessage.GetComponent<TextMessageScript>().Attachment = true;
			}
		}
		if (this.Height[this.ID] == 9 && this.Speaker[this.ID] == 2)
		{
			this.Buttons.gameObject.SetActive(false);
			this.Darkness.enabled = true;
			this.Jukebox.Stop();
			this.Timer = -99999f;
		}
		this.AutoLimit = (float)(this.Height[this.ID] + 1);
		this.NewMessage.GetComponent<TextMessageScript>().Label.text = this.Text[this.ID];
	}

	// Token: 0x06001A94 RID: 6804 RVA: 0x0011D954 File Offset: 0x0011BB54
	private void LoveSickColorSwap()
	{
		foreach (GameObject gameObject in UnityEngine.Object.FindObjectsOfType<GameObject>())
		{
			UISprite component = gameObject.GetComponent<UISprite>();
			if (component != null && component.color != Color.black && component.transform.parent)
			{
				component.color = new Color(1f, 0f, 0f, component.color.a);
			}
			UILabel component2 = gameObject.GetComponent<UILabel>();
			if (component2 != null && component2.color != Color.black)
			{
				component2.color = new Color(1f, 0f, 0f, component2.color.a);
			}
			this.Darkness.color = Color.black;
		}
	}

	// Token: 0x04002BF5 RID: 11253
	public OsanaTextMessageScript OsanaMessages;

	// Token: 0x04002BF6 RID: 11254
	public GameObject[] RightMessage;

	// Token: 0x04002BF7 RID: 11255
	public GameObject[] LeftMessage;

	// Token: 0x04002BF8 RID: 11256
	public AudioClip[] VoiceClips;

	// Token: 0x04002BF9 RID: 11257
	public AudioClip SubtleWhoosh;

	// Token: 0x04002BFA RID: 11258
	public AudioClip AppInstall;

	// Token: 0x04002BFB RID: 11259
	public GameObject NewMessage;

	// Token: 0x04002BFC RID: 11260
	public AudioSource Jukebox;

	// Token: 0x04002BFD RID: 11261
	public Transform OldMessages;

	// Token: 0x04002BFE RID: 11262
	public Transform PauseMenu;

	// Token: 0x04002BFF RID: 11263
	public Transform InfoIcon;

	// Token: 0x04002C00 RID: 11264
	public Transform Buttons;

	// Token: 0x04002C01 RID: 11265
	public Transform Panel;

	// Token: 0x04002C02 RID: 11266
	public Vignetting Vignette;

	// Token: 0x04002C03 RID: 11267
	public UITexture OsanaCover;

	// Token: 0x04002C04 RID: 11268
	public UISprite Darkness;

	// Token: 0x04002C05 RID: 11269
	public UISprite Sprite;

	// Token: 0x04002C06 RID: 11270
	public int[] Speaker;

	// Token: 0x04002C07 RID: 11271
	public string[] Text;

	// Token: 0x04002C08 RID: 11272
	public int[] Height;

	// Token: 0x04002C09 RID: 11273
	public AudioClip[] KidnapClip;

	// Token: 0x04002C0A RID: 11274
	public int[] KidnapSpeaker;

	// Token: 0x04002C0B RID: 11275
	public string[] KidnapText;

	// Token: 0x04002C0C RID: 11276
	public int[] KidnapHeight;

	// Token: 0x04002C0D RID: 11277
	public AudioClip[] BefriendClip;

	// Token: 0x04002C0E RID: 11278
	public int[] BefriendSpeaker;

	// Token: 0x04002C0F RID: 11279
	public string[] BefriendText;

	// Token: 0x04002C10 RID: 11280
	public int[] BefriendHeight;

	// Token: 0x04002C11 RID: 11281
	public AudioClip[] NonlethalClip;

	// Token: 0x04002C12 RID: 11282
	public string[] NonlethalText;

	// Token: 0x04002C13 RID: 11283
	public int[] NonlethalHeight;

	// Token: 0x04002C14 RID: 11284
	public bool ManuallyAdvance;

	// Token: 0x04002C15 RID: 11285
	public bool MeetingInfoChan;

	// Token: 0x04002C16 RID: 11286
	public bool PostElimination;

	// Token: 0x04002C17 RID: 11287
	public bool ShowPauseMenu;

	// Token: 0x04002C18 RID: 11288
	public bool FadeOut;

	// Token: 0x04002C19 RID: 11289
	public bool Auto;

	// Token: 0x04002C1A RID: 11290
	public float PauseMenuTimer;

	// Token: 0x04002C1B RID: 11291
	public float AutoLimit;

	// Token: 0x04002C1C RID: 11292
	public float AutoTimer;

	// Token: 0x04002C1D RID: 11293
	public float Timer;

	// Token: 0x04002C1E RID: 11294
	public int PauseMenuPhase;

	// Token: 0x04002C1F RID: 11295
	public int ID;
}
