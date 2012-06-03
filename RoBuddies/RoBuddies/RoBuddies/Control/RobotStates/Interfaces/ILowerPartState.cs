
namespace RoBuddies.Control.RobotStates.Interfaces
{
    interface ILowerPartState
    {
        void ToWalking();
        void ToWaiting();
        void ToPushing();
        void ToCombine();
        void ToJumping();
    }
}
