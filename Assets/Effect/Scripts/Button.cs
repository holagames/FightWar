using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {
	public Transform effectToPlay; //Particle effect you want to play
	public Transform objectToAnimate; //The game object you want to animate

	
	void OnMouseDown() {
		StartCoroutine(PlayEffect());
	}
	
	 IEnumerator PlayEffect() {
       	//Play Attack animation
		objectToAnimate.animation.Play("attack");
		//Animation to play after attacking
		objectToAnimate.animation.PlayQueued("idle");
		
        yield return new WaitForSeconds(0.4f);
		
       	// Distance from your player    
		float distance = 0;     
		
		// Transforms a forward position relative to your player into the world space    
		Vector3 effectPos =  objectToAnimate.TransformPoint( objectToAnimate.forward * distance ); 
		effectPos.y = effectPos.y+0f;
		effectPos.x = effectPos.x-0f;
		
		// Instantate the particle effect in front of the object  
		Transform effect = Instantiate ( effectToPlay, effectPos, Quaternion.identity ) as Transform;
		
		// Makes the particle effect "One Shot" or 
		// automatically destroys the particle after playing by adding "EFfectSelfDestruct" script.
		effect.gameObject.AddComponent<EffectSelfDestruct>(); 
    }

}
