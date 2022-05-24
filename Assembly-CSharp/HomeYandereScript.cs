﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x0200032F RID: 815
public class HomeYandereScript : MonoBehaviour
{
	// Token: 0x060018DA RID: 6362 RVA: 0x000F4CEC File Offset: 0x000F2EEC
	public void Start()
	{
		this.VtuberCheck();
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		if (this.CutsceneYandere != null)
		{
			this.CutsceneYandere.GetComponent<Animation>()["f02_midoriTexting_00"].speed = 0.1f;
		}
		if (SceneManager.GetActiveScene().name == "HomeScene")
		{
			if (!YanvaniaGlobals.DraculaDefeated && !HomeGlobals.MiyukiDefeated)
			{
				base.transform.position = Vector3.zero;
				base.transform.eulerAngles = Vector3.zero;
				if (!GameGlobals.Eighties && DateGlobals.Weekday == DayOfWeek.Sunday)
				{
					this.Nude();
				}
				else if (!HomeGlobals.Night)
				{
					if (DateGlobals.Weekday == DayOfWeek.Sunday)
					{
						this.WearPajamas();
					}
					else
					{
						this.ChangeSchoolwear();
						base.StartCoroutine(this.ApplyCustomCostume());
					}
				}
				else
				{
					this.WearPajamas();
				}
			}
			else if (HomeGlobals.StartInBasement)
			{
				HomeGlobals.StartInBasement = false;
				base.transform.position = new Vector3(0f, -135f, 0f);
				base.transform.eulerAngles = Vector3.zero;
			}
			else if (HomeGlobals.MiyukiDefeated)
			{
				base.transform.position = new Vector3(1f, 0f, 0f);
				base.transform.eulerAngles = new Vector3(0f, 90f, 0f);
				this.CharacterAnimation.Play("f02_discScratch_00");
				this.Controller.transform.localPosition = new Vector3(0.09425f, 0.0095f, 0.01878f);
				this.Controller.transform.localEulerAngles = new Vector3(0f, 0f, -180f);
				this.HomeCamera.Destination = this.HomeCamera.Destinations[5];
				this.HomeCamera.Target = this.HomeCamera.Targets[5];
				this.Disc.SetActive(true);
				this.WearPajamas();
				this.MyAudio.clip = this.MiyukiReaction;
			}
			else
			{
				base.transform.position = new Vector3(1f, 0f, 0f);
				base.transform.eulerAngles = new Vector3(0f, 90f, 0f);
				this.CharacterAnimation.Play("f02_discScratch_00");
				this.Controller.transform.localPosition = new Vector3(0.09425f, 0.0095f, 0.01878f);
				this.Controller.transform.localEulerAngles = new Vector3(0f, 0f, -180f);
				this.HomeCamera.Destination = this.HomeCamera.Destinations[5];
				this.HomeCamera.Target = this.HomeCamera.Targets[5];
				this.Disc.SetActive(true);
				this.WearPajamas();
			}
			if (GameGlobals.BlondeHair)
			{
				this.PonytailRenderer.material.mainTexture = this.BlondePony;
				this.LongHairRenderer.material.mainTexture = this.BlondeLong;
			}
		}
		Time.timeScale = 1f;
		this.IdleAnim = "f02_idleShort_00";
		this.WalkAnim = "f02_newWalk_00";
		this.RunAnim = "f02_newSprint_00";
		if (GameGlobals.Eighties)
		{
			this.StudentManager.Eighties = true;
			this.RyobaHair.SetActive(true);
			this.Hairstyle = 0;
			this.UpdateHair();
			this.IdleAnim = "f02_ryobaIdle_00";
			this.WalkAnim = "f02_ryobaWalk_00";
			this.RunAnim = "f02_ryobaRun_00";
			if (DateGlobals.Weekday != DayOfWeek.Sunday)
			{
				if (!this.Pajamas.gameObject.activeInHierarchy && !this.Vtuber)
				{
					this.MyRenderer.SetBlendShapeWeight(0, 50f);
					this.MyRenderer.SetBlendShapeWeight(5, 25f);
					this.MyRenderer.SetBlendShapeWeight(9, 0f);
					this.MyRenderer.SetBlendShapeWeight(12, 100f);
					this.ChangeSchoolwear();
				}
				this.MyRenderer.materials[0].mainTexture = this.EightiesSocks;
			}
			this.BreastSize = 1.5f;
			this.BreastR.localScale = new Vector3(this.BreastSize, this.BreastSize, this.BreastSize);
			this.BreastL.localScale = new Vector3(this.BreastSize, this.BreastSize, this.BreastSize);
		}
		else
		{
			this.PonytailRenderer.transform.parent.gameObject.SetActive(true);
			this.RyobaHair.SetActive(false);
			if (HomeGlobals.Night)
			{
				this.Hairstyle = 2;
				this.UpdateHair();
			}
			else
			{
				this.Hairstyle = 1;
				this.UpdateHair();
			}
		}
		if (DateGlobals.Week != 1 || DateGlobals.Weekday != DayOfWeek.Monday)
		{
			this.CannotAlphabet = true;
		}
		PlayerGlobals.BringingItem = 0;
	}

