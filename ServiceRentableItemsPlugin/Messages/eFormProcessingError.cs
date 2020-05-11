namespace ServiceRentableItemsPlugin.Messages
{
    public class eFormProcessingError
    {
        public int caseId { get; protected set; }

        public eFormProcessingError(int caseId)
        {
            this.caseId = caseId;
        }
    }
}