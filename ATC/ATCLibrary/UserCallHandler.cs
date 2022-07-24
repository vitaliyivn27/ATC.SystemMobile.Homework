
namespace ATC.ATCLibrary
{
    public delegate void UserCallHandler(object sender, UserCallArgs e);
    public class UserCallArgs
    {

        public string Message { get; private set; }

        public decimal Time { get; private set; }

        public UserCallArgs(string _mes)
        {
            Message = _mes;
        }
    }
}
