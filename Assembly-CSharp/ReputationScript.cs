﻿using System;
using UnityEngine;

// Token: 0x020003D7 RID: 983
public class ReputationScript : MonoBehaviour
{
	// Token: 0x06001B95 RID: 7061 RVA: 0x00137EFB File Offset: 0x001360FB
	private void Start()
	{
		this.RepUpdateLabel.enabled = true;
		if (MissionModeGlobals.MissionMode)
		{
			this.RepUpdateLabel.enabled = false;
			this.MissionMode = true;
		}
		if (GameGlobals.Eighties)
		{
			this.BecomeEighties();
		}
		this.Reputation = PlayerGlobals.Reputation;
	}

	// Token: 0x06001B96 RID: 7062 RVA: 0x00137F3C File Offset: 0x0013613C
	private void Update()
	{
		switch (this.Phase)
		{
		case 1:
			if (this.Clock.PresentTime / 60f > 8.5f)
			{
				this.Phase++;
			}
			break;
		case 2:
			if (this.Clock.PresentTime / 60f > 13.5f)
			{
				this.Phase++;
			}
			break;
		case 3:
			if (this.Clock.PresentTime / 60f > 18f)
			{
				this.RepUpdateLabel.text = "REP WILL UPDATE AFTER SCHOOL";
				this.Phase++;
			}
			break;
		}
		if (this.CheckedRep < this.Phase && !this.StudentManager.Yandere.Struggling && !this.StudentManager.Yandere.DelinquentFighting && !this.StudentManager.Yandere.Pickpocketing && !this.StudentManager.Yandere.Noticed && !this.ArmDetector.SummonDemon)
		{
			this.UpdateRep();
			if (this.Reputation <= -100f)
			{
				this.Portal.EndDay();
			}
		}
		if (!this.MissionMode)
		{
			if (this.LerpTimer < 1f)
			{
				this.CurrentRepMarker.localPosition = new Vector3(Mathf.Lerp(this.CurrentRepMarker.localPosition.x, -830f + this.Reputation * 1.5f, Time.deltaTime * 10f), this.CurrentRepMarker.localPosition.y, this.CurrentRepMarker.localPosition.z);
				this.PendingRepMarker.localPosition = new Vector3(Mathf.Lerp(this.PendingRepMarker.localPosition.x, this.CurrentRepMarker.transform.localPosition.x + this.PendingRep * 1.5f, Time.deltaTime * 10f), this.PendingRepMarker.localPosition.y, this.PendingRepMarker.localPosition.z);
				this.LerpTimer += Time.deltaTime;
			}
			if (this.PendingRep != this.PreviousRep)
			{
				this.UpdatePendingRepLabel();
			}
		}
		else
		{
			this.PendingRepMarker.localPosition = new Vector3(Mathf.Lerp(this.PendingRepMarker.localPosition.x, -980f + this.PendingRep * -3f, Time.deltaTime * 10f), this.PendingRepMarker.localPosition.y, this.PendingRepMarker.localPosition.z);
			if (this.PendingRep < 0f)
			{
				this.PendingRepLabel.text = (-this.PendingRep).ToString("F1");
			}
			else
			{
				this.PendingRepLabel.text = string.Empty;
			}
		}
		if (this.CurrentRepMarker.localPosition.x < -980f)
		{
			this.CurrentRepMarker.localPosition = new Vector3(-980f, this.CurrentRepMarker.localPosition.y, this.CurrentRepMarker.localPosition.z);
		}
		if (this.PendingRepMarker.localPosition.x < -980f)
		{
			this.PendingRepMarker.localPosition = new Vector3(-980f, this.PendingRepMarker.localPosition.y, this.PendingRepMarker.localPosition.z);
		}
		if (this.CurrentRepMarker.localPosition.x > -680f)
		{
			this.CurrentRepMarker.localPosition = new Vector3(-680f, this.CurrentRepMarker.localPosition.y, this.CurrentRepMarker.localPosition.z);
		}
		if (this.PendingRepMarker.localPosition.x > -680f)
		{
			this.PendingRepMarker.localPosition = new Vector3(-680f, this.PendingRepMarker.localPosition.y, this.PendingRepMarker.localPosition.z);
		}
	}

	// Token: 0x06001B97 RID: 7063 RVA: 0x00138360 File Offset: 0x00136560
	public void UpdateRep()
	{
		this.Reputation += this.PendingRep;
		this.PendingRep = 0f;
		this.CheckedRep++;
		if (this.StudentManager.Yandere.Club == ClubType.Delinquent)
		{
			if (this.Reputation > -33.33333f)
			{
				this.Reputation = -33.33333f;
			}
		}
		else if (this.Reputation > 100f)
		{
			this.Reputation = 100f;
		}
		this.StudentManager.WipePendingRep();
	}

	// Token: 0x06001B98 RID: 7064 RVA: 0x001383EA File Offset: 0x001365EA
	public void BecomeEighties()
	{
		this.StudentManager.EightiesifyLabel(this.PendingRepLabel);
		this.StudentManager.EightiesifyLabel(this.RepUpdateLabel);
		this.StudentManager.EightiesifyLabel(this.RepLabel);
	}

	// Token: 0x06001B99 RID: 7065 RVA: 0x00138420 File Offset: 0x00136620
	public void UpdatePendingRepLabel()
	{
		this.PreviousRep = this.PendingRep;
		this.LerpTimer = 0f;
		if (!this.MissionMode)
		{
			this.RepUpdateLabel.enabled = true;
		}
		if (this.PendingRep > 0f)
		{
			this.PendingRepLabel.text = "+" + this.PendingRep.ToString("F1");
			return;
		}
		if (this.PendingRep < 0f)
		{
			this.StudentManager.TutorialWindow.ShowRepMessage = true;
			this.PendingRepLabel.text = this.PendingRep.ToString("F1");
			return;
		}
		this.RepUpdateLabel.enabled = false;
		this.PendingRepLabel.text = string.Empty;
	}

	// Token: 0x04002F64 RID: 12132
	public StudentManagerScript StudentManager;

	// Token: 0x04002F65 RID: 12133
	public ArmDetectorScript ArmDetector;

	// Token: 0x04002F66 RID: 12134
	public PortalScript Portal;

	// Token: 0x04002F67 RID: 12135
	public Transform CurrentRepMarker;

	// Token: 0x04002F68 RID: 12136
	public Transform PendingRepMarker;

	// Token: 0x04002F69 RID: 12137
	public UILabel PendingRepLabel;

	// Token: 0x04002F6A RID: 12138
	public UILabel RepUpdateLabel;

	// Token: 0x04002F6B RID: 12139
	public UILabel RepLabel;

	// Token: 0x04002F6C RID: 12140
	public ClockScript Clock;

	// Token: 0x04002F6D RID: 12141
	public float Reputation;

	// Token: 0x04002F6E RID: 12142
	public float LerpTimer;

	// Token: 0x04002F6F RID: 12143
	public float PreviousRep;

	// Token: 0x04002F70 RID: 12144
	public float PendingRep;

	// Token: 0x04002F71 RID: 12145
	public int CheckedRep = 1;

	// Token: 0x04002F72 RID: 12146
	public int Phase;

	// Token: 0x04002F73 RID: 12147
	public bool MissionMode;
}
