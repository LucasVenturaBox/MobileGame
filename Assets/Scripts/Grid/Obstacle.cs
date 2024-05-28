using MobileGame.Move;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public ObstacleSpawner _parent;
    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.GetComponent<Movement>() != null)
        {
            other.gameObject.GetComponent<Movement>().Die();
        }

        if(other.gameObject.layer != LayerMask.NameToLayer("Obstacle"))
        {
            _parent.UpdateWaveState(this);

            Debug.Log("Is being destroyed");
            Destroy(gameObject);
        }
       
    }
}
