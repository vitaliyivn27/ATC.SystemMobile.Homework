
namespace ATC.ATCLibrary
{
    public delegate void UserStateHandler(object sender, UserEventArgs e);

    public class UserEventArgs
    {
        public string Message { get; private set; }

        public decimal Sum { get; private set; }

        public UserEventArgs(string _mes, decimal _sum)
        {
            Message = _mes;
            Sum = _sum;
        }
    }
}
