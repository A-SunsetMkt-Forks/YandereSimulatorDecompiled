﻿using System;
using UnityEngine;

// Token: 0x020003E5 RID: 997
public class OsanaFridayLunchEventScript : MonoBehaviour
{
	// Token: 0x06001BCD RID: 7117 RVA: 0x0013E240 File Offset: 0x0013C440
	private void Start()
	{
		this.EventSubtitle.transform.localScale = Vector3.zero;
		this.Spy.Prompt.enabled = false;
		this.Spy.Prompt.Hide();
		if (DateGlobals.Weekday != this.EventDay || GameGlobals.RivalEliminationID > 0 || GameGlobals.Eighties)
		{
			base.enabled = false;
		}
	}

	// Token: 0x06001BCE RID: 7118 RVA: 0x0013E2A8 File Offset: 0x0013C4A8
	private void Update()
	{
		if (this.Phase == 0)
		{
			if (this.Frame > 0)
			{
				this.Senpai = this.StudentManager.Students[1];
				if (this.StudentManager.Students[this.RivalID] != null)
				{
					this.Rival = this.StudentManager.Students[this.RivalID];
				}
				else
				{
					base.enabled = false;
				}
			}
			if (this.Frame > 1 && this.Clock.HourTime > 13f && this.Senpai.gameObject.activeInHierarchy && this.Rival != null)
			{
				if (this.Rival.Bullied)
				{
					base.enabled = false;
				}
				else
				{
					if (!this.Senpai.InEvent)
					{
						this.Senpai.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
						this.Senpai.CharacterAnimation.CrossFade(this.Senpai.WalkAnim);
						this.Senpai.Pathfinding.target = this.Location[1];
						this.Senpai.CurrentDestination = this.Location[1];
						this.Senpai.Pathfinding.canSearch = true;
						this.Senpai.Pathfinding.canMove = true;
						this.Senpai.SmartPhone.SetActive(false);
						this.Senpai.InEvent = true;
						this.Senpai.DistanceToDestination = 100f;
						this.Spy.Prompt.enabled = true;
					}
					if (this.Rival.enabled && !this.Rival.InEvent && !this.Rival.Phoneless)
					{
						Debug.Log("Osana's Friday lunch event has begun.");
						this.Rival.CharacterAnimation.cullingType = AnimationCullingType.AlwaysAnimate;
						this.Rival.CharacterAnimation.CrossFade(this.Rival.WalkAnim);
						this.Rival.Pathfinding.target = this.Location[2];
						this.Rival.CurrentDestination = this.Location[2];
						this.Rival.Pathfinding.canSearch = true;
						this.Rival.Pathfinding.canMove = true;
						this.Rival.SmartPhone.SetActive(false);
						this.Rival.InEvent = true;
						this.Rival.DistanceToDestination = 100f;
						this.Spy.Prompt.enabled = true;
					}
					if (this.StudentManager.Students[this.FriendID] != null && !PlayerGlobals.RaibaruLoner)
					{
						this.Friend = this.StudentManager.Students[this.FriendID];
					}
					if (this.Senpai.CurrentDestination == this.Location[1] && this.Senpai.DistanceToDestination < 0.5f)
					{
						if (!this.Impatient)
						{
							this.Senpai.CharacterAnimation.CrossFade("waiting_00");
							this.Senpai.Pathfinding.canSearch = false;
							this.Senpai.Pathfinding.canMove = false;
						}
						else if (this.Senpai.CharacterAnimation["impatience_00"].time >= this.Senpai.CharacterAnimation["impatience_00"].length)
						{
							this.EndEvent();
						}
					}
					if (this.Rival.CurrentDestination == this.Location[2] && this.Rival.DistanceToDestination < 0.5f)
					{
						this.Rival.CharacterAnimation.CrossFade(this.Rival.IdleAnim);
						this.Rival.Pathfinding.canSearch = false;
						this.Rival.Pathfinding.canMove = false;
					}
					if (!this.HintGiven)
					{
						this.Yandere.PauseScreen.Hint.Show = true;
						this.Yandere.PauseScreen.Hint.QuickID = 7;
						this.HintGiven = true;
					}
					if (this.Rival.CurrentDestination == this.Location[2] && this.Senpai.CurrentDestination == this.Location[1] && this.Senpai.DistanceToDestination < 0.5f && this.Rival.DistanceToDestination < 0.5f && !this.Impatient)
					{
						this.Phase++;
					}
				}
			}
			this.Frame++;
		}
		else if (this.Phase == 1)
		{
			this.Sabotaged = this.AudioSoftware.AudioDoctored;
			if (this.Rival.Phoneless)
			{
				this.Cancelled = true;
			}
			if (this.Cancelled)
			{
				AudioClipPlayer.Play(this.CancelledSpeechClip, this.Epicenter.position + Vector3.up * 1.5f, 5f, 10f, out this.VoiceClip, this.Yandere.transform.position.y);
				this.Transfer = false;
				this.TakeOut = false;
				this.Suffix = "C";
			}
			else if (!this.Sabotaged)
			{
				AudioClipPlayer.Play(this.SpeechClip, this.Epicenter.position + Vector3.up * 1.5f, 5f, 10f, out this.VoiceClip, this.Yandere.transform.position.y);
				this.TakeOutTime = 2.5f;
				this.TransferTime = 7f;
				this.ReturnTime = 19.33333f;
				this.PutAwayTime = 24.33333f;
				this.Suffix = "A";
			}
			else
			{
				AudioClipPlayer.Play(this.SabotagedSpeechClip, this.Epicenter.position + Vector3.up * 1.5f, 5f, 10f, out this.VoiceClip, this.Yandere.transform.position.y);
				this.TakeOutTime = 2.5f;
				this.TransferTime = 7f;
				this.ReturnTime = 16.66666f;
				this.PutAwayTime = 21.5f;
				this.Suffix = "B";
			}
			this.Rival.CharacterAnimation.CrossFade("f02_" + this.Weekday + "_3" + this.Suffix);
			this.Senpai.CharacterAnimation.CrossFade(this.Weekday + "_3" + this.Suffix);
			this.Timer = 0f;
			this.Phase++;
			if (this.Friend != null)
			{
				this.Friend.CurrentDestination = this.Location[3];
				this.Friend.Pathfinding.target = this.Location[3];
				this.Friend.IdleAnim = "f02_cornerPeek_00";
				this.Friend.SlideIn = true;
			}
		}
		else
		{
			this.Timer += Time.deltaTime;
			if (this.VoiceClip != null)
			{
				this.VoiceClip.GetComponent<AudioSource>().pitch = Time.timeScale;
			}
			if (this.Cancelled)
			{
				if (this.SpeechPhase < this.CancelledSpeechTime.Length && this.Timer > this.CancelledSpeechTime[this.SpeechPhase])
				{
					this.EventSubtitle.text = this.CancelledSpeechText[this.SpeechPhase];
					this.SpeechPhase++;
				}
			}
			else if (!this.Sabotaged)
			{
				if (this.SpeechPhase < this.SpeechTime.Length && this.Timer > this.SpeechTime[this.SpeechPhase])
				{
					this.EventSubtitle.text = this.SpeechText[this.SpeechPhase];
					this.SpeechPhase++;
				}
			}
			else
			{
				if (this.SpeechPhase < this.SabotagedSpeechTime.Length && this.Timer > this.SabotagedSpeechTime[this.SpeechPhase])
				{
					this.EventSubtitle.text = this.SabotagedSpeechText[this.SpeechPhase];
					this.SpeechPhase++;
				}
				if (this.Senpai.CharacterAnimation[this.Weekday + "_3" + this.Suffix].time >= this.Senpai.CharacterAnimation[this.Weekday + "_3" + this.Suffix].length)
				{
					ScheduleBlock scheduleBlock = this.Senpai.ScheduleBlocks[4];
					scheduleBlock.destination = "Hangout";
					scheduleBlock.action = "Eat";
					this.Senpai.GetDestinations();
					if (this.Senpai.InEvent)
					{
						this.Rival.StopRotating = true;
						this.LookAtSenpai = true;
						this.EndSenpai();
					}
				}
				if (this.LookAtSenpai)
				{
					this.Rival.targetRotation = Quaternion.LookRotation(this.Senpai.transform.position - this.Rival.transform.position);
					this.Rival.transform.rotation = Quaternion.Slerp(this.Rival.transform.rotation, this.Rival.targetRotation, 10f * Time.deltaTime);
				}
			}
			if (this.Rival.CharacterAnimation["f02_" + this.Weekday + "_3" + this.Suffix].time >= this.Rival.CharacterAnimation["f02_" + this.Weekday + "_3" + this.Suffix].length)
			{
				this.EndEvent();
			}
			if (this.TakeOut && this.Rival.CharacterAnimation["f02_" + this.Weekday + "_3" + this.Suffix].time > this.TakeOutTime)
			{
				this.Rival.SmartPhone.SetActive(true);
				this.TakeOut = false;
				this.PutAway = true;
			}
			if (this.PutAway && this.Rival.CharacterAnimation["f02_" + this.Weekday + "_3" + this.Suffix].time > this.PutAwayTime)
			{
				this.Rival.SmartPhone.SetActive(false);
				this.PutAway = false;
			}
			if (this.Transfer && this.Rival.CharacterAnimation["f02_" + this.Weekday + "_3" + this.Suffix].time > this.TransferTime)
			{
				this.OriginalRotation = this.Rival.SmartPhone.transform.localEulerAngles;
				this.OriginalPosition = this.Rival.SmartPhone.transform.localPosition;
				this.Rival.SmartPhone.transform.parent = this.Senpai.SmartPhone.transform.parent;
				this.Rival.SmartPhone.transform.localEulerAngles = this.Senpai.SmartPhone.transform.localEulerAngles;
				this.Rival.SmartPhone.transform.localPosition = this.Senpai.SmartPhone.transform.localPosition;
				this.Transfer = false;
				this.Return = true;
			}
			if (this.Return && this.Rival.CharacterAnimation["f02_" + this.Weekday + "_3" + this.Suffix].time > this.ReturnTime)
			{
				this.Rival.SmartPhone.transform.parent = this.Rival.ItemParent;
				this.Rival.SmartPhone.transform.localEulerAngles = this.OriginalRotation;
				this.Rival.SmartPhone.transform.localPosition = this.OriginalPosition;
				this.Return = false;
			}
			if (this.Senpai.Alarmed || this.Rival.Alarmed || this.Rival.Splashed || this.Rival.Dodging || this.Clock.Period == 4)
			{
				if (!this.Rival.Splashed)
				{
					UnityEngine.Object.Instantiate<GameObject>(this.AlarmDisc, this.Yandere.transform.position + Vector3.up, Quaternion.identity).GetComponent<AlarmDiscScript>().NoScream = true;
				}
				this.EndEvent();
			}
		}
		if (base.enabled && (this.Phase > 0 || this.Impatient))
		{
			this.Distance = Vector3.Distance(this.Yandere.transform.position, this.Epicenter.position);
			if (this.Distance - 4f < 15f)
			{
				this.Scale = Mathf.Abs(1f - (this.Distance - 4f) / 15f);
				if (this.Scale < 0f)
				{
					this.Scale = 0f;
				}
				if (this.Scale > 1f)
				{
					this.Scale = 1f;
				}
				this.Jukebox.Dip = 1f - 0.5f * this.Scale;
				this.EventSubtitle.transform.localScale = new Vector3(this.Scale, this.Scale, this.Scale);
				if (this.VoiceClip != null)
				{
					this.VoiceClip.GetComponent<AudioSource>().volume = this.Scale;
				}
				this.Yandere.Eavesdropping = (this.Distance < 2.5f);
				return;
			}
			this.EventSubtitle.transform.localScale = Vector3.zero;
			if (this.VoiceClip != null)
			{
				this.VoiceClip.GetComponent<AudioSource>().volume = 0f;
			}
		}
	}

