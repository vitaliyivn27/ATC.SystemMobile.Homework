
namespace ATC.ATCLibrary
{
    public class User : IUser
    {

        protected internal event UserStateHandler Withdrawed;

        protected internal event UserStateHandler Added;
 
        protected internal event UserStateHandler Opened;

        protected internal event UserStateHandler Closed;

        protected internal event UserCallHandler Connected;

        protected internal event UserCallHandler Disconnected;

        protected internal event UserCallHandler Calling;

        protected internal event UserCallHandler Called;

        protected internal event UserCallHandler Busy;

        protected internal event UserCallHandler Ended;

        public static long firstNumber = 375290000000;

        protected int _days = 0;

        public User(decimal summury)
        {
            Sum = summury;
            PortId = Guid.NewGuid();
            Number = ++firstNumber;
        }

        public decimal Sum { get; set; }

        public Guid PortId { get; private set; }

        public long Number { get; private set; }

        private void CallEvent(UserEventArgs e, UserStateHandler handler)
        {
            if (e != null)
                handler?.Invoke(this, e);
        }

        protected virtual void OnOpened(UserEventArgs e)
        {
            CallEvent(e, Opened);
        }

        protected virtual void OnWithdrawed(UserEventArgs e)
        {
            CallEvent(e, Withdrawed);
        }

        protected virtual void OnAdded(UserEventArgs e)
        {
            CallEvent(e, Added);
        }

        protected virtual void OnClosed(UserEventArgs e)
        {
            CallEvent(e, Closed);
        }

        private void CallEventCall(UserCallArgs e, UserCallHandler handler)
        {
            if (e != null)
                handler?.Invoke(this, e);
        }

        protected virtual void OnConnectToPort(UserCallArgs e)
        {

            CallEventCall(e, Connected);
        }

        protected virtual void OnDisconnectFromPort(UserCallArgs e)
        {

            CallEventCall(e, Disconnected);
        }

        protected virtual void OnCalling(UserCallArgs e)
        {

            CallEventCall(e, Calling);
        }

        protected virtual void OnCalled(UserCallArgs e)
        {

            CallEventCall(e, Called);
        }

        protected virtual void OnCallingToBusyNumber(UserCallArgs e)
        {

            CallEventCall(e, Busy);
        }

        protected virtual void OnEndedCall(UserCallArgs e)
        {

            CallEventCall(e, Ended);
        }

        protected internal virtual void ConnectToPort()
        {
            OnConnectToPort(new UserCallArgs($"{Number} connect to MTC port id: {PortId}"));
        }

        protected internal virtual void DisconnectFromPort()
        {
            OnDisconnectFromPort(new UserCallArgs($"{Number} disconnect from MTC port id: {PortId}"));
        }

        public void TryToCall()
        {
            OnCalling(new UserCallArgs($"+375290000001 calling +375290000002 \nDo you want to answer?"));
        }

        public void AnswerTheCall()
        {
            OnCalled(new UserCallArgs($"Start the call"));
        }

        public void TryToCallOnBusyNumber()
        {
            OnCallingToBusyNumber(new UserCallArgs($"+375290000003 calling +375290000002, but number is busy"));
        }

        public decimal EndTheCall()
        {
            DateTime startCallTime = DateTime.Now;
            OnEndedCall(new UserCallArgs($"The call between +375290000001 and +375290000002 ended"));
            TimeSpan result;
            var random = new Random();
            var randomCallTime = random.Next(1, 3000);
            var endCallTime = startCallTime.AddSeconds(randomCallTime);
            result = endCallTime - startCallTime;
            decimal callTime = (decimal)result.TotalSeconds;
            using (StreamWriter file = new StreamWriter("Calls.txt"))
            {
                file.WriteLine($"The call between +375290000001 and +375290000002 from {startCallTime} to {endCallTime} takes {callTime} seconds");
            }
            return callTime;
        }

        public virtual void Put(decimal summary)
        {
            Sum += summary;
            OnAdded(new UserEventArgs($"Received on account +{Number} : " + summary, summary));
        }

        public virtual decimal Withdraw(decimal summary)
        {
            decimal result = 0;
            if (Sum >= summary)
            {
                Sum -= summary;
                result = summary;
                OnWithdrawed(new UserEventArgs($"Withdrawn {summary} from account {Number}", summary));
            }
            else
            {
                OnWithdrawed(new UserEventArgs($"Failed to withdrawn money from {Number} account", 0));
                result = 0;
            }
            return result;
        }
   
        protected internal virtual void Open()
        {
            OnOpened(new UserEventArgs($"Opened new account! Number of account: {Number}", Sum));
        }
       
        protected internal virtual void Close()
        {
            OnClosed(new UserEventArgs($"Account {Number} is closed.  Summary: {Sum}", Sum));
        }        
    }
}
