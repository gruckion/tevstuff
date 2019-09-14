namespace App.Services
{
    using System;
    using System.CodeDom.Compiler;
    using System.ServiceModel;

    [GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [ServiceContractAttribute(ConfigurationName = "App.ICustomerCreditService")]
    public interface ICustomerCreditService
    {
        [OperationContractAttribute(
			Action = "http://tempuri.org/ICustomerCreditService/GetCreditLimit",
			ReplyAction = "http://tempuri.org/ICustomerCreditService/GetCreditLimitResponse")]
        int GetCreditLimit(int customerId, DateTime dateOfBirth);
    }
}
