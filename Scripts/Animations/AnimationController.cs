using UnityEngine;

namespace ZentySpeede.Animations
{
    public class AnimationController : MonoBehaviour
    {
        Animator anim;
        private void Awake() => anim = GetComponentInChildren<Animator>();

        public void ChangeTo(string t) =>  anim.SetTrigger(t);


        public Animator GetAnim()
        {
            return anim;
        }
    }


}
