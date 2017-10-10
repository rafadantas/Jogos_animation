 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private Animator anim;
	private Rigidbody2D rb2DPlayer;
	public float velocidade;
	private float h;
	public bool lookRight;//para fazer flip
	private SpriteRenderer sprite;
	private bool estaNoPiso; //para verificar se está no chao
	public float forcaPulo; //para pular
	public Transform piso_verificar;
	public Transform localTiro;//para local de onde sai o tiro
	public GameObject prefabTiro;//para o prefab do tiro
	//private bool dead; //para verificar se está morto
	private Rigidbody2D rb2DTiro;
	public float forcaTiro;
	//para fazer flip


	public AnimationClip animClip;
	public AnimationEvent animEvent;

	private bool Dead;


	// Use this for initialization
	void Start () {
		animEvent = new AnimationEvent ();
		anim = GetComponent<Animator> ();
		rb2DPlayer = GetComponent<Rigidbody2D> ();
		sprite = GetComponent<SpriteRenderer> ();

		animEvent.functionName = "setDeadFalse";
		animClip.AddEvent (animEvent);
		rb2DTiro = prefabTiro.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {

		//ressuscitar
		if (Input.GetKeyDown (KeyCode.R)) {
			Dead = false;
			transform.position = new Vector2 (-1.53f, -0.56f);
			anim.Play ("Idle");
		}

		if (!Dead) {
			h = Input.GetAxisRaw ("Horizontal");

			estaNoPiso = Physics2D.Linecast (transform.position, piso_verificar.position, 1 << LayerMask.NameToLayer ("piso"));

			rb2DPlayer.velocity = new Vector2 (h * velocidade * Time.deltaTime, rb2DPlayer.velocity.y);

			//PULAR
			if (Input.GetButtonDown ("Jump") && estaNoPiso) {
				GetComponent<Rigidbody2D> ().AddForce (transform.up * forcaPulo);
			}
			//script do tiro
			if (Input.GetKeyDown (KeyCode.Z)) {
				GameObject tiroTemporario = Instantiate (prefabTiro, localTiro.position, Quaternion.identity);
				rb2DTiro = tiroTemporario.GetComponent<Rigidbody2D> ();
				rb2DTiro.AddForce (new Vector2 (forcaTiro, 0));
			}

			if (h < 0) {
				sprite.flipX = true;
				//lookRight = false;
			}
			if (h > 0) {
				sprite.flipX = false;
				//lookRight = true;
			}
			//if (Input.GetKey ("m")) {
			//	dead = true;
			//}


			if (h != 0) {
				anim.SetBool ("Walk", true);
			} else {
				anim.SetBool ("Walk", false);
			}

			if (Input.GetKey ("down")) {
				anim.SetBool ("Slide", true);
			} else {
				anim.SetBool ("Slide", false);
			}
			if (Input.GetKey (KeyCode.Z)) {
				anim.SetBool ("Shoot", true);
				anim.SetBool ("Tiro", true);
			} else {
				anim.SetBool ("Shoot", false);
				anim.SetBool ("Tiro", false);
			}
		}

	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.CompareTag ("Morto") && !Dead) {
			Dead = true;
			anim.SetBool ("Dead", Dead);
		}
	}

	void flipZoombieByRenderer(){
		sprite.flipX = !sprite.flipX;
	}

	public void setDeadFalse(){
		anim.SetBool ("Dead", false);
		print ("acionado");
	}
}
