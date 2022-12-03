using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool aim;
		public bool shoot;
		public bool dash;
		public bool openshop;
		public bool useItem1;
		public bool useItem2;

		[Header("Movement Settings")]
		public bool analogMovement;

		// [Header("Mouse Cursor Settings")]
		// public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}

		public void OnAim(InputValue value)
		{
			AimInput(value.isPressed);
		}

		public void OnShoot(InputValue value)
		{
			ShootInput(value.isPressed);
		}

		public void OnDash(InputValue value)
		{
			DashInput(value.isPressed);
		}

		public void OnOpenShop(InputValue value)
		{
			OpenShopInput(value.isPressed);
		}

		public void OnUseItem1(InputValue value)
		{
			UseItem1(value.isPressed);
		}

		public void OnUseItem2(InputValue value)
		{
			UseItem2(value.isPressed);
		}
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		public void AimInput(bool newAimState)
		{
			aim = newAimState;
		}

		public void ShootInput(bool newShootState) {
			shoot = newShootState;
		}

		public void DashInput(bool newDashstate) {
			dash = newDashstate;
		}

		public void OpenShopInput(bool newOpenShopState) {
			openshop = newOpenShopState;
		}

		public void UseItem1(bool newUseItem1State) {
			useItem1 = newUseItem1State;
		}

			public void UseItem2(bool newUseItem2State) {
			useItem2 = newUseItem2State;
		}

		// private void OnApplicationFocus(bool hasFocus)
		// {
		// 	SetCursorState(cursorLocked);
		// }

		// private void SetCursorState(bool newState)
		// {
		// 	Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		// }
	}
	
}