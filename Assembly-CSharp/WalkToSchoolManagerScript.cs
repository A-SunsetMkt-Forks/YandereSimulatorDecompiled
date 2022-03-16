﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020004BA RID: 1210
public class WalkToSchoolManagerScript : MonoBehaviour
{
	// Token: 0x06001FB1 RID: 8113 RVA: 0x001BDB38 File Offset: 0x001BBD38
	private void Start()
	{
		Application.targetFrameRate = 60;
		if (SchoolGlobals.SchoolAtmosphere < 0.5f || GameGlobals.LoveSick)
		{
			this.Darkness.color = new Color(0f, 0f, 0f, 1f);
		}
		else
		{
			this.Darkness.color = new Color(1f, 1f, 1f, 1f);
		}
		this.Window.localScale = new Vector3(0f, 0f, 0f);
		this.Yandere.Character.GetComponent<Animation>()["f02_newWalk_00"].time = UnityEngine.Random.Range(0f, this.Yandere.Character.GetComponent<Animation>()["f02_newWalk_00"].length);
		this.Yandere.WearOutdoorShoes();
		this.Senpai.WearOutdoorShoes();
		this.Rival.WearOutdoorShoes();
		if (GameGlobals.BlondeHair)
		{
			this.PonytailRenderer.material.mainTexture = this.BlondePony;
		}
	}

