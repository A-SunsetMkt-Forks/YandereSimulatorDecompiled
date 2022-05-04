﻿using System;
using UnityEngine;

// Token: 0x0200031B RID: 795
public class HomeCorkboardScript : MonoBehaviour
{
	// Token: 0x0600188A RID: 6282 RVA: 0x000EDEC8 File Offset: 0x000EC0C8
	private void Update()
	{
		if (!this.HomeYandere.CanMove)
		{
			if (!this.Loaded)
			{
				this.PhotoGallery.LoadingScreen.SetActive(false);
				this.PhotoGallery.UpdateButtonPrompts();
				this.PhotoGallery.enabled = true;
				this.PhotoGallery.gameObject.SetActive(true);
				this.Loaded = true;
			}
			if (!this.PhotoGallery.Adjusting && !this.PhotoGallery.Viewing && !this.PhotoGallery.LoadingScreen.activeInHierarchy && Input.GetButtonDown("B"))
			{
				this.HomeCamera.Destination = this.HomeCamera.Destinations[0];
				this.HomeCamera.Target = this.HomeCamera.Targets[0];
				this.HomeCamera.CorkboardLabel.SetActive(true);
				this.PhotoGallery.PromptBar.Show = false;
				this.PhotoGallery.enabled = false;
				this.HomeYandere.CanMove = true;
				this.HomeYandere.gameObject.SetActive(true);
				this.HomeWindow.Show = false;
				base.enabled = false;
				this.Loaded = false;
				this.PhotoGallery.SaveAllPhotographs();
				this.PhotoGallery.SaveAllStrings();
			}
		}
	}

	// Token: 0x040024B8 RID: 9400
	public InputManagerScript InputManager;

	// Token: 0x040024B9 RID: 9401
	public PhotoGalleryScript PhotoGallery;

	// Token: 0x040024BA RID: 9402
	public HomeYandereScript HomeYandere;

	// Token: 0x040024BB RID: 9403
	public HomeCameraScript HomeCamera;

	// Token: 0x040024BC RID: 9404
	public HomeWindowScript HomeWindow;

	// Token: 0x040024BD RID: 9405
	public bool Loaded;
}