	// Token: 0x06001BCF RID: 7119 RVA: 0x0013F0DC File Offset: 0x0013D2DC
	public void EndEvent()
	{
		Debug.Log("Osana's Friday lunchtime event has ended.");
		if (this.VoiceClip != null)
		{
			UnityEngine.Object.Destroy(this.VoiceClip);
		}
		if (this.Senpai.InEvent)
		{
			this.EndSenpai();
		}
		if (!this.Rival.Ragdoll.Zs.activeInHierarchy)
		{
			if (!this.Rival.Alarmed)
			{
				this.Rival.Pathfinding.canSearch = true;
				this.Rival.Pathfinding.canMove = true;
				this.Rival.Routine = true;
			}
			this.Rival.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
			this.Rival.SmartPhone.SetActive(false);
			this.Rival.Prompt.enabled = true;
			this.Rival.InEvent = false;
			this.Rival.Private = false;
			this.Rival.CurrentDestination = this.Rival.Destinations[this.Rival.Phase];
			this.Rival.Pathfinding.target = this.Rival.Destinations[this.Rival.Phase];
			this.Rival.DistanceToDestination = 100f;
			this.Rival.Pathfinding.speed = 1f;
			this.Rival.StopRotating = false;
			this.Rival.Hurry = false;
		}
		if (this.Friend != null)
		{
			ScheduleBlock scheduleBlock = this.Friend.ScheduleBlocks[4];
			scheduleBlock.destination = "LunchSpot";
			scheduleBlock.action = "Eat";
			this.Friend.GetDestinations();
			this.Friend.CurrentDestination = this.Friend.Destinations[this.Friend.Phase];
			this.Friend.Pathfinding.target = this.Friend.Destinations[this.Friend.Phase];
			this.Friend.DistanceToDestination = 100f;
			this.Friend.IdleAnim = this.Friend.OriginalIdleAnim;
			this.Friend.SlideIn = false;
			Debug.Log("''Friend'' is being told to set her destination to her current phase's destination.");
		}
		if (!this.StudentManager.Stop)
		{
			this.StudentManager.UpdateStudents(0);
		}
		this.Spy.Prompt.Hide();
		this.Spy.Prompt.enabled = false;
		if (this.Spy.Phase > 0)
		{
			this.Spy.End();
		}
		if (this.Sabotaged)
		{
			this.Rival.WalkAnim = "f02_sadWalk_00";
		}
		if (this.Rival.SmartPhone.transform.parent != this.Rival.ItemParent)
		{
			this.Rival.SmartPhone.transform.parent = this.Rival.ItemParent;
			this.Rival.SmartPhone.transform.localEulerAngles = this.OriginalRotation;
			this.Rival.SmartPhone.transform.localPosition = this.OriginalPosition;
		}
		this.Jukebox.Dip = 1f;
		this.Yandere.Eavesdropping = false;
		this.EventSubtitle.text = string.Empty;
		base.enabled = false;
	}

