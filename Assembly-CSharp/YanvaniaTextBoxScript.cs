﻿using System;
using UnityEngine;

// Token: 0x020004E5 RID: 1253
public class YanvaniaTextBoxScript : MonoBehaviour
{
	// Token: 0x060020C8 RID: 8392 RVA: 0x001E2204 File Offset: 0x001E0404
	private void Start()
	{
		this.Portrait.transform.localScale = Vector3.zero;
		this.BloodWipe.transform.localScale = new Vector3(0f, this.BloodWipe.transform.localScale.y, this.BloodWipe.transform.localScale.z);
		this.SpeakerLabel.text = string.Empty;
		this.Border.color = new Color(this.Border.color.r, this.Border.color.g, this.Border.color.b, 0f);
		this.BG.color = new Color(this.BG.color.r, this.BG.color.g, this.BG.color.b, 0f);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060020C9 RID: 8393 RVA: 0x001E2310 File Offset: 0x001E0510
	private void Update()
	{
		if (!this.Leave)
		{
			if (this.BloodWipe.transform.localScale.x == 0f)
			{
				this.BloodWipe.transform.localScale = new Vector3(this.BloodWipe.transform.localScale.x + Time.deltaTime, this.BloodWipe.transform.localScale.y, this.BloodWipe.transform.localScale.z);
			}
			if (this.BloodWipe.transform.localScale.x > 50f)
			{
				this.BloodWipe.color = new Color(this.BloodWipe.color.r, this.BloodWipe.color.g, this.BloodWipe.color.b, this.BloodWipe.color.a - Time.deltaTime);
				this.Border.color = new Color(this.Border.color.r, this.Border.color.g, this.Border.color.b, this.Border.color.a + Time.deltaTime);
				this.BG.color = new Color(this.BG.color.r, this.BG.color.g, this.BG.color.b, 0.5f);
			}
			else
			{
				this.BloodWipe.transform.localScale = new Vector3(this.BloodWipe.transform.localScale.x + this.BloodWipe.transform.localScale.x * 0.1f, this.BloodWipe.transform.localScale.y, this.BloodWipe.transform.localScale.z);
			}
			if (this.BloodWipe.color.a <= 0f)
			{
				if (!this.Display)
				{
					if (this.LineID < this.Lines.Length - 1)
					{
						if (this.NewLabel != null)
						{
							UnityEngine.Object.Destroy(this.NewLabel);
						}
						this.UpdatePortrait = true;
						this.Display = true;
						this.PortraitID = ((this.PortraitID == 1) ? 2 : 1);
						this.SpeakerLabel.text = string.Empty;
					}
				}
				else if (this.NewLabelScript != null)
				{
					AudioSource component = base.GetComponent<AudioSource>();
					if (!this.NewLabelScript.enabled)
					{
						this.NewLabelScript.enabled = true;
						component.clip = this.Voices[this.LineID];
						this.NewLineTimer = 0f;
						component.Play();
					}
					else
					{
						this.NewLineTimer += Time.deltaTime;
						if (this.NewLineTimer > component.clip.length + 0.5f || Input.GetButtonDown("A") || Input.GetKeyDown("z") || Input.GetKeyDown("x"))
						{
							this.Display = false;
						}
					}
				}
			}
			if (this.UpdatePortrait)
			{
				if (!this.Grow)
				{
					this.Portrait.transform.localScale = Vector3.MoveTowards(this.Portrait.transform.localScale, Vector3.zero, Time.deltaTime * 10f);
					if (this.Portrait.transform.localScale.x == 0f)
					{
						this.Portrait.mainTexture = this.Portraits[this.PortraitID];
						this.Grow = true;
					}
				}
				else
				{
					this.Portrait.transform.localScale = Vector3.MoveTowards(this.Portrait.transform.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
					if (this.Portrait.transform.localScale.x == 1f)
					{
						this.SpeakerLabel.text = this.SpeakerNames[this.PortraitID];
						this.UpdatePortrait = false;
						this.AnimTimer = 0f;
						this.Grow = false;
						this.LineID++;
						this.SpawnLabel();
					}
				}
			}
			this.AnimTimer += Time.deltaTime;
			if (this.LineID == 2)
			{
				this.NewTypewriter.charsPerSecond = 15;
				this.NewTypewriter.delayOnPeriod = 1.5f;
				if (this.AnimTimer < 0.5f)
				{
					this.NewTypewriter.delayOnComma = true;
				}
			}
			Animation component2 = this.Yanmont.Character.GetComponent<Animation>();
			if (this.LineID == 3)
			{
				this.NewTypewriter.delayOnComma = true;
				this.NewTypewriter.delayOnPeriod = 0.75f;
				if (this.AnimTimer < 1f)
				{
					component2.CrossFade("f02_yanvaniaCutsceneAction1_00");
				}
				if (component2["f02_yanvaniaCutsceneAction1_00"].time >= component2["f02_yanvaniaCutsceneAction1_00"].length)
				{
					component2.CrossFade("f02_yanvaniaDramaticIdle_00");
				}
			}
			Animation component3 = this.Dracula.Character.GetComponent<Animation>();
			if (this.LineID == 5)
			{
				this.NewTypewriter.charsPerSecond = 15;
				component2.CrossFade("f02_yanvaniaCutsceneAction2_00");
				if (component2["f02_yanvaniaCutsceneAction2_00"].time >= component2["f02_yanvaniaCutsceneAction2_00"].length)
				{
					component2.CrossFade("f02_yanvaniaDramaticIdle_00");
				}
				if (this.AnimTimer > 4f)
				{
					component3.CrossFade("DraculaDrink");
				}
				if (this.AnimTimer > 4.5f)
				{
					this.Glass.GetComponent<Renderer>().materials[0].color = new Color(1f, 1f, 1f, 0.5f);
				}
				if (this.AnimTimer > 5f && component3["DraculaDrink"].time >= component3["DraculaDrink"].length)
				{
					component3.CrossFade("DraculaIdle");
				}
			}
			if (this.LineID == 6)
			{
				component2.CrossFade("f02_yanvaniaDramaticIdle_00");
				if (this.AnimTimer < 1f)
				{
					this.NewTypewriter.delayOnPeriod = 2.25f;
				}
				if (this.AnimTimer > 1f && this.AnimTimer < 2f)
				{
					component3.CrossFade("DraculaToss");
				}
				if (this.Glass != null)
				{
					Rigidbody component4 = this.Glass.GetComponent<Rigidbody>();
					if (this.AnimTimer > 1.4f && !component4.useGravity)
					{
						component4.useGravity = true;
						this.Glass.transform.parent = null;
						component4.AddForce(-100f, 100f, -100f);
					}
				}
				if (this.AnimTimer > 2f + component3["DraculaToss"].length && this.AnimTimer < 6f)
				{
					component3.CrossFade("DraculaIdle");
				}
				if (this.AnimTimer > 4f)
				{
					this.NewTypewriter.delayOnPeriod = 1f;
					this.NewTypewriter.charsPerSecond = 15;
				}
				if (this.AnimTimer > 6f && this.AnimTimer < 9.5f)
				{
					this.Dracula.transform.position = Vector3.Lerp(this.Dracula.transform.position, new Vector3(-34.675f, 7.5f, 2.8f), Time.deltaTime * 10f);
					component3.CrossFade("succubus_a_idle_01");
				}
				if (this.AnimTimer > 9.5f)
				{
					this.NewLabelScript.text = string.Empty;
					this.SpeakerLabel.text = string.Empty;
					this.Dracula.SpawnTeleportEffect();
					this.Dracula.enabled = true;
					this.Jukebox.BossBattle();
					this.Leave = true;
				}
			}
			if (Input.GetKeyDown(KeyCode.Alpha3))
			{
				if (this.NewLabel != null)
				{
					UnityEngine.Object.Destroy(this.NewLabel);
				}
				if (this.NewLabelScript != null)
				{
					this.NewLabelScript.text = string.Empty;
				}
				this.SpeakerLabel.text = string.Empty;
				this.Dracula.SpawnTeleportEffect();
				this.Dracula.enabled = true;
				this.Jukebox.BossBattle();
				UnityEngine.Object.Destroy(this.BloodWipe);
				UnityEngine.Object.Destroy(this.Glass);
				this.Leave = true;
				return;
			}
		}
		else
		{
			this.Portrait.transform.localScale = Vector3.MoveTowards(this.Portrait.transform.localScale, Vector3.zero, Time.deltaTime * 10f);
			if (this.Portrait.transform.localScale.x == 0f)
			{
				this.Border.transform.position = new Vector3(this.Border.transform.position.x, this.Border.transform.position.y + Time.deltaTime, this.Border.transform.position.z);
				this.BG.transform.position = new Vector3(this.BG.transform.position.x, this.BG.transform.position.y + Time.deltaTime, this.BG.transform.position.z);
				if (!this.Yanmont.enabled)
				{
					this.Yanmont.YanvaniaCamera.TargetZoom = 0f;
					this.Yanmont.Character.transform.localScale = new Vector3(-1f, 1f, 1f);
					this.Yanmont.EnterCutscene = false;
					this.Yanmont.Cutscene = false;
					this.Yanmont.enabled = true;
				}
			}
		}
	}

	// Token: 0x060020CA RID: 8394 RVA: 0x001E2D20 File Offset: 0x001E0F20
	private void SpawnLabel()
	{
		this.NewLabel = UnityEngine.Object.Instantiate<GameObject>(this.Label, base.transform.position, Quaternion.identity);
		this.NewLabel.transform.parent = this.NewLabelSpawnPoint;
		this.NewLabel.transform.localEulerAngles = Vector3.zero;
		this.NewLabel.transform.localPosition = Vector3.zero;
		this.NewLabel.transform.localScale = new Vector3(1f, 1f, 1f);
		this.NewTypewriter = this.NewLabel.GetComponent<TypewriterEffect>();
		this.NewLabelScript = this.NewLabel.GetComponent<UILabel>();
		this.NewLabelScript.text = this.Lines[this.LineID];
		this.NewLabelScript.enabled = false;
	}

	// Token: 0x040047F7 RID: 18423
	private TypewriterEffect NewTypewriter;

	// Token: 0x040047F8 RID: 18424
	private UILabel NewLabelScript;

	// Token: 0x040047F9 RID: 18425
	private GameObject NewLabel;

	// Token: 0x040047FA RID: 18426
	public YanvaniaJukeboxScript Jukebox;

	// Token: 0x040047FB RID: 18427
	public YanvaniaDraculaScript Dracula;

	// Token: 0x040047FC RID: 18428
	public YanvaniaYanmontScript Yanmont;

	// Token: 0x040047FD RID: 18429
	public Transform NewLabelSpawnPoint;

	// Token: 0x040047FE RID: 18430
	public GameObject Glass;

	// Token: 0x040047FF RID: 18431
	public GameObject Label;

	// Token: 0x04004800 RID: 18432
	public UILabel SpeakerLabel;

	// Token: 0x04004801 RID: 18433
	public UITexture BloodWipe;

	// Token: 0x04004802 RID: 18434
	public UITexture Portrait;

	// Token: 0x04004803 RID: 18435
	public UITexture Border;

	// Token: 0x04004804 RID: 18436
	public UITexture BG;

	// Token: 0x04004805 RID: 18437
	public bool UpdatePortrait;

	// Token: 0x04004806 RID: 18438
	public bool Display;

	// Token: 0x04004807 RID: 18439
	public bool Leave;

	// Token: 0x04004808 RID: 18440
	public bool Grow;

	// Token: 0x04004809 RID: 18441
	public string[] SpeakerNames;

	// Token: 0x0400480A RID: 18442
	public Texture[] Portraits;

	// Token: 0x0400480B RID: 18443
	public AudioClip[] Voices;

	// Token: 0x0400480C RID: 18444
	public string[] Lines;

	// Token: 0x0400480D RID: 18445
	public int PortraitID = 1;

	// Token: 0x0400480E RID: 18446
	public int LineID;

	// Token: 0x0400480F RID: 18447
	public float NewLineTimer;

	// Token: 0x04004810 RID: 18448
	public float AnimTimer;

	// Token: 0x04004811 RID: 18449
	public float Timer;
}
