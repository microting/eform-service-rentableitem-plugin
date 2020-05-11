namespace ServiceRentableItemsPlugin.Messages
{
    public class eFormProcessed
    {
        public int caseId { get; protected set; }

        public eFormProcessed(int caseId)
        {
            this.caseId = caseId;
        }
    }
}