	// Token: 0x06001BD0 RID: 7120 RVA: 0x0013F424 File Offset: 0x0013D624
	private void EndSenpai()
	{
		if (!this.Senpai.Alarmed)
		{
			this.Senpai.Pathfinding.canSearch = true;
			this.Senpai.Pathfinding.canMove = true;
			this.Senpai.Routine = true;
		}
		this.Senpai.CharacterAnimation.cullingType = AnimationCullingType.BasedOnRenderers;
		this.Senpai.SmartPhone.SetActive(false);
		this.Senpai.InEvent = false;
		this.Senpai.Private = false;
		this.Senpai.CurrentDestination = this.Senpai.Destinations[this.Senpai.Phase];
		this.Senpai.Pathfinding.target = this.Senpai.Destinations[this.Senpai.Phase];
		this.Senpai.DistanceToDestination = 100f;
		if (this.Sabotaged)
		{
			this.StudentManager.SabotageProgress++;
			Debug.Log("Sabotage Progress: " + this.StudentManager.SabotageProgress.ToString() + "/5");
		}
	}

	// Token: 0x0400300A RID: 12298
	public StudentManagerScript StudentManager;

	// Token: 0x0400300B RID: 12299
	public AudioSoftwareScript AudioSoftware;

	// Token: 0x0400300C RID: 12300
	public JukeboxScript Jukebox;

