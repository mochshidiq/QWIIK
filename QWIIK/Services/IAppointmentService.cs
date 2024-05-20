using QWIIK.Models;
using QWIIK.Services;

namespace QWIIK.Services
{
    public interface IAppointmentService
    {
        List<CustomerModels> GetQueue(DateTime date);
        TokenModels IssueToken(CustomerModels customer, DateTime date);
        void BookAppointment(CustomerModels customer, DateTime date);
    }
}
