
namespace RoBuddies.Control.RobotStates.Interfaces
{
    interface IPartsCombinedTransition : Transition
    {
        void ToSeperated(State state);
        void ToJumping(State state);
        void ToPushing(State state);
        void ToPulling(State state);
        void ToWaiting(State state);
        void ToWalking(State state);
    }
}
