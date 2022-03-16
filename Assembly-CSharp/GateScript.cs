﻿using System;
using UnityEngine;

// Token: 0x020002DB RID: 731
public class GateScript : MonoBehaviour
{
	// Token: 0x060014DB RID: 5339 RVA: 0x000CE4A0 File Offset: 0x000CC6A0
	private void Update()
	{
		if (!this.ManuallyAdjusted)
		{
			if (this.Clock.PresentTime / 60f > 8f && this.Clock.PresentTime / 60f < 15.5f)
			{
				if (!this.Closed)
				{
					this.PlayAudio();
					this.Closed = true;
					if (this.EmergencyDoor.enabled)
					{
						this.EmergencyDoor.enabled = false;
					}
				}
			}
			else if (this.Closed)
			{
				this.PlayAudio();
				this.Closed = false;
				if (!this.EmergencyDoor.enabled)
				{
					this.EmergencyDoor.enabled = true;
				}
			}
		}
		if (this.StudentManager.Students[97] != null)
		{
			if (this.StudentManager.Students[97].CurrentAction == StudentActionType.AtLocker && this.StudentManager.Students[97].Routine && this.StudentManager.Students[97].Alive)
			{
				if (Vector3.Distance(this.StudentManager.Students[97].transform.position, this.StudentManager.Podiums.List[0].position) < 0.1f)
				{
					if (this.ManuallyAdjusted)
					{
						this.ManuallyAdjusted = false;
					}
					this.Prompt.enabled = false;
					this.Prompt.Hide();
				}
				else
				{
					this.Prompt.enabled = true;
				}
			}
			else
			{
				this.Prompt.enabled = true;
			}
		}
		else
		{
			this.Prompt.enabled = true;
		}
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			this.PlayAudio();
			this.EmergencyDoor.enabled = !this.EmergencyDoor.enabled;
			this.ManuallyAdjusted = true;
			this.Closed = !this.Closed;
			if (this.StudentManager.Students[97] != null && this.StudentManager.Students[97].Investigating)
			{
				this.StudentManager.Students[97].StopInvestigating();
			}
		}
		if (!this.Closed)
		{
			if (this.RightGate.localPosition.x != 7f)
			{
				this.RightGate.localPosition = new Vector3(Mathf.MoveTowards(this.RightGate.localPosition.x, 7f, Time.deltaTime), this.RightGate.localPosition.y, this.RightGate.localPosition.z);
				this.LeftGate.localPosition = new Vector3(Mathf.MoveTowards(this.LeftGate.localPosition.x, -7f, Time.deltaTime), this.LeftGate.localPosition.y, this.LeftGate.localPosition.z);
				if (!this.AudioPlayed && this.RightGate.localPosition.x == 7f)
				{
					this.RightGateAudio.clip = this.StopOpen;
					this.LeftGateAudio.clip = this.StopOpen;
					this.RightGateAudio.Play();
					this.LeftGateAudio.Play();
					this.RightGateLoop.Stop();
					this.LeftGateLoop.Stop();
					this.AudioPlayed = true;
					return;
				}
			}
		}
		else if (this.RightGate.localPosition.x != 2.325f)
		{
			if (this.RightGate.localPosition.x < 2.4f)
			{
				this.Crushing = true;
			}
			this.RightGate.localPosition = new Vector3(Mathf.MoveTowards(this.RightGate.localPosition.x, 2.325f, Time.deltaTime), this.RightGate.localPosition.y, this.RightGate.localPosition.z);
			this.LeftGate.localPosition = new Vector3(Mathf.MoveTowards(this.LeftGate.localPosition.x, -2.325f, Time.deltaTime), this.LeftGate.localPosition.y, this.LeftGate.localPosition.z);
			if (!this.AudioPlayed && this.RightGate.localPosition.x == 2.325f)
			{
				this.RightGateAudio.clip = this.StopOpen;
				this.LeftGateAudio.clip = this.StopOpen;
				this.RightGateAudio.Play();
				this.LeftGateAudio.Play();
				this.RightGateLoop.Stop();
				this.LeftGateLoop.Stop();
				this.AudioPlayed = true;
				this.Crushing = false;
			}
		}
	}

	// Token: 0x060014DC RID: 5340 RVA: 0x000CE964 File Offset: 0x000CCB64
	public void PlayAudio()
	{
		this.RightGateAudio.clip = this.Start;
		this.LeftGateAudio.clip = this.Start;
		this.RightGateAudio.Play();
		this.LeftGateAudio.Play();
		this.RightGateLoop.Play();
		this.LeftGateLoop.Play();
		this.AudioPlayed = false;
	}

	// Token: 0x040020F3 RID: 8435
	public StudentManagerScript StudentManager;

	// Token: 0x040020F4 RID: 8436
	public PromptScript Prompt;

	// Token: 0x040020F5 RID: 8437
	public ClockScript Clock;

	// Token: 0x040020F6 RID: 8438
	public Collider EmergencyDoor;

	// Token: 0x040020F7 RID: 8439
	public Collider GateCollider;

	// Token: 0x040020F8 RID: 8440
	public Transform RightGate;

	// Token: 0x040020F9 RID: 8441
	public Transform LeftGate;

	// Token: 0x040020FA RID: 8442
	public bool ManuallyAdjusted;

	// Token: 0x040020FB RID: 8443
	public bool AudioPlayed;

	// Token: 0x040020FC RID: 8444
	public bool UpdateGates;

	// Token: 0x040020FD RID: 8445
	public bool Crushing;

	// Token: 0x040020FE RID: 8446
	public bool Closed;

	// Token: 0x040020FF RID: 8447
	public AudioSource RightGateAudio;

	// Token: 0x04002100 RID: 8448
	public AudioSource LeftGateAudio;

	// Token: 0x04002101 RID: 8449
	public AudioSource RightGateLoop;

	// Token: 0x04002102 RID: 8450
	public AudioSource LeftGateLoop;

	// Token: 0x04002103 RID: 8451
	public AudioClip Start;

	// Token: 0x04002104 RID: 8452
	public AudioClip StopOpen;

	// Token: 0x04002105 RID: 8453
	public AudioClip StopClose;
}
