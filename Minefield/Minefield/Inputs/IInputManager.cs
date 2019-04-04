using Minefield.Model;

namespace Minefield.Inputs
{
    public interface IInputManager
    {
        PlayerDirection? GetDirection();
        bool GetYesNoResponse();
        void WaitForAnyInput();
    }
}
