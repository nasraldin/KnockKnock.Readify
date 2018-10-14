using System;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace KnockKnock.Readify
{
    /// <summary>
    /// Represents the Red Pill service contract.
    /// </summary>
    [ServiceContract(Namespace = "http://KnockKnock.readify.net", Name = "IRedPill")]
    public interface IRedPill
    {
        [OperationContract]
        Guid WhatIsYourToken();

        [OperationContract]
        long FibonacciNumber(long number);

        [OperationContract]
        string ReverseWords(string words);

        [OperationContract]
        TriangleType WhatShapeIsThis(int a, int b, int c);
    }

    [DataContract]
    public enum TriangleType
    {
        [EnumMember]
        Error,
        [EnumMember]
        Equilateral,
        [EnumMember]
        Isosceles,
        [EnumMember]
        Scalene
    }
}
