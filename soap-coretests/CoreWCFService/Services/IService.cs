﻿namespace CoreWCFService.Services
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        string GetData(int value);

        //[OperationContract]
        //CompositeType GetDataUsingDataContract(CompositeType composite);
    }   
}