	// Token: 0x060018DB RID: 6363 RVA: 0x000F51D8 File Offset: 0x000F33D8
	private void Update()
	{
		if (this.UpdateFace)
		{
			if (this.Pajamas.newRenderer != null)
			{
				if (!this.Vtuber)
				{
					this.Pajamas.newRenderer.SetBlendShapeWeight(0, 50f);
					this.Pajamas.newRenderer.SetBlendShapeWeight(5, 25f);
					this.Pajamas.newRenderer.SetBlendShapeWeight(9, 0f);
					this.Pajamas.newRenderer.SetBlendShapeWeight(12, 100f);
				}
				else
				{
					for (int i = 0; i < 13; i++)
					{
						this.Pajamas.newRenderer.SetBlendShapeWeight(i, 0f);
					}
					this.Pajamas.newRenderer.SetBlendShapeWeight(0, 100f);
					this.Pajamas.newRenderer.SetBlendShapeWeight(9, 100f);
					this.Pajamas.newRenderer.materials[1].mainTexture = this.FaceTexture;
					Debug.Log("Updating pajama mesh with Vtuber face.");
				}
			}
			this.UpdateFace = false;
		}
		if (!this.Disc.activeInHierarchy)
		{
			if (this.CanMove)
			{
				if (!OptionGlobals.ToggleRun)
				{
					this.Running = false;
					if (Input.GetButton("LB"))
					{
						this.Running = true;
					}
				}
				else if (Input.GetButtonDown("LB"))
				{
					this.Running = !this.Running;
				}
				this.MyController.Move(Physics.gravity * 0.01f);
				float axis = Input.GetAxis("Vertical");
				float axis2 = Input.GetAxis("Horizontal");
				Vector3 vector = Camera.main.transform.TransformDirection(Vector3.forward);
				vector.y = 0f;
				vector = vector.normalized;
				Vector3 a = new Vector3(vector.z, 0f, -vector.x);
				Vector3 vector2 = axis2 * a + axis * vector;
				if (vector2 != Vector3.zero)
				{
					Quaternion b = Quaternion.LookRotation(vector2);
					base.transform.rotation = Quaternion.Lerp(base.transform.rotation, b, Time.deltaTime * 10f);
				}
				if (axis != 0f || axis2 != 0f)
				{
					if (this.Running)
					{
						this.CharacterAnimation.CrossFade(this.RunAnim);
						this.MyController.Move(base.transform.forward * this.RunSpeed * Time.deltaTime);
					}
					else
					{
						this.CharacterAnimation.CrossFade(this.WalkAnim);
						this.MyController.Move(base.transform.forward * this.WalkSpeed * Time.deltaTime);
					}
				}
				else
				{
					this.CharacterAnimation.CrossFade(this.IdleAnim);
				}
			}
			else
			{
				this.CharacterAnimation.CrossFade(this.IdleAnim);
			}
		}
		else if (this.HomeDarkness.color.a == 0f)
		{
			if (this.Timer == 0f)
			{
				this.MyAudio.Play();
			}
			else if (this.Timer > this.MyAudio.clip.length + 1f)
			{
				YanvaniaGlobals.DraculaDefeated = false;
				HomeGlobals.MiyukiDefeated = false;
				this.Disc.SetActive(false);
				this.HomeVideoGames.Quit();
			}
			this.Timer += Time.deltaTime;
		}
		Rigidbody component = base.GetComponent<Rigidbody>();
		if (component != null)
		{
			component.velocity = Vector3.zero;
		}
		if (base.transform.position.y < -10f)
		{
			base.transform.position = new Vector3(base.transform.position.x, -10f, base.transform.position.z);
		}
	}

