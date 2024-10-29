using System.Collections;
using UnityEngine;

public class DropFromPlatform : MonoBehaviour
{
    private Collider2D _collider;
    private bool _isPlayerOnTop; //Bool that checks if the character is stepping on the platform
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_isPlayerOnTop && Input.GetAxisRaw("Vertical") < 0){
            _collider.enabled = false;
            StartCoroutine(EnableCollider());
        }
    }

    private void SetIsPlayerOnTop(Collision2D other, bool value){
        var player = other.gameObject.GetComponent<PlayerMovement>();

        if(player != null) {
            _isPlayerOnTop = value;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        SetIsPlayerOnTop(other, true);
    }

    private void OnCollisionExit2D(Collision2D other) {
        SetIsPlayerOnTop(other, false);
    }

    private IEnumerator EnableCollider() {
        yield return new WaitForSeconds(0.5f);
        _collider.enabled = true;
    }
}
