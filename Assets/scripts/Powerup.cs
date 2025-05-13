using UnityEngine;

public class Powerup : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   public PowerUpType powerUpType;

    private void OnTriggerEnter(Collider other)
    {
        if(!other.TryGetComponent(out PlayerBombManager playerBombManager)) return;
        {
            switch(powerUpType)
            {
                case PowerUpType.ExtraBomb:
                    playerBombManager.AddExtraBomb();
                    break;
                case PowerUpType.ExtraRange:
                    playerBombManager.AddExtraRange();
                    break;
            }
        }
    }
    public enum PowerUpType
    {
        ExtraBomb,
        ExtraRange
    }
}