	// Token: 0x060018DC RID: 6364 RVA: 0x000F55C4 File Offset: 0x000F37C4
	private void LateUpdate()
	{
		if (!this.CannotAlphabet && Input.GetKeyDown(this.Letter[this.AlphabetID]))
		{
			this.AlphabetID++;
			if (this.AlphabetID == this.Letter.Length)
			{
				GameGlobals.AlphabetMode = true;
				StudentGlobals.MemorialStudents = 0;
				for (int i = 1; i < 101; i++)
				{
					StudentGlobals.SetStudentDead(i, false);
					StudentGlobals.SetStudentKidnapped(i, false);
					StudentGlobals.SetStudentArrested(i, false);
					StudentGlobals.SetStudentExpelled(i, false);
				}
				SceneManager.LoadScene("LoadingScene");
			}
		}
	}

	// Token: 0x060018DD RID: 6365 RVA: 0x000F564C File Offset: 0x000F384C
	private void UpdateHair()
	{
		if (this.Hairstyle == 0)
		{
			this.LongHairRenderer.gameObject.SetActive(false);
			this.PonytailRenderer.enabled = false;
			return;
		}
		if (this.Hairstyle == 1)
		{
			this.LongHairRenderer.gameObject.SetActive(false);
			this.PonytailRenderer.enabled = true;
			return;
		}
		if (this.Hairstyle == 2)
		{
			this.LongHairRenderer.gameObject.SetActive(true);
			this.PonytailRenderer.enabled = false;
		}
	}

	// Token: 0x060018DE RID: 6366 RVA: 0x000F56CC File Offset: 0x000F38CC
	private void ChangeSchoolwear()
	{
		this.MyRenderer.sharedMesh = this.Uniforms[StudentGlobals.FemaleUniform];
		this.MyRenderer.materials[0].mainTexture = this.UniformTextures[StudentGlobals.FemaleUniform];
		this.MyRenderer.materials[1].mainTexture = this.UniformTextures[StudentGlobals.FemaleUniform];
		this.MyRenderer.materials[2].mainTexture = this.FaceTexture;
		base.StartCoroutine(this.ApplyCustomCostume());
	}

	// Token: 0x060018DF RID: 6367 RVA: 0x000F5754 File Offset: 0x000F3954
	private void WearPajamas()
	{
		this.Pajamas.gameObject.SetActive(true);
		this.MyRenderer.sharedMesh = null;
		this.MyRenderer.materials[0].mainTexture = this.PajamaTexture;
		this.MyRenderer.materials[1].mainTexture = this.PajamaTexture;
		this.MyRenderer.materials[2].mainTexture = this.FaceTexture;
		base.StartCoroutine(this.ApplyCustomFace());
		if (GameGlobals.Eighties)
		{
			this.UpdateFace = true;
		}
	}

	// Token: 0x060018E0 RID: 6368 RVA: 0x000F57E4 File Offset: 0x000F39E4
	private void Nude()
	{
		this.MyRenderer.sharedMesh = this.NudeMesh;
		this.MyRenderer.materials[0].mainTexture = this.NudeTexture;
		this.MyRenderer.materials[1].mainTexture = this.NudeTexture;
		this.MyRenderer.materials[2].mainTexture = this.NudeTexture;
	}

