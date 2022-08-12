namespace Identity.Infrastructure
{
    public class OperationDetails
    {
        public bool Succedeed { get; }
        public string Message { get; }
        public string Property { get; }

        public OperationDetails(bool succedeed, string message, string prop)
        {
            Succedeed = succedeed;
            Message = message;
            Property = prop;
        }
    }
}
