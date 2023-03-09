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

        public bool magic;

        public bool firstAbility;

        public bool secondAbility;

        public bool captureBase;

        public bool throwFlashbang;
        public bool throwSmoke;

        public bool placeExplosiveTrap;
        public bool placePoisonTrap;
        public bool placeSlowTrap;
        
        public bool openShop;
        public bool openScoreboard;


        [Header("Movement Settings")]
        public bool analogMovement;

        [Header("Mouse Cursor Settings")]
        public bool cursorLocked = true;

        public bool cursorInputForLook = true;


#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
        public void OnMove(InputValue value)
        {
            MoveInput(value.Get<Vector2>());
        }

        public void OnLook(InputValue value)
        {
            if (cursorInputForLook)
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

        public void OnMagic(InputValue value)
        {
            MagicInput(value.isPressed);
        }

        public void OnFirstAbility(InputValue value)
        {
            FirstAbilityInput(value.isPressed);
        }

        public void OnSecondAbility(InputValue value)
        {
            SecondAbilityInput(value.isPressed);
        }

        public void OnCaptureBase(InputValue value)
        {
            CaptureBaseInput(value.isPressed);
        }

        public void OnThrowFlashbang(InputValue value)
        {
            ThrowFlashbangInput(value.isPressed);
        }
        public void OnThrowSmoke(InputValue value)
        {
            ThrowSmokeInput(value.isPressed);
        }
        public void OnPlaceExplosiveTrap(InputValue value)
        {
            PlaceExplosiveTrapInput(value.isPressed);
        }
        public void OnPlacePoisonTrap(InputValue value)
        {
            PlacePoisonTrapInput(value.isPressed);
        }
        public void OnPlaceSlowTrap(InputValue value)
        {
            PlaceSlowTrapInput(value.isPressed);
        }

        public void OnOpenShop(InputValue value)
        {
            OpenShop(value.isPressed);
        }
        public void OnOpenScoreboard(InputValue value)
        {
            OpenScoreBoard(value.isPressed);
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
        public void ShootInput(bool newShootState)
        {
            shoot = newShootState;
        }
        public void MagicInput(bool newMagicState)
        {
            magic = newMagicState;
        }
        public void FirstAbilityInput(bool newFirstAbilityState)
        {
            firstAbility = newFirstAbilityState;
        }
        public void SecondAbilityInput(bool newSecondAbilityState)
        {
            secondAbility = newSecondAbilityState;
        }
        public void CaptureBaseInput(bool newCaptureBaseState)
        {
            captureBase = newCaptureBaseState;
        }
        public void ThrowFlashbangInput(bool newThrowFlashbangState)
        {
            throwFlashbang = newThrowFlashbangState;
        }
        public void ThrowSmokeInput(bool newThrowSmokeState)
        {
            throwSmoke = newThrowSmokeState;
        }
        public void PlaceExplosiveTrapInput(bool newPlaceExplosiveTrapState)
        {
            placeExplosiveTrap = newPlaceExplosiveTrapState;
        }
        public void PlacePoisonTrapInput(bool newPlacePoisonTrapState)
        {
            placePoisonTrap = newPlacePoisonTrapState;
        }
        public void PlaceSlowTrapInput(bool newPlaceSlowTrapState)
        {
            placeSlowTrap = newPlaceSlowTrapState;
        }
        public void OpenShop(bool newOpenShopState)
        {
            openShop = newOpenShopState;
        }
        public void OpenScoreBoard(bool newOpenScoreboardState)
        {
            openScoreboard = newOpenScoreboardState;
        }
        // private void OnApplicationFocus(bool hasFocus)
        // {
        //     SetCursorState(cursorLocked);
        // }
        // private void SetCursorState(bool newState)
        // {
        //     Cursor.lockState =
        //         newState ? CursorLockMode.Locked : CursorLockMode.None;
        // }
    }
}
