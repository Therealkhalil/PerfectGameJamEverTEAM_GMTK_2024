using UnityEngine;
#if ENABLE_INPUT_SYSTEM
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
		public bool throwBigger;
		public bool throwSmaller;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

		[Header("UI Control Settings")]
		public bool escAction;
        public bool tabAction;


#if ENABLE_INPUT_SYSTEM
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

		public void OnThrowBigger(InputValue value)
		{
			ThrowBiggerInput(value.isPressed);
		}
		
		public void OnThrowSmaller(InputValue value)
		{
			ThrowSmallerInput(value.isPressed);
		}

		public void OnEscAction(InputValue value)
		{
			EscActionInput(value.isPressed);
		}
		
		public void OnTabAction(InputValue value)
		{
			TabActionInput(value.isPressed);
		}
#endif

		/// <summary>
		/// For menuManager to click UI
		/// </summary>
		/// <param name="val"> cursor on/off </param>
		public void ChangeCursorInput(bool val)
		{
			SetCursorState(val);
			cursorInputForLook = val;
		}
		
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
		
		public void ThrowBiggerInput(bool newSprintState)
		{
			throwBigger = newSprintState;
		}
		
		public void ThrowSmallerInput(bool newSprintState)
		{
			throwSmaller = newSprintState;
		}
		
		public void EscActionInput(bool newSprintState)
		{
			escAction = newSprintState;
		}
		
		public void TabActionInput(bool newSprintState)
		{
			tabAction = newSprintState;
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
		
	}
	
}