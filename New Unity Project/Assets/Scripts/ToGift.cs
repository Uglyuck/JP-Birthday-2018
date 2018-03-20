using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This was a test for giving him a gift via URL. But this didn't seem to work on Itch.io
// Leaving it incase someone adds to it.
public class ToGift : MonoBehaviour 
{
	public void GoToLink(string Link)
	{
		Application.OpenURL(Link);
	}
}