	// Token: 0x060018E1 RID: 6369 RVA: 0x000F584A File Offset: 0x000F3A4A
	private IEnumerator ApplyCustomCostume()
	{
		if (StudentGlobals.FemaleUniform == 1)
		{
			WWW CustomUniform = new WWW("file:///" + Application.streamingAssetsPath + "/CustomUniform.png");
			yield return CustomUniform;
			if (CustomUniform.error == null)
			{
				this.MyRenderer.materials[0].mainTexture = CustomUniform.texture;
				this.MyRenderer.materials[1].mainTexture = CustomUniform.texture;
			}
			CustomUniform = null;
		}
		else if (StudentGlobals.FemaleUniform == 2)
		{
			WWW CustomUniform = new WWW("file:///" + Application.streamingAssetsPath + "/CustomLong.png");
			yield return CustomUniform;
			if (CustomUniform.error == null)
			{
				this.MyRenderer.materials[0].mainTexture = CustomUniform.texture;
				this.MyRenderer.materials[1].mainTexture = CustomUniform.texture;
			}
			CustomUniform = null;
		}
		else if (StudentGlobals.FemaleUniform == 3)
		{
			WWW CustomUniform = new WWW("file:///" + Application.streamingAssetsPath + "/CustomSweater.png");
			yield return CustomUniform;
			if (CustomUniform.error == null)
			{
				this.MyRenderer.materials[0].mainTexture = CustomUniform.texture;
				this.MyRenderer.materials[1].mainTexture = CustomUniform.texture;
			}
			CustomUniform = null;
		}
		else if (StudentGlobals.FemaleUniform == 4 || StudentGlobals.FemaleUniform == 5)
		{
			WWW CustomUniform = new WWW("file:///" + Application.streamingAssetsPath + "/CustomBlazer.png");
			yield return CustomUniform;
			if (CustomUniform.error == null)
			{
				this.MyRenderer.materials[0].mainTexture = CustomUniform.texture;
				this.MyRenderer.materials[1].mainTexture = CustomUniform.texture;
			}
			CustomUniform = null;
		}
		base.StartCoroutine(this.ApplyCustomFace());
		yield break;
	}

	// Token: 0x060018E2 RID: 6370 RVA: 0x000F5859 File Offset: 0x000F3A59
	private IEnumerator ApplyCustomFace()
	{
		WWW CustomFace = new WWW("file:///" + Application.streamingAssetsPath + "/CustomFace.png");
		yield return CustomFace;
		if (CustomFace.error == null)
		{
			this.MyRenderer.materials[2].mainTexture = CustomFace.texture;
			this.FaceTexture = CustomFace.texture;
		}
		WWW CustomHair = new WWW("file:///" + Application.streamingAssetsPath + "/CustomHair.png");
		yield return CustomHair;
		if (CustomHair.error == null)
		{
			this.PonytailRenderer.material.mainTexture = CustomHair.texture;
		}
		yield break;
	}

	// Token: 0x060018E3 RID: 6371 RVA: 0x000F5868 File Offset: 0x000F3A68
	private void VtuberCheck()
	{
		if (GameGlobals.VtuberID > 0)
		{
			for (int i = 1; i < this.OriginalHairs.Length; i++)
			{
				this.OriginalHairs[i].transform.localPosition = new Vector3(0f, 1f, 0f);
			}
			this.VtuberHairs[GameGlobals.VtuberID].SetActive(true);
			for (int i = 0; i < 13; i++)
			{
				this.MyRenderer.SetBlendShapeWeight(i, 0f);
			}
			this.MyRenderer.SetBlendShapeWeight(0, 100f);
			this.MyRenderer.SetBlendShapeWeight(9, 100f);
			this.FaceTexture = this.VtuberFaces[GameGlobals.VtuberID];
			Debug.Log("FaceTexture changed to Vtuber's face texture.");
			this.Eyes[1].material.mainTexture = this.VtuberFaces[GameGlobals.VtuberID];
			this.Eyes[2].material.mainTexture = this.VtuberFaces[GameGlobals.VtuberID];
			this.UpdateFace = true;
			this.Vtuber = true;
			return;
		}
		this.VtuberHairs[1].SetActive(false);
	}

	// Token: 0x040025F5 RID: 9717
	public CharacterController MyController;

