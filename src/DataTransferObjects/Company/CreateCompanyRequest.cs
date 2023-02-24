namespace InnspireWebAPI.DataTransferObjects.Company
{
    public class CreateCompanyRequest
    {
        public string CompanyName { get; set; }

        public CreateCompanyRequest(string companyName)
        {
            CompanyName = companyName;
        }
    }
}
