
namespace RoBuddies.Control.RobotStates.Interfaces
{
    interface IIUpperpartState
    {
        void ToClimbing();
        void ToCombine();
        void ToFalling();
        void ToShooting();
        void ToWaiting();
    }
}
