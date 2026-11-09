namespace RaceDay_API.Services
{
    public interface IEnrollmentServices
    {
       
        string enter_event(int authentication_id, int category_id, int event_id, string role);
    }
}
