﻿using System;
using UnityEngine;

// Token: 0x02000489 RID: 1161
public class TranqCaseScript : MonoBehaviour
{
	// Token: 0x06001F19 RID: 7961 RVA: 0x001B6E8B File Offset: 0x001B508B
	private void Start()
	{
		this.Prompt.enabled = false;
	}

	// Token: 0x06001F1A RID: 7962 RVA: 0x001B6E9C File Offset: 0x001B509C
	private void Update()
	{
		if (this.Yandere.transform.position.x > base.transform.position.x && Vector3.Distance(base.transform.position, this.Yandere.transform.position) < 1f)
		{
			if (this.Yandere.Dragging)
			{
				if (this.Ragdoll == null)
				{
					this.Ragdoll = this.Yandere.Ragdoll.GetComponent<RagdollScript>();
				}
				if (this.Ragdoll.Tranquil)
				{
					if (!this.Prompt.enabled)
					{
						this.Prompt.enabled = true;
					}
				}
				else if (this.Prompt.enabled)
				{
					this.Prompt.Hide();
					this.Prompt.enabled = false;
				}
			}
			else if (this.Prompt.enabled)
			{
				this.Prompt.Hide();
				this.Prompt.enabled = false;
			}
		}
		else if (this.Prompt.enabled)
		{
			this.Prompt.Hide();
			this.Prompt.enabled = false;
		}
		if (this.Prompt.enabled && this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.Circle[0].fillAmount = 1f;
			if (!this.Yandere.Chased && this.Yandere.Chasers == 0)
			{
				this.Yandere.TranquilHiding = true;
				this.Yandere.CanMove = false;
				this.Prompt.enabled = false;
				this.Prompt.Hide();
				this.Ragdoll.TranqCase = this;
				this.VictimClubType = this.Ragdoll.Student.Club;
				this.VictimID = this.Ragdoll.StudentID;
				this.Door.Prompt.enabled = true;
				this.Door.enabled = true;
				this.Occupied = true;
				this.Animate = true;
				this.Open = true;
			}
		}
		if (this.Animate)
		{
			if (this.Open)
			{
				this.Rotation = Mathf.Lerp(this.Rotation, 105f, Time.deltaTime * 10f);
			}
			else
			{
				this.Rotation = Mathf.Lerp(this.Rotation, 0f, Time.deltaTime * 10f);
				this.Ragdoll.Student.OsanaHairL.transform.localScale = Vector3.MoveTowards(this.Ragdoll.Student.OsanaHairL.transform.localScale, new Vector3(0f, 0f, 0f), Time.deltaTime * 10f);
				this.Ragdoll.Student.OsanaHairR.transform.localScale = Vector3.MoveTowards(this.Ragdoll.Student.OsanaHairR.transform.localScale, new Vector3(0f, 0f, 0f), Time.deltaTime * 10f);
				if (this.Rotation < 1f)
				{
					this.Animate = false;
					this.Rotation = 0f;
				}
			}
			this.Hinge.localEulerAngles = new Vector3(0f, 0f, this.Rotation);
		}
	}

	// Token: 0x040040DE RID: 16606
	public YandereScript Yandere;

	// Token: 0x040040DF RID: 16607
	public RagdollScript Ragdoll;

	// Token: 0x040040E0 RID: 16608
	public PromptScript Prompt;

	// Token: 0x040040E1 RID: 16609
	public DoorScript Door;

	// Token: 0x040040E2 RID: 16610
	public Transform Hinge;

	// Token: 0x040040E3 RID: 16611
	public bool Occupied;

	// Token: 0x040040E4 RID: 16612
	public bool Open;

	// Token: 0x040040E5 RID: 16613
	public int VictimID;

	// Token: 0x040040E6 RID: 16614
	public ClubType VictimClubType;

	// Token: 0x040040E7 RID: 16615
	public float Rotation;

	// Token: 0x040040E8 RID: 16616
	public bool Animate;
}