	// Token: 0x06001FB2 RID: 8114 RVA: 0x001BDC50 File Offset: 0x001BBE50
	private void Update()
	{
		for (int i = 1; i < 3; i++)
		{
			Transform transform = this.Neighborhood[i];
			transform.position = new Vector3(transform.position.x - Time.deltaTime * this.ScrollSpeed, transform.position.y, transform.position.z);
			if (transform.position.x < -160f)
			{
				transform.position = new Vector3(transform.position.x + 320f, transform.position.y, transform.position.z);
			}
		}
		if (!this.FadeOut)
		{
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 0f, Time.deltaTime));
			if (this.Darkness.color.a == 0f)
			{
				if (!this.ShowWindow)
				{
					if (!this.Ending)
					{
						if (Input.GetButtonDown("A"))
						{
							this.Timer = 1f;
						}
						this.Timer += Time.deltaTime;
						if (this.Timer > 1f)
						{
							this.RivalEyeRTarget = this.RivalEyeR.localEulerAngles.y;
							this.RivalEyeLTarget = this.RivalEyeL.localEulerAngles.y;
							this.SenpaiEyeRTarget = this.SenpaiEyeR.localEulerAngles.y;
							this.SenpaiEyeLTarget = this.SenpaiEyeL.localEulerAngles.y;
							this.ShowWindow = true;
							this.PromptBar.ClearButtons();
							this.PromptBar.Label[0].text = "Continue";
							this.PromptBar.Label[2].text = "Skip";
							this.PromptBar.UpdateButtons();
							this.PromptBar.Show = true;
						}
					}
					else
					{
						this.Window.localScale = Vector3.Lerp(this.Window.localScale, new Vector3(0f, 0f, 0f), Time.deltaTime * 10f);
						if ((double)this.Window.localScale.x < 0.01)
						{
							this.Timer += Time.deltaTime;
							if (this.Timer > 1f)
							{
								this.FadeOut = true;
							}
						}
					}
				}
				else
				{
					this.Window.localScale = Vector3.Lerp(this.Window.localScale, new Vector3(1f, 1f, 1f), Time.deltaTime * 10f);
					if ((double)this.Window.localScale.x > 0.99)
					{
						if (this.Frame > 3)
						{
							this.Typewriter.mLabel.color = new Color(1f, 1f, 1f, 1f);
						}
						this.Frame++;
					}
					if (!this.Talk)
					{
						if ((double)this.Window.localScale.x > 0.99)
						{
							this.Talk = true;
							this.UpdateNameLabel();
							this.Typewriter.enabled = true;
							this.Typewriter.ResetToBeginning();
							this.Typewriter.mFullText = this.Lines[this.ID];
							this.Typewriter.mLabel.text = this.Lines[this.ID];
							this.Typewriter.mLabel.color = new Color(1f, 1f, 1f, 0f);
							this.MyAudio.clip = this.Speech[this.ID];
							this.MyAudio.Play();
						}
					}
					else
					{
						if (this.Auto && !this.MyAudio.isPlaying)
						{
							this.AutoTimer += Time.deltaTime;
						}
						if (Input.GetButtonDown("A") || this.AutoTimer > 1f)
						{
							Debug.Log("Detected button press.");
							this.AutoTimer = 0f;
							if (this.ID < this.Lines.Length - 1)
							{
								if (this.Typewriter.mCurrentOffset < this.Typewriter.mFullText.Length)
								{
									Debug.Log("Line not finished yet.");
									this.Typewriter.Finish();
									this.Typewriter.mCurrentOffset = this.Typewriter.mFullText.Length;
								}
								else
								{
									Debug.Log("Line finished.");
									this.ID++;
									this.Frame = 0;
									this.Typewriter.ResetToBeginning();
									this.Typewriter.mFullText = this.Lines[this.ID];
									this.Typewriter.mLabel.text = this.Lines[this.ID];
									this.Typewriter.mLabel.color = new Color(1f, 1f, 1f, 0f);
									this.MyAudio.clip = this.Speech[this.ID];
									this.MyAudio.Play();
									this.UpdateNameLabel();
								}
							}
							else if (this.Typewriter.mCurrentOffset < this.Typewriter.mFullText.Length)
							{
								this.Typewriter.Finish();
							}
							else
							{
								this.End();
							}
						}
						if (Input.GetButtonDown("X"))
						{
							this.End();
						}
					}
				}
			}
		}
		else
		{
			this.MyAudio.volume -= Time.deltaTime;
			this.Darkness.color = new Color(this.Darkness.color.r, this.Darkness.color.g, this.Darkness.color.b, Mathf.MoveTowards(this.Darkness.color.a, 1f, Time.deltaTime));
			if (this.Darkness.color.a == 1f && !this.Debugging)
			{
				SceneManager.LoadScene("LoadingScene");
			}
		}
		if (Input.GetKeyDown(KeyCode.Equals))
		{
			Time.timeScale += 10f;
		}
		if (Input.GetKeyDown(KeyCode.Minus))
		{
			Time.timeScale -= 10f;
		}
	}

	// Token: 0x06001FB3 RID: 8115 RVA: 0x001BE2FC File Offset: 0x001BC4FC
	private void LateUpdate()
	{
		if (this.Talk)
		{
			if (!this.Ending)
			{
				this.RivalNeckTarget = Mathf.Lerp(this.RivalNeckTarget, 15f, Time.deltaTime * 3.6f);
				this.RivalHeadTarget = Mathf.Lerp(this.RivalHeadTarget, 15f, Time.deltaTime * 3.6f);
				this.RivalEyeRTarget = Mathf.Lerp(this.RivalEyeRTarget, 95f, Time.deltaTime * 3.6f);
				this.RivalEyeLTarget = Mathf.Lerp(this.RivalEyeLTarget, 275f, Time.deltaTime * 3.6f);
				this.SenpaiNeckTarget = Mathf.Lerp(this.SenpaiNeckTarget, -15f, Time.deltaTime * 3.6f);
				this.SenpaiHeadTarget = Mathf.Lerp(this.SenpaiHeadTarget, -15f, Time.deltaTime * 3.6f);
				this.SenpaiEyeRTarget = Mathf.Lerp(this.SenpaiEyeRTarget, 85f, Time.deltaTime * 3.6f);
				this.SenpaiEyeLTarget = Mathf.Lerp(this.SenpaiEyeLTarget, 265f, Time.deltaTime * 3.6f);
				this.YandereNeckTarget = Mathf.Lerp(this.YandereNeckTarget, 7.5f, Time.deltaTime * 3.6f);
				this.YandereHeadTarget = Mathf.Lerp(this.YandereHeadTarget, 7.5f, Time.deltaTime * 3.6f);
			}
			else
			{
				this.RivalNeckTarget = Mathf.Lerp(this.RivalNeckTarget, 0f, Time.deltaTime * 3.6f);
				this.RivalHeadTarget = Mathf.Lerp(this.RivalHeadTarget, 0f, Time.deltaTime * 3.6f);
				this.RivalEyeRTarget = Mathf.Lerp(this.RivalEyeRTarget, 90f, Time.deltaTime * 3.6f);
				this.RivalEyeLTarget = Mathf.Lerp(this.RivalEyeLTarget, 270f, Time.deltaTime * 3.6f);
				this.SenpaiNeckTarget = Mathf.Lerp(this.SenpaiNeckTarget, 0f, Time.deltaTime * 3.6f);
				this.SenpaiHeadTarget = Mathf.Lerp(this.SenpaiHeadTarget, 0f, Time.deltaTime * 3.6f);
				this.SenpaiEyeRTarget = Mathf.Lerp(this.SenpaiEyeRTarget, 90f, Time.deltaTime * 3.6f);
				this.SenpaiEyeLTarget = Mathf.Lerp(this.SenpaiEyeLTarget, 270f, Time.deltaTime * 3.6f);
				this.YandereNeckTarget = Mathf.Lerp(this.YandereNeckTarget, 0f, Time.deltaTime * 3.6f);
				this.YandereHeadTarget = Mathf.Lerp(this.YandereHeadTarget, 0f, Time.deltaTime * 3.6f);
			}
			this.RivalNeck.localEulerAngles = new Vector3(this.RivalNeck.localEulerAngles.x, this.RivalNeckTarget, this.RivalNeck.localEulerAngles.z);
			this.RivalHead.localEulerAngles = new Vector3(this.RivalHead.localEulerAngles.x, this.RivalHeadTarget, this.RivalHead.localEulerAngles.z);
			this.RivalEyeR.localEulerAngles = new Vector3(this.RivalEyeR.localEulerAngles.x, this.RivalEyeRTarget, this.RivalEyeR.localEulerAngles.z);
			this.RivalEyeL.localEulerAngles = new Vector3(this.RivalEyeL.localEulerAngles.x, this.RivalEyeLTarget, this.RivalEyeL.localEulerAngles.z);
			this.SenpaiNeck.localEulerAngles = new Vector3(this.SenpaiNeck.localEulerAngles.x, this.SenpaiNeckTarget, this.SenpaiNeck.localEulerAngles.z);
			this.SenpaiHead.localEulerAngles = new Vector3(this.SenpaiHead.localEulerAngles.x, this.SenpaiHeadTarget, this.SenpaiHead.localEulerAngles.z);
			this.SenpaiEyeR.localEulerAngles = new Vector3(this.SenpaiEyeR.localEulerAngles.x, this.SenpaiEyeRTarget, this.SenpaiEyeR.localEulerAngles.z);
			this.SenpaiEyeL.localEulerAngles = new Vector3(this.SenpaiEyeL.localEulerAngles.x, this.SenpaiEyeLTarget, this.SenpaiEyeL.localEulerAngles.z);
			this.YandereNeck.localEulerAngles = new Vector3(this.YandereNeck.localEulerAngles.x, this.YandereNeckTarget, this.YandereNeck.localEulerAngles.z);
			this.YandereHead.localEulerAngles = new Vector3(this.YandereHead.localEulerAngles.x, this.YandereHeadTarget, this.YandereHead.localEulerAngles.z);
			if (this.MyAudio.isPlaying)
			{
				this.MouthTimer += Time.deltaTime;
				if (this.MouthTimer > this.TimerLimit)
				{
					this.MouthTarget = UnityEngine.Random.Range(40f, 40f + this.MouthExtent);
					this.MouthTimer = 0f;
				}
				if (this.Speakers[this.ID])
				{
					this.RivalJaw.localEulerAngles = new Vector3(this.RivalJaw.localEulerAngles.x, this.RivalJaw.localEulerAngles.y, Mathf.Lerp(this.RivalJaw.localEulerAngles.z, this.MouthTarget, Time.deltaTime * this.TalkSpeed));
					this.RivalLipL.localPosition = new Vector3(this.RivalLipL.localPosition.x, Mathf.Lerp(this.RivalLipL.localPosition.y, 0.02632812f + this.MouthTarget * this.LipStrength, Time.deltaTime * this.TalkSpeed), this.RivalLipL.localPosition.z);
					this.RivalLipR.localPosition = new Vector3(this.RivalLipR.localPosition.x, Mathf.Lerp(this.RivalLipR.localPosition.y, 0.02632812f + this.MouthTarget * this.LipStrength, Time.deltaTime * this.TalkSpeed), this.RivalLipR.localPosition.z);
					return;
				}
				this.SenpaiJaw.localEulerAngles = new Vector3(this.SenpaiJaw.localEulerAngles.x, this.SenpaiJaw.localEulerAngles.y, Mathf.Lerp(this.SenpaiJaw.localEulerAngles.z, this.MouthTarget, Time.deltaTime * this.TalkSpeed));
				this.SenpaiLipL.localPosition = new Vector3(this.SenpaiLipL.localPosition.x, Mathf.Lerp(this.SenpaiLipL.localPosition.y, 0.02632812f + this.MouthTarget * this.LipStrength, Time.deltaTime * this.TalkSpeed), this.SenpaiLipL.localPosition.z);
				this.SenpaiLipR.localPosition = new Vector3(this.SenpaiLipR.localPosition.x, Mathf.Lerp(this.SenpaiLipR.localPosition.y, 0.02632812f + this.MouthTarget * this.LipStrength, Time.deltaTime * this.TalkSpeed), this.SenpaiLipR.localPosition.z);
			}
		}
	}

	// Token: 0x06001FB4 RID: 8116 RVA: 0x001BEA7C File Offset: 0x001BCC7C
	public void UpdateNameLabel()
	{
		if (this.Speakers[this.ID])
		{
			this.NameLabel.text = "Osana-chan";
			return;
		}
		this.NameLabel.text = "Senpai-kun";
	}

	// Token: 0x06001FB5 RID: 8117 RVA: 0x001BEAAE File Offset: 0x001BCCAE
	public void End()
	{
		this.PromptBar.Show = false;
		this.ShowWindow = false;
		this.Ending = true;
		this.Timer = 0f;
	}

	// Token: 0x04004220 RID: 16928
	public PromptBarScript PromptBar;

	// Token: 0x04004221 RID: 16929
	public CosmeticScript Yandere;

	// Token: 0x04004222 RID: 16930
	public CosmeticScript Senpai;

	// Token: 0x04004223 RID: 16931
	public CosmeticScript Rival;

	// Token: 0x04004224 RID: 16932
	public UISprite Darkness;

	// Token: 0x04004225 RID: 16933
	public Transform[] Neighborhood;

	// Token: 0x04004226 RID: 16934
	public Transform Window;

	// Token: 0x04004227 RID: 16935
	public Transform RivalNeck;

	// Token: 0x04004228 RID: 16936
	public Transform RivalHead;

	// Token: 0x04004229 RID: 16937
	public Transform RivalEyeR;

	// Token: 0x0400422A RID: 16938
	public Transform RivalEyeL;

	// Token: 0x0400422B RID: 16939
	public Transform RivalJaw;

	// Token: 0x0400422C RID: 16940
	public Transform RivalLipL;

	// Token: 0x0400422D RID: 16941
	public Transform RivalLipR;

	// Token: 0x0400422E RID: 16942
	public Transform SenpaiNeck;

	// Token: 0x0400422F RID: 16943
	public Transform SenpaiHead;

	// Token: 0x04004230 RID: 16944
	public Transform SenpaiEyeR;

	// Token: 0x04004231 RID: 16945
	public Transform SenpaiEyeL;

	// Token: 0x04004232 RID: 16946
	public Transform SenpaiJaw;

	// Token: 0x04004233 RID: 16947
	public Transform SenpaiLipL;

	// Token: 0x04004234 RID: 16948
	public Transform SenpaiLipR;

	// Token: 0x04004235 RID: 16949
	public Transform YandereNeck;

	// Token: 0x04004236 RID: 16950
	public Transform YandereHead;

	// Token: 0x04004237 RID: 16951
	public Transform YandereEyeR;

	// Token: 0x04004238 RID: 16952
	public Transform YandereEyeL;

	// Token: 0x04004239 RID: 16953
	public AudioSource MyAudio;

	// Token: 0x0400423A RID: 16954
	public float ScrollSpeed = 1f;

	// Token: 0x0400423B RID: 16955
	public float LipStrength = 0.0001f;

	// Token: 0x0400423C RID: 16956
	public float TimerLimit = 0.1f;

	// Token: 0x0400423D RID: 16957
	public float TalkSpeed = 10f;

	// Token: 0x0400423E RID: 16958
	public float AutoTimer;

	// Token: 0x0400423F RID: 16959
	public float Timer;

	// Token: 0x04004240 RID: 16960
	public float MouthExtent = 5f;

	// Token: 0x04004241 RID: 16961
	public float MouthTarget;

	// Token: 0x04004242 RID: 16962
	public float MouthTimer;

	// Token: 0x04004243 RID: 16963
	public float RivalNeckTarget;

	// Token: 0x04004244 RID: 16964
	public float RivalHeadTarget;

	// Token: 0x04004245 RID: 16965
	public float RivalEyeRTarget;

	// Token: 0x04004246 RID: 16966
	public float RivalEyeLTarget;

	// Token: 0x04004247 RID: 16967
	public float SenpaiNeckTarget;

	// Token: 0x04004248 RID: 16968
	public float SenpaiHeadTarget;

	// Token: 0x04004249 RID: 16969
	public float SenpaiEyeRTarget;

	// Token: 0x0400424A RID: 16970
	public float SenpaiEyeLTarget;

	// Token: 0x0400424B RID: 16971
	public float YandereNeckTarget;

	// Token: 0x0400424C RID: 16972
	public float YandereHeadTarget;

	// Token: 0x0400424D RID: 16973
	public bool ShowWindow;

	// Token: 0x0400424E RID: 16974
	public bool Debugging;

	// Token: 0x0400424F RID: 16975
	public bool FadeOut;

	// Token: 0x04004250 RID: 16976
	public bool Ending;

	// Token: 0x04004251 RID: 16977
	public bool Auto;

	// Token: 0x04004252 RID: 16978
	public bool Talk;

	// Token: 0x04004253 RID: 16979
	public TypewriterEffect Typewriter;

	// Token: 0x04004254 RID: 16980
	public UILabel NameLabel;

	// Token: 0x04004255 RID: 16981
	public AudioClip[] Speech;

	// Token: 0x04004256 RID: 16982
	public string[] Lines;

	// Token: 0x04004257 RID: 16983
	public bool[] Speakers;

	// Token: 0x04004258 RID: 16984
	public int Frame;

	// Token: 0x04004259 RID: 16985
	public int ID;

	// Token: 0x0400425A RID: 16986
	public Renderer PonytailRenderer;

	// Token: 0x0400425B RID: 16987
	public Texture BlondePony;
}
