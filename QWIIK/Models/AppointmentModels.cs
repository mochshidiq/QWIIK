namespace QWIIK.Models
{
    public class AppointmentModels
    {
        public int Id { get; set; }
        public CustomerModels Customer { get; set; }
        public DateTime Date { get; set; }
    }
}
