using UnityEngine;

namespace ZentySpeede.General
{
    public abstract class Spawnable: MonoBehaviour
    {
        public delegate void DeactivateEvent(Spawnable o);
        public DeactivateEvent Deactivate { get; set; }
        public virtual void Initialization() { }
        public virtual void Deactivation()
        {
            gameObject.SetActive(false);
            Deactivate.Invoke(this);
        }

    }
}

