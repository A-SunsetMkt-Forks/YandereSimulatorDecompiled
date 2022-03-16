﻿using System;
using Bayat.SaveSystem;
using HighlightingSystem;
using UnityEngine;
using UnityEngine.PostProcessing;

// Token: 0x02000442 RID: 1090
public class StalkerYandereScript : MonoBehaviour
{
	// Token: 0x06001D0F RID: 7439 RVA: 0x0015B1F0 File Offset: 0x001593F0
	public void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		if (this.BlondePony != null && GameGlobals.BlondeHair)
		{
			this.PonytailRenderer.material.mainTexture = this.BlondePony;
		}
		if (GameGlobals.Eighties)
		{
			this.Eighties = true;
		}
		if (GameGlobals.Eighties && this.EightiesAttacher != null)
		{
			this.EightiesAttacher.SetActive(true);
			this.MyRenderer.sharedMesh = this.HeadOnlyMesh;
			this.PonytailRenderer.gameObject.SetActive(false);
			this.RyobaHair.SetActive(true);
			this.MyRenderer.SetBlendShapeWeight(0, 50f);
			this.MyRenderer.SetBlendShapeWeight(5, 25f);
			this.MyRenderer.SetBlendShapeWeight(9, 0f);
			this.MyRenderer.SetBlendShapeWeight(12, 100f);
			this.IdleAnim = "f02_ryobaIdle_00";
			this.WalkAnim = "f02_ryobaWalk_00";
			this.RunAnim = "f02_ryobaRun_00";
			this.MyRenderer.materials[0].mainTexture = this.MyRenderer.materials[2].mainTexture;
			this.Eighties = true;
		}
		else
		{
			if (!this.Asylum)
			{
				this.BreastL.transform.localScale = new Vector3(1f, 1f, 1f);
				this.BreastR.transform.localScale = new Vector3(1f, 1f, 1f);
			}
			if (this.ClothingAttacher != null && !this.Initialized)
			{
				Debug.Log("Regular Renderer disabled, ClothingAttacher activated.");
				this.ClothingAttacher.SetActive(true);
				this.MyRenderer.gameObject.SetActive(false);
				this.Initialized = true;
			}
			this.UpdatePebbles();
		}
		this.VtuberCheck();
	}

	// Token: 0x06001D10 RID: 7440 RVA: 0x0015B3D0 File Offset: 0x001595D0
	private void Update()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		if (this.UpdateTextures)
		{
			Debug.Log("Attempting to update textures...");
			if (this.ClothingAttacher != null && this.ClothingAttacher.GetComponent<RiggedAccessoryAttacher>().newRenderer != null)
			{
				Debug.Log("ClothingAttacher was not null.");
				this.MyRenderer = this.ClothingAttacher.GetComponent<RiggedAccessoryAttacher>().newRenderer;
				this.MyRenderer.materials[1].mainTexture = this.VtuberFaces[GameGlobals.VtuberID];
			}
			else
			{
				Debug.Log("ClothingAttacher was null.");
				for (int i = 0; i < 13; i++)
				{
					this.MyRenderer.SetBlendShapeWeight(i, 0f);
				}
				this.MyRenderer.SetBlendShapeWeight(0, 100f);
				this.MyRenderer.SetBlendShapeWeight(9, 100f);
				if (this.Eighties && this.Street)
				{
					this.MyRenderer.materials[0].mainTexture = this.VtuberFaces[GameGlobals.VtuberID];
					this.MyRenderer.materials[1].mainTexture = this.VtuberFaces[GameGlobals.VtuberID];
					this.MyRenderer.materials[2].mainTexture = this.VtuberFaces[GameGlobals.VtuberID];
				}
			}
			this.UpdateTextures = false;
		}
		if (Input.GetKeyDown("m") && this.Jukebox != null)
		{
			if (this.Jukebox.isPlaying)
			{
				this.Jukebox.Stop();
			}
			else
			{
				this.Jukebox.Play();
			}
		}
		if (this.CanMove)
		{
			if (this.CameraTarget != null)
			{
				this.CameraTarget.localPosition = new Vector3(0f, 1f + (this.RPGCamera.distanceMax - this.RPGCamera.distance) * 0.2f, 0f);
			}
			if (this.InDesert && base.transform.position.y < 13.7f)
			{
				UnityEngine.Object.Instantiate<GameObject>(this.GroundImpact, base.transform.position + new Vector3(0f, 0.1f, 0f), Quaternion.identity);
				this.InDesert = false;
			}
			this.UpdateMovement();
			if (this.YandereFilter != null)
			{
				this.UpdateYandereVision();
			}
			if (this.Pebbles > 0)
			{
				if (!this.Arc.gameObject.activeInHierarchy)
				{
					this.Arc.Timer = 1f;
				}
				this.Arc.gameObject.SetActive(Input.GetButton("X"));
				if (this.Arc.gameObject.activeInHierarchy)
				{
					this.ThrowButton.SetActive(true);
					this.AimButton.SetActive(false);
					if (Input.GetButtonDown("A"))
					{
						this.MyAudio.Play();
						Rigidbody component = UnityEngine.Object.Instantiate<GameObject>(this.Pebble, this.Arc.transform.position, base.transform.rotation).GetComponent<Rigidbody>();
						component.isKinematic = false;
						component.useGravity = true;
						component.AddRelativeForce(Vector3.up * 250f);
						component.AddRelativeForce(Vector3.forward * 250f);
						this.Pebbles--;
						this.UpdatePebbles();
						if (this.Pebbles < 1)
						{
							this.Arc.gameObject.SetActive(false);
						}
					}
				}
				else
				{
					this.ThrowButton.SetActive(false);
					this.AimButton.SetActive(true);
				}
			}
		}
		else if (this.CameraTarget != null)
		{
			if (this.Climbing)
			{
				if (this.ClimbPhase == 1)
				{
					if (this.MyAnimation["f02_climbTrellis_00"].time < this.MyAnimation["f02_climbTrellis_00"].length - 1f)
					{
						this.CameraTarget.position = Vector3.MoveTowards(this.CameraTarget.position, this.Hips.position + new Vector3(0f, 0.103729f, 0.003539f), Time.deltaTime);
					}
					else
					{
						this.CameraTarget.position = Vector3.MoveTowards(this.CameraTarget.position, new Vector3(-9.5f, 5f, -2.5f), Time.deltaTime);
					}
					this.MoveTowardsTarget(this.TrellisClimbSpot.position);
					this.SpinTowardsTarget(this.TrellisClimbSpot.rotation);
					if (this.MyAnimation["f02_climbTrellis_00"].time > 7.5f)
					{
						this.RPGCamera.transform.position = this.EntryPOV.position;
						this.RPGCamera.transform.eulerAngles = this.EntryPOV.eulerAngles;
						this.RPGCamera.enabled = false;
						RenderSettings.ambientIntensity = 8f;
						this.ClimbPhase++;
					}
				}
				else
				{
					this.RPGCamera.transform.position = this.EntryPOV.position;
					this.RPGCamera.transform.eulerAngles = this.EntryPOV.eulerAngles;
					if (this.MyAnimation["f02_climbTrellis_00"].time > 11f)
					{
						base.transform.position = Vector3.MoveTowards(base.transform.position, this.TrellisClimbSpot.position + new Vector3(0.4f, 0f, 0f), Time.deltaTime * 0.5f);
					}
				}
				if (this.MyAnimation["f02_climbTrellis_00"].time > this.MyAnimation["f02_climbTrellis_00"].length)
				{
					this.MyAnimation.Play("f02_idleShort_00");
					base.transform.position = new Vector3(-9.1f, 4f, -2.5f);
					this.CameraTarget.position = base.transform.position + new Vector3(0f, 1f, 0f);
					this.RPGCamera.enabled = true;
					this.Climbing = false;
					this.CanMove = true;
					Physics.SyncTransforms();
				}
			}
			else if (this.Chased)
			{
				Quaternion b = Quaternion.LookRotation(this.Stalker.transform.position - base.transform.position);
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, b, 10f * Time.deltaTime);
			}
		}
		if (this.Street && base.transform.position.x < -16f)
		{
			base.transform.position = new Vector3(-16f, 0f, base.transform.position.z);
		}
		if (this.Profile != null)
		{
			if (this.Stance.Current == StanceType.Crouching && this.Hidden)
			{
				if (this.Intensity != 1f)
				{
					this.Intensity = Mathf.MoveTowards(this.Intensity, 1f, Time.deltaTime);
					this.UpdateVignette();
					return;
				}
			}
			else if (this.Intensity != 0.45f)
			{
				this.Intensity = Mathf.MoveTowards(this.Intensity, 0.45f, Time.deltaTime);
				this.UpdateVignette();
			}
		}
	}

	// Token: 0x06001D11 RID: 7441 RVA: 0x0015BB50 File Offset: 0x00159D50
	private void UpdateMovement()
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
		this.MyController.Move(Physics.gravity * Time.deltaTime);
		float axis = Input.GetAxis("Vertical");
		float axis2 = Input.GetAxis("Horizontal");
		Vector3 vector = this.MainCamera.transform.TransformDirection(Vector3.forward);
		vector.y = 0f;
		vector = vector.normalized;
		Vector3 a = new Vector3(vector.z, 0f, -vector.x);
		Vector3 vector2 = axis2 * a + axis * vector;
		Quaternion b = Quaternion.identity;
		if (vector2 != Vector3.zero)
		{
			b = Quaternion.LookRotation(vector2);
		}
		if (vector2 != Vector3.zero)
		{
			base.transform.rotation = Quaternion.Lerp(base.transform.rotation, b, Time.deltaTime * 10f);
		}
		else
		{
			b = new Quaternion(0f, 0f, 0f, 0f);
		}
		if (!this.Street)
		{
			if (this.Stance.Current == StanceType.Standing)
			{
				if (Input.GetButtonDown("RS"))
				{
					this.Stance.Current = StanceType.Crouching;
					this.MyController.center = new Vector3(0f, 0.5f, 0f);
					this.MyController.height = 1f;
				}
			}
			else if (Input.GetButtonDown("RS"))
			{
				this.Stance.Current = StanceType.Standing;
				this.MyController.center = new Vector3(0f, 0.75f, 0f);
				this.MyController.height = 1.5f;
			}
		}
		if (axis != 0f || axis2 != 0f)
		{
			if (this.Running)
			{
				if (this.Stance.Current == StanceType.Crouching)
				{
					this.MyAnimation.CrossFade(this.CrouchRunAnim);
					this.MyController.Move(base.transform.forward * this.CrouchRunSpeed * Time.deltaTime);
					return;
				}
				this.MyAnimation.CrossFade(this.RunAnim);
				this.MyController.Move(base.transform.forward * this.RunSpeed * Time.deltaTime);
				return;
			}
			else
			{
				if (this.Stance.Current == StanceType.Crouching)
				{
					this.MyAnimation.CrossFade(this.CrouchWalkAnim);
					this.MyController.Move(base.transform.forward * (this.CrouchWalkSpeed * Time.deltaTime));
					return;
				}
				this.MyAnimation.CrossFade(this.WalkAnim);
				this.MyController.Move(base.transform.forward * (this.WalkSpeed * Time.deltaTime));
				return;
			}
		}
		else
		{
			if (this.Stance.Current == StanceType.Crouching)
			{
				this.MyAnimation.CrossFade(this.CrouchIdleAnim);
				return;
			}
			this.MyAnimation.CrossFade(this.IdleAnim);
			return;
		}
	}

	// Token: 0x06001D12 RID: 7442 RVA: 0x0015BE9C File Offset: 0x0015A09C
	private void LateUpdate()
	{
		if (this.Object != null)
		{
			if (this.RightArm != null)
			{
				this.RightArm.localEulerAngles = new Vector3(this.RightArm.localEulerAngles.x, this.RightArm.localEulerAngles.y + 15f, this.RightArm.localEulerAngles.z);
			}
			this.Object.LookAt(this.ObjectTarget);
		}
	}

	// Token: 0x06001D13 RID: 7443 RVA: 0x0015BF1C File Offset: 0x0015A11C
	private void MoveTowardsTarget(Vector3 target)
	{
		Vector3 a = target - base.transform.position;
		this.MyController.Move(a * (Time.deltaTime * 10f));
	}

	// Token: 0x06001D14 RID: 7444 RVA: 0x0015BF58 File Offset: 0x0015A158
	private void SpinTowardsTarget(Quaternion target)
	{
		base.transform.rotation = Quaternion.Slerp(base.transform.rotation, target, Time.deltaTime * 10f);
	}

	// Token: 0x06001D15 RID: 7445 RVA: 0x0015BF84 File Offset: 0x0015A184
	public void UpdateVignette()
	{
		VignetteModel.Settings settings = this.Profile.vignette.settings;
		settings.color = new Color(0f, 0f, 0f, 1f);
		settings.intensity = this.Intensity;
		settings.smoothness = 0.2f;
		settings.roundness = 1f;
		this.Profile.vignette.settings = settings;
	}

	// Token: 0x06001D16 RID: 7446 RVA: 0x0015BFF8 File Offset: 0x0015A1F8
	public void BeginStruggle()
	{
		this.MyAnimation.CrossFade("f02_struggleA_00");
		this.Struggling = true;
		this.Object.gameObject.GetComponent<Rigidbody>().isKinematic = false;
		this.Object.gameObject.GetComponent<Rigidbody>().useGravity = true;
		this.Object.gameObject.GetComponent<Collider>().isTrigger = false;
		this.Object.parent = null;
		this.Object = null;
	}

	// Token: 0x06001D17 RID: 7447 RVA: 0x0015C074 File Offset: 0x0015A274
	public void UpdateYandereVision()
	{
		if (Input.GetButton("RB"))
		{
			if (this.YandereFilter.FadeFX < 1f)
			{
				if (!this.HighlightingR.enabled)
				{
					this.YandereFilter.enabled = true;
					this.HighlightingR.enabled = true;
					this.HighlightingB.enabled = true;
				}
				Time.timeScale = Mathf.Lerp(Time.timeScale, 0.5f, Time.unscaledDeltaTime * 10f);
				this.YandereFilter.FadeFX = Mathf.Lerp(this.YandereFilter.FadeFX, 1f, Time.unscaledDeltaTime * 10f);
				if (this.YandereFilter.FadeFX == 1f)
				{
					Time.timeScale = 0.5f;
					return;
				}
			}
		}
		else if (this.YandereFilter.FadeFX > 0f)
		{
			if (this.HighlightingR.enabled)
			{
				this.HighlightingR.enabled = false;
				this.HighlightingB.enabled = false;
			}
			Time.timeScale = Mathf.Lerp(Time.timeScale, 1f, Time.unscaledDeltaTime * 10f);
			this.YandereFilter.FadeFX = Mathf.Lerp(this.YandereFilter.FadeFX, 0f, Time.unscaledDeltaTime * 10f);
			if (this.YandereFilter.FadeFX == 0f)
			{
				Time.timeScale = 1f;
			}
		}
	}

	// Token: 0x06001D18 RID: 7448 RVA: 0x0015C1E0 File Offset: 0x0015A3E0
	private void ResetYandereEffects()
	{
		this.HighlightingR.enabled = false;
		this.HighlightingB.enabled = false;
		this.YandereFilter.enabled = true;
		this.YandereFilter.FadeFX = 0f;
		Time.timeScale = 1f;
		this.Intensity = 0f;
		this.UpdateVignette();
	}

	// Token: 0x06001D19 RID: 7449 RVA: 0x0015C23C File Offset: 0x0015A43C
	public void UpdatePebbles()
	{
		if (this.PebbleIcon != null)
		{
			if (this.Pebbles == 0)
			{
				this.PebbleIcon.SetActive(false);
				return;
			}
			this.PebbleIcon.SetActive(true);
			this.PebbleLabel.text = "PEBBLES: " + this.Pebbles.ToString();
		}
	}

	// Token: 0x06001D1A RID: 7450 RVA: 0x0015C298 File Offset: 0x0015A498
	public void VtuberCheck()
	{
		if (GameGlobals.VtuberID > 0)
		{
			for (int i = 1; i < this.OriginalHairs.Length; i++)
			{
				this.OriginalHairs[i].transform.localPosition = new Vector3(0f, 100f, 0f);
			}
			this.VtuberHairs[GameGlobals.VtuberID].SetActive(true);
			if (this.ClothingAttacher != null && this.ClothingAttacher.GetComponent<RiggedAccessoryAttacher>().newRenderer != null)
			{
				this.MyRenderer = this.ClothingAttacher.GetComponent<RiggedAccessoryAttacher>().newRenderer;
				this.MyRenderer.materials[1].mainTexture = this.VtuberFaces[GameGlobals.VtuberID];
			}
			else
			{
				this.MyRenderer.materials[2].mainTexture = this.VtuberFaces[GameGlobals.VtuberID];
				for (int i = 0; i < 13; i++)
				{
					this.MyRenderer.SetBlendShapeWeight(i, 0f);
				}
				this.MyRenderer.SetBlendShapeWeight(0, 100f);
				this.MyRenderer.SetBlendShapeWeight(9, 100f);
			}
			this.UpdateTextures = true;
			this.Vtuber = true;
			return;
		}
		this.VtuberHairs[1].SetActive(false);
	}

	// Token: 0x040034D2 RID: 13522
	public CharacterController MyController;

	// Token: 0x040034D3 RID: 13523
	public PostProcessingProfile Profile;

	// Token: 0x040034D4 RID: 13524
	public AutoSaveManager SaveManager;

	// Token: 0x040034D5 RID: 13525
	public StalkerScript Stalker;

	// Token: 0x040034D6 RID: 13526
	public ArcScript Arc;

	// Token: 0x040034D7 RID: 13527
	public Transform TrellisClimbSpot;

	// Token: 0x040034D8 RID: 13528
	public Transform CameraTarget;

	// Token: 0x040034D9 RID: 13529
	public Transform ObjectTarget;

	// Token: 0x040034DA RID: 13530
	public Transform RightHand;

	// Token: 0x040034DB RID: 13531
	public Transform EntryPOV;

	// Token: 0x040034DC RID: 13532
	public Transform RightArm;

	// Token: 0x040034DD RID: 13533
	public Transform Object;

	// Token: 0x040034DE RID: 13534
	public Transform Hips;

	// Token: 0x040034DF RID: 13535
	public Renderer PonytailRenderer;

	// Token: 0x040034E0 RID: 13536
	public GameObject GroundImpact;

	// Token: 0x040034E1 RID: 13537
	public Animation MyAnimation;

	// Token: 0x040034E2 RID: 13538
	public RPG_Camera RPGCamera;

	// Token: 0x040034E3 RID: 13539
	public AudioSource Jukebox;

	// Token: 0x040034E4 RID: 13540
	public Camera MainCamera;

	// Token: 0x040034E5 RID: 13541
	public bool Struggling;

	// Token: 0x040034E6 RID: 13542
	public bool Climbing;

	// Token: 0x040034E7 RID: 13543
	public bool Running;

	// Token: 0x040034E8 RID: 13544
	public bool Invisible;

	// Token: 0x040034E9 RID: 13545
	public bool Eighties;

	// Token: 0x040034EA RID: 13546
	public bool InDesert;

	// Token: 0x040034EB RID: 13547
	public bool CanMove;

	// Token: 0x040034EC RID: 13548
	public bool Chased;

	// Token: 0x040034ED RID: 13549
	public bool Hidden;

	// Token: 0x040034EE RID: 13550
	public bool Street;

	// Token: 0x040034EF RID: 13551
	public Stance Stance = new Stance(StanceType.Standing);

	// Token: 0x040034F0 RID: 13552
	public string IdleAnim;

	// Token: 0x040034F1 RID: 13553
	public string WalkAnim;

	// Token: 0x040034F2 RID: 13554
	public string RunAnim;

	// Token: 0x040034F3 RID: 13555
	public string CrouchIdleAnim;

	// Token: 0x040034F4 RID: 13556
	public string CrouchWalkAnim;

	// Token: 0x040034F5 RID: 13557
	public string CrouchRunAnim;

	// Token: 0x040034F6 RID: 13558
	public float WalkSpeed;

	// Token: 0x040034F7 RID: 13559
	public float RunSpeed;

	// Token: 0x040034F8 RID: 13560
	public float CrouchWalkSpeed;

	// Token: 0x040034F9 RID: 13561
	public float CrouchRunSpeed;

	// Token: 0x040034FA RID: 13562
	public float Intensity = 0.45f;

	// Token: 0x040034FB RID: 13563
	public int ClimbPhase;

	// Token: 0x040034FC RID: 13564
	public int Pebbles;

	// Token: 0x040034FD RID: 13565
	public int Frame;

	// Token: 0x040034FE RID: 13566
	public SkinnedMeshRenderer MyRenderer;

	// Token: 0x040034FF RID: 13567
	public GameObject ClothingAttacher;

	// Token: 0x04003500 RID: 13568
	public GameObject EightiesAttacher;

	// Token: 0x04003501 RID: 13569
	public GameObject RyobaHair;

	// Token: 0x04003502 RID: 13570
	public Material Transparent;

	// Token: 0x04003503 RID: 13571
	public Texture BlondePony;

	// Token: 0x04003504 RID: 13572
	public Mesh HeadOnlyMesh;

	// Token: 0x04003505 RID: 13573
	public Transform BreastL;

	// Token: 0x04003506 RID: 13574
	public Transform BreastR;

	// Token: 0x04003507 RID: 13575
	public AudioSource MyAudio;

	// Token: 0x04003508 RID: 13576
	public GameObject Pebble;

	// Token: 0x04003509 RID: 13577
	public bool LethalPoison;

	// Token: 0x0400350A RID: 13578
	public bool Initialized;

	// Token: 0x0400350B RID: 13579
	public bool Sedative;

	// Token: 0x0400350C RID: 13580
	public bool Asylum;

	// Token: 0x0400350D RID: 13581
	public bool Cigs;

	// Token: 0x0400350E RID: 13582
	public CameraFilterPack_Colors_Adjust_PreFilters YandereFilter;

	// Token: 0x0400350F RID: 13583
	public HighlightingRenderer HighlightingR;

	// Token: 0x04003510 RID: 13584
	public HighlightingBlitter HighlightingB;

	// Token: 0x04003511 RID: 13585
	public GameObject ThrowButton;

	// Token: 0x04003512 RID: 13586
	public GameObject PebbleIcon;

	// Token: 0x04003513 RID: 13587
	public GameObject AimButton;

	// Token: 0x04003514 RID: 13588
	public UILabel PebbleLabel;

	// Token: 0x04003515 RID: 13589
	public GameObject[] OriginalHairs;

	// Token: 0x04003516 RID: 13590
	public GameObject[] VtuberHairs;

	// Token: 0x04003517 RID: 13591
	public Texture[] VtuberFaces;

	// Token: 0x04003518 RID: 13592
	public bool UpdateTextures;

	// Token: 0x04003519 RID: 13593
	public bool Vtuber;
}