	// Token: 0x0400300D RID: 12301
	public UILabel EventSubtitle;

	// Token: 0x0400300E RID: 12302
	public YandereScript Yandere;

	// Token: 0x0400300F RID: 12303
	public ClockScript Clock;

	// Token: 0x04003010 RID: 12304
	public SpyScript Spy;

	// Token: 0x04003011 RID: 12305
	public StudentScript Friend;

	// Token: 0x04003012 RID: 12306
	public StudentScript Senpai;

	// Token: 0x04003013 RID: 12307
	public StudentScript Rival;

	// Token: 0x04003014 RID: 12308
	public Transform[] Location;

	// Token: 0x04003015 RID: 12309
	public Transform Epicenter;

	// Token: 0x04003016 RID: 12310
	public AudioClip CancelledSpeechClip;

	// Token: 0x04003017 RID: 12311
	public string[] CancelledSpeechText;

	// Token: 0x04003018 RID: 12312
	public float[] CancelledSpeechTime;

	// Token: 0x04003019 RID: 12313
	public AudioClip SabotagedSpeechClip;

	// Token: 0x0400301A RID: 12314
	public string[] SabotagedSpeechText;

	// Token: 0x0400301B RID: 12315
	public float[] SabotagedSpeechTime;

	// Token: 0x0400301C RID: 12316
	public AudioClip SpeechClip;

