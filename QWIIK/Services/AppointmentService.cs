using QWIIK.Models;

namespace QWIIK.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly List<AppointmentModels> _AppointmentModels;

        public AppointmentService()
        {
            _AppointmentModels = new List<AppointmentModels>();
        }

        public List<CustomerModels> GetQueue(DateTime date)
        {
            return _AppointmentModels
                .Where(a => a.Date.Date == date.Date)
                .Select(a => a.Customer)
                .ToList();
        }

        public TokenModels IssueToken(CustomerModels CustomerModels, DateTime date)
        {
            var TokenModels = new TokenModels
            {
                TokenNumber = Guid.NewGuid(),
                Date = date
            };
            return TokenModels;
        }

        public void BookAppointment(CustomerModels CustomerModels, DateTime date)
        {
            _AppointmentModels.Add(new AppointmentModels { Customer = CustomerModels, Date = date });
        }
    }
}
