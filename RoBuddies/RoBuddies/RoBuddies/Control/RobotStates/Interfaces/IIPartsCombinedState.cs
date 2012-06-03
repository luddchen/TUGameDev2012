
namespace RoBuddies.Control.RobotStates.Interfaces
{
    interface IIPartsCombinedState
    {
        void ToSeperated();
        void ToJumping();
        void ToPushing();
        void ToPulling();
        void ToWaiting();
        void ToWalking();
    }
}
