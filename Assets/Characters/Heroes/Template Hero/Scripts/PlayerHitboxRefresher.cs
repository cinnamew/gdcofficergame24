using UnityEngine;

public class PlayerHitboxRefresher : MonoBehaviour
{
    [SerializeField]public float refreshEvery;
    float currCooldown;
    HeroHitbox hitbox;

    void Start()
    {
        GameObject.FindGameObjectWithTag("PlayerWeapon").GetComponent<HeroHitbox>();
        currCooldown = refreshEvery;
    }

    private void Update() {
        currCooldown -= Time.deltaTime;
        if (currCooldown <= 0) {
            Refresh();
        }
    }

    void Refresh() {
        hitbox.RefreshHitbox();
        currCooldown = refreshEvery;
    }
}
