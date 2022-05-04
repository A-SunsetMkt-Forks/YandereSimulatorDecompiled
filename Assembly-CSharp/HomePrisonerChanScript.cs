﻿using System;
using UnityEngine;

// Token: 0x02000325 RID: 805
public class HomePrisonerChanScript : MonoBehaviour
{
	// Token: 0x060018B2 RID: 6322 RVA: 0x000F1FD4 File Offset: 0x000F01D4
	private void Start()
	{
		if (SchoolGlobals.KidnapVictim > 0)
		{
			this.StudentID = SchoolGlobals.KidnapVictim;
			if (StudentGlobals.GetStudentSanity(this.StudentID) == 100f)
			{
				this.AnkleRopes.SetActive(false);
			}
			this.PermanentAngleR = this.TwintailR.eulerAngles;
			this.PermanentAngleL = this.TwintailL.eulerAngles;
			if (!StudentGlobals.GetStudentArrested(this.StudentID) && !StudentGlobals.GetStudentDead(this.StudentID))
			{
				this.Cosmetic.StudentID = this.StudentID;
				this.Cosmetic.enabled = true;
				this.BreastSize = this.JSON.Students[this.StudentID].BreastSize;
				this.RightEyeRotOrigin = this.RightEye.localEulerAngles;
				this.LeftEyeRotOrigin = this.LeftEye.localEulerAngles;
				this.RightEyeOrigin = this.RightEye.localPosition;
				this.LeftEyeOrigin = this.LeftEye.localPosition;
				this.UpdateSanity();
				this.TwintailR.transform.localEulerAngles = new Vector3(0f, 180f, -90f);
				this.TwintailL.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
				this.Blindfold.SetActive(false);
				this.Tripod.SetActive(false);
				if (this.StudentID == 81 && !StudentGlobals.GetStudentBroken(81) && SchemeGlobals.HelpingKokona)
				{
					this.Blindfold.SetActive(true);
					this.Tripod.SetActive(true);
				}
			}
			else
			{
				SchoolGlobals.KidnapVictim = 0;
				base.gameObject.SetActive(false);
			}
		}
		else
		{
			base.gameObject.SetActive(false);
		}
		if (GameGlobals.Eighties)
		{
			if (this.Eighties)
			{
				for (int i = 0; i < this.Cosmetic.Student.Ragdoll.AllRigidbodies.Length; i++)
				{
					this.Cosmetic.Student.Ragdoll.AllRigidbodies[i].isKinematic = true;
					this.Cosmetic.Student.Ragdoll.AllColliders[i].enabled = false;
				}
				this.Cosmetic.Student.DisableFemaleProps();
				this.Cosmetic.Student.SetSplashes(false);
				this.Cosmetic.Student.DisableProps();
				this.Blindfold.SetActive(true);
				return;
			}
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x060018B3 RID: 6323 RVA: 0x000F224C File Offset: 0x000F044C
	private void LateUpdate()
	{
		this.Skirt.transform.localPosition = new Vector3(0f, -0.135f, 0.01f);
		this.Skirt.transform.localScale = new Vector3(this.Skirt.transform.localScale.x, 1.2f, this.Skirt.transform.localScale.z);
		if (!this.Tortured)
		{
			if (this.Sanity > 0f)
			{
				if (this.LookAhead)
				{
					this.Neck.localEulerAngles = new Vector3(this.Neck.localEulerAngles.x - 45f, this.Neck.localEulerAngles.y, this.Neck.localEulerAngles.z);
				}
				else if (this.YandereDetector.YandereDetected && Vector3.Distance(base.transform.position, this.HomeYandere.position) < 2f)
				{
					Quaternion b;
					if (this.HomeCamera.Target == this.HomeCamera.Targets[10])
					{
						b = Quaternion.LookRotation(this.HomeCamera.transform.position + Vector3.down * (1.5f * ((100f - this.Sanity) / 100f)) - this.Neck.position);
						this.HairRotation = Mathf.Lerp(this.HairRotation, this.HairRot1, Time.deltaTime * 2f);
					}
					else
					{
						b = Quaternion.LookRotation(this.HomeYandere.position + Vector3.up * 1.5f - this.Neck.position);
						this.HairRotation = Mathf.Lerp(this.HairRotation, this.HairRot2, Time.deltaTime * 2f);
					}
					this.Neck.rotation = Quaternion.Slerp(this.LastRotation, b, Time.deltaTime * 2f);
					this.TwintailR.transform.localEulerAngles = new Vector3(this.HairRotation, 180f, -90f);
					this.TwintailL.transform.localEulerAngles = new Vector3(-this.HairRotation, 0f, -90f);
				}
				else
				{
					if (this.HomeCamera.Target == this.HomeCamera.Targets[10])
					{
						Quaternion b2 = Quaternion.LookRotation(this.HomeCamera.transform.position + Vector3.down * (1.5f * ((100f - this.Sanity) / 100f)) - this.Neck.position);
						this.HairRotation = Mathf.Lerp(this.HairRotation, this.HairRot3, Time.deltaTime * 2f);
					}
					else
					{
						Quaternion b2 = Quaternion.LookRotation(base.transform.position + base.transform.forward - this.Neck.position);
						this.Neck.rotation = Quaternion.Slerp(this.LastRotation, b2, Time.deltaTime * 2f);
					}
					this.HairRotation = Mathf.Lerp(this.HairRotation, this.HairRot4, Time.deltaTime * 2f);
					this.TwintailR.transform.localEulerAngles = new Vector3(this.HairRotation, 180f, -90f);
					this.TwintailL.transform.localEulerAngles = new Vector3(-this.HairRotation, 0f, -90f);
				}
			}
			else
			{
				this.Neck.localEulerAngles = new Vector3(this.Neck.localEulerAngles.x - 45f, this.Neck.localEulerAngles.y, this.Neck.localEulerAngles.z);
			}
		}
		this.LastRotation = this.Neck.rotation;
		if (!this.Tortured && this.Sanity < 100f && this.Sanity > 0f)
		{
			this.TwitchTimer += Time.deltaTime;
			if (this.TwitchTimer > this.NextTwitch)
			{
				this.Twitch = new Vector3((1f - this.Sanity / 100f) * UnityEngine.Random.Range(-10f, 10f), (1f - this.Sanity / 100f) * UnityEngine.Random.Range(-10f, 10f), (1f - this.Sanity / 100f) * UnityEngine.Random.Range(-10f, 10f));
				this.NextTwitch = UnityEngine.Random.Range(0f, 1f);
				this.TwitchTimer = 0f;
			}
			this.Twitch = Vector3.Lerp(this.Twitch, Vector3.zero, Time.deltaTime * 10f);
			this.Neck.localEulerAngles += this.Twitch;
		}
		if (this.Tortured)
		{
			this.HairRotation = Mathf.Lerp(this.HairRotation, this.HairRot5, Time.deltaTime * 2f);
			this.TwintailR.transform.localEulerAngles = new Vector3(this.HairRotation, 180f, -90f);
			this.TwintailL.transform.localEulerAngles = new Vector3(-this.HairRotation, 0f, -90f);
		}
	}

	// Token: 0x060018B4 RID: 6324 RVA: 0x000F27FC File Offset: 0x000F09FC
	public void UpdateSanity()
	{
		this.Sanity = StudentGlobals.GetStudentSanity(this.StudentID);
		bool active = this.Sanity == 0f;
		this.RightMindbrokenEye.SetActive(active);
		this.LeftMindbrokenEye.SetActive(active);
	}

	// Token: 0x04002559 RID: 9561
	public HomeYandereDetectorScript YandereDetector;

	// Token: 0x0400255A RID: 9562
	public HomeCameraScript HomeCamera;

	// Token: 0x0400255B RID: 9563
	public CosmeticScript Cosmetic;

	// Token: 0x0400255C RID: 9564
	public JsonScript JSON;

	// Token: 0x0400255D RID: 9565
	public Vector3 RightEyeRotOrigin;

	// Token: 0x0400255E RID: 9566
	public Vector3 LeftEyeRotOrigin;

	// Token: 0x0400255F RID: 9567
	public Vector3 PermanentAngleR;

	// Token: 0x04002560 RID: 9568
	public Vector3 PermanentAngleL;

	// Token: 0x04002561 RID: 9569
	public Vector3 RightEyeOrigin;

	// Token: 0x04002562 RID: 9570
	public Vector3 LeftEyeOrigin;

	// Token: 0x04002563 RID: 9571
	public Vector3 Twitch;

	// Token: 0x04002564 RID: 9572
	public Quaternion LastRotation;

	// Token: 0x04002565 RID: 9573
	public Transform HomeYandere;

	// Token: 0x04002566 RID: 9574
	public Transform RightBreast;

	// Token: 0x04002567 RID: 9575
	public Transform LeftBreast;

	// Token: 0x04002568 RID: 9576
	public Transform TwintailR;

	// Token: 0x04002569 RID: 9577
	public Transform TwintailL;

	// Token: 0x0400256A RID: 9578
	public Transform RightEye;

	// Token: 0x0400256B RID: 9579
	public Transform LeftEye;

	// Token: 0x0400256C RID: 9580
	public Transform Skirt;

	// Token: 0x0400256D RID: 9581
	public Transform Neck;

	// Token: 0x0400256E RID: 9582
	public GameObject RightMindbrokenEye;

	// Token: 0x0400256F RID: 9583
	public GameObject LeftMindbrokenEye;

	// Token: 0x04002570 RID: 9584
	public GameObject AnkleRopes;

	// Token: 0x04002571 RID: 9585
	public GameObject Blindfold;

	// Token: 0x04002572 RID: 9586
	public GameObject Character;

	// Token: 0x04002573 RID: 9587
	public GameObject Tripod;

	// Token: 0x04002574 RID: 9588
	public float HairRotation;

	// Token: 0x04002575 RID: 9589
	public float TwitchTimer;

	// Token: 0x04002576 RID: 9590
	public float NextTwitch;

	// Token: 0x04002577 RID: 9591
	public float BreastSize;

	// Token: 0x04002578 RID: 9592
	public float EyeShrink;

	// Token: 0x04002579 RID: 9593
	public float Sanity;

	// Token: 0x0400257A RID: 9594
	public float HairRot1;

	// Token: 0x0400257B RID: 9595
	public float HairRot2;

	// Token: 0x0400257C RID: 9596
	public float HairRot3;

	// Token: 0x0400257D RID: 9597
	public float HairRot4;

	// Token: 0x0400257E RID: 9598
	public float HairRot5;

	// Token: 0x0400257F RID: 9599
	public bool LookAhead;

	// Token: 0x04002580 RID: 9600
	public bool Tortured;

	// Token: 0x04002581 RID: 9601
	public bool Eighties;

	// Token: 0x04002582 RID: 9602
	public bool Male;

	// Token: 0x04002583 RID: 9603
	public int StudentID;
}
