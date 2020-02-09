using UnityEngine;
using Fakejam.Players;

[RequireComponent(typeof(CircleCollider2D))]
public class SquadInfluence : MonoBehaviour
{
    private CircleCollider2D zone;
    public CircleCollider2D Zone => zone;

    public SpriteRenderer displaySprite;

    private void Awake()
    {
        zone = GetComponent<CircleCollider2D>();
    }

    public void setColorToOwner( PlayerType owner )
    {
        switch (owner)
        {
            case PlayerType.Player:
                displaySprite.color = new Color(1f, 1f, 1f, 0.1f);
                return;
            case PlayerType.Enemy1:
                displaySprite.color = new Color(1f, 0f, 0f, 0.1f);
                return;
        }
    }
}
