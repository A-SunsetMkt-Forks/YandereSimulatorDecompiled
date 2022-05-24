﻿using System;
using UnityEngine;

// Token: 0x0200033F RID: 831
public class IntroYandereScript : MonoBehaviour
{
	// Token: 0x06001914 RID: 6420 RVA: 0x000FB84C File Offset: 0x000F9A4C
	private void LateUpdate()
	{
		this.Hips.localEulerAngles = new Vector3(this.Hips.localEulerAngles.x + this.X, this.Hips.localEulerAngles.y, this.Hips.localEulerAngles.z);
		this.Spine.localEulerAngles = new Vector3(this.Spine.localEulerAngles.x + this.X, this.Spine.localEulerAngles.y, this.Spine.localEulerAngles.z);
		this.Spine1.localEulerAngles = new Vector3(this.Spine1.localEulerAngles.x + this.X, this.Spine1.localEulerAngles.y, this.Spine1.localEulerAngles.z);
		this.Spine2.localEulerAngles = new Vector3(this.Spine2.localEulerAngles.x + this.X, this.Spine2.localEulerAngles.y, this.Spine2.localEulerAngles.z);
		this.Spine3.localEulerAngles = new Vector3(this.Spine3.localEulerAngles.x + this.X, this.Spine3.localEulerAngles.y, this.Spine3.localEulerAngles.z);
		this.Neck.localEulerAngles = new Vector3(this.Neck.localEulerAngles.x + this.X, this.Neck.localEulerAngles.y, this.Neck.localEulerAngles.z);
		this.Head.localEulerAngles = new Vector3(this.Head.localEulerAngles.x + this.X, this.Head.localEulerAngles.y, this.Head.localEulerAngles.z);
		this.RightUpLeg.localEulerAngles = new Vector3(this.RightUpLeg.localEulerAngles.x - this.X, this.RightUpLeg.localEulerAngles.y, this.RightUpLeg.localEulerAngles.z);
		this.RightLeg.localEulerAngles = new Vector3(this.RightLeg.localEulerAngles.x - this.X, this.RightLeg.localEulerAngles.y, this.RightLeg.localEulerAngles.z);
		this.RightFoot.localEulerAngles = new Vector3(this.RightFoot.localEulerAngles.x - this.X, this.RightFoot.localEulerAngles.y, this.RightFoot.localEulerAngles.z);
		this.LeftUpLeg.localEulerAngles = new Vector3(this.LeftUpLeg.localEulerAngles.x - this.X, this.LeftUpLeg.localEulerAngles.y, this.LeftUpLeg.localEulerAngles.z);
		this.LeftLeg.localEulerAngles = new Vector3(this.LeftLeg.localEulerAngles.x - this.X, this.LeftLeg.localEulerAngles.y, this.LeftLeg.localEulerAngles.z);
		this.LeftFoot.localEulerAngles = new Vector3(this.LeftFoot.localEulerAngles.x - this.X, this.LeftFoot.localEulerAngles.y, this.LeftFoot.localEulerAngles.z);
	}

	// Token: 0x04002719 RID: 10009
	public Transform Hips;

	// Token: 0x0400271A RID: 10010
	public Transform Spine;

	// Token: 0x0400271B RID: 10011
	public Transform Spine1;

	// Token: 0x0400271C RID: 10012
	public Transform Spine2;

	// Token: 0x0400271D RID: 10013
	public Transform Spine3;

	// Token: 0x0400271E RID: 10014
	public Transform Neck;

	// Token: 0x0400271F RID: 10015
	public Transform Head;

	// Token: 0x04002720 RID: 10016
	public Transform RightUpLeg;

	// Token: 0x04002721 RID: 10017
	public Transform RightLeg;

	// Token: 0x04002722 RID: 10018
	public Transform RightFoot;

	// Token: 0x04002723 RID: 10019
	public Transform LeftUpLeg;

	// Token: 0x04002724 RID: 10020
	public Transform LeftLeg;

	// Token: 0x04002725 RID: 10021
	public Transform LeftFoot;

	// Token: 0x04002726 RID: 10022
	public float X;
}
