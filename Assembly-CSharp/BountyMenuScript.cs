﻿using System;
using UnityEngine;

// Token: 0x020000F6 RID: 246
public class BountyMenuScript : MonoBehaviour
{
	// Token: 0x06000A61 RID: 2657 RVA: 0x0005C4C7 File Offset: 0x0005A6C7
	private void Start()
	{
		this.DescLabel.text = this.Descriptions[this.Clock.Day];
		this.GetPortrait(this.StudentIDs[this.Clock.Day]);
	}

	// Token: 0x06000A62 RID: 2658 RVA: 0x0005C4FE File Offset: 0x0005A6FE
	private void Update()
	{
		this.DescLabel.text = this.DescLabel.text.Replace('@', '\n');
	}

	// Token: 0x06000A63 RID: 2659 RVA: 0x0005C520 File Offset: 0x0005A720
	private void GetPortrait(int ID)
	{
		WWW www = new WWW(string.Concat(new string[]
		{
			"file:///",
			Application.streamingAssetsPath,
			"/Portraits/Student_",
			ID.ToString(),
			".png"
		}));
		this.Portrait.mainTexture = www.texture;
	}

	// Token: 0x04000C0A RID: 3082
	public ClockScript Clock;

	// Token: 0x04000C0B RID: 3083
	public UITexture Portrait;

	// Token: 0x04000C0C RID: 3084
	public UILabel DescLabel;

	// Token: 0x04000C0D RID: 3085
	public string[] Descriptions;

	// Token: 0x04000C0E RID: 3086
	public int[] StudentIDs;
}
