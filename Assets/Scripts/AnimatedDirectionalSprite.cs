using UnityEngine;

public class AnimatedDirectionalSprite:MonoBehaviour{
    Animator _anim;

    void Awake() => _anim = GetComponent<Animator>();
    public void SetAnimation(Direction direction){
        gameObject.SetActive(true);
        _anim.SetTrigger(direction.ToString());
    }
    public void ClearAnimation() => gameObject.SetActive(false);
}
