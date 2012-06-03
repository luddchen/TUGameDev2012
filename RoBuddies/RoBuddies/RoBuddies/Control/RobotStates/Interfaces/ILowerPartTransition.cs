
namespace RoBuddies.Control.RobotStates.Interfaces
{
    interface ILowerPartTransition
    {
        void ToWalking();
        void ToWaiting();
        void ToPushing();
        void ToCombine();
        void ToJumping();
    }
}
