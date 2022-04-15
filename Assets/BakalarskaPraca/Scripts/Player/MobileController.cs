using UnityEngine;

public class MobileController : MonoBehaviour
{
	public static MobileController instance;

	PlayerController player;

	[Header("Referencie na tlačidlá")]
	[SerializeField] GameObject swordButton;
	[SerializeField] GameObject bowButton;

	public bool moveLeft, moveRight, canRoll;

    private void Awake()
    {
		instance = this;
    }

    // Use this for initialization
    void Start()
	{
		player = FindObjectOfType<PlayerController>();
	}

	public void OnHoldLeftButton() 
	{
		moveLeft = true;
		player.MovePlayer();
	}

	public void OnReleaseLeftButton()
	{
		moveLeft = false;
	}

	public void OnHoldRightButton()
	{
		moveRight  = true;
		player.MovePlayer();
	}

	public void OnReleaseRightButton()
	{
		moveRight = false;
	}

	public void OnPressJumpButton() 
	{
		player.Jump();
	}

	public void OnPressRollButton()
	{
		if (!player.rollActive) 
		{
			player.Roll();
		}
	}

	// Túto metódu použijeme pri štarte dialógu aby sme predošli bugom typu beh na mieste
	public void StopMovement() 
	{
		moveLeft = false;
		moveRight = false;
	}

	public void ChangeWeapon() 
	{
		player.ChangeWeapon();

		if (player.weapon == Weapon.sword)
		{
			swordButton.SetActive(true);
			bowButton.SetActive(false);
		}
		else if (player.weapon == Weapon.bow) 
		{
			swordButton.SetActive(false);
			bowButton.SetActive(true);
		}
	}

	public void SwordAttack() 
	{
		player.SwordAttack();
	}


	public void BowAttack() 
	{
		player.BowAttack();
	}


}