	// Token: 0x040025F6 RID: 9718
	public StudentManagerScript StudentManager;

	// Token: 0x040025F7 RID: 9719
	public HomeVideoGamesScript HomeVideoGames;

	// Token: 0x040025F8 RID: 9720
	public HomeCameraScript HomeCamera;

	// Token: 0x040025F9 RID: 9721
	public UISprite HomeDarkness;

	// Token: 0x040025FA RID: 9722
	public Animation CharacterAnimation;

	// Token: 0x040025FB RID: 9723
	public GameObject CutsceneYandere;

	// Token: 0x040025FC RID: 9724
	public GameObject Controller;

	// Token: 0x040025FD RID: 9725
	public GameObject Character;

	// Token: 0x040025FE RID: 9726
	public GameObject RyobaHair;

	// Token: 0x040025FF RID: 9727
	public GameObject Disc;

	// Token: 0x04002600 RID: 9728
	public Renderer LongHairRenderer;

	// Token: 0x04002601 RID: 9729
	public Renderer PonytailRenderer;

	// Token: 0x04002602 RID: 9730
	public AudioClip MiyukiReaction;

	// Token: 0x04002603 RID: 9731
	public AudioClip DiscScratch;

	// Token: 0x04002604 RID: 9732
	public AudioSource MyAudio;

	// Token: 0x04002605 RID: 9733
	public Texture EightiesSocks;

	// Token: 0x04002606 RID: 9734
	public Texture BlondePony;

	// Token: 0x04002607 RID: 9735
	public Texture BlondeLong;

	// Token: 0x04002608 RID: 9736
	public float WalkSpeed;

	// Token: 0x04002609 RID: 9737
	public float RunSpeed;

	// Token: 0x0400260A RID: 9738
	public bool CannotAlphabet;

	// Token: 0x0400260B RID: 9739
	public bool UpdateFace;

	// Token: 0x0400260C RID: 9740
	public bool CanMove;

	// Token: 0x0400260D RID: 9741
	public bool Running;

	// Token: 0x0400260E RID: 9742
	public bool HidePony;

	// Token: 0x0400260F RID: 9743
	public string IdleAnim = "";

	// Token: 0x04002610 RID: 9744
	public string WalkAnim = "";

	// Token: 0x04002611 RID: 9745
	public string RunAnim = "";

	// Token: 0x04002612 RID: 9746
	public int Hairstyle;

	// Token: 0x04002613 RID: 9747
	public int VictimID;

	// Token: 0x04002614 RID: 9748
	public float Timer;

	// Token: 0x04002615 RID: 9749
	public float BreastSize = 1f;

	// Token: 0x04002616 RID: 9750
	public Transform BreastR;

	// Token: 0x04002617 RID: 9751
	public Transform BreastL;

	// Token: 0x04002618 RID: 9752
	public int AlphabetID;

	// Token: 0x04002619 RID: 9753
	public string[] Letter;

	// Token: 0x0400261A RID: 9754
	public SkinnedMeshRenderer MyRenderer;

	// Token: 0x0400261B RID: 9755
	public Texture[] UniformTextures;

	// Token: 0x0400261C RID: 9756
	public Texture FaceTexture;

	// Token: 0x0400261D RID: 9757
	public Mesh[] Uniforms;

	// Token: 0x0400261E RID: 9758
	public RiggedAccessoryAttacher Pajamas;

	// Token: 0x0400261F RID: 9759
	public Texture PajamaTexture;

	// Token: 0x04002620 RID: 9760
	public Mesh PajamaMesh;

	// Token: 0x04002621 RID: 9761
	public Texture NudeTexture;

	// Token: 0x04002622 RID: 9762
	public Mesh NudeMesh;

	// Token: 0x04002623 RID: 9763
	public GameObject[] OriginalHairs;

	// Token: 0x04002624 RID: 9764
	public GameObject[] VtuberHairs;

	// Token: 0x04002625 RID: 9765
	public Texture[] VtuberFaces;

	// Token: 0x04002626 RID: 9766
	public Renderer[] Eyes;

	// Token: 0x04002627 RID: 9767
	public bool Vtuber;
}