	// Token: 0x0400301D RID: 12317
	public string[] SpeechText;

	// Token: 0x0400301E RID: 12318
	public float[] SpeechTime;

	// Token: 0x0400301F RID: 12319
	public GameObject AlarmDisc;

	// Token: 0x04003020 RID: 12320
	public GameObject VoiceClip;

	// Token: 0x04003021 RID: 12321
	public bool LookAtSenpai;

	// Token: 0x04003022 RID: 12322
	public bool EventActive;

	// Token: 0x04003023 RID: 12323
	public bool Cancelled;

	// Token: 0x04003024 RID: 12324
	public bool HintGiven;

	// Token: 0x04003025 RID: 12325
	public bool Impatient;

	// Token: 0x04003026 RID: 12326
	public bool Sabotaged;

	// Token: 0x04003027 RID: 12327
	public bool Transfer;

	// Token: 0x04003028 RID: 12328
	public bool TakeOut;

	// Token: 0x04003029 RID: 12329
	public bool PutAway;

	// Token: 0x0400302A RID: 12330
	public bool Return;

	// Token: 0x0400302B RID: 12331
	public Vector3 OriginalRotation;

	// Token: 0x0400302C RID: 12332
	public Vector3 OriginalPosition;

	// Token: 0x0400302D RID: 12333
	public float TransferTime;

	// Token: 0x0400302E RID: 12334
	public float ReturnTime;

	// Token: 0x0400302F RID: 12335
	public float TakeOutTime;

	// Token: 0x04003030 RID: 12336
	public float PutAwayTime;

	// Token: 0x04003031 RID: 12337
	public float Distance;

	// Token: 0x04003032 RID: 12338
	public float Scale;

	// Token: 0x04003033 RID: 12339
	public float Timer;

	// Token: 0x04003034 RID: 12340
	public DayOfWeek EventDay;

	// Token: 0x04003035 RID: 12341
	public int SpeechPhase = 1;

	// Token: 0x04003036 RID: 12342
	public int FriendID = 10;

	// Token: 0x04003037 RID: 12343
	public int RivalID = 11;

	// Token: 0x04003038 RID: 12344
	public int Phase;

	// Token: 0x04003039 RID: 12345
	public int Frame;

	// Token: 0x0400303A RID: 12346
	public string Weekday = string.Empty;

	// Token: 0x0400303B RID: 12347
	public string Suffix = string.Empty;
}
