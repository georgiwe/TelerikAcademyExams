using Exam.Data;
using Exam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Exam.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUsersService" in both code and config file together.
    [ServiceContract]
    public interface IUsersService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/users")]
        IEnumerable<User> GetAllUsers();

        [OperationContract]
        [WebGet(UriTemplate = "/users/id")]
        object GetById(string id);

        [OperationContract]
        IEnumerable<User> GetAllUsers(int page);
    }
}
