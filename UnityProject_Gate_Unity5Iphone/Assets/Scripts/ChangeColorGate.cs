using Assets.Scripts;
using UnityEngine;
using AssemblyCSharp;

public class ChangeColorGate : BaseGate {
    //public ColorEnum ToColor;
	public GameObject ToProjectile1;
	public GameObject ToProjectile2;
	public Texture ProjectileTexture1;
	public Texture ProjectileTexture2;
	public ColorEnum CorrectColor;
	public Rigidbody CurrentProjectile;
	public Texture CurrentTexture;
	public AudioClip onColorChangeAudio;
	//public Animator toAnimation;

    private float clickStart;
    private Vector3 currentPosition;
    public bool isDragged;

	private Vector3 finalPosition;

    private void Start() {
		CurrentProjectile = ToProjectile1.GetComponent<Rigidbody>();
		CurrentTexture = transform.GetComponent<Renderer>().material.mainTexture;
    }

    private void OnMouseDown() {
		if (IsDisabledStartAnimation){return;}
		if (IsDisabled) { return; }
        isDragged = false;
        clickStart = Time.time;
        currentPosition = transform.position;
    }

    private void OnMouseUp() {
		if (IsDisabledStartAnimation){return;}
		if (IsDisabled) { return; }

		finalPosition = transform.position;
		float differenceX = Mathf.Abs (finalPosition.x - currentPosition.x);
		float differenceY = Mathf.Abs (finalPosition.y - currentPosition.y);

		if((differenceX < 1) && (differenceY < 1)){
			//if (!isDragged) {
			if ((Time.time - clickStart) < 0.7f) {
				OnClick();
				clickStart = -1;
			}
		}
		isDragged = false;
    }

    private void OnMouseDrag() {
		if (IsDisabledStartAnimation){return;}
		if (IsDisabled) { return; }
        isDragged = !CompareApproximate(currentPosition, transform.position);
    }

	public bool CompareApproximate(Vector3 a, Vector3 b)	{
		if(!Mathf.Approximately(a.x, b.x))
			return false;
		if(!Mathf.Approximately(a.y, b.y))
			return false;
		return true;
	}

    private void OnClick() {
		if (CurrentProjectile == ToProjectile1.GetComponent<Rigidbody>()) {
			CurrentProjectile = ToProjectile2.GetComponent<Rigidbody>();
			CurrentTexture = ProjectileTexture2;
		}else{
			CurrentProjectile = ToProjectile1.GetComponent<Rigidbody>();
			CurrentTexture = ProjectileTexture1;
		}

		transform.GetComponent<Renderer>().material.mainTexture = CurrentTexture;

		AudioSource.PlayClipAtPoint (onColorChangeAudio, transform.position);
    }

    public void DoColorChange(Ball ball) {
		if (isDragged) { return; }
        Rigidbody clone;
		if (ball.Color == Color) {
            Vector3 newPos = ball.transform.position;
			var direction = ball.CurrentDirection;			
			Destroy(ball.gameObject);
			clone = Instantiate(CurrentProjectile, newPos, transform.rotation) as Rigidbody;
            
            // cloonimise unity kiiks - kui materjali ei küsi siis seda ei cloonita 
            clone.tag = "Ball";
            var ballComponent = clone.GetComponent(typeof(Ball)) as Ball;
            if (ballComponent != null) {
				ballComponent.go = true;
				ballComponent.Rotate(direction);
				ballComponent.SetIdelFalse();
                // ei saanud aru miks see gun oli
            }
        }
    }
